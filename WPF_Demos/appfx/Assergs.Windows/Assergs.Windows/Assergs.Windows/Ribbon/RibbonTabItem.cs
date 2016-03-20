using System;
using System.Windows;
using System.Windows.Controls;

namespace Assergs.Windows.Controls
{
	public class RibbonTabItem : TabItem
	{
		static RibbonTabItem()
		{
			RibbonTabItem.DefaultStyleKeyProperty.OverrideMetadata(typeof(RibbonTabItem),
		        new FrameworkPropertyMetadata(typeof(RibbonTabItem)));

			//ComponentResourceKey resourceKey = new ComponentResourceKey(typeof(RibbonTabItem), "RibbonTabItemSelectedBackgroundBrush");

			//RibbonTabItem._SelectedBackgroundBrush = (Brush)Application.Current.FindResource(resourceKey);

			//resourceKey = new ComponentResourceKey(typeof(RibbonTabItem), "RibbonTabItemFocusedBackgroundBrush");

			//RibbonTabItem._FocusedBackgroundBrush = (Brush)Application.Current.FindResource(resourceKey);			
		}		
	}
}
