#region Using

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Windows.Input; 

#endregion

namespace Assergs.Windows
{
    public static class MessageBox
    {
        #region Declare

        public delegate void DialogResultEventHandler(MessageBoxResultEventArgs result);
        public static event DialogResultEventHandler DialogResult;
        private static ToolWindow _ActualToolWindow = null; 

        #endregion

        #region Constructor

        static MessageBox()
        {
            string resourcesPath = "pack://application:,,,/Assergs.Windows;component/Resources";
            MessageBox._IconInformation = new Uri(Path.Combine(resourcesPath, "Information48.png"));
            MessageBox._IconExclamation = new Uri(Path.Combine(resourcesPath, "Exclamation48.png"));
            MessageBox._IconError = new Uri("pack://application:,,,/Assergs.Windows;component/resources/Error48.png", UriKind.Absolute);
            MessageBox._IconQuestion = new Uri(Path.Combine(resourcesPath, "Question48.png"));
        }

        #endregion

        #region Show

        #region Overloads

        public static void Show(string messageBoxText)
        {
            MessageBox.Show(messageBoxText, String.Empty, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK, null);
        }

        public static void Show(string messageBoxText, string caption)
        {
            MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.OK, null);
        }

        public static void Show(string messageBoxText, string caption, MessageBoxButton button, DialogResultEventHandler dialogResult)
        {
            MessageBox.Show(messageBoxText, caption, button, MessageBoxImage.None, MessageBoxResult.None, dialogResult);
        }

        public static void Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.None, null);
        }

        public static void Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, DialogResultEventHandler dialogResult)
        {
            MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.None, dialogResult);
        } 

        #endregion

        #region Show Core

        public static void Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, DialogResultEventHandler dialogResult)
        {
			#region Adjust Culture

			string okLabel = "Ok";
			string cancelLabel = "Cancel";
			string yesLabel = "Yes";
			string noLabel = "No";

			if (Thread.CurrentThread.CurrentCulture.Equals(CultureInfo.GetCultureInfo("pt-BR")))
			{
				okLabel = "Ok";
				cancelLabel = "Cancelar";
				yesLabel = "Sim";
				noLabel = "Não";
			} 

			#endregion

            if (MessageBox._ActualToolWindow != null)
            {
                throw new Exception("Only one MessageBox can be showed at same time.");
            }

            MessageBox.DialogResult = null;
            MessageBox.DialogResult += dialogResult;

            #region Define ToolWindow.Content

            Grid grid = new Grid();
            grid.Margin = new Thickness(4);

            #region Rows and Columns Definitions

            RowDefinition rowDef1 = new RowDefinition();
            RowDefinition rowDef2 = new RowDefinition();
			rowDef1.Height = new GridLength(100, GridUnitType.Star);
            rowDef2.Height = GridLength.Auto;

            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            colDef1.Width = GridLength.Auto;
            colDef2.Width = new GridLength(100, GridUnitType.Star);

            grid.RowDefinitions.Add(rowDef1);
            grid.RowDefinitions.Add(rowDef2);

            grid.ColumnDefinitions.Add(colDef1);
            grid.ColumnDefinitions.Add(colDef2);

            #endregion

            #endregion

            #region Define buttons

            StackPanel buttonsStackPanel = new StackPanel();
            buttonsStackPanel.Orientation = Orientation.Horizontal;
            buttonsStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            grid.Children.Add(buttonsStackPanel);
            Grid.SetRow(buttonsStackPanel, 1);
            Grid.SetColumnSpan(buttonsStackPanel, 2);

            Thickness buttonsThickness = new Thickness(2, 4, 2, 4);
            Button btn1, btn2, btn3;

            switch (button)
            {
                case MessageBoxButton.OK:
                    btn1 = new Button();
					btn1.Content = okLabel;
                    btn1.Tag = MessageBoxResult.OK;
                    buttonsStackPanel.Children.Add(btn1);
                    break;

                case MessageBoxButton.OKCancel:
                    btn1 = new Button();
					btn1.Content = okLabel;
                    btn1.Tag = MessageBoxResult.OK;
                    buttonsStackPanel.Children.Add(btn1);
                    btn2 = new Button();
					btn2.Content = cancelLabel;
                    btn2.Tag = MessageBoxResult.Cancel;
                    buttonsStackPanel.Children.Add(btn2);
                    break;

                case MessageBoxButton.YesNo:
                    btn1 = new Button();
					btn1.Content = yesLabel;
                    btn1.Tag = MessageBoxResult.Yes;
                    buttonsStackPanel.Children.Add(btn1);
                    btn2 = new Button();
					btn2.Content = noLabel;
                    btn2.Tag = MessageBoxResult.No;
                    buttonsStackPanel.Children.Add(btn2);
                    break;

                case MessageBoxButton.YesNoCancel:
                    btn1 = new Button();
					btn1.Content = yesLabel;
                    btn1.Tag = MessageBoxResult.Yes;
                    buttonsStackPanel.Children.Add(btn1);
                    btn2 = new Button();
					btn2.Content = noLabel;
                    btn2.Tag = MessageBoxResult.No;
                    buttonsStackPanel.Children.Add(btn2);
                    btn3 = new Button();
					btn3.Content = cancelLabel;
                    btn3.Tag = MessageBoxResult.Cancel;
                    buttonsStackPanel.Children.Add(btn3);
                    break;
            }

            foreach (UIElement element in buttonsStackPanel.Children)
            {
                Button btn = element as Button;
                btn.Margin = buttonsThickness;
                btn.Click += new RoutedEventHandler(btn_Click);
                btn.IsDefault = (btn.Tag.ToString() == defaultResult.ToString());
                btn.Focus();
            }

            #endregion

            #region Define Text

            TextBox txt = new TextBox();
            txt.Background = Brushes.Transparent;
            txt.BorderBrush = Brushes.Transparent;
            txt.IsReadOnly = true;
            txt.TextWrapping = TextWrapping.Wrap;
            txt.Margin = new Thickness(4, 2, 0, 4);
            txt.Text = messageBoxText;

            if (icon == MessageBoxImage.None)
            {
                txt.HorizontalAlignment = HorizontalAlignment.Center;
            }

            ScrollViewer textScrollViewer = new ScrollViewer();
            textScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            textScrollViewer.Content = txt;

            grid.Children.Add(textScrollViewer);
            Grid.SetRow(textScrollViewer, 0);
            Grid.SetColumn(textScrollViewer, 1);

            #endregion

            #region Define Icon

            if (icon != MessageBoxImage.None)
            {
                Image messageIcon = new Image();
                messageIcon.VerticalAlignment = VerticalAlignment.Top;
                messageIcon.Margin = new Thickness(2);
                messageIcon.Width = 32;

                BitmapImage iconImage = new BitmapImage();
                iconImage.BeginInit();

                switch ((int)icon)
                {
                    case 16:
                        iconImage.UriSource = _IconError;
                        break;
                    case 32:
                        iconImage.UriSource = _IconQuestion;
                        break;
                    case 48:
                        iconImage.UriSource = _IconExclamation;
                        break;
                    case 64:
                        iconImage.UriSource = _IconInformation;
                        break;
                }

                iconImage.DecodePixelWidth = 32;
                iconImage.EndInit();

                messageIcon.Source = iconImage;

                grid.Children.Add(messageIcon);
                Grid.SetRow(messageIcon, 0);
                Grid.SetColumn(messageIcon, 0);
            }

            #endregion

            #region Define ToolWindow

            ToolWindow window = new ToolWindow();
            window.MinWidth = 200;
            window.MinHeight = 120;
            window.Header = caption;
            window.ShowStatusBar = false;
            window.CanResize = false;
            window.Content = grid;
			window.KeyUp += new KeyEventHandler(delegate(object sender, System.Windows.Input.KeyEventArgs e)
				{
					if (e.Key == Key.Enter)
					{
						foreach (UIElement element in buttonsStackPanel.Children)
						{
							Button btn = element as Button;
							if (btn.IsDefault)
							{
								btn_Click(btn, null);
								break;
							}
						}
					}
				}); 
			window.Closed += new RoutedEventHandler(delegate(object sender, RoutedEventArgs e)
				{
					MessageBox._ActualToolWindow = null;
				});
            MessageBox._ActualToolWindow = window;

            #endregion

            ToolWindow.ModalContainerPanel.Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                new DispatcherOperationCallback(MessageBox._ContainerPanel_Loaded), window);
        }

        #endregion
 
        #endregion
                        
        #region Properties
                
        #region IconError

        private static Uri _IconError;

        public static Uri IconError
        {
            get { return _IconError; }
            set { _IconError = value; }
        }

        #endregion

        #region IconExclamation

        private static Uri _IconExclamation;

        public static Uri IconExclamation
        {
            get { return _IconExclamation; }
            set { _IconExclamation = value; }
        }

        #endregion

        #region IconInformation

        private static Uri _IconInformation;

        public static Uri IconInformation
        {
            get { return _IconInformation; }
            set { _IconInformation = value; }
        }

        #endregion

        #region IconQuestion

        private static Uri _IconQuestion;

        public static Uri IconQuestion
        {
            get { return _IconQuestion; }
            set { _IconQuestion = value; }
        }

        #endregion 

        #endregion

        #region Event Handlers

        #region btn_Click

        static void btn_Click(object sender, RoutedEventArgs e)
        {
			MessageBox._ActualToolWindow.Close();
			MessageBox._ActualToolWindow = null;

            Button btn = sender as Button;
            MessageBoxResult messageResult = (MessageBoxResult)btn.Tag;
            if (MessageBox.DialogResult != null)
            {
                MessageBox.DialogResult(new MessageBoxResultEventArgs(messageResult));
            }
        }

        #endregion

        #region _ContainerPanel_Loaded

        static object _ContainerPanel_Loaded(object window)
        {
            ToolWindow win = window as ToolWindow;
            win.MaxWidth = ToolWindow.ModalContainerPanel.ActualWidth - 100;
            win.MaxHeight = ToolWindow.ModalContainerPanel.ActualHeight - 100;
            win.StartPosition = ToolWindowStartPosition.CenterParent;
            win.ShowModalDialog();
            return null;
        }

        #endregion 

        #endregion

        #region class MessageBoxResultEventArgs

        public class MessageBoxResultEventArgs: EventArgs
        {
            #region Constructor

            public MessageBoxResultEventArgs(MessageBoxResult result)
            {
                this._MessageResult = result;
            } 

            #endregion

            #region MessageResult

            private MessageBoxResult _MessageResult;

            public MessageBoxResult MessageResult
            {
                get { return _MessageResult; }
                set { _MessageResult = value; }
            } 

            #endregion
        } 

        #endregion
    }
}
