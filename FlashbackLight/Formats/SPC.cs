using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashbackLight.Utils;
using static FlashbackLight.Formats.V3Format;

namespace FlashbackLight.Formats
{
    public class SPC : V3Format
    {
        public byte[] Unk1;
        public uint Unk2;
        public Dictionary<string, SPCEntry> Entries;

#pragma warning disable CS0618 // SPC files are allowed to use this constructor.
        public SPC()
        {
            Unk1 = new byte[0x24];
            Unk2 = 4;
            Entries = new Dictionary<string, SPCEntry>();
        }

        public override void FromBytesDefault(byte[] bytes)
        {
            using BinaryReader reader = new BinaryReader(new MemoryStream(bytes), Encoding.UTF8);

            string spcMagic = new string(reader.ReadChars(4));
            if (spcMagic == "$CMP")
            {
                throw new NotImplementedException("Error parsing SPC file: SRD-compressed SPC files are not yet supported.");
            }
            else if (spcMagic != "CPS.")
            {
                throw new InvalidDataException("Error parsing SPC file: Invalid magic number.");
            }

            Unk1 = reader.ReadBytes(0x24);
            uint fileCount = reader.ReadUInt32();
            Unk2 = reader.ReadUInt32();
            reader.BaseStream.Seek(0x10, SeekOrigin.Current);

            string tableMagic = new string(reader.ReadChars(4));
            if (tableMagic != "Root")
            {
                throw new InvalidDataException("Error parsing SPC file: Invalid file table identifier.");
            }
            reader.BaseStream.Seek(0x0C, SeekOrigin.Current);

            Entries = new Dictionary<string, SPCEntry>();
            for (int i = 0; i < fileCount; i++)
            {
                SPCEntry entry = new SPCEntry();

                entry.CmpFlag = reader.ReadUInt16();
                entry.UnkFlag = reader.ReadUInt16();
                entry.CmpSize = (int)reader.ReadUInt32();
                entry.DecSize = (int)reader.ReadUInt32();
                int nameLen = reader.ReadInt32();
                reader.BaseStream.Seek(0x10, SeekOrigin.Current);

                int namePadding = (0x10 - (nameLen + 1) % 0x10) % 0x10;
                int dataPadding = (0x10 - entry.CmpSize % 0x10) % 0x10;
                entry.Filename = new string(reader.ReadChars(nameLen));
                reader.BaseStream.Seek(namePadding + 1, SeekOrigin.Current);

                entry.Contents = reader.ReadBytes(entry.CmpSize);
                reader.BaseStream.Seek(dataPadding, SeekOrigin.Current);

                Entries[entry.Filename] = entry;
            }
        }

        public override byte[] ToBytesDefault()
        {
            List<byte> result = new List<byte>();

            result.AddRange(Encoding.UTF8.GetBytes("CPS."));
            result.AddRange(Unk1);
            result.AddRange(BitConverter.GetBytes(Entries.Count));
            result.AddRange(BitConverter.GetBytes(Unk2));
            for(int i = 0; i < 0x10; i++) result.Add(0x00);
            result.AddRange(Encoding.UTF8.GetBytes("Root"));
            for (int i = 0; i < 0x0C; i++) result.Add(0x00);

            foreach (KeyValuePair<string, SPCEntry> pair in Entries)
            {
                SPCEntry entry = pair.Value;

                result.AddRange(BitConverter.GetBytes(entry.CmpFlag));
                result.AddRange(BitConverter.GetBytes(entry.UnkFlag));
                result.AddRange(BitConverter.GetBytes(entry.CmpSize));
                result.AddRange(BitConverter.GetBytes(entry.DecSize));
                result.AddRange(BitConverter.GetBytes(entry.Filename.Length));
                for (int i = 0; i < 0x10; i++) result.Add(0x00);

                uint namePadding = (uint)((0x10 - (entry.Filename.Length + 1) % 0x10) % 0x10);
                uint dataPadding = (uint)((0x10 - entry.CmpSize % 0x10) % 0x10);

                result.AddRange(Encoding.UTF8.GetBytes(entry.Filename));
                for (int i = 0; i < namePadding + 1; i++) result.Add(0x00);

                result.AddRange(entry.Contents);
                for (int i = 0; i < dataPadding; i++) result.Add(0x00);
            }

            return result.ToArray();
        }


        // First, read from the readahead area into the sequence one byte at a time.
        // Then, see if the sequence already exists in the previous 1023 bytes.
        // If it does, note its position. Once we encounter a sequence that
        // is not duplicated, take the last found duplicate and compress it.
        // If we haven't found any duplicate sequences, add the first byte as raw data.
        // If we did find a duplicate sequence, and it is adjacent to the readahead area,
        // see how many bytes of that sequence can be repeated until we encounter
        // a non-duplicate byte or reach the end of the readahead area.
        public static byte[] CompressEntry(byte[] decData)
        {
            List<byte> result = new List<byte>(decData.Length);
            BinaryReader reader = new BinaryReader(new MemoryStream(decData));
            List<byte> block = new List<byte>(16);
            long decSize = decData.LongLength;
            

            byte flag = 0;
            byte flagBit = 0;

            // This repeats until we've stored the final compressed block,
            // after we reach the end of the uncompressed data.
            while (true)
            {
                // At the end of each 8-byte block (or the end of the uncompressed data),
                // append the flag and compressed block to the compressed data.
                if (flagBit == 8 || reader.BaseStream.Position >= decSize)
                {
                    flag = (byte)((flag * 0x0202020202 & 0x010884422010) % 1023);
                    result.Add(flag);
                    result.AddRange(block);

                    block.Clear();
                    block.Capacity = 16;

                    flag = 0;
                    flagBit = 0;
                }

                if (reader.BaseStream.Position >= decSize)
                    break;



                long oldPos = reader.BaseStream.Position;

                int lookaheadLen = (int)Math.Min(decSize - reader.BaseStream.Position, 65);
                byte[] lookahead = reader.ReadBytes(lookaheadLen);  reader.BaseStream.Seek(oldPos, SeekOrigin.Begin);
                int searchbackLen = (int)Math.Min(reader.BaseStream.Position, 1024);
                byte[] window = decData.Skip((int)(reader.BaseStream.Position - searchbackLen)).Take(searchbackLen + lookaheadLen - 1).ToArray();

                // Find the largest matching sequence in the window.
                int s = -1;
                int l = 1;
                List<byte> seq = new List<byte>(65);
                seq.Add(lookahead[0]);

                for (; l <= lookaheadLen; ++l)
                {
                    int lastS = s;
                    if (searchbackLen < 1)
                        break;

                    s = window.LastIndexOfSeq(seq.ToArray(), searchbackLen - 1);

                    if (s == -1)
                    {
                        if (l > 1)
                        {
                            --l;
                            seq.RemoveAt(seq.Count - 1);
                        }
                        s = lastS;
                        break;
                    }

                    if (l == lookaheadLen)
                        break;

                    seq.Add(lookahead[l]);
                }

                // if (seq.size() >= 2)
                if (l >= 2 && s != 1)
                {
                    // We found a duplicate sequence
                    int repeatData = 0;
                    repeatData |= 1024 - searchbackLen + s;
                    repeatData |= (l - 2) << 10;
                    block.Add((byte)repeatData); block.Add((byte)(repeatData >> 8));
                }
                else
                {
                    // We found a new raw byte
                    flag |= (byte)(1 << flagBit);
                    block.AddRange(seq);
                }


                ++flagBit;
                // Seek forward to the end of the duplicated sequence,
                // in case it continued into the lookahead buffer.
                reader.BaseStream.Seek(l, SeekOrigin.Current);
            }

            return result.ToArray();
        }


        // This is the compression scheme used for
        // individual files in an spc archive
        public static byte[] DecompressEntry(byte[] cmpData)
        {
            List<byte> result = new List<byte>();
            MemoryStream reader = new MemoryStream(cmpData);
            long cmpSize = cmpData.LongLength;

            long flag = 1;
            while (reader.Position < cmpSize)
            {
                if (flag == 1)
                    flag = 0x100 | (reader.ReadByte() * 0x0202020202 & 0x010884422010) % 1023;

                if ((flag & 1) == 1)
                {
                    result.Add((byte)reader.ReadByte());
                }
                else
                {
                    // Pull from the buffer
                    // xxxxxxyy yyyyyyyy
                    // Count  -> x + 2 (max length of 65 bytes)
                    // Offset -> y (from the beginning of a 1023-byte sliding window)
                    ushort b = (ushort)(reader.ReadByte() | (reader.ReadByte() << 8));
                    byte count = (byte)((b >> 10) + 2);
                    short offset = (short)(b & 1023);

                    for (int i = 0; i < count; ++i)
                    {
                        int reverse_index = result.Count - 1024 + offset;
                        result.Add(result[reverse_index]);
                    }
                }

                flag >>= 1;
            }

            return result.ToArray();
        }

        public override Dictionary<string, (ToBytes, FromBytes)> FileConversions => new Dictionary<string, (ToBytes, FromBytes)>
        {
            { FileExtensionDefault, (ToBytesDefault, FromBytesDefault) },
        };
        public override string FileExtensionDefault => "SPC";
    }

    public class SPCEntry
    {
        /// <summary>
        /// 0x01: Uncompressed;
        /// 0x02: Compressed;
        /// 0x03: Load from external file (?) 
        /// </summary>
        public ushort CmpFlag;
        public ushort UnkFlag;
        public int CmpSize;
        public int DecSize;
        public string Filename;
        public byte[] Contents;

        public V3Format Load()
        {
            V3Format loaded = Path.GetExtension(Filename).ToUpper() switch
            {
                //".DAT" => ,
                //".SPC" => ,
                //".SRD" => ,
                ".STX" => new STX(this),
                ".WRD" => new WRD(this, MainForm.currentSPCFilename, Filename),
                _ => null,
            };
            loaded.FromBytesDefault(Contents);
            return loaded;
        }
    }
}
