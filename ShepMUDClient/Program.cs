using System;
using System.Net;

namespace ShepMUDClient
{
    class Program
    {
        static void Main(string[] args)
        {

            int port = 25565;

            Console.Write("Input IP Address: ");
            string ip = Console.ReadLine();
            if (ip == "default")
            {
                ip = "73.180.152.206";
            }
            else
            {
                Console.Write("Input Port: ");
                
                try
                {
                    port = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("There was an issue with the port");
                }
            }
            NetworkConnection multiClient = new NetworkConnection(port, IPAddress.Parse(ip));
            multiClient.Connect();
            //multiClient.TransmitToServer("A new client has appeared.");
        }
    }
}
