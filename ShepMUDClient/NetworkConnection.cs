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
        public NetworkConnection(Int32 port, string ip)
        {
            this.hostPort = port;
            this.hostIP = IPAddress.Parse(ip);
            client = new TcpClient();
        }

        public void Connect()
        {
            int i = 0;
            while (i < 4)
            {
                Main.WriteToChat("Trying to connect...");
                try
                {
                    client.Connect(hostIP, hostPort);
                    i = 10;
                }
                catch (Exception e)
                {
                    Main.WriteToChat("Connection Failed. ");
                    i++;
                    Thread.Sleep(2000);
                }
            }
            if (i < 10)
            {
                Main.WriteToChat("The server didn't respond.  Exiting.  Press enter to continue.");
                Environment.Exit(65);
            }
            else
            {
                Main.WriteToChat("Connected!");
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
                if (stream.DataAvailable)
                {
                    stream.Read(data, 0, data.Length);
                    DataHandler.TranslateData(data);
                }
                
            }
            catch (Exception e)
            { }
        }

        public void TransmitToServer(byte header, byte[] mask, byte[] data)
        {
            try
            {
                byte[] message = data;
                //Array.Reverse(message);
                //Array.Resize(ref message, data.Length + 5);
                //Array.Reverse(mask);
                message = Utility.ShiftAndResizeArray(message, 5);
                message[0] = header;
                int index = 1;
                foreach (byte b in mask)
                {
                    message[index] = b;
                    index++;
                }
                
                //Array.Reverse(message);

                NetworkStream stream = client.GetStream();

                stream.Write(message);

                //client.Connect(hostIP, hostPort);
                //Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                //NetworkStream stream = client.GetStream();

                //stream.Write(data, 0, data.Length);
                //Console.WriteLine("Sent: {0}", message);

                //data = new Byte[265];
                //String responseData = String.Empty;
                //Int32 bytes = stream.Read(data, 0, data.Length);
                //responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                //Console.WriteLine("Received: {0}", responseData);

                //stream.Close();
                //client.Close();
            }
            catch (Exception e)
            {

                //Main.WriteToChat(e.ToString());
            }
            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}
