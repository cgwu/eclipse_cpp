using System;
using Assergs.Windows;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Input;

namespace Assergs.Windows.Tests
{
	public partial class MessageWindow : ToolWindow
	{
		public MessageWindow()
		{
			InitializeComponent();

            this.btn1.Click += new RoutedEventHandler(btn1_Click);
		}

        void btn1_Click(object sender, RoutedEventArgs e)
        {
            this.CanDrag = false;
        }
	}
}