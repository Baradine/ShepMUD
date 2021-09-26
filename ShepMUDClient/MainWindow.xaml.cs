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
        bool test = false;
        // public TextBlock tb { set; get; }
        public Window1()
        {
            InitializeComponent();
            Title = "ShepMUD";
            Width = 960;
            Height = 540;
            Show();

            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer.Start();
            //ExecutionLoop();
        }

        public void ChangeText(string text)
        {
            ConsoleBox.Text = text;
        }

        

        public void ExecutionLoop(object sender, EventArgs e)
        {
            
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (test)
            {
                ChangeText("test");
                test = false;
            }
            else if (!test)
            {
                ChangeText("blessed");
                test = true;
            }
            
        }

    }
}
