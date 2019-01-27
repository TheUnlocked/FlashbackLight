using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashbackLight.Formats
{
    abstract class V3Format
    {
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

        public abstract byte[] ToBytes();

        public void UpdateSPCEntry()
        {
            entry.Contents = ToBytes();
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
