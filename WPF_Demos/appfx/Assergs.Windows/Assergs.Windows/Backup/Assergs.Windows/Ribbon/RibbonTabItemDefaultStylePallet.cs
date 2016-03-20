#region Using

using System;
using System.Windows.Media; 

#endregion

namespace Assergs.Windows
{
    #region RibbonTabItemDefaultStylePallet

    public static class RibbonTabItemDefaultStylePallet
    {
        #region Declare

        public static Color RibbonBorderBrush;

        #endregion

        #region Constructor

        static RibbonTabItemDefaultStylePallet()
        {
            RibbonTabItemDefaultStylePallet.Reset();
            OfficeColors.RegistersTypes.Add(typeof(RibbonTabItemDefaultStylePallet));
        }

        #endregion

        #region Reset

        public static void Reset()
        {
            RibbonBorderBrush = OfficeColors.Background.OfficeColor11;
        }

        #endregion
    } 

    #endregion
}
