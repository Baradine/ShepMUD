using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepMUD
{
    static class Utility
    {
        public static byte[] ShiftAndResizeArray(byte[] arr, int shift)
        {
            if (shift == 0)
            {
                return arr;
            }
            if ((arr.Length + shift) <= 0)
            {
                // should probably do an error code here, at some point.
                return arr;
            }
            byte[] newArr = new byte[arr.Length + shift];

            for (int i = shift; i < newArr.Length; i++)
            {
                if (i < 0)
                {
                    continue;
                }
                newArr[i] = arr[(i - shift)];
            }
            return newArr;
        }

    }
}
