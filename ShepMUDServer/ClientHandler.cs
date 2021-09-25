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
        static List<TcpClient> connectedClients = new List<TcpClient>();

        public static void AddClient(TcpClient client)
        {
            mut.WaitOne();
            bool noAdd = false;
            foreach (TcpClient t in connectedClients)
            {
                if (client.Equals(t))
                {
                    noAdd = true;
                }
            }
            if (!noAdd)
            {
                connectedClients.Add(client);
            }
            mut.ReleaseMutex();
        }

        public static void DropClient(TcpClient client)
        {
            mut.WaitOne();
            connectedClients.Remove(client);
            mut.ReleaseMutex();
        }

        public static void ReadClientList()
        {
            foreach (TcpClient t in connectedClients)
            {
                Console.Write(t.ToString() + " ");
            }
        }

        // Use this for read only operations, please and thank you.
        public static List<TcpClient> GetClients()
        {
            return connectedClients;
        }
    }
}
