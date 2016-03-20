using System;
using Assergs.Windows;
using System.Windows;

namespace Assergs.Windows.Tests
{
	public partial class HelpWindow : ToolWindow
	{
		public HelpWindow()
		{
			InitializeComponent();
		}

		void ColorizationButton_Click(object sender, RoutedEventArgs e)
		{
            ColorationWindow window = new ColorationWindow();
            window.Parent = this.Parent;
            window.ShowStatusBar = false;
            window.Show();
		}

		void SampleButton_Click(object sender, RoutedEventArgs e)
		{
			PersonWindow window = new PersonWindow();
            window.Parent = this.Parent;
            window.StartPosition = ToolWindowStartPosition.CenterParent;
            window.Show();
		}

		void ModalButton_Click(object sender, RoutedEventArgs e)
		{
			MessageWindow window = new MessageWindow();
            window.Parent = this.Parent;
            window.StartPosition = ToolWindowStartPosition.CenterParent;
			window.ShowModalDialog();
		}

        private void MessageBoxSample_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxSamples window = new MessageBoxSamples();
            window.Parent = this.Parent;
            window.StartPosition = ToolWindowStartPosition.CenterParent;
            window.Show();
        }

		private void DataGridSample_Click(object sender, RoutedEventArgs e)
        {
            //DataGridSample window = new DataGridSample();
            //window.Parent = this.Parent;
            //window.StartPosition = ToolWindowStartPosition.CenterParent;
            //window.Show();
        }
	}
}