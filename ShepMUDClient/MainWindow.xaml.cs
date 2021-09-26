using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ShepMUDClient
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        bool textBlockChange = true;
        private const int CLOCK_SPEED = 100;
        private static System.Timers.Timer aTimer;
        public ConsoleHandler console { get; set; }
        public delegate void SystemTimerDelegate();
        // public TextBlock tb { set; get; }
        public Window1()
        {

            console = new ConsoleHandler(this);

            InitializeComponent();
            Title = "ShepMUD";
            Width = 960;
            Height = 540;
            Show();
            TimingStart();
        }

        public void TimingStart()
        {
            aTimer = new System.Timers.Timer(CLOCK_SPEED);
            aTimer.Elapsed += ExecutionLoop;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public void ChangeText(string text)
        {
            ConsoleBox.Text = text;
        }

        

        public void ExecutionLoop(object sender, EventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new SystemTimerDelegate(dispatcherTimer_Tick));
        }

        private void dispatcherTimer_Tick()
        {
            if (console.IsConsoleUpdated())
            {
                console.getUpdate();
            }
        }

    }
}
