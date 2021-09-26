using System;
using System.Net;
using System.Threading;

namespace ShepMUD
{
    static class Program
    {
        public static NetworkListener net;
        static void Main(string[] args)
        {
            Chat.InitGlobal();
            net = new NetworkListener(25565, IPAddress.Parse("10.0.0.13"));
            net.InitServer();
            net.ReadForClients();
            Thread listenerThread = new Thread(new ThreadStart(net.ReadCurrentClientData));
            listenerThread.Start();
        }
    }
}
