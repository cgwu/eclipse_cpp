#region Using

using System;
using System.Windows.Media; 

#endregion

namespace Assergs.Windows
{
    #region RibbonTabItemChromeDefaultStylePallet

    public static class RibbonTabItemChromeDefaultStylePallet
    {
        #region Declare

        public static Color RibbonBorderBrush;

        public static Color RibbonTabItemBackground1;
		public static Color RibbonTabItemBackground2;
		public static Color RibbonTabItemBackground3;

        public static Color RibbonTabItemChromeKeyboardFocused1;
		public static Color RibbonTabItemChromeKeyboardFocused2;
		public static Color RibbonTabItemChromeKeyboardFocused3;
		public static Color RibbonTabItemChromeKeyboardFocused4;

        public static Color RibbonTabItemMouseOverBackground;
        public static Color RibbonTabItemChromeMouseOver;

        #endregion

        #region Constructor

        static RibbonTabItemChromeDefaultStylePallet()
        {
            RibbonTabItemChromeDefaultStylePallet.Reset();
            OfficeColors.RegistersTypes.Add(typeof(RibbonTabItemChromeDefaultStylePallet));
        }

        #endregion

        #region Reset

        public static void Reset()
        {
            RibbonBorderBrush = OfficeColors.Background.OfficeColor11;

            RibbonTabItemBackground1 = OfficeColors.Background.OfficeColor22;
		    RibbonTabItemBackground2 = OfficeColors.Background.OfficeColor23;
		    RibbonTabItemBackground3 = OfficeColors.Background.OfficeColor24;

            RibbonTabItemChromeKeyboardFocused1 = OfficeColors.HighLight.OfficeColor15;
            RibbonTabItemChromeKeyboardFocused2 = OfficeColors.HighLight.OfficeColor16;
            RibbonTabItemChromeKeyboardFocused3 = OfficeColors.HighLight.OfficeColor17;
            RibbonTabItemChromeKeyboardFocused4 = OfficeColors.HighLight.OfficeColor18;

            RibbonTabItemMouseOverBackground = OfficeColors.Background.OfficeColor25;
            RibbonTabItemChromeMouseOver = OfficeColors.HighLight.OfficeColor19;
        }

        #endregion
    } 

    #endregion
}
