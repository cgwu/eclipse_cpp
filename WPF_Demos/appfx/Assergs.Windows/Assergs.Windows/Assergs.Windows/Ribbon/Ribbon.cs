using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace Assergs.Windows.Controls
{
	public class Ribbon: TabControl
	{
		static Ribbon()
		{
			Ribbon.DefaultStyleKeyProperty.OverrideMetadata(typeof(Ribbon),
			    new FrameworkPropertyMetadata(typeof(Ribbon)));
		}
	}
}
