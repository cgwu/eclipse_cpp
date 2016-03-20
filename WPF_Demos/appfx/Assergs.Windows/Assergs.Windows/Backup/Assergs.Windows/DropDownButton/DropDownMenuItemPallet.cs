#region Using

using System;
using System.Windows.Media; 

#endregion

namespace Assergs.Windows
{
	#region DropDownMenuItemPallet

	public static class DropDownMenuItemPallet
    {
        #region Declare

        public static Color Foreground;
        public static Color NormalBorder;
        public static Color Glyph;
        public static Color LightBorder;
        public static Color PlusLightBorder;
        public static Color DisabledBorder;
        
        public static Color EditableControlBackground;
		public static Color MenuItemsBackground;

        public static Color MenuItemHighlightBackGround1;
		public static Color MenuItemHighlightBackGround2;
		public static Color MenuItemHighlightBackGround3;
		public static Color MenuItemHighlightBackGround4;

		public static Color MenuItemLeftBackground1;
		public static Color MenuItemLeftBackground2;

        public static Color NormalBackground;

		public static Color DisabledForeground;

        #endregion

        #region Constructor

		static DropDownMenuItemPallet()
        {
			DropDownMenuItemPallet.Reset();
			OfficeColors.RegistersTypes.Add(typeof(DropDownMenuItemPallet));
        }

        #endregion

        #region Reset

        public static void Reset()
        {
            Foreground = OfficeColors.Foreground.OfficeColor1;
            NormalBorder = OfficeColors.Background.OfficeColor82;
            Glyph = OfficeColors.Foreground.OfficeColor3;
            EditableControlBackground = OfficeColors.EditableControlsBackground.OfficeColor1;
			MenuItemsBackground = OfficeColors.Background.OfficeColor32;
			
			LightBorder = OfficeColors.HighLight.OfficeColor20;
            PlusLightBorder = OfficeColors.HighLight.OfficeColor21;
            DisabledBorder = OfficeColors.Disabled.OfficeColor3;
            DisabledForeground = OfficeColors.Disabled.OfficeColor4;
            
		    MenuItemHighlightBackGround1 = OfficeColors.HighLight.OfficeColor3;
			MenuItemHighlightBackGround2 = OfficeColors.HighLight.OfficeColor4;
			MenuItemHighlightBackGround3 = OfficeColors.HighLight.OfficeColor5;
			MenuItemHighlightBackGround4 = OfficeColors.HighLight.OfficeColor6;

			MenuItemLeftBackground1 = OfficeColors.Background.OfficeColor33;
			MenuItemLeftBackground2 = OfficeColors.Background.OfficeColor28;
						
			NormalBackground = OfficeColors.EditableControlsBackground.OfficeColor1;
        }

        #endregion
    } 

    #endregion
}
