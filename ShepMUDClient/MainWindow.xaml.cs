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
    }
}
