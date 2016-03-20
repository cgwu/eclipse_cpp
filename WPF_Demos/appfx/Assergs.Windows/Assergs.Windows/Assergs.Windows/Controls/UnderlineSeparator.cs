# region Using Directives

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

# endregion

namespace Assergs.Windows.Controls
{
	public class UnderlineSeparator : HeaderedContentControl
	{
		# region Static Constructor

		static UnderlineSeparator()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(UnderlineSeparator),
				new FrameworkPropertyMetadata(typeof(UnderlineSeparator)));
		}

		# endregion

		#region UnderlineHeight

		public double UnderlineHeight
		{
			get { return (double)GetValue(UnderlineHeightProperty); }
			set { SetValue(UnderlineHeightProperty, value); }
		}

		public static readonly DependencyProperty UnderlineHeightProperty =
			DependencyProperty.Register("UnderlineHeight", typeof(double), typeof(UnderlineSeparator),
				new FrameworkPropertyMetadata(2D)); 

		#endregion
	}
}
