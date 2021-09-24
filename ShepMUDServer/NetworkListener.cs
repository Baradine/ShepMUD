using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void StartListening()
        {
            try
            {
                while (listen)
                {
                    Console.Write("Waiting for a connection...");

                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    str = null;

                    NetworkStream stream = client.GetStream();
                    int i;

                    while ((i = stream.Read(data, 0, data.Length)) != 0)
                    {
                        str = System.Text.Encoding.ASCII.GetString(data, 0, i);
                        Console.WriteLine("Received: {0}", str);

                        str = str.ToUpper();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(str);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", str);
                    }
                    client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }
            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}
