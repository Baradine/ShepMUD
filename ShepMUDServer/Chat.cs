using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepMUD
{
    static class Chat
    {
        static Channel[] channels = new Channel[100000];
        public static void HandleChat(Byte[] data, int index, int endex, int ID)
        {
            // Last 4 bytes detail  what channel it gets sent to.  We can use channels for private messages as well, simply by protecting the first
            // However many digits that are normal/global channels, and the rest are assigned to users/guilds.
            // Given how that's over 4 Billion different channels, I highly doubt we'll ever reach a limit.
            string str = System.Text.Encoding.UTF8.GetString(data, index, endex-4);
            int ch = BitConverter.ToInt32(data, endex - 3);

            if (channels[ch] == null)
            {
                Channel channel = new Channel(ch);
                channels[ch] = channel;
                channel.HandleMessage(str, ID);
            }
            else
            {
                channels[ch].HandleMessage(str, ID);
            }
        }
    }
}
