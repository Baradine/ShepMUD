using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ShepMUD
{
    class NetworkListener
    {
        Int32 port;
        IPAddress hostAddress;
        TcpListener server;
        Byte[] data;
        string str;
        bool listen;

        

        public NetworkListener(Int32 port, IPAddress hostIP)
        {
            this.port = port;
            this.hostAddress = hostIP;
            this.data = new Byte[256];
        }

        public void InitServer()
        {
            server = null;
            server = new TcpListener(hostAddress, port);
            server.Start();
            listen = true;
        }

        public void ReadForClients()
        {
            Thread thread = new Thread(new ThreadStart(AcceptClient));
            thread.Start();
        }

        public void AcceptClient()
        {
            while (true)
            {
                try
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected new client!");
                    ClientHandler.AddClient(client);
                    ClientHandler.ReadClientList();
                }
                catch (Exception e)
                {

                }
            }
        }

        public void ReadCurrentClientData()
        {
            NetworkStream stream;
            NetworkDataHandler dataHandle = new NetworkDataHandler();
            foreach (ConnectedUser t in ClientHandler.GetClients())
            {
                dataHandle.NewPacket(1024);
                stream = t.connection.GetStream();
                int i;
                while ((i = stream.Read(data, 0, data.Length)) != 0)
                {
                    dataHandle.AddDataToPacket(data);
                }
                GameManager.GetClientData(dataHandle.GetPacket(), t.UserID);
            }
        }

        public void SendToUsers(List<ConnectedUser> users, byte[] data)
        {
            NetworkStream stream;
            foreach (ConnectedUser u in users)
            {
                stream = u.connection.GetStream();
                stream.Write(data, 0, data.Length);
            }
        }

        //public void StartListening()
        //{
        //    try
        //    {
        //        while (listen)
        //        {
        //            Console.Write("Waiting for a connection...");

        //            TcpClient client = server.AcceptTcpClient();
        //            Console.WriteLine("Connected!");
        //            str = null;

        //            NetworkStream stream = client.GetStream();
        //            int i;

        //            while ((i = stream.Read(data, 0, data.Length)) != 0)
        //            {
        //                str = System.Text.Encoding.ASCII.GetString(data, 0, i);
        //                Console.WriteLine("Received: {0}", str);

        //                str = str.ToUpper();

        //                byte[] msg = System.Text.Encoding.ASCII.GetBytes(str);

        //                // Send back a response.
        //                stream.Write(msg, 0, msg.Length);
        //                Console.WriteLine("Sent: {0}", str);
        //            }
        //            client.Close();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("SocketException: {0}", e);
        //    }
        //    finally
        //    {
        //        server.Stop();
        //    }
        //    Console.WriteLine("\nHit enter to continue...");
        //    Console.Read();
        //}
    }
}
