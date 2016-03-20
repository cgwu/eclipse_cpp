#region Using

using System;
using System.Windows.Media; 

#endregion

namespace Assergs.Windows
{
    #region RibbonPanelDefaultStylePallet

    public static class RibbonPanelDefaultStylePallet
    {
        #region Declare

        public static Color RibbonPanelBorder;
        public static Color RibbonPanelBackground;
        public static Color RibbonPanelMouseEnterBorder;
        public static Color RibbonPanelMouseEnterBackground;
            
		public static Color BottonBorder1;
        public static Color BottonBorder2;

        #endregion

        #region Constructor

        static RibbonPanelDefaultStylePallet()
        {
            RibbonPanelDefaultStylePallet.Reset();
            OfficeColors.RegistersTypes.Add(typeof(RibbonPanelDefaultStylePallet));
        }

        #endregion

        #region Reset

        public static void Reset()
        {
            RibbonPanelBorder = OfficeColors.Background.OfficeColor18;
            RibbonPanelBackground = OfficeColors.Background.OfficeColor19;
            RibbonPanelMouseEnterBorder = OfficeColors.Background.OfficeColor20;
            RibbonPanelMouseEnterBackground = OfficeColors.Background.OfficeColor21;
            
            BottonBorder1 = OfficeColors.Background.OfficeColor19;
		    BottonBorder2 = Colors.White;
        }

        #endregion
    } 

    #endregion
}
