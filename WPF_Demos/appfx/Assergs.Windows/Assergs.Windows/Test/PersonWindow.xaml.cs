using System;
using Assergs.Windows;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Assergs.Windows.Tests
{
	public partial class PersonWindow : ToolWindow
	{
		public PersonWindow()
		{
			InitializeComponent();
		}

		# region OnButtonClick

		private void OnButtonClick(object sender, RoutedEventArgs e)
		{
            ColorChooser colorChooser = new ColorChooser();
            colorChooser.ToolWindowState = ToolWindowState.Maximized;
            colorChooser.StartPosition = ToolWindowStartPosition.CenterParent;
            colorChooser.Parent = this.Parent;
            colorChooser.Show();
		}
		
		# endregion
	}
}