using System;
using System.Net;

namespace ShepMUDClient
{
    class Program
    {
        static void Main(string[] args)
        {
            

            Console.Write("Input IP Address: ");
            string ip = Console.ReadLine();
            Console.Write("Input Port: ");
            int port = 25565;
            try
            {
                port = int.Parse(Console.ReadLine());
            } catch (Exception e)
            {
                Console.WriteLine("There was an issue with the port");
            }

            if(ip == "default")
            {
                ip = "73.180.152.206";
                port = 25565;
            }


            NetworkConnection multiClient = new NetworkConnection(port, IPAddress.Parse(ip));
            multiClient.TransmitToServer("A new client has appeared.");
        }
    }
}
