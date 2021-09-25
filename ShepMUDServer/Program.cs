using System;
using System.Net;

namespace ShepMUD
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkListener net = new NetworkListener(25565, IPAddress.Parse("10.0.0.13"));
            net.InitServer();
            net.ReadForClients();
        }
    }
}
