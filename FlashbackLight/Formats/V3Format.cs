using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashbackLight.Formats
{
    public abstract class V3Format
    {
        //public delegate byte[] ToBytes();

        public SPCEntry entry;

        /// <summary>
        /// ONLY for SPC files.
        /// </summary>
        [Obsolete("ONLY use this constructor for SPC files.")]
        protected V3Format()
        {

        }

        protected V3Format(SPCEntry entry)
        {
            this.entry = entry;

            if (entry.CmpFlag == 0x02) // Decompress data if needed
            {
                byte[] result = SPC.DecompressEntry(entry.Contents);
                if (result.Length != entry.DecSize)
                {
                    throw new Exception($"Size mismatch: Size was {result.Length} but should be {entry.DecSize}");
                }
                entry.Contents = result;
                entry.CmpFlag = 0x01;
                entry.CmpSize = entry.Contents.Length;
            }
        }

        public delegate byte[] ToBytes();
        public delegate void FromBytes(byte[] bytes);

        public abstract Dictionary<string, (ToBytes toBytes, FromBytes fromBytes)> FileConversions { get; }

        public abstract byte[] ToBytesDefault();
        public abstract void FromBytesDefault(byte[] bytes);
        public abstract string FileExtensionDefault { get; }

        public void UpdateSPCEntry()
        {
            entry.Contents = ToBytesDefault();
            entry.CmpSize = entry.Contents.Length;
            entry.DecSize = entry.Contents.Length;
        }

        public void Recompress()
        {
            if (entry.CmpFlag == 0x01)
            {
                byte[] result = SPC.CompressEntry(entry.Contents);
                if (result.Length < entry.Contents.Length)
                {
                    entry.DecSize = entry.Contents.Length;
                    entry.Contents = result;
                    entry.CmpFlag = 0x02;
                    entry.CmpSize = entry.Contents.Length;
                }
            }
        }
    }
}
