using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace Assergs.Windows.Controls
{
	public class DropDownButton: Menu
	{
		# region Constructor

        # region static

		static DropDownButton()
        {
			DropDownButton.DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownButton),
				new FrameworkPropertyMetadata(typeof(DropDownButton)));
        }

        # endregion

        # endregion
	}
}
