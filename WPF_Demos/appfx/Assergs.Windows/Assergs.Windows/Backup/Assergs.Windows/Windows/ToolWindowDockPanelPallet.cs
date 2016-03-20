#region Using

using System;
using System.Windows.Media; 

#endregion

namespace Assergs.Windows
{
    #region ToolWindowDockPanelPallet

    public static class ToolWindowDockPanelPallet
    {
        #region Declare

        public static Color TabsBackground;

        #endregion

        #region Constructor

        static ToolWindowDockPanelPallet()
        {
            ToolWindowDockPanelPallet.Reset();
            OfficeColors.RegistersTypes.Add(typeof(ToolWindowDockPanelPallet));
        }

        #endregion

        #region Reset

        public static void Reset()
        {
            TabsBackground = OfficeColors.Background.OfficeColor26;
        }

        #endregion
    } 

    #endregion
}
