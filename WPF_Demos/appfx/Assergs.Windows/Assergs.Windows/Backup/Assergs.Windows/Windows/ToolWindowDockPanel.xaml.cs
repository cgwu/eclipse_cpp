# region Using Directives

using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Interop;
using System.Windows.Media;
using Assergs.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Windows.Documents;

# endregion

namespace Assergs.Windows
{
    [TemplatePart(Name = ToolWindowDockPanel._PART_MainContainer, Type = typeof(Canvas))]
    [TemplatePart(Name = ToolWindowDockPanel._PART_WindowsTabs, Type = typeof(TabControl))]
    [TemplatePart(Name = ToolWindowDockPanel._PART_WindowsContainer, Type = typeof(ToolWindowPanelContainer))]
    [TemplatePart(Name = ToolWindowDockPanel._PART_VerticalScroll, Type = typeof(ScrollBar))]
    [TemplatePart(Name = ToolWindowDockPanel._PART_HorizontalScroll, Type = typeof(ScrollBar))]
    [TemplatePart(Name = ToolWindowDockPanel._PART_WindowsTabsBackground, Type = typeof(Rectangle))]
    public partial class ToolWindowDockPanel : UserControl
    {
        # region Declarations

        private const string _PART_MainContainer = "PART_MainContainer";
        private Canvas _PART_MainContainer_Element;

        private const string _PART_WindowsTabs = "PART_WindowsTabs";
        private TabControl _PART_WindowsTabs_Element;

        private const string _PART_WindowsContainer = "PART_WindowsContainer";
        private ToolWindowPanelContainer _PART_WindowsContainer_Element;

        private const string _PART_VerticalScroll = "PART_VerticalScroll";
        private ScrollBar _PART_VerticalScroll_Element;

        private const string _PART_HorizontalScroll = "PART_HorizontalScroll";
        private ScrollBar _PART_HorizontalScroll_Element;

        private const string _PART_WindowsTabsBackground = "PART_WindowsTabsBackground";
        private Rectangle _PART_WindowsTabsBackground_Element;

		public static RoutedCommand MaximizeAllWindowsCommand = new RoutedCommand();
		public static RoutedCommand RestoreAllWindowsCommand = new RoutedCommand();

        # endregion

        # region Constructor

        #region Static Constructor

        static ToolWindowDockPanel()
        {
            if (!BrowserInteropHelper.IsBrowserHosted)
            {
                KeyboardNavigation.ControlTabNavigationProperty.OverrideMetadata(
                    typeof(ToolWindowDockPanel), new FrameworkPropertyMetadata(KeyboardNavigationMode.Cycle));
            }
        }

        #endregion

        #region Public Constructor

        public ToolWindowDockPanel()
        {
            this.InitializeComponent();
			this._OpenedWindows = new List<ToolWindow>();

			EventManager.RegisterClassHandler(typeof(ToolWindow), ToolWindow.CreatedEvent, new RoutedEventHandler(this.ToolWindow_CreatedEvent));
			EventManager.RegisterClassHandler(typeof(ToolWindow), ToolWindow.ClosedEvent, new RoutedEventHandler(this.ToolWindow_ClosedEvent));
            EventManager.RegisterClassHandler(typeof(ToolWindow), ToolWindow.MaximizeChangedEvent, new RoutedEventHandler(this.ToolWindow_MaximizeChanged));
            EventManager.RegisterClassHandler(typeof(ToolWindowTabItemContainer), ToolWindowTabItemContainer.TabRestoredEvent, new RoutedEventHandler(this.ToolWindow_TabRestoredEvent));
            EventManager.RegisterClassHandler(typeof(ToolWindowTabItemContainer), ToolWindowTabItemContainer.TabClosedEvent, new RoutedEventHandler(this.ToolWindow_TabClosedEvent));
						
            base.ApplyTemplate();
        }

        #endregion

        # endregion

        # region Properties

        #region WindowsContainer

        public ToolWindowPanelContainer WindowsContainer
        {
            get { return this._PART_WindowsContainer_Element; }
        }

        #endregion

        #region WindowsTabsBackground

        public Brush WindowsTabsBackground
        {
            get { return (Brush)GetValue(WindowsTabsBackgroundProperty); }
            set { SetValue(WindowsTabsBackgroundProperty, value); }
        }

        public static readonly DependencyProperty WindowsTabsBackgroundProperty =
            DependencyProperty.Register("WindowsTabsBackground", typeof(Brush), typeof(ToolWindowDockPanel),
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        #endregion

		#region OpenedWindows

		private List<ToolWindow> _OpenedWindows;

		public ToolWindow[] OpenedWindows
		{
			get 
			{ 
				ToolWindow[] ToolWindowArray = new ToolWindow[this._OpenedWindows.Count];
				this._OpenedWindows.CopyTo(ToolWindowArray);
				return ToolWindowArray; 
			}
		} 

		#endregion

        # endregion

        #region Methods

		#region ShowWindow

		public bool ShowWindow(ToolWindow window)
		{
			if (this._OpenedWindows.Contains(window))
			{
				if (window.IsMaximized)
				{
					foreach (object objItem in this._PART_WindowsTabs_Element.Items)
					{
						TabItem tabItem = objItem as TabItem;
						ToolWindowTabItemContainerPanel panel = tabItem.Content as ToolWindowTabItemContainerPanel;
						ToolWindow internalWindow = panel.Children[0] as ToolWindow;
						if (window.Equals(internalWindow))
						{
							this.BringContainerToFront(this._PART_WindowsTabs_Element);
							this._PART_WindowsTabs_Element.SelectedItem = tabItem;

							return true;
						}
					}
				}
				else
				{
					this.BringContainerToFront(this._PART_WindowsContainer_Element);
					window.Show();
					return true;
				}
			}

			return false;
		} 

		#endregion

        #region AdjustEnvironment

        private void AdjustEnvironment()
        {
            Size s = new Size(this._PART_WindowsContainer_Element.ActualWidth, this._PART_WindowsContainer_Element.ActualHeight);
            this.AdjustEnvironment(s);
        }

        private void AdjustEnvironment(Size newSize)
        {
            #region Declare

            bool vScrollIsVisible;
            bool hScrollIsVisible;

            double availableWidth;
			double availableHeight;

            #endregion

            if (Panel.GetZIndex(this._PART_WindowsTabs_Element) < Panel.GetZIndex(this._PART_WindowsContainer_Element))
            {
                #region Adjust ScrollBars

				System.Windows.Visibility auxNewVerticalVisibility = this._PART_VerticalScroll_Element.Visibility;
                System.Windows.Visibility auxNewHorizontalVisibility = this._PART_HorizontalScroll_Element.Visibility;

                #region Define ScrollBars Visibilities

                System.Windows.Visibility auxVerticalVisibility;
                System.Windows.Visibility auxHorizontalVisibility;

                do
                {
                    auxVerticalVisibility = auxNewVerticalVisibility;
                    auxHorizontalVisibility = auxNewHorizontalVisibility;

                    double vDecrement = (auxNewVerticalVisibility == Visibility.Visible ? this._PART_VerticalScroll_Element.Width : 0);
                    double hDecrement = (auxNewHorizontalVisibility == Visibility.Visible ? this._PART_HorizontalScroll_Element.Height : 0);

                    vScrollIsVisible = newSize.Height > this._PART_MainContainer_Element.ActualHeight - hDecrement;
                    hScrollIsVisible = newSize.Width > this._PART_MainContainer_Element.ActualWidth - vDecrement;

                    if (vScrollIsVisible)
                        auxNewVerticalVisibility = Visibility.Visible;
                    else
                        auxNewVerticalVisibility = Visibility.Hidden;

                    if (hScrollIsVisible)
                        auxNewHorizontalVisibility = Visibility.Visible;
                    else
                        auxNewHorizontalVisibility = Visibility.Hidden;

                } while (auxVerticalVisibility != auxNewVerticalVisibility
                         || auxHorizontalVisibility != auxNewHorizontalVisibility);

                #endregion

                this._PART_VerticalScroll_Element.Visibility = auxNewVerticalVisibility;
                this._PART_HorizontalScroll_Element.Visibility = auxNewHorizontalVisibility;

				availableWidth = Math.Max(0 ,this._PART_MainContainer_Element.ActualWidth - (vScrollIsVisible ? this._PART_VerticalScroll_Element.Width : 0));
				availableHeight = Math.Max(0 ,this._PART_MainContainer_Element.ActualHeight - (hScrollIsVisible ? this._PART_HorizontalScroll_Element.Height : 0));

				this._PART_VerticalScroll_Element.Minimum = availableHeight;
                this._PART_VerticalScroll_Element.Maximum = newSize.Height;
				this._PART_VerticalScroll_Element.ViewportSize = availableHeight;
				this._PART_VerticalScroll_Element.SmallChange = 10;
				this._PART_VerticalScroll_Element.LargeChange = availableHeight * 0.9;

				this._PART_HorizontalScroll_Element.Minimum = availableWidth;
                this._PART_HorizontalScroll_Element.Maximum = newSize.Width;
				this._PART_HorizontalScroll_Element.ViewportSize = availableWidth;
				this._PART_HorizontalScroll_Element.SmallChange = 10;
				this._PART_HorizontalScroll_Element.LargeChange = availableWidth * 0.9;

                Canvas.SetLeft(this._PART_VerticalScroll_Element, this._PART_MainContainer_Element.ActualWidth - this._PART_VerticalScroll_Element.Width);
                Canvas.SetTop(this._PART_HorizontalScroll_Element, this._PART_MainContainer_Element.ActualHeight - this._PART_HorizontalScroll_Element.Height);

				this._PART_VerticalScroll_Element.Height = availableHeight;
				this._PART_HorizontalScroll_Element.Width = availableWidth;

				this._PART_WindowsContainer_Element.CalculatedDesiredSize = new Size(availableWidth, availableHeight);
                this._PART_WindowsContainer_Element.InvalidateMeasure();

                #endregion
            }
            else
            {
                #region Adjust WindowsTabs

                this._PART_VerticalScroll_Element.Visibility = Visibility.Collapsed;
                this._PART_HorizontalScroll_Element.Visibility = Visibility.Collapsed;

				availableWidth = Math.Max(0, this._PART_MainContainer_Element.ActualWidth);
				availableHeight = Math.Max(0, this._PART_MainContainer_Element.ActualHeight);

                #endregion
            }

            this._PART_WindowsTabs_Element.Width =
				this._PART_WindowsTabsBackground_Element.Width = availableWidth;
            this._PART_WindowsTabs_Element.Height =
				this._PART_WindowsTabsBackground_Element.Height = availableHeight;
        }

        #endregion

        #region MaximizeAllWindows

        public void MaximizeAllWindows()
        {
            for (int i = 0; i < this._PART_WindowsContainer_Element.Children.Count; )
            {
                ToolWindow window = this._PART_WindowsContainer_Element.Children[i] as ToolWindow;
                window.IsMaximized = true;
            }
        }

        #endregion

        #region RestoreAllWindows

        public void RestoreAllWindows()
        {
            for (int i = 0; i < this._PART_WindowsTabs_Element.Items.Count; )
            {
                ToolWindowTabItemContainer tabItem = this._PART_WindowsTabs_Element.Items[i] as ToolWindowTabItemContainer;
                this.RestoreWindowFromTabItem(tabItem);
            }
        }

        #endregion

        #region RestoreWindowFromTabItem

        private void RestoreWindowFromTabItem(ToolWindowTabItemContainer tabItem)
        {
            ToolWindowTabItemContainerPanel panel = tabItem.Content as ToolWindowTabItemContainerPanel;
            ToolWindow window = panel.Children[0] as ToolWindow;
            panel.Children.Remove(window);
            this._PART_WindowsTabs_Element.Items.Remove(tabItem);

            if (this._PART_WindowsTabs_Element.Items.Count == 0)
            {
                this._PART_WindowsTabs_Element.Visibility = Visibility.Hidden;
                this.BringContainerToFront(this._PART_WindowsContainer_Element);
            }

            window.IsMaximized = false;
            window.Show();
        }

        #endregion

        #region BringContainerToFront

        private void BringContainerToFront(UIElement container)
        {
            if (container == this._PART_WindowsTabs_Element)
            {
                Canvas.SetZIndex(this._PART_VerticalScroll_Element, 0);
                Canvas.SetZIndex(this._PART_HorizontalScroll_Element, 1);
                Canvas.SetZIndex(this._PART_WindowsContainer_Element, 2);
                Canvas.SetZIndex(this._PART_WindowsTabsBackground_Element, 3);
                Canvas.SetZIndex(this._PART_WindowsTabs_Element, 4);
            }
            else
            {
                Canvas.SetZIndex(this._PART_WindowsTabsBackground_Element, 0);
                Canvas.SetZIndex(this._PART_WindowsTabs_Element, 1);
                Canvas.SetZIndex(this._PART_WindowsContainer_Element, 2);
                Canvas.SetZIndex(this._PART_VerticalScroll_Element, 3);
                Canvas.SetZIndex(this._PART_HorizontalScroll_Element, 4);
            }

            this.AdjustEnvironment();
        }

        #endregion

        #region OnApplyTemplate

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._PART_MainContainer_Element = this.GetTemplateChild(ToolWindowDockPanel._PART_MainContainer) as Canvas;
            this._PART_WindowsTabs_Element = this.GetTemplateChild(ToolWindowDockPanel._PART_WindowsTabs) as TabControl;
            this._PART_WindowsContainer_Element = this.GetTemplateChild(ToolWindowDockPanel._PART_WindowsContainer) as ToolWindowPanelContainer;
            this._PART_VerticalScroll_Element = this.GetTemplateChild(ToolWindowDockPanel._PART_VerticalScroll) as ScrollBar;
            this._PART_HorizontalScroll_Element = this.GetTemplateChild(ToolWindowDockPanel._PART_HorizontalScroll) as ScrollBar;
            this._PART_WindowsTabsBackground_Element = this.GetTemplateChild(ToolWindowDockPanel._PART_WindowsTabsBackground) as Rectangle;

            this._PART_MainContainer_Element.SizeChanged += new SizeChangedEventHandler(_PART_MainContainer_Element_SizeChanged);
            this._PART_WindowsContainer_Element.SizeChanged += new SizeChangedEventHandler(WindowsContainer_SizeChanged);

            this._PART_VerticalScroll_Element.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.ScrollBars_ValueChanged);
            this._PART_HorizontalScroll_Element.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.ScrollBars_ValueChanged);

            this._PART_WindowsTabs_Element.PreviewMouseDown += new MouseButtonEventHandler(_PART_WindowsTabs_Element_PreviewMouseDown);
            this._PART_WindowsContainer_Element.ChildAdded += new EventHandler<ToolWindowEventArgs>(_PART_WindowsContainer_Element_ChildAdded);
			
            this._PART_WindowsTabs_Element.IsVisibleChanged += new DependencyPropertyChangedEventHandler(_PART_WindowsTabs_Element_IsVisibleChanged);

            this.BringContainerToFront(this._PART_WindowsContainer_Element);
        }
				
        #endregion

        #endregion

        #region Event Handlers

		#region MaximizeAllWindows_Executed

		public void MaximizeAllWindows_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			this.MaximizeAllWindows();
		} 

		#endregion

		#region RestoreAllWindows_Executed

		public void RestoreAllWindows_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			this.RestoreAllWindows();
		}

		#endregion

        #region _PART_WindowsContainer_Element_ChildAdded

        private void _PART_WindowsContainer_Element_ChildAdded(object sender, ToolWindowEventArgs e)
        {
            this.BringContainerToFront(this._PART_WindowsContainer_Element);
            this.AdjustEnvironment();
        }

        #endregion
				
        #region _PART_WindowsTabs_Element_PreviewMouseDown

        private void _PART_WindowsTabs_Element_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.BringContainerToFront(this._PART_WindowsTabs_Element);
        }

        #endregion

        #region _PART_MainContainer_Element_SizeChanged

        private void _PART_MainContainer_Element_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this._PART_MainContainer_Element.IsLoaded)
            {
                this.AdjustEnvironment();
            }
            else
            {
                this._PART_MainContainer_Element.Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                    new NoArgsEventHandler(this.AdjustEnvironment));
            }
        }

        #endregion

        #region _PART_WindowsTabs_Element_IsVisibleChanged

        private void _PART_WindowsTabs_Element_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                this._PART_WindowsTabsBackground_Element.Visibility = Visibility.Visible;
            }
            else
            {
                this._PART_WindowsTabsBackground_Element.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

		#region ToolWindow_CreatedEvent

		private void ToolWindow_CreatedEvent(object sender, RoutedEventArgs e)
        {
			this._OpenedWindows.Add(sender as ToolWindow);
			e.Handled = true;
        }

        #endregion

		#region ToolWindow_ClosedEvent

		private void ToolWindow_ClosedEvent(object sender, RoutedEventArgs e)
		{
			this._OpenedWindows.Remove(sender as ToolWindow);
			//e.Handled = true;
		}

		#endregion

        #region ToolWindow_TabRestoredEvent

        private void ToolWindow_TabRestoredEvent(object sender, RoutedEventArgs e)
        {
            this.RestoreWindowFromTabItem(sender as ToolWindowTabItemContainer);
        }

        #endregion

        #region ToolWindow_TabClosedEvent

        private void ToolWindow_TabClosedEvent(object sender, RoutedEventArgs e)
        {
            ToolWindowTabItemContainer tabItem = sender as ToolWindowTabItemContainer;
			ToolWindowTabItemContainerPanel panel = tabItem.Content as ToolWindowTabItemContainerPanel;
			ToolWindow window = panel.Children[0] as ToolWindow;
			window.Close();
			
            this._PART_WindowsTabs_Element.Items.Remove(tabItem);

            if (this._PART_WindowsTabs_Element.Items.Count == 0)
            {
                this._PART_WindowsTabs_Element.Visibility = Visibility.Hidden;
            }
        }

        #endregion

		#region ToolWindow_MaximizeChanged

		private void ToolWindow_MaximizeChanged(object sender, RoutedEventArgs e)
        {
            ToolWindow window = sender as ToolWindow;
            if (window.IsMaximized)
            {
                this._PART_WindowsContainer_Element.Children.Remove(window);

                window.Margin = new Thickness(0);

                ToolWindowTabItemContainer newTabItem = new ToolWindowTabItemContainer();
                newTabItem.Header = window.Header;
                newTabItem.Icon = window.Icon;

                ToolWindowTabItemContainerPanel tabItemContentPanel = new ToolWindowTabItemContainerPanel();
                tabItemContentPanel.Children.Add(window);

                newTabItem.Content = tabItemContentPanel;

                this._PART_WindowsTabs_Element.SelectedIndex = this._PART_WindowsTabs_Element.Items.Add(newTabItem);
                this._PART_WindowsTabs_Element.Visibility = Visibility.Visible;

                this.BringContainerToFront(this._PART_WindowsTabs_Element);
            }
        }

        #endregion

        #region WindowsContainer_SizeChanged

        void WindowsContainer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.AdjustEnvironment(e.NewSize);
        }

        #endregion

        #region ScrollBars_ValueChanged

        void ScrollBars_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ScrollBar scrollBar = sender as ScrollBar;

            double value = (e.NewValue - scrollBar.Minimum) * -1D;

            if (scrollBar == this._PART_VerticalScroll_Element)
            {
                Canvas.SetTop(this._PART_WindowsContainer_Element, value);
            }
            else
            {
                Canvas.SetLeft(this._PART_WindowsContainer_Element, value);
            }
        }

        #endregion

        #endregion
    }
}
