using System;
using System.Windows.Controls;
using System.Windows;

namespace Assergs.Windows.Controls
{
	public class RibbonPanel: HeaderedContentControl
	{
		static RibbonPanel()
		{
		    DefaultStyleKeyProperty.OverrideMetadata(typeof(RibbonPanel),
		        new FrameworkPropertyMetadata(typeof(RibbonPanel)));
		}		
	}
}
