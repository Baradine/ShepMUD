using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace ShepMUD
{
    
    static class ClientHandler
    {
        private static Mutex mut = new Mutex();
        //static List<TcpClient> connectedClients = new List<TcpClient>();
        static List<ConnectedUser> connectedClients = new List<ConnectedUser>();

        // Replace this with a database call later
        static int nextUniqueID = 1000;

        public static void AddClient(TcpClient client)
        {
            mut.WaitOne();
            bool noAdd = false;
            foreach (ConnectedUser t in connectedClients)
            {
                // This currently doesn't work, we'd replace it with the login check later on
                if (client.Equals(t.connection))
                {
                    noAdd = true;
                }
            }
            if (!noAdd)
            {
                
                ConnectedUser newUser = new ConnectedUser(client, nextUniqueID);
                connectedClients.Add(newUser);
                Chat.channels[Channel.GLOBAL].AddSubscriber(newUser);
                nextUniqueID++;
            }
            mut.ReleaseMutex();
        }

        public static void DropClient(ConnectedUser client)
        {
            mut.WaitOne();
            connectedClients.Remove(client);
            Chat.channels[Channel.GLOBAL].RemoveSubscriber(client);
            mut.ReleaseMutex();
        }

        public static void ReadClientList()
        {
            foreach (ConnectedUser t in connectedClients)
            {
                Console.Write(t.connection.ToString() + " ");
            }
        }

        // Use this for read only operations, please and thank you.
        public static List<ConnectedUser> GetClients()
        {
            mut.WaitOne();
            List<ConnectedUser> c = new List<ConnectedUser>();
            foreach (ConnectedUser u in connectedClients)
            {
                c.Add(u);
            }
            mut.ReleaseMutex();
            return c;
        }

        //For now, this will just spit back out the ID as a string.  We'll have an actual lookup table added once we add logons
        public static string GetUsername(int ID)
        {
            return "" + ID;
        }
    }

    public struct ConnectedUser
    {
        public TcpClient connection;
        public int UserID;

        public ConnectedUser(TcpClient c, int ID)
        {
            this.connection = c;
            this.UserID = ID;
        }
    }

}
