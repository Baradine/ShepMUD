using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepMUDClient
{
    static class Chat
    {
        public static List<Channel> channels = new List<Channel>();
        public static void HandleChat(Byte[] data, int index, int endex)
        {
            // Last 4 bytes detail  what channel it gets sent to.  We can use channels for private messages as well, simply by protecting the first
            // However many digits that are normal/global channels, and the rest are assigned to users/guilds.
            // Given how that's over 4 Billion different channels, I highly doubt we'll ever reach a limit.
            string str = System.Text.Encoding.UTF8.GetString(data, index, endex - 4);
            int ch = BitConverter.ToInt32(data, endex - 3);

            bool found = false;
            foreach (Channel c in channels)
            {
                if (c.channelID == ch)
                {
                    found = true;
                    c.AddMessage(str);
                }
            }
            if (!found)
            {
                Channel c = new Channel(ch);
                channels.Add(c);
                c.AddMessage(str);
            }
        }

        public static void Update(int id, string str)
        {
            // Once the GUI is up and running, we'd also have this add extra chat tabs
            // If necessary.  For now, just outputs to console.
            Console.WriteLine(str);
        }

        public static void InitGlobal()
        {
            Channel global = new Channel(Channel.GLOBAL);
            channels[Channel.GLOBAL] = global;
        }
    }
}
