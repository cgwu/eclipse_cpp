#region Using

using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Windows.Controls.Primitives;
using System.Windows.Threading; 

#endregion

namespace Assergs.Windows.Controls
{
	[TemplatePart(Name = DropDownHeaderButton._PART_Button, Type = typeof(Button))]
	[TemplatePart(Name = DropDownHeaderButton._PART_ToggleButton, Type = typeof(ToggleButton))]
	public partial class DropDownHeaderButton : System.Windows.Controls.UserControl
	{
		#region Declare

		private const string _PART_Button = "PART_Button";
		private const string _PART_ToggleButton = "PART_ToggleButton";
		private Button _PART_Button_Element;
		private ToggleButton _PART_ToggleButton_Element;

		public static readonly RoutedEvent ButtonClickEvent = EventManager.RegisterRoutedEvent(
				"ButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DropDownHeaderButton));

		#endregion

		#region Constructor

		#region static

		static DropDownHeaderButton()
		{
			DropDownHeaderButton.DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownHeaderButton),
				new FrameworkPropertyMetadata(typeof(DropDownHeaderButton)));
		}

		#endregion

		#region public

		public DropDownHeaderButton()
		{
			InitializeComponent();
		}

		#endregion 

		#endregion

		#region ButtonClickEvent

		public event RoutedEventHandler ButtonClick
		{
			add { AddHandler(DropDownHeaderButton.ButtonClickEvent, value); }
			remove { RemoveHandler(DropDownHeaderButton.ButtonClickEvent, value); }
		}

		private void RaiseButtonClick()
		{
			RoutedEventArgs e = new RoutedEventArgs(DropDownHeaderButton.ButtonClickEvent);
			RaiseEvent(e);
		}

		#endregion
				
		#region ToggleButtonMenu

		public ContextMenu ToggleButtonMenu
		{
			get { return (ContextMenu)GetValue(ToggleButtonMenuProperty); }
			set { SetValue(ToggleButtonMenuProperty, value); }
		}

		public static readonly DependencyProperty ToggleButtonMenuProperty =
			DependencyProperty.Register("ToggleButtonMenu", typeof(ContextMenu), typeof(DropDownHeaderButton),
				new FrameworkPropertyMetadata(null)); 

		#endregion
		
		#region Orientation

		public Orientation Orientation
		{
			get { return (Orientation)GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value);	}
		}

		public static readonly DependencyProperty OrientationProperty =
			DependencyProperty.Register("Orientation", typeof(Orientation), typeof(DropDownHeaderButton),
				new FrameworkPropertyMetadata(Orientation.Horizontal));

		#endregion

		#region OnApplyTemplate

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			this._PART_Button_Element = this.GetTemplateChild(DropDownHeaderButton._PART_Button) as Button;
			this._PART_ToggleButton_Element = this.GetTemplateChild(DropDownHeaderButton._PART_ToggleButton) as ToggleButton;
						
			this._PART_Button_Element.Click += delegate(object sender, RoutedEventArgs e)
			{
				this.RaiseButtonClick();
			};
		}

		#endregion
				
		#region IsToggleButtonChecked

		public bool IsToggleButtonChecked
		{
			get { return (bool)GetValue(IsToggleButtonCheckedProperty); }
			set { SetValue(IsToggleButtonCheckedProperty, value); }
		}

		public static readonly DependencyProperty IsToggleButtonCheckedProperty =
			DependencyProperty.Register("IsToggleButtonChecked", typeof(bool), typeof(DropDownHeaderButton),
				new FrameworkPropertyMetadata(false)); 

		#endregion

		#region Text

		public object Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register("Text", typeof(string), typeof(DropDownHeaderButton),
				new FrameworkPropertyMetadata(String.Empty)); 

		#endregion
		
		#region DropDownButtonMode

		public DropDownButtonMode DropDownButtonMode
		{
			get { return (DropDownButtonMode)GetValue(DropDownButtonModeProperty); }
			set { SetValue(DropDownButtonModeProperty, value); }
		}

		public static readonly DependencyProperty DropDownButtonModeProperty =
			DependencyProperty.Register("DropDownButtonMode", typeof(DropDownButtonMode), typeof(DropDownHeaderButton),
				new FrameworkPropertyMetadata(DropDownButtonMode.ButtonAndToggleButton)); 

		#endregion
	}

	#region DropDownButtonMode

	public enum DropDownButtonMode
	{
		ButtonAndToggleButton,
		ToggleButtonOnly
	} 

	#endregion
}
