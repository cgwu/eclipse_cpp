# region Declarations

using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Media;

//using Xceed.Wpf.DataGrid;
//using Xceed.Wpf.DataGrid.Converters;
//using Xceed.Wpf.DataGrid.ValidationRules;
//using Xceed.Wpf.DataGrid.Views;
//using Xceed.Wpf.Controls;

# endregion

namespace Assergs.Windows.Tests
{
	public partial class App : Application
	{		
		# region App_Startup

		void App_Startup(object sender, StartupEventArgs args)
		{
			//Xceed.Wpf.DataGrid.Licenser.LicenseKey = "DGF10-AE45U-MBGUJ-L8JA";

			this.DispatcherUnhandledException += this.App_DispatcherUnhandledException;
			this.Resources = new OfficeStyle();
								
	 	    if (BrowserInteropHelper.IsBrowserHosted)
			{
				this.StartupUri = new Uri("MainPage.xaml", UriKind.Relative);
			} 
			else
			{
                Window mainWindow = new Window();
                mainWindow.Icon = BitmapFrame.Create(new Uri(@"pack://application:,,/Resources/Icons/16x16/Darf16.png"));
                this.MainWindow = mainWindow;
                mainWindow.Content = new MainPage();
                mainWindow.Show();
			}
		}

		# endregion

		# region App_DispatcherUnhandledException

		void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			if (BrowserInteropHelper.IsBrowserHosted)
			{
                //String exception = String.Format(
                //      "Message: {0}{1}{0}{0}"
                //    + "Source: {0}{2}{0}{0}"
                //    + "StackTrace: {0}{3}"
                //    , Environment.NewLine
                //    , e.Exception.Message
                //    , e.Exception.Source
                //    , e.Exception.StackTrace
                //    );

                //MessageBox.Show(exception, "UnhandledException",
                //    MessageBoxButton.OK,
                //    MessageBoxImage.Error);

                throw e.Exception;
			}
			else
			{
				Exception ex = e.Exception;
				string message = ex.Message;

				while (ex.InnerException != null)
				{
					ex = ex.InnerException;
					message = ex.Message + "\r\n\r\n" + message;
				}
			
				System.Windows.MessageBox.Show(message);
			}
            
		}

		# endregion
	}
}