using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace ShepMUDClient
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            string ip = "default";
            int port = 25565;

            ConsoleHandler cs = new ConsoleHandler();

            Window1 mainWindow = new Window1();
            mainWindow.Title = "ShepMUD";
            mainWindow.Width = 960;
            mainWindow.Height = 540;
            mainWindow.ShowDialog();

            

            //mainWindow.tb.Text = "Test\nfor\nfucks sake";
            //mainWindow.tb.Inlines.Add("asdhjasghkdahsdsa");

            // Console.Write("Input IP Address: ");
            //string ip = Console.ReadLine();
            /*
            if (ip == "default")
            {
                ip = "73.180.152.206";
            }
            else if(ip != "default" || ip == null)
            {

               // Console.Write("Input Port: ");

                try
                {
                    //port = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("There was an issue with the port");
                }
            }


            NetworkConnection multiClient = new NetworkConnection(port, IPAddress.Parse(ip));
            multiClient.Connect();
            //multiClient.TransmitToServer("A new client has appeared.");
            */
        }
    }
}
