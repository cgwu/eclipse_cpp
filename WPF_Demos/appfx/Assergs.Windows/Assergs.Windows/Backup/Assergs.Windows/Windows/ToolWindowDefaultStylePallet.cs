#region Using

using System;
using System.Windows.Media; 

#endregion

namespace Assergs.Windows
{
    #region ToolWindowDefaultStylePallet

    public static class ToolWindowDefaultStylePallet
    {
        #region Declare

        public static Color ForegroundColorBrush = OfficeColors.Foreground.OfficeColor1;

        public static Color ToolWindowBackground;
	    public static Color ToolWindowBorder;
	    public static Color ToolWindowDisabledBorder;
	    public static Color ToolWindowControlButtonForeground;
	    public static Color ToolWindowSeparatorBarLight;
	    public static Color ToolWindowSeparatorBarDark;
        public static Color ToolWindowDisabledOver;

		public static Color ToolWindowTitleBar1;
        public static Color ToolWindowTitleBar2;
        public static Color ToolWindowTitleBar3;
        public static Color ToolWindowTitleBar4;
        public static Color ToolWindowTitleBar5;
        public static Color ToolWindowTitleBar6;
        public static Color ToolWindowTitleBar7;
        public static Color ToolWindowTitleBar8;
        public static Color ToolWindowTitleBar9;

		public static Color ToolWindowSideBorderDark;
		public static Color ToolWindowSideBorderLight;
		
		public static Color ToolWindowLeftStatusBar1;
		public static Color ToolWindowLeftStatusBar2;
        public static Color ToolWindowLeftStatusBar3;
        public static Color ToolWindowLeftStatusBar4;
        public static Color ToolWindowLeftStatusBar5;
        public static Color ToolWindowLeftStatusBar6;
        public static Color ToolWindowLeftStatusBar7;
        public static Color ToolWindowLeftStatusBar8;

		public static Color ToolWindowRightStatusBar1;
        public static Color ToolWindowRightStatusBar2;
        public static Color ToolWindowRightStatusBar3;
        public static Color ToolWindowRightStatusBar4;
        public static Color ToolWindowRightStatusBar5;

        #endregion

        #region Constructor

        static ToolWindowDefaultStylePallet()
        {
            ToolWindowDefaultStylePallet.Reset();
            OfficeColors.RegistersTypes.Add(typeof(ToolWindowDefaultStylePallet));
        }

        #endregion

        #region Reset

        public static void Reset()
        {
            ForegroundColorBrush = OfficeColors.Foreground.OfficeColor1;

            ToolWindowBackground = OfficeColors.Background.OfficeColor30;
	        ToolWindowBorder = OfficeColors.Background.OfficeColor29;
	        ToolWindowDisabledBorder = OfficeColors.Background.OfficeColor31;
	        ToolWindowControlButtonForeground = OfficeColors.Foreground.OfficeColor2;
	        ToolWindowSeparatorBarLight = OfficeColors.Background.OfficeColor55;
	        ToolWindowSeparatorBarDark = OfficeColors.Background.OfficeColor56;
            ToolWindowDisabledOver = OfficeColors.Background.OfficeColor57;

		    ToolWindowTitleBar1 = OfficeColors.Background.OfficeColor32;
            ToolWindowTitleBar2 = OfficeColors.Background.OfficeColor33;
            ToolWindowTitleBar3 = OfficeColors.Background.OfficeColor34;
            ToolWindowTitleBar4 = OfficeColors.Background.OfficeColor35;
            ToolWindowTitleBar5 = OfficeColors.Background.OfficeColor36;
            ToolWindowTitleBar6 = OfficeColors.Background.OfficeColor37;
            ToolWindowTitleBar7 = OfficeColors.Background.OfficeColor38;
            ToolWindowTitleBar8 = OfficeColors.Background.OfficeColor39;
            ToolWindowTitleBar9 = OfficeColors.Background.OfficeColor40;

		    ToolWindowSideBorderDark = OfficeColors.Background.OfficeColor42;
		    ToolWindowSideBorderLight = OfficeColors.Background.OfficeColor41;
    		
		    ToolWindowLeftStatusBar1 = Colors.White;
		    ToolWindowLeftStatusBar2 = OfficeColors.Background.OfficeColor43;
            ToolWindowLeftStatusBar3 = OfficeColors.Background.OfficeColor44;
            ToolWindowLeftStatusBar4 = OfficeColors.Background.OfficeColor45;
            ToolWindowLeftStatusBar5 = OfficeColors.Background.OfficeColor46;
            ToolWindowLeftStatusBar6 = OfficeColors.Background.OfficeColor47;
            ToolWindowLeftStatusBar7 = OfficeColors.Background.OfficeColor48;
            ToolWindowLeftStatusBar8 = OfficeColors.Background.OfficeColor49;

		    ToolWindowRightStatusBar1 = OfficeColors.Background.OfficeColor50;
            ToolWindowRightStatusBar2 = OfficeColors.Background.OfficeColor51;
            ToolWindowRightStatusBar3 = OfficeColors.Background.OfficeColor52;
            ToolWindowRightStatusBar4 = OfficeColors.Background.OfficeColor53;
            ToolWindowRightStatusBar5 = OfficeColors.Background.OfficeColor54;
        }

        #endregion
    } 

    #endregion
}
