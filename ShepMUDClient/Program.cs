using System;
using System.Net;

namespace ShepMUDClient
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkConnection multiClient = new NetworkConnection(13000, IPAddress.Parse("10.0.0.13"));
            multiClient.TransmitToServer("do you read cap'n");
        }
    }
}
