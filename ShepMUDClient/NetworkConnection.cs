using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ShepMUDClient
{
    class NetworkConnection
    {
        Int32 hostPort;
        IPAddress hostIP;
        TcpClient client;
        public NetworkConnection(Int32 port, IPAddress ip)
        {
            this.hostPort = port;
            this.hostIP = ip;
            client = new TcpClient();
        }

        public void Connect()
        {
            int i = 0;
            while (i < 4)
            {
                Console.Write("Trying to connect...");
                try
                {
                    client.Connect(hostIP, hostPort);
                    i = 10;
                }
                catch (Exception e)
                {
                    Console.Write("Connection Failed. ");
                    i++;
                    Thread.Sleep(2000);
                }
            }
            if (i < 10)
            {
                Console.Write("The server didn't respond.  Exiting.  Press enter to continue.");
                Console.Read();
                Environment.Exit(65);
            }
            i = 0;
        }

        public void Read()
        {
            // TODO: Read first 5 bytes, then use that header data to determine how much of the rest
            // We should read.
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] data = new byte[265];
                stream.Read(data, 0, data.Length);
            }
            catch (Exception e)
            { }
        }

        public void TransmitToServer(string message)
        {
            try
            {

                client.Connect(hostIP, hostPort);
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                NetworkStream stream = client.GetStream();

                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: {0}", message);

                data = new Byte[256];
                String responseData = String.Empty;
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);

                //stream.Close();
                //client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("NetworkError: {0}", e);
            }
            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}
