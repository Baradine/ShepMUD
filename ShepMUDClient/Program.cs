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


            Window mainWindow = new Window();
            mainWindow.Title = "ShepMUD";
            mainWindow.Width = 400;
            mainWindow.Height = 300;
            mainWindow.ShowDialog();
            
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

            if(ip == "default" || ip == null)
            {
                ip = "73.180.152.206";
                port = 25565;
            }


            NetworkConnection multiClient = new NetworkConnection(port, IPAddress.Parse(ip));
            multiClient.TransmitToServer("A new client has appeared.");
        }
    }
}
