using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashbackLight.Formats
{
    class WRD : V3Format
    {
        public List<string> Labels;
        public List<string> Params;
        public List<string> Strings;
        public List<WRDCmd> Code;
        private bool externalStrings;

        public byte[] bytes; // TEMP CODE

        public string filename;

        public WRD()
        {

        }

        public WRD(byte[] bytes, string spcName, string wrdName)
        {
            this.bytes = bytes; // TEMP CODE

            this.filename = spcName + "::" + wrdName;

            BinaryReader reader = new BinaryReader(new MemoryStream(bytes), Encoding.UTF8);

            ushort stringCount = reader.ReadUInt16();
            ushort labelCount = reader.ReadUInt16();
            ushort paramCount = reader.ReadUInt16();
            ushort sublabelCount = reader.ReadUInt16();
            reader.BaseStream.Seek(4, SeekOrigin.Current);

            uint sublabelOffsetsPointer = reader.ReadUInt32();
            uint labelOffsetsPointer = reader.ReadUInt32();
            uint labelNamesPointer = reader.ReadUInt32();
            uint paramsPointer = reader.ReadUInt32();
            uint stringsPointer = reader.ReadUInt32();

            Labels = new List<string>();
            reader.BaseStream.Seek(labelNamesPointer, SeekOrigin.Begin);
            for (ushort i = 0; i < labelCount; ++i)
            {
                reader.ReadByte(); // CHECK
                Labels.Add(reader.ReadString());
                reader.ReadByte();  // Skip null terminator
            }

            reader.BaseStream.Seek(0x20, SeekOrigin.Begin);
            Code = new List<WRDCmd>();
            // We need at least 2 bytes for each command
            while (reader.BaseStream.Position + 1 < sublabelOffsetsPointer)
            {
                byte b = reader.ReadByte();
                if (b != 0x70) throw new InvalidDataException($"Error parsing WRD file at ${reader.BaseStream.Position}: Expected opcode header byte 0x70, but got {b}");

                WRDCmd cmd = new WRDCmd
                {
                    Opcode = reader.ReadByte()
                };

                // Read command arguments, if any
                List<ushort> argList = new List<ushort>();
                while (reader.BaseStream.Position + 1 < sublabelOffsetsPointer)
                {
                    byte[] arg = reader.ReadBytes(2);
                    if (arg[0] == 0x70)
                    {
                        reader.BaseStream.Seek(-2, SeekOrigin.Current);
                        break;
                    }

                    argList.Add(BitConverter.ToUInt16(arg.Reverse().ToArray(), 0));
                }
                cmd.ArgData = argList.ToArray();

                if (cmd.ArgTypes.Length != cmd.ArgData.Length && cmd.Opcode != 0x01 && cmd.Opcode != 0x03)
                {
                    throw new InvalidDataException($"Error parsing WRD file: Opcode {cmd.Opcode} expected {cmd.ArgTypes.Length} arguments, but found {cmd.ArgData.Length}.");
                }

                Code.Add(cmd);
            }

            Params = new List<string>();
            reader.BaseStream.Seek(paramsPointer, SeekOrigin.Begin);
            for (ushort p = 0; p < paramCount; ++p)
            {
                Params.Add(reader.ReadString());
                reader.ReadByte();  // Skip null terminator
            }

            externalStrings = (stringsPointer == 0);

            // Read dialogue text strings
            Strings = new List<string>();
            if (stringCount > 0)
            {
                // If we already know that there are no strings,
                // there's no need to go through the work to find them.

                if (externalStrings)
                {
                    // Strings are stored in the "(current spc name)_text_(region).spc" file,
                    // within an STX file with the same name as the current WRD file.
                    string textSPCName = textSPCName = spcName.Insert(spcName.LastIndexOf('.'), string.Format("_text_{0}", MainForm.RegionString));
                    if (!File.Exists(textSPCName))
                    {
                        // If the first filename fails, we probably need to remove a duplicate
                        // region tag from the filename before "_text_".
                        textSPCName = textSPCName.Remove(textSPCName.LastIndexOf("_text_") - 3, 3);

                        if (!File.Exists(textSPCName))
                        {
                            // If the file still doesn't exist, it's probably not
                            // there and the strings should just be abandoned.
                            System.Windows.Forms.MessageBox.Show($"{spcName} does not have an associated .stx text file.",
                                "Missing .stx file",
                                System.Windows.Forms.MessageBoxButtons.OK,
                                System.Windows.Forms.MessageBoxIcon.Warning,
                                System.Windows.Forms.MessageBoxDefaultButton.Button1);
                            return;
                        }
                    }

                    string stxName = wrdName.Replace(".wrd", ".stx");
                    byte[] spcData = File.ReadAllBytes(textSPCName);
                    SPC textSPC = new SPC(spcData, textSPCName);

                    Strings = new STX(textSPC.Entries[stxName].Contents).Strings;
                }
                else
                {
                    reader.BaseStream.Seek(stringsPointer, SeekOrigin.Begin);
                    for (ushort i = 0; i < stringCount; ++i)
                    {
                        short stringLen = 0;

                        // The string length is a signed byte, so if it's larger than 0x7F,
                        // that means the length is actually stored in a signed short,
                        // since we can't have a negative string length.
                        // ┐(´∀｀)┌
                        if ((byte)reader.PeekChar() >= 0x80)
                        {
                            stringLen = reader.ReadInt16();
                        }
                        else
                        {
                            stringLen = reader.ReadByte();
                        }
                        stringLen += 2; // Null terminator

                        List<char> stringData = new List<char>(stringLen / 2);
                        for (int j = 0; j < (stringLen / 2); ++j)
                        {
                            char c = Convert.ToChar(reader.ReadUInt16());
                            stringData.Add(c);

                            // We can't always trust stringLen apparently, so break if we've hit a null terminator.
                            if (c == 0)
                                break;
                        }

                        string str = new string(stringData.ToArray());
                        str = str.Replace("\r", "\\r");
                        str = str.Replace("\n", "\\n");
                        Strings.Add(str);
                    }
                }
            }
        }

        public override byte[] ToBytes()
        {

            List<byte> result = new List<byte>();

            result.AddRange(BitConverter.GetBytes((ushort)Strings.Count));
            result.AddRange(BitConverter.GetBytes((ushort)Labels.Count));
            result.AddRange(BitConverter.GetBytes((ushort)Params.Count));
            result.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x00 });

            List<byte> codeData = new List<byte>();
            List<byte> codeOffsets = new List<byte>();
            List<byte> labelNamesData = new List<byte>();
            List<ushort> sublabelOffsets = new List<ushort>();

            foreach(string label in Labels)
            {
                byte[] labelData = Encoding.UTF8.GetBytes(label);
                labelNamesData.Add((byte)(labelData.Length - 1));
                labelNamesData.AddRange(labelData);

                codeOffsets.AddRange(BitConverter.GetBytes((ushort)codeData.Count));
            }

            foreach(WRDCmd cmd in Code)
            {
                // Re-calculate sublabel offsets
                if (cmd.Opcode == 0x4A) // LBN
                {
                    // The current data size value is also equal to the current opcode's location.
                    sublabelOffsets.Add((ushort)codeData.Count);
                }

                codeData.Add(0x70);
                codeData.Add(cmd.Opcode);
                foreach(ushort arg in cmd.ArgData)
                {
                    codeData.AddRange(BitConverter.GetBytes(arg));
                }
            }

            result.AddRange(BitConverter.GetBytes((ushort)sublabelOffsets.Count));

            List<byte> sublabelOffsetBytes = new List<byte>();
            for (ushort i = 0; i < sublabelOffsets.Count; ++i)
            {
                sublabelOffsetBytes.AddRange(BitConverter.GetBytes(i));                         // sublabel number
                sublabelOffsetBytes.AddRange(BitConverter.GetBytes(sublabelOffsetBytes[i]));    // sublabel offset
            }

            List<byte> flagsData = new List<byte>();
            foreach(string flag in Params)
            {
                byte[] flagData = Encoding.UTF8.GetBytes(flag);
                flagsData.Add((byte)(flagData.Length - 1));
                flagsData.AddRange(flagData);
            }

            const uint HEADER_END = 0x20;
            uint sublabelOffsetsPtr = HEADER_END + (uint)codeData.Count;
            uint codeOffsetsPtr = sublabelOffsetsPtr + (uint)sublabelOffsetBytes.Count;
            uint labelNamesPtr = codeOffsetsPtr + (uint)codeOffsets.Count;
            uint flagsPtr = labelNamesPtr + (uint)labelNamesData.Count;
            uint strPtr = !externalStrings ? flagsPtr + (uint)flagsData.Count : 0;

            result.AddRange(BitConverter.GetBytes(sublabelOffsetsPtr));
            result.AddRange(BitConverter.GetBytes(codeOffsetsPtr));
            result.AddRange(BitConverter.GetBytes(labelNamesPtr));
            result.AddRange(BitConverter.GetBytes(flagsPtr));
            result.AddRange(BitConverter.GetBytes(strPtr));

            // Now that the offsets/ptrs to the data have been calculated, append the actual data
            result.AddRange(codeData);
            result.AddRange(sublabelOffsetBytes);
            result.AddRange(codeOffsets);
            result.AddRange(labelNamesData);
            result.AddRange(flagsData);
            if (!externalStrings)
            {
                foreach(string str in Strings)
                {
                    byte[] strData = Encoding.UTF8.GetBytes(str);
                    result.Add((byte)(strData.Length - 1));
                    result.AddRange(strData);
                }
            }

            result.Add(0x00);

            return result.ToArray();
        }
    }

    class WRDCmd
    {
        public byte Opcode;
        public ushort[] ArgData;

        public string Name
        {
            get { return NAME_LIST[Opcode]; }
        }

        public byte[] ArgTypes
        {
            get { return ARGTYPE_LIST[Opcode]; }
            
        }

        public bool IsVarLength
        {
            get
            {
                // TODO: opcode 0x02 and 0x07 might not have variable-length parameters
                if ((Opcode >= 0x01 && Opcode <= 0x03) || Opcode == 0x07)
                    return true;
                else
                    return false;
            }
        }

        /// Official command names found in game_resident/command_label.dat
        public static string[] NAME_LIST =
        {
            "FLG", "IFF", "WAK", "IFW", "SWI", "CAS", "MPF", "SPW", "MOD", "HUM",
            "CHK", "KTD", "CLR", "RET", "KNM", "CAP", "FIL", "END", "SUB", "RTN",
            "LAB", "JMP", "MOV", "FLS", "FLM", "VOI", "BGM", "SE_", "JIN", "CHN",
            "VIB", "FDS", "FLA", "LIG", "CHR", "BGD", "CUT", "ADF", "PAL", "MAP",
            "OBJ", "BUL", "CRF", "CAM", "KWM", "ARE", "KEY", "WIN", "MSC", "CSM",
            "PST", "KNS", "FON", "BGO", "LOG", "SPT", "CDV", "SZM", "PVI", "EXP",
            "MTA", "MVP", "POS", "ICO", "EAI", "COL", "CFP", "CLT=", "R=", "PAD=",
            "LOC", "BTN", "ENT", "CED", "LBN", "JMN"
        };

        public static byte[][] ARGTYPE_LIST = new[]
        {
            new byte[] {0,0}, new byte[] {0,0,0}, new byte[] {0,0,0}, new byte[] {0,0,1}, new byte[] {0}, new byte[] {1}, new byte[] {0,0,0}, new byte[] {}, new byte[] {0,0,0,0}, new byte[] {0},
            new byte[] {0}, new byte[] {0,0}, new byte[] {}, new byte[] {}, new byte[] {0,0,0,0,0}, new byte[] {}, new byte[] {0,0}, new byte[] {}, new byte[] {0,0}, new byte[] {},
            new byte[] {3}, new byte[] {0}, new byte[] {0,0}, new byte[] {0,0,0,0}, new byte[] {0,0,0,0,0,0}, new byte[] {0,0}, new byte[] {0,0,0}, new byte[] {0,0}, new byte[] {0,0}, new byte[] {0},
            new byte[] {0,0,0}, new byte[] {0,0,0}, new byte[] {}, new byte[] {0,1,0}, new byte[] {0,0,0,0,0}, new byte[] {0,0,0,0}, new byte[] {0,0}, new byte[] {0,0,0,0,0}, new byte[] {}, new byte[] {0,0,0},
            new byte[] {0,0,0}, new byte[] {0,0,0,0,0,0,0,0}, new byte[] {0,0,0,0,0,0,0}, new byte[] {0,0,0,0,0}, new byte[] {0}, new byte[] {0,0,0}, new byte[] {0,0}, new byte[] {0,0,0,0}, new byte[] {}, new byte[] {},
            new byte[] {0,0,1,1,1}, new byte[] {0,1,1,1,1}, new byte[] {1,1}, new byte[] {0,0,0,0,0}, new byte[] {}, new byte[] {0}, new byte[] {0,0,0,0,0,0,0,0,0,0}, new byte[] {0,0,0,0}, new byte[] {0}, new byte[] {0},
            new byte[] {0}, new byte[] {0,0,0}, new byte[] {0,0,0,0,0}, new byte[] {0,0,0,0}, new byte[] {0,0,0,0,0,0,0,0,0,0}, new byte[] {0,0,0}, new byte[] {0,0,0,0,0,0,0,0,0}, new byte[] {0}, new byte[] {}, new byte[] {0},
            new byte[] {2}, new byte[] {}, new byte[] {}, new byte[] {}, new byte[] {1}, new byte[] {1}
        };
    }
}
