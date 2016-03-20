#region Using

using System;
using System.Windows.Media; 

#endregion

namespace Assergs.Windows
{
    #region RibbonDefaultStylePallet

    public static class RibbonDefaultStylePallet
    {
        #region Declare

        public static Color ForegroundColorBrush = OfficeColors.Foreground.OfficeColor1;
        public static Color RibbonBorderBrush = OfficeColors.Background.OfficeColor11;
        public static Color RibbonOuterBackgroundTop = OfficeColors.Background.OfficeColor12;
        public static Color RibbonOuterBackgroundBottom = OfficeColors.Background.OfficeColor13;
        public static Color RibbonBackground1 = OfficeColors.Background.OfficeColor14;
        public static Color RibbonBackground2 = OfficeColors.Background.OfficeColor15;
        public static Color RibbonBackground3 = OfficeColors.Background.OfficeColor16;
        public static Color RibbonBackground4 = OfficeColors.Background.OfficeColor17;

        #endregion

        #region Constructor

        static RibbonDefaultStylePallet()
        {
            RibbonDefaultStylePallet.Reset();
            OfficeColors.RegistersTypes.Add(typeof(RibbonDefaultStylePallet));
        }

        #endregion

        #region Reset

        public static void Reset()
        {
            ForegroundColorBrush = OfficeColors.Foreground.OfficeColor1;
            RibbonBorderBrush = OfficeColors.Background.OfficeColor11;
            RibbonOuterBackgroundTop = OfficeColors.Background.OfficeColor12;
            RibbonOuterBackgroundBottom = OfficeColors.Background.OfficeColor13;
            RibbonBackground1 = OfficeColors.Background.OfficeColor14;
            RibbonBackground2 = OfficeColors.Background.OfficeColor15;
            RibbonBackground3 = OfficeColors.Background.OfficeColor16;
            RibbonBackground4 = OfficeColors.Background.OfficeColor17;
        }

        #endregion
    } 

    #endregion
}
