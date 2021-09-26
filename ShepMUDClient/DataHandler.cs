using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepMUDClient
{
    static class DataHandler
    {
        public static void TranslateData(byte[] data)
        {
            //First 4 bytes after the 1st byte flag data types sent, translated using a bitmask.
            if ((data[1] & 1) == 1)
            {
                Chat.HandleChat(data, 5, 264);
            }
        }
    }
}
