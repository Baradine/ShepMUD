using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepMUDClient
{
    class Channel
    {
        public const int ADMIN = 0;
        public const int GLOBAL = 1;
        public const int MAX_LOG = 500;

        public int channelID { get; }

        string[] messageLog;

        // We might allow for persistant chat logs saved client side later, for now, restart log every time.
        int currentIndex;

        public Channel(int id)
        {
            this.channelID = id;
            this.messageLog = new string[MAX_LOG];
            this.currentIndex = 0;
        }

        public void AddMessage(string str)
        {
            messageLog[currentIndex] = str;
            if (currentIndex == MAX_LOG - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
            
            Chat.Update(channelID, str);
        }
    }
}
