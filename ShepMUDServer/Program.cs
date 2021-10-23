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
            net = new NetworkListener(2456, IPAddress.Parse("192.168.0.244"));
            net.InitServer();
            net.ReadForClients();
            Thread listenerThread = new Thread(new ThreadStart(net.ReadCurrentClientData));
            listenerThread.Start();
        }
    }
}
