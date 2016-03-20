# region Using Directives

using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Interop;
using System.Threading;
using Assergs.Windows.Controls;

using System.Windows.Threading;
using System.Diagnostics;

# endregion

namespace Assergs.Windows.Controls
{
	#region class DropDownMenuItem

	[TemplatePart(Name = DropDownMenuItem._PART_Button, Type = typeof(DropDownHeaderButton))]
	public class DropDownMenuItem : MenuItem
    {
		#region Declare

		private DropDownHeaderButton _PART_Button_Element;
		private const string _PART_Button = "PART_Button";
		public static readonly RoutedEvent ButtonClickEvent;

		#endregion

        # region Constructor

        # region static

        static DropDownMenuItem()
        {
			DropDownMenuItem.DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownMenuItem),
                new FrameworkPropertyMetadata(typeof(DropDownMenuItem)));

			DropDownMenuItem.ButtonClickEvent = EventManager.RegisterRoutedEvent(
				"ButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DropDownMenuItem));
        }
				
        # endregion

		#region DropDownMenuItem

		public DropDownMenuItem()
		{
			EventManager.RegisterClassHandler(typeof(DropDownHeaderButton),
				DropDownHeaderButton.ButtonClickEvent,
				new RoutedEventHandler(delegate(object sender, RoutedEventArgs e)
				{
					this.CloseMenuItems();
				}
				));
		} 

		#endregion
				
        # endregion

		#region CloseMenuItems

		public void CloseMenuItems()
		{
			ItemsControl control = ItemsControl.ItemsControlFromItemContainer(this);
			if (control == null)
				control = VisualTreeHelper.GetParent(this) as ItemsControl;

			while (control is MenuItem)
			{
				if (control is MenuItem)
					((MenuItem)control).IsSubmenuOpen = false;

				control = ItemsControl.ItemsControlFromItemContainer(control);
				if (control == null)
					control = VisualTreeHelper.GetParent(this) as ItemsControl;
			}

			if (control is MenuBase)
			{
				((MenuBase)control).ReleaseMouseCapture();
			}
		} 

		#endregion

		#region ButtonClickEvent

		public event RoutedEventHandler ButtonClick
		{
			add { AddHandler(DropDownMenuItem.ButtonClickEvent, value); }
			remove { RemoveHandler(DropDownMenuItem.ButtonClickEvent, value); }
		}

		private void RaiseButtonClick()
		{
			RoutedEventArgs e = new RoutedEventArgs(DropDownMenuItem.ButtonClickEvent);
			RaiseEvent(e);
		}

		#endregion

		#region OnSubmenuClosed

		protected override void OnSubmenuClosed(RoutedEventArgs e)
		{
			base.OnSubmenuClosed(e);
			if (this.Role == MenuItemRole.TopLevelHeader)
			{
				Mouse.Capture(null);
			}
		} 

		#endregion
				
		#region OnApplyTemplate

		public override void OnApplyTemplate()
		{
			if (this.Role == MenuItemRole.TopLevelHeader)
			{
				this._PART_Button_Element = this.GetTemplateChild(DropDownMenuItem._PART_Button) as DropDownHeaderButton;
				
				if (this._PART_Button_Element != null)
				{ 
					
				}
			}

			base.OnApplyTemplate();
		} 

		#endregion
		
		#region TopLevelButtonOrientation

		public Orientation TopLevelButtonOrientation
		{
			get { return (Orientation)GetValue(TopLevelButtonOrientationProperty); }
			set { SetValue(TopLevelButtonOrientationProperty, value); }
		}

		public static readonly DependencyProperty TopLevelButtonOrientationProperty =
			DependencyProperty.Register("TopLevelButtonOrientation", typeof(Orientation), typeof(DropDownMenuItem),
				new FrameworkPropertyMetadata(Orientation.Horizontal));

		#endregion

		#region HeaderText

		public string HeaderText
		{
			get { return (string)GetValue(HeaderTextProperty); }
			set { SetValue(HeaderTextProperty, value); }
		}

		public static readonly DependencyProperty HeaderTextProperty =
			DependencyProperty.Register("HeaderText", typeof(string), typeof(DropDownMenuItem),
				new FrameworkPropertyMetadata(null));

		#endregion

		#region DropDownButtonMode

		public DropDownButtonMode DropDownButtonMode
		{
			get { return (DropDownButtonMode)GetValue(DropDownButtonModeProperty); }
			set { SetValue(DropDownButtonModeProperty, value); }
		}

		public static readonly DependencyProperty DropDownButtonModeProperty =
			DependencyProperty.Register("DropDownButtonMode", typeof(DropDownButtonMode), typeof(DropDownMenuItem),
				new FrameworkPropertyMetadata(DropDownButtonMode.ButtonAndToggleButton));

		#endregion

		#region IsPanelContainer

		public bool IsPanelContainer
		{
			get { return (bool)GetValue(IsPanelContainerProperty); }
			set { SetValue(IsPanelContainerProperty, value); }
		}

		public static readonly DependencyProperty IsPanelContainerProperty =
			DependencyProperty.Register("IsPanelContainer", typeof(bool), typeof(DropDownMenuItem),
				new FrameworkPropertyMetadata(false)); 

		#endregion
    } 

    #endregion
}
