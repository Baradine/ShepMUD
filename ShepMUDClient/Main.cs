using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ShepMUDClient
{
    static class Main
    {
        private static System.Timers.Timer aTimer;

        private static Window1 window;
        private const int CLOCK_SPEED = 100;
        static ConsoleHandler console { get; set; }
        public delegate void SystemTimerDelegate();

        static NetworkConnection connect;
        public static void Startup(Window1 win)
        {
            window = win;
            console = new ConsoleHandler(win);
            connect = new NetworkConnection(25565, "73.180.152.206");
            Thread thread = new Thread(new ThreadStart(connect.Connect));
            thread.Start();
            TimingStart();
        }

       static void TimingStart()
        {
            aTimer = new System.Timers.Timer(CLOCK_SPEED);
            aTimer.Elapsed += ExecutionLoop;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public static void ExecutionLoop(object sender, EventArgs e)
        {
            window.Dispatcher.Invoke(DispatcherPriority.Normal, new SystemTimerDelegate(dispatcherTimer_Tick));
        }
        
        private static void dispatcherTimer_Tick()
        {
            if (console.IsConsoleUpdated())
            {
                console.getUpdate();
            }
            connect.Read();
        }

        public static void sendMessage()
        {
            string message = window.InputBox.Text;

            //Command check
            if (message[0] == '~') //~ is command operators
            {
                console.executeCommand(message);
            }
            //------

            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            if (data.Length > 256)
            {
                Array.Resize(ref data, 256);
            }
            Array.Resize(ref data, 260);
            // Change this later to a byte array that we plug into the chat.  For now, only global
            data[256] = 1;
            connect.TransmitToServer(0, new byte[] { 1, 0, 0, 0 }, data);

            window.InputBox.Text = "";
        }

        public static void WriteToChat(string str)
        {
            console.writeLine(str);
        }
    }
}
