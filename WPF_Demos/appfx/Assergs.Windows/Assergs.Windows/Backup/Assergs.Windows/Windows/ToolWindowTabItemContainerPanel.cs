#region Using

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows; 

#endregion

namespace Assergs.Windows.Controls
{
    public class ToolWindowTabItemContainerPanel: Panel
    {
        #region Constructor

        public ToolWindowTabItemContainerPanel()
            : base()
        {
            this.VerticalAlignment = VerticalAlignment.Stretch;
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
        } 

        #endregion

        #region MeasureOverride

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement child in this.Children)
            {
                if (child is ToolWindow)
                {
                    ToolWindow win = (ToolWindow)child;
                    win.Height = availableSize.Height;
                    win.Width = availableSize.Width;
                }

                child.Measure(availableSize);
            }

            return availableSize;
        } 

        #endregion

        #region ArrangeOverride

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement child in this.Children)
            {
                child.Arrange(new Rect(child.DesiredSize));
            }

            return finalSize;
        } 

        #endregion
    }
}
