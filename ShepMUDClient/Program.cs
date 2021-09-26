using System;
using System.Net;
using System.Windows;


namespace ShepMUDClient
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            int port = 25565;

            Window mainWindow = new Window();
            mainWindow.Title = "ShepMUD";
            mainWindow.Width = 400;
            mainWindow.Height = 300;
            mainWindow.ShowDialog();
            
            Console.Write("Input IP Address: ");
            string ip = Console.ReadLine();
            if (ip == "default")
            {
                ip = "73.180.152.206";
            }
            else if(ip != "default" || ip == null)
            {
                Console.Write("Input Port: ");
                ip = "127.0.0.1";
                
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
