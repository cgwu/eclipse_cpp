using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MSMQNoSecurityClient.MSMQServiceReference;

namespace MSMQNoSecurityClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MSMQServiceReference.MSMQServiceClient client;
        public MainWindow()
        {
            InitializeComponent();
            client=new MSMQServiceClient();
        }

        private void buttonSendMsg_Click(object sender, RoutedEventArgs e)
        {
            client.ShowMessage(textBox1.Text.ToString());
            this.Title = "Done at :" + DateTime.Now.ToString();
        }

     
    }
}
