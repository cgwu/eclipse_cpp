#region Using

using System;
using System.Windows.Media; 

#endregion

namespace Assergs.Windows
{
    #region UnderlineSeparatorPallet

    public static class UnderlineSeparatorPallet
    {
        #region Declare

        public static Color Foreground;
        public static Color NormalBrush1;
		public static Color NormalBrush2;
		public static Color NormalBrush3;
		        
        #endregion

        #region Constructor

		static UnderlineSeparatorPallet()
        {
			UnderlineSeparatorPallet.Reset();
			OfficeColors.RegistersTypes.Add(typeof(UnderlineSeparatorPallet));
        }

        #endregion

        #region Reset

        public static void Reset()
        {
            Foreground = OfficeColors.Foreground.OfficeColor1;
			NormalBrush1 = OfficeColors.Background.OfficeColor6;
			NormalBrush2 = OfficeColors.Background.OfficeColor4;
			NormalBrush3 = NormalBrush2;
			NormalBrush3.A = 0;
        }

        #endregion
    } 

    #endregion
}
