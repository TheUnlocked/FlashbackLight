using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashbackLight.Formats
{
    class STX : V3Format
    {
        public List<string> Strings;
        public string lang;

        public STX(SPCEntry entry) : base(entry)
        {

        }

        public override Dictionary<string, (ToBytes, FromBytes)> FileConversions => new Dictionary<string, (ToBytes, FromBytes)>
        {
            { FileExtensionDefault, (ToBytesDefault, FromBytesDefault) },
            { "TXT", (ToBytesTxt, FromBytesTxt) }
        };
        public override string FileExtensionDefault => "STX";

        public override byte[] ToBytesDefault()
        {
            List<byte> result = new List<byte>();
            uint tableOffset = 0x20;
            int tableLength = Strings.Count;

            result.AddRange(Encoding.UTF8.GetBytes("STXT"));
            result.AddRange(Encoding.UTF8.GetBytes(lang));
            result.AddRange(BitConverter.GetBytes((uint)0x01)); // Table count?
            result.AddRange(BitConverter.GetBytes(tableOffset)); // Table offset
            result.AddRange(BitConverter.GetBytes((uint)0x08)); // unk2
            result.AddRange(BitConverter.GetBytes(tableLength)); // Table length

            int[] offsets = new int[tableLength];
            int highestIndex = 0;
            for (int i = 0; i < tableLength; i++)
            {
                uint paddingLength = tableOffset + (uint)(i * 8) - (uint)result.Count;
                for (int j = 0; j < paddingLength; j++) result.Add(0x00);

                int strOffset = 0;
                for (int j = 0; j < i; j++)
                {
                    if (Strings[i] == Strings[j])
                    {
                        strOffset = offsets[j];
                    }
                }

                if (i == 0)
                {
                    strOffset = 0x20 + (8 * tableLength);
                }
                else if (strOffset == 0)
                {
                    strOffset = offsets[highestIndex] + ((Strings[highestIndex].Length + 1) * 2);
                    highestIndex = i;
                }

                offsets[i] = strOffset;

                result.AddRange(BitConverter.GetBytes(i));
                result.AddRange(BitConverter.GetBytes(strOffset));
            }

            foreach (string str in Strings.Distinct())
            {
                result.AddRange(Encoding.Unicode.GetBytes(str + "\0"));
            }

            return result.ToArray();
        }

        public override void FromBytesDefault(byte[] bytes)
        {
            using (var reader = new BinaryReader(new MemoryStream(bytes), Encoding.UTF8))
            {
                if (new string(reader.ReadChars(4)) != "STXT")
                    throw new InvalidDataException("Error parsing STX file: Invalid magic number.");
                lang = new string(reader.ReadChars(4));
            }

            using (var reader = new BinaryReader(new MemoryStream(entry.Contents), Encoding.Unicode))
            {
                reader.BaseStream.Seek(8, SeekOrigin.Begin);
                uint unk1 = reader.ReadUInt32();    // Table count?
                uint tableOffset = reader.ReadUInt32();
                uint unk2 = reader.ReadUInt32();
                uint tableLen = reader.ReadUInt32();

                Strings = new List<string>();
                for (uint s = 0; s < tableLen; s++)
                {
                    reader.BaseStream.Seek(tableOffset + (8 * s), SeekOrigin.Begin);
                    uint stringIndex = reader.ReadUInt32();
                    uint stringOffset = reader.ReadUInt32();

                    reader.BaseStream.Seek(stringOffset, SeekOrigin.Begin);
                    List<char> charList = new List<char>();
                    while (reader.PeekChar() != 0x00)
                    {
                        charList.Add(reader.ReadChar());
                    }
                    string finalString = new string(charList.ToArray());
                    Strings.Add(finalString);
                }
            }
        }

        byte[] ToBytesTxt()
        {
            StringBuilder sb = new StringBuilder()
                .Append(lang)
                .Append('\0')
                .Append(string.Join("\0", Strings))
                .Replace("\n", "\\n")
                .Replace("\r", "\\r")
                .Replace('\0', '\n');

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        void FromBytesTxt(byte[] bytes)
        {
            var strs = Encoding.UTF8.GetString(bytes).Split(new string[] { "\n", "\r\n" }, StringSplitOptions.None);
            lang = strs[0];
            Strings = strs.Skip(1).ToList();
        }
    }
}
