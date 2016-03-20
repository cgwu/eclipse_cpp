# region Using Directives

using System;
using Assergs.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

using System.Windows.Threading;
using System.Threading;
using System.Windows.Input;
using Assergs.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;

# endregion

namespace Assergs.Windows.Tests
{
	public partial class MainPage : Page
	{
        #region Constructor

		public MainPage()
		{
			InitializeComponent();
			ToolWindow.ModalContainerPanel = this._ModalContainer;
			this.btnHelp_Click(null, null);

			CommandBinding cmdMaximizeAllWindows = new CommandBinding(
													   ToolWindowDockPanel.MaximizeAllWindowsCommand,
													   new ExecutedRoutedEventHandler(this.MaximizeAllWindow_Executed),
													   new CanExecuteRoutedEventHandler(this.CanExecuteMaximizeCommand));
			this.CommandBindings.Add(cmdMaximizeAllWindows);

			CommandBinding cmdRestoreAllWindows = new CommandBinding(
													  ToolWindowDockPanel.RestoreAllWindowsCommand,
													  new ExecutedRoutedEventHandler(this.RestoreAllWindow_Executed),
													  new CanExecuteRoutedEventHandler(this.CanExecuteRestoreCommand));
			this.CommandBindings.Add(cmdRestoreAllWindows);
		} 

		#endregion

		#region MaximizeAllWindow_Executed

		private void MaximizeAllWindow_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			this.content.MaximizeAllWindows();
		} 

		#endregion

		#region RestoreAllWindow_Executed

		private void RestoreAllWindow_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			this.content.RestoreAllWindows();
		} 

		#endregion

		#region CanExecuteMaximizeCommand

		private void CanExecuteMaximizeCommand(object sender, CanExecuteRoutedEventArgs e)
		{
			foreach (ToolWindow window in this.content.OpenedWindows)
			{
				if (window.IsMaximized == false)
				{
					e.CanExecute = true;
					return;
				}
			}

			e.CanExecute = false;
		} 

		#endregion

		#region CanExecuteRestoreCommand

		private void CanExecuteRestoreCommand(object sender, CanExecuteRoutedEventArgs e)
		{
			foreach (ToolWindow window in this.content.OpenedWindows)
			{
				if (window.IsMaximized == true)
				{
					e.CanExecute = true;
					return;
				}
			}

			e.CanExecute = false;
		} 

		#endregion

		#region WindowsSubmenu_Opened

		private void WindowsSubmenu_Opened(object sender, RoutedEventArgs e)
		{
			this._WrapPanel.Children.Clear();

			foreach (ToolWindow window in this.content.OpenedWindows)
			{
				ReflectedButton btn = new ReflectedButton();

				Image img = new Image();
				img.VerticalAlignment = VerticalAlignment.Top;
				img.Margin = new Thickness(2);
				img.Width = 64;

				BitmapImage iconImage = new BitmapImage();
				iconImage.BeginInit();
				iconImage.UriSource = new Uri("Resources/Icons/128x128/Window128.png", UriKind.Relative);
				iconImage.DecodePixelWidth = 64;
				iconImage.EndInit();

				img.Source = iconImage;

				btn.Content = img;
				btn.Margin = new Thickness(5);
				btn.Text = window.Header.ToString();
				btn.Tag = window;

				Border b = new Border();
				b.Width = 100;
				b.Height = 100;
				b.Background = Brushes.AliceBlue;

				ToolTip t = new ToolTip();

				LinearGradientBrush toolTipBackground = new LinearGradientBrush();
				toolTipBackground.StartPoint = new Point(0, 0);
				toolTipBackground.EndPoint = new Point(0, 1);
				toolTipBackground.GradientStops.Add(new GradientStop(OfficeColors.Background.OfficeColor4, 0));
				toolTipBackground.GradientStops.Add(new GradientStop(OfficeColors.Background.OfficeColor5, 0.5));
				toolTipBackground.GradientStops.Add(new GradientStop(OfficeColors.Background.OfficeColor4, 1));

				t.Background = Brushes.White;
				t.BorderThickness = new Thickness(3);
				t.BorderBrush = toolTipBackground;
				t.Content = new Rectangle();
				t.Padding = new Thickness(4);
				
				btn.ToolTip = t;
				btn.ToolTipOpening += new ToolTipEventHandler(btn_ToolTipOpening);
				this._WrapPanel.Children.Add(btn);
			}
		}

		void btn_ToolTipOpening(object sender, ToolTipEventArgs e)
		{
			ReflectedButton btn = sender as ReflectedButton;
			ToolWindow window = btn.Tag as ToolWindow;
			ToolTip toolTip = btn.ToolTip as ToolTip;
			Rectangle rect = toolTip.Content as Rectangle;
			
			VisualBrush vb = new VisualBrush(window);
			vb.Opacity = 0.8;
			vb.AlignmentX = AlignmentX.Center;
			vb.AlignmentY = AlignmentY.Center;
			vb.Stretch = Stretch.Uniform;
			rect.Fill = vb;
			rect.Width = 250;
			rect.Height = window.ActualHeight * (250 / window.ActualWidth);
		} 

		#endregion

        #region btnHelp_Click

        void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow window = new HelpWindow();
            window.Parent = this.content.WindowsContainer as Panel;
            window.StartPosition = ToolWindowStartPosition.CenterParent;
            window.CanResize = false;
            window.ShowStatusBar = false;
            window.Show();
        } 

        #endregion

        #region OnClick3

        private void OnClick3(object sender, RoutedEventArgs e)
        {
            TestWindow window = new TestWindow();
            window.Parent = this.content.WindowsContainer as Panel;
            window.Show();
        } 

        #endregion

        #region Maximize / Restore

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            this.content.MaximizeAllWindows();
        }

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            this.content.RestoreAllWindows();
        } 

        #endregion

        #region WindowsSubmenu_ButtonClick

        private void WindowsSubmenu_ButtonClick(object sender, RoutedEventArgs e)
        {
            ReflectedButton btn = e.Source as ReflectedButton;

            this.content.ShowWindow(btn.Tag as ToolWindow);

            if (sender is DropDownMenuItem)
            {
                ((DropDownMenuItem)sender).CloseMenuItems();
            }
        } 

        #endregion
    }
}