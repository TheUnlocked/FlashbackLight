using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashbackLight.Utils
{
    public static class ExtensionHelper
    {
        public static int LastIndexOfSeq<T>(this T[] array, T[] subset, int from)
        {
            int start = subset.Length - 1;
            
            int searchPos = start;
            for (int i = Math.Min(from + start, array.Length - 1); i >= 0; i--)
            {
                if (EqualityComparer<T>.Default.Equals(array[i], subset[searchPos]))
                {
                    if (searchPos-- == 0)
                    {
                        return i;
                    }
                }
                else
                {
                    searchPos = start;
                }
            }

            return -1;
        }

        public static int LastIndexOfSeq<T>(this T[] array, T[] subset)
        {
            return LastIndexOfSeq(array, subset, array.Length - 1);
        }
    }
}
