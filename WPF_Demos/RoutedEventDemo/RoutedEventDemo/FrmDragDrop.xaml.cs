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
    /// FrmDragDrop.xaml 的交互逻辑
    /// </summary>
    public partial class FrmDragDrop : Window
    {
        public FrmDragDrop()
        {
            InitializeComponent();
        }

        private void textBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox txtSource = sender as TextBox;
            DragDrop.DoDragDrop(txtSource, txtSource.Text, DragDropEffects.Copy);
            //DragDrop.DoDragDrop(txtSource, txtSource.Text, DragDropEffects.All);
        }

  

        private void lblTarget1_Drop(object sender, DragEventArgs e)
        {
            (sender as Label).Content = e.Data.GetData(DataFormats.Text);
        }

        private void lblTarget2_Drop(object sender, DragEventArgs e)
        {
            (sender as Label).Content = e.Data.GetData(DataFormats.Text);
        }
    }
}
