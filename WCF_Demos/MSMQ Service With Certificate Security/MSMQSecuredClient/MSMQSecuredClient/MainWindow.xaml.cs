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

namespace MSMQSecuredClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SecureMSMQServiceReference.SecuredMSMQServiceClient client;
        public MainWindow()
        {
            InitializeComponent();
            client = new SecureMSMQServiceReference.SecuredMSMQServiceClient();
        }

        private void buttonSendMsg_Click(object sender, RoutedEventArgs e)
        {
            client.ShowSecureMessage(textBox1.Text.ToString());
        }
    }
}
