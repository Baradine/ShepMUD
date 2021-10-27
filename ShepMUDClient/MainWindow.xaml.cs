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
using System.Diagnostics;
using System.ComponentModel;

namespace ShepMUDClient
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        bool menuVis = false;   //whether or not the dropdown menu is visible. Default False.

        //Main main;
        public Window1()
        {
            InitializeComponent();
            Title = "ShepMUD";
            Width = 960;
            Height = 540;
            Show();
            //main = new Main();
            Main.Startup(this);

            
        }

        public void ChangeText(string text)
        {
            ConsoleBox.Text = text;
            ConsoleBox.ScrollToEnd(); //automatically scroll to the bottom
        }

        private void EnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Main.sendMessage();
                e.Handled = true;
            }
        }

        private void ConsoleBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /**
         *  Action Handler for Menu
         **/
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender == M1)                       //Menu1 pressed
            {
                Trace.WriteLine("M1 Pressed");                    //<---- debugging behaviour. Change before deployment.
                CommandControl.HandleCommand("~SetIP 192.168.0.244 2456");
                CommandControl.HandleCommand("~Connect");
            }
            else if (sender == M2)                  //Menu2 pressed
            {
                Trace.WriteLine("M2 Pressed");
            }
            else if (sender == M3)                 //Menu3 pressed
            {
                Trace.WriteLine("M3 Pressed");
            }
        }

        /**
         *  Actionhandler which displays the menu items
         **/
        private void menuDropDown_Click(object sender, RoutedEventArgs e)
        {
            if(menuVis == false)    //if hidden
            {
                M1.Visibility = Visibility.Visible; //make not hidden
                M2.Visibility = Visibility.Visible;
                M3.Visibility = Visibility.Visible;
            } else          //else
            {
                M1.Visibility = Visibility.Hidden;  //make hidden
                M2.Visibility = Visibility.Hidden;
                M3.Visibility = Visibility.Hidden;
            }
            menuVis = !menuVis;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            
        }
    }
}
