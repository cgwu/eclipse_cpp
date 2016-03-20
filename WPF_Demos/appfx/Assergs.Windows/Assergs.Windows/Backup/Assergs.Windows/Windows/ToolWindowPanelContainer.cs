#region Using

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows; 

#endregion

namespace Assergs.Windows.Controls
{
    public class ToolWindowPanelContainer: Panel
    {
        public event EventHandler<ToolWindowEventArgs> ChildAdded;
        public event EventHandler<ToolWindowEventArgs> ChildRemoved;

        #region Constructor

        public ToolWindowPanelContainer()
            : base()
        {
            this.VerticalAlignment = VerticalAlignment.Top;
            this.HorizontalAlignment = HorizontalAlignment.Left;
        } 

        #endregion

        #region MeasureOverride

        protected override Size MeasureOverride(Size availableSize)
        {
            Size totalAvaliableSize = this._CalculatedDesiredSize;
            
            foreach (UIElement child in this.Children)
            {
                child.Measure(availableSize);

                totalAvaliableSize = new Size(Math.Max(totalAvaliableSize.Width, child.DesiredSize.Width),
                    Math.Max(totalAvaliableSize.Height, child.DesiredSize.Height));
            }

            return totalAvaliableSize;
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

        #region OnVisualChildrenChanged

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);

            if (visualAdded != null)
            {
                if (ChildAdded != null)
                    ChildAdded(this, new ToolWindowEventArgs(visualAdded as ToolWindow));
            }
            else if (visualRemoved != null)
            {
                if (ChildRemoved != null)
                    ChildRemoved(this, new ToolWindowEventArgs(visualRemoved as ToolWindow));
            }
        } 

        #endregion

        #region CalculatedDesiredSize

        private Size _CalculatedDesiredSize = new Size();

        public Size CalculatedDesiredSize
        {
            get
            {
                return _CalculatedDesiredSize;
            }
            set
            {
                _CalculatedDesiredSize = value;
            }
        }

        #endregion
    }

    #region ToolWindowEventArgs

    public class ToolWindowEventArgs : EventArgs
    {
        private ToolWindow _ToolWindow;

        #region Constructor

        public ToolWindowEventArgs(ToolWindow toolWindow)
        {
            this._ToolWindow = toolWindow;
        } 

        #endregion

        #region ToolWindow

        public ToolWindow ToolWindow
        {
            get { return this._ToolWindow; }
            set { this._ToolWindow = value; }
        } 

        #endregion
    } 

    #endregion
}
