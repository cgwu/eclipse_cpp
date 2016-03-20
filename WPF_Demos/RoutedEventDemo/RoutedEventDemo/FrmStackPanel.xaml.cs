using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RoutedEventDemo
{
    /// <summary>
    /// FrmStackPanel.xaml 的交互逻辑
    /// </summary>
    public partial class FrmStackPanel : Window
    {
        public FrmStackPanel()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(e.Source == cmd1)
            {
                MessageBox.Show("cmd1 clicked.");
            }
            else if (e.Source == cmd2)
            {
                MessageBox.Show("cmd2 clicked.");
            }
            else if (e.Source == cmd3)
            {
                MessageBox.Show("cmd3 clicked.");
            }
        }

        private void StackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            Point pt = e.GetPosition(this);
            lblPos.Content = String.Format("You are at({0},{1}) in window corrdinates.", pt.X, pt.Y);
        }
    }
}
