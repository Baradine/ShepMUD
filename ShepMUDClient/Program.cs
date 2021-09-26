using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Media;

namespace ShepMUDClient 
{
    class Program : System.Windows.Application
    {
        Window1 mainWindow = new Window1();
        [STAThread]
        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.Run();
            //string ip = "default";
            //int port = 25565;

            //ConsoleHandler cs = new ConsoleHandler();

            // Console.Write("Input IP Address: ");
            //string ip = Console.ReadLine();
            //
            //if (ip == "default")
            //{
            //    ip = "73.180.152.206";
            //}
            //else if(ip != "default" || ip == null)
            //{

            //   // Console.Write("Input Port: ");

            //    try
            //    {
            //        //port = int.Parse(Console.ReadLine());
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine("There was an issue with the port");
            //    }
            //}


            //NetworkConnection multiClient = new NetworkConnection(port, IPAddress.Parse(ip));
            //multiClient.Connect();
            ////multiClient.TransmitToServer("A new client has appeared.");
            //Console.Read();
        }
    }
}
