using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ShepMUD
{
    class Channel
    {
        public const int ADMIN = 0;
        public const int GLOBAL = 1;
        public const int MAX_LOG = 5000;

        int channelID;

        Message[] messageLog;
        int currentIndex;

        List<ConnectedUser> subUsers;

        public Channel(int ID)
        {
            this.channelID = ID;
            this.messageLog = new Message[MAX_LOG];
            this.currentIndex = 0;
            subUsers = new List<ConnectedUser>();
        }

        public void HandleMessage(string str, int sendID)
        {
            Message mess = new Message(str, sendID);
            messageLog[currentIndex] = mess;
            currentIndex++;
            string username = ClientHandler.GetUsername(sendID);
            string message = username + ": " + str;
            SendMessage(message, channelID);
        }

        void SendMessage(string message, int ch)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            Array.Resize(ref data, 260);
            int i = data.Length - 4;
            byte[] channel = BitConverter.GetBytes(ch);
            foreach(byte b in channel)
            {
                data[i] = b;
                i++;
            }

            Program.net.SendToUsers(subUsers, data, 1, new byte[] { 1, 0, 0, 0 });
        }

        public void AddSubscriber(ConnectedUser user)
        {
            subUsers.Add(user);
        }

        public void RemoveSubscriber(ConnectedUser user)
        {
            subUsers.Remove(user);
        }


    }

    struct Message
    {
        public string message;
        public int senderID;
        public Message(string str, int ID)
        {
            this.message = str;
            this.senderID = ID;
        }
    }
}
