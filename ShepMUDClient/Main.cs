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
        public static ConsoleHandler console { get; set; }
        public delegate void SystemTimerDelegate();

        public static NetworkConnection connect;
        public static void Startup(Window1 win)
        {
            window = win;
            console = new ConsoleHandler(win);

            Chat.InitGlobal();
            CommandControl.InitCommands();
            console.SetCurrentChannel(Chat.GetChannel(Channel.GLOBAL));
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
            if (connect != null)
            {
                connect.Read();
            }
        }

        public static void sendMessage()
        {
            string message = window.InputBox.Text;

            if (message.Length == 0)
            {
                return;
            }

            //Command check
            if (message[0] == '~') //~ is command operators
            {
                //console.executeCommand(message);
                CommandControl.HandleCommand(message);
                window.InputBox.Text = "";
                return;
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
            if (connect != null)
            {
                connect.TransmitToServer(0, new byte[] { 1, 0, 0, 0 }, data);
            }

            window.InputBox.Text = "";
        }

        public static void WriteToChat()
        {
            console.writeLine();
        }
    }
}
