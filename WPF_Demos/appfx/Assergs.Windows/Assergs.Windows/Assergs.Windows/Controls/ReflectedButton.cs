# region Using Directives

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

# endregion

namespace Assergs.Windows.Controls
{
	public class ReflectedButton : Button
	{
		# region Declarations

		
		# endregion

		# region Static Constructor

		static ReflectedButton()
		{
			ReflectedButton.DefaultStyleKeyProperty.OverrideMetadata(typeof(ReflectedButton),
				new FrameworkPropertyMetadata(typeof(ReflectedButton)));
		}

		# endregion



		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register("Text", typeof(string), typeof(ReflectedButton), 
				new FrameworkPropertyMetadata(null));


	}
}
