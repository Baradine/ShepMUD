using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepMUD
{
    static class GameManager
    {
        public static void GetClientData(Byte[] data, int ID)
        {
            //First 4 bytes flag data types sent, translated using a bitmask.
            if ((data[0] & 1) == 1)
            {
                Chat.HandleChat(data, 4, 260, ID);
            }
        }
    }
}
