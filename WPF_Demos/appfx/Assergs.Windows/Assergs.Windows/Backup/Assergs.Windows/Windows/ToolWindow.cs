# region Using Directives

using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Interop;
using System.Threading;
using Assergs.Windows.Controls;

using System.Windows.Threading;
using System.Diagnostics;
using System.Windows.Data;
using System.Collections;

# endregion

namespace Assergs.Windows 
{
    #region class ToolWindow 

    [TemplatePart(Name = ToolWindow.PART_Dragger, Type = typeof(UIElement))]
    [TemplatePart(Name = ToolWindow.PART_ResizeGrip, Type = typeof(ResizeGrip))]
    public class ToolWindow : HeaderedContentControl
    {
        # region Declarations

        internal const string PART_Dragger = "PART_Dragger";
        private UIElement _PART_Dragger_Element;

        internal const string PART_ResizeGrip = "PART_ResizeGrip";
        private ResizeGrip _PART_ResizeGrip_Element;

        private DragOrResizeStatus _PreviewDragOrResizeStatus;
        private DragOrResizeStatus _DragOrResizeStatus;
        private Point _StartMousePosition;
        private Point _PreviousMousePosition;

        public static RoutedCommand CloseCommand;
        public static RoutedCommand MaximizeCommand;

		public static readonly RoutedEvent CreatedEvent;
        public static readonly RoutedEvent ClosedEvent;
        public static readonly RoutedEvent MaximizeChangedEvent;

        private Thickness _OldThickness;
        private double _OldWidth;
        private double _OldHeight;
        private bool _OldCanDrag;
        private bool _OldCanResize;
        private bool _IsInitialized;
		private bool _IsShowed;
		//private bool _CanRetainMouseCapture = true;

        # endregion

        # region Constructor

        # region static

        static ToolWindow()
        {
            ToolWindow.DefaultStyleKeyProperty.OverrideMetadata(typeof(ToolWindow),
                new FrameworkPropertyMetadata(typeof(ToolWindow)));

            KeyGesture keyClose = new KeyGesture(Key.F4, ModifierKeys.Alt | ModifierKeys.Control);
                        
            ToolWindow.CloseCommand = new RoutedCommand("Close", typeof(ToolWindow)); //, fooInputs);
            CloseCommand.InputGestures.Add(keyClose);
            ToolWindow.MaximizeCommand = new RoutedCommand("Maximize", typeof(ToolWindow));

			ToolWindow.CreatedEvent = EventManager.RegisterRoutedEvent("Created", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ToolWindow));
            ToolWindow.ClosedEvent = EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ToolWindow));
            ToolWindow.MaximizeChangedEvent = EventManager.RegisterRoutedEvent("Maximize", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ToolWindow));

            Control.IsTabStopProperty.OverrideMetadata(typeof(ToolWindow),
                new FrameworkPropertyMetadata(false));
            KeyboardNavigation.DirectionalNavigationProperty.OverrideMetadata(
                typeof(ToolWindow), new FrameworkPropertyMetadata(KeyboardNavigationMode.Cycle));

            if (BrowserInteropHelper.IsBrowserHosted)
            {
                KeyboardNavigation.TabNavigationProperty.OverrideMetadata(
                typeof(ToolWindow), new FrameworkPropertyMetadata(KeyboardNavigationMode.Local));
            }
            else
            {
                KeyboardNavigation.TabNavigationProperty.OverrideMetadata(
                typeof(ToolWindow), new FrameworkPropertyMetadata(KeyboardNavigationMode.Cycle));

                KeyboardNavigation.ControlTabNavigationProperty.OverrideMetadata(
                typeof(ToolWindow), new FrameworkPropertyMetadata(KeyboardNavigationMode.Once));
            }

            FocusManager.IsFocusScopeProperty.OverrideMetadata(
                typeof(ToolWindow), new FrameworkPropertyMetadata(true));
        }

        # endregion

        #region public

        public ToolWindow()
        {	
            this.Visibility = Visibility.Hidden;
			this._IsShowed = false;

            this._DragOrResizeStatus = DragOrResizeStatus.None;
            this._StartMousePosition = new Point();

            EventManager.RegisterClassHandler(typeof(ToolWindow), ToolWindow.ActivatedEvent,
                new RoutedEventHandler(this.ToolWindow_Activated));

            CommandBinding bindingClose = new CommandBinding(ToolWindow.CloseCommand, new ExecutedRoutedEventHandler(this.CloseCommand_Executed));
            this.CommandBindings.Add(bindingClose);
            
            CommandBinding bindingMaximize = new CommandBinding(ToolWindow.MaximizeCommand, new ExecutedRoutedEventHandler(this.MaximizeCommand_Executed));
            this.CommandBindings.Add(bindingMaximize);

            Thickness _OldThickness = this.Margin;
            this._OldWidth = this.Width;
            this._OldHeight = this.Height;
            this._OldCanDrag = this.CanDrag;
            this._OldCanResize = this.CanResize;

            this.StartPosition = ToolWindowStartPosition.Manual;
            this.ToolWindowState = ToolWindowState.Normal;
        } 

        #endregion

        # endregion

        # region Properties

        #region ModalContainerPanel

        private static Panel _ModalContainerPanel;

        public static Panel ModalContainerPanel
        {
            get
            {
                return _ModalContainerPanel;
            }
            set
            {
                _ModalContainerPanel = value;
            }
        }

        #endregion

        # region Parent

        private Panel _Parent;

        public new Panel Parent
        {
            get
            {
                if (this._Parent == null)
                {
                    throw new Exception("ToolWindow.Parent cannot be null.");
                }

                return this._Parent;
            }
            set
            {
                this._Parent = value;
            }
        }

        # endregion

        # region Icon

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(ToolWindow),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public ImageSource Icon
        {
            get
            {
                return (ImageSource)this.GetValue(ToolWindow.IconProperty);
            }
            set
            {
                this.SetValue(ToolWindow.IconProperty, value);
            }
        }

        # endregion

        # region LeftStatusContent

        public static readonly DependencyProperty LeftStatusContentProperty =
            DependencyProperty.Register("LeftStatusContent", typeof(object), typeof(ToolWindow), new FrameworkPropertyMetadata(null));

        public object LeftStatusContent
        {
            get
            {
                return (object)this.GetValue(ToolWindow.LeftStatusContentProperty);
            }
            set
            {
                this.SetValue(ToolWindow.LeftStatusContentProperty, value);
            }
        }

        # endregion

        # region RightStatusContent

        public static readonly DependencyProperty RightStatusContentProperty =
            DependencyProperty.Register("RightStatusContent", typeof(object), typeof(ToolWindow), new FrameworkPropertyMetadata(null));

        public object RightStatusContent
        {
            get
            {
                return (object)this.GetValue(ToolWindow.RightStatusContentProperty);
            }
            set
            {
                this.SetValue(ToolWindow.RightStatusContentProperty, value);
            }
        }

        # endregion

        # region IsActive

        private static readonly DependencyPropertyKey IsActivePropertyKey =
            DependencyProperty.RegisterReadOnly("IsActive", typeof(bool),
            typeof(ToolWindow), new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty IsActiveProperty =
            ToolWindow.IsActivePropertyKey.DependencyProperty;

        public bool IsActive
        {
            get
            {
                return (bool)this.GetValue(ToolWindow.IsActiveProperty);
            }
        }

        # endregion

        #region IsMaximized

        public bool IsMaximized
        {
            get
            {
                return (bool)GetValue(IsMaximizedProperty);
            }
            set
            {
                if (this.IsMaximized != value)
                {
                    if (value)
                    {
                        this._OldThickness = this.Margin;
                        this._OldWidth = this.Width;
                        this._OldHeight = this.Height;
                        this._OldCanDrag = this.CanDrag;
                        this._OldCanResize = this.CanResize;

                        this.CanDrag = false;
                        this.CanResize = false;
                    }
                    else
                    {
                        this.Margin = this._OldThickness;
                        this.Width = this._OldWidth;
                        this.Height = this._OldHeight;
                        this.CanDrag = this._OldCanDrag;
                        this.CanResize = this._OldCanResize;
                    }

                    SetValue(IsMaximizedProperty, value);
                    this.RaiseMaximizeChangedEvent();
                }
                else
                {
                    SetValue(IsMaximizedProperty, value);
                }
            }
        }

        public static readonly DependencyProperty IsMaximizedProperty =
            DependencyProperty.Register("IsMaximized", typeof(bool), typeof(ToolWindow), new FrameworkPropertyMetadata(false));

        #endregion

        #region IsModal

        public bool IsModal
        {
            get
            {
                return (bool)GetValue(IsModalProperty);
            }
            set
            {
                SetValue(IsModalProperty, value);

                if (value)
                {
                    KeyboardNavigation.SetTabNavigation(this, KeyboardNavigationMode.Cycle);
                    KeyboardNavigation.SetDirectionalNavigation(this, KeyboardNavigationMode.None);

					this.LockMouseOutside();
                }
                else
                {
					this.LockMouseOutside(false);
                }
            }
        }

        public static readonly DependencyProperty IsModalProperty =
            DependencyProperty.Register("IsModal", typeof(bool), typeof(ToolWindow),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure));

        #endregion

        #region StartPosition

        public ToolWindowStartPosition StartPosition
        {
            get
            {
                return (ToolWindowStartPosition)GetValue(StartPositionProperty);
            }
            set
            {
                SetValue(StartPositionProperty, value);
            }
        }

        public static readonly DependencyProperty StartPositionProperty =
            DependencyProperty.Register("StartPosition", typeof(ToolWindowStartPosition), typeof(ToolWindow),
                new FrameworkPropertyMetadata(ToolWindowStartPosition.WindowsDefaultLocation, FrameworkPropertyMetadataOptions.AffectsArrange));

        #endregion

        #region ToolWindowState

        public ToolWindowState ToolWindowState
        {
            get
            {
                return (ToolWindowState)GetValue(ToolWindowStateProperty);
            }
            set
            {
                SetValue(ToolWindowStateProperty, value);

            }
        }

        public static readonly DependencyProperty ToolWindowStateProperty =
            DependencyProperty.Register("ToolWindowState", typeof(ToolWindowState), typeof(ToolWindow),
                new FrameworkPropertyMetadata(ToolWindowState.Normal));

        #endregion

        #region ShowStatusBar

        public bool ShowStatusBar
        {
            get { return (bool)GetValue(ShowStatusBarProperty); }
            set { SetValue(ShowStatusBarProperty, value); }
        }

        public static readonly DependencyProperty ShowStatusBarProperty =
            DependencyProperty.Register("ShowStatusBar", typeof(bool), typeof(ToolWindow),
                new FrameworkPropertyMetadata(true));

        #endregion

		#region ShowCloseButton

		public bool ShowCloseButton
		{
			get { return (bool)GetValue(ShowCloseButtonProperty); }
			set { SetValue(ShowCloseButtonProperty, value); }
		}

		public static readonly DependencyProperty ShowCloseButtonProperty =
			DependencyProperty.Register("ShowCloseButton", typeof(bool), typeof(ToolWindow),
				new FrameworkPropertyMetadata(true));

		#endregion

        #region CanDrag

        public bool CanDrag
        {
            get { return (bool)GetValue(CanDragProperty); }
            set { SetValue(CanDragProperty, value); }
        }

        public static readonly DependencyProperty CanDragProperty =
            DependencyProperty.Register("CanDrag", typeof(bool), typeof(ToolWindow),
                new FrameworkPropertyMetadata(true));

        #endregion

        #region CanResize

        public bool CanResize
        {
            get { return (bool)GetValue(CanResizeProperty); }
            set { SetValue(CanResizeProperty, value); }
        }

        public static readonly DependencyProperty CanResizeProperty =
            DependencyProperty.Register("CanResize", typeof(bool), typeof(ToolWindow),
                new FrameworkPropertyMetadata(true));

        #endregion

        # endregion

        #region Methods

        # region Routed Events

        # region DragStarted

        public static readonly RoutedEvent DragStartedEvent = EventManager.
            RegisterRoutedEvent("DragStarted", RoutingStrategy.Bubble,
            typeof(DragStartedEventHandler), typeof(ToolWindow));

        public event DragStartedEventHandler DragStarted
        {
            add
            {
                base.AddHandler(ToolWindow.DragStartedEvent, value);
            }
            remove
            {
                base.RemoveHandler(ToolWindow.DragStartedEvent, value);
            }
        }

        # endregion

        # region DragDelta

        public static readonly RoutedEvent DragDeltaEvent = EventManager.
            RegisterRoutedEvent("DragDelta", RoutingStrategy.Bubble,
            typeof(DragDeltaEventHandler), typeof(ToolWindow));

        public event DragDeltaEventHandler DragDelta
        {
            add
            {
                base.AddHandler(ToolWindow.DragDeltaEvent, value);
            }
            remove
            {
                base.RemoveHandler(ToolWindow.DragDeltaEvent, value);
            }
        }

        # endregion

        # region DragCompleted

        public static readonly RoutedEvent DragCompletedEvent = EventManager.
            RegisterRoutedEvent("DragCompleted", RoutingStrategy.Bubble,
            typeof(DragCompletedEventHandler), typeof(ToolWindow));

        public event DragCompletedEventHandler DragCompleted
        {
            add
            {
                base.AddHandler(ToolWindow.DragCompletedEvent, value);
            }
            remove
            {
                base.RemoveHandler(ToolWindow.DragCompletedEvent, value);
            }
        }

        # endregion

        # region BringToFront

        private void BringToFront()
        {
            int index = Panel.GetZIndex(this);
            Panel.SetZIndex(this, this.Parent.Children.Count + 1);

			#region Define ZIndex Order

			UIElement[] sortAux = new UIElement[this.Parent.Children.Count];
			for (int i = 0; i < this.Parent.Children.Count; i++)
			{
				sortAux[i] = this.Parent.Children[i];
			}

			Array.Sort<UIElement>(sortAux, new Comparison<UIElement>(delegate(UIElement a, UIElement b)
			  {
				  int aIndex = Panel.GetZIndex(a);
				  int bIndex = Panel.GetZIndex(b);
				  if (aIndex > bIndex)
					  return 1;
				  else if (aIndex == bIndex)
					  return 0;
				  else
					  return -1;
			  })); 

			#endregion

			for (int i = 0; i < sortAux.Length; i++)
			{
				Panel.SetZIndex(sortAux[i], i);
			}
        }

        # endregion

        # region Activated

        protected virtual void OnActivated()
        {
            if (!(bool)this.GetValue(ToolWindow.IsActiveProperty))
            {
                this.Visibility = Visibility.Visible;
                this.SetValue(ToolWindow.IsActivePropertyKey, true);
				this.BringToFront();
                this.Focus();
                RoutedEventArgs e = new RoutedEventArgs(ToolWindow.ActivatedEvent, this);
                base.RaiseEvent(e);
            }
        }

        public static readonly RoutedEvent ActivatedEvent = EventManager.
            RegisterRoutedEvent("Activated", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(ToolWindow));

        public event RoutedEventHandler Activated
        {
            add
            {
                base.AddHandler(ToolWindow.ActivatedEvent, value);
            }
            remove
            {
                base.RemoveHandler(ToolWindow.ActivatedEvent, value);
            }
        }

        # endregion

        # region Deactivated

        protected virtual void OnDeactivated()
        {
            this.SetValue(ToolWindow.IsActivePropertyKey, false);

            RoutedEventArgs e = new RoutedEventArgs(ToolWindow.DeactivatedEvent, this);
            base.RaiseEvent(e);
        }

        public static readonly RoutedEvent DeactivatedEvent = EventManager.
            RegisterRoutedEvent("Deactivated", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(ToolWindow));

        public event RoutedEventHandler Deactivated
        {
            add
            {
                base.AddHandler(ToolWindow.DeactivatedEvent, value);
            }
            remove
            {
                base.RemoveHandler(ToolWindow.DeactivatedEvent, value);
            }
        }

        # endregion

        # endregion

        #region Activate

        public void Activate()
        {
            this.OnActivated();
        }

        #endregion

        #region Show

        public void Show()
        {
            if (!this.Parent.Children.Contains(this))
            {
                this.Parent.Children.Add(this);
            }

			if (!this._IsShowed)
			{
				this.RaiseCreatedEvent();
				this._IsShowed = true;
			}

            this.OnActivated();
        }

        #endregion

        #region ShowModalDialog

        public void ShowModalDialog()
        {
            this._Parent = ToolWindow._ModalContainerPanel;
            this.IsModal = true;
			this.Show();
            this.Focus();
        }

        #endregion

        #region Close

        public void Close()
        {
			this.LockMouseOutside(false);
            this.Parent.Children.Remove(this);
			this.RaiseCloseEvent();
			GC.SuppressFinalize(this);
        }

        #endregion

		#region LockMouseOutside

		private void LockMouseOutside()
		{
			this.LockMouseOutside(true);
		}
		
		private void LockMouseOutside(bool doLock)
        {
			if (doLock)
			{
				if (ToolWindow._ModalContainerPanel != null)
				{
					Binding wBinding = new Binding("ActualWidth");
					wBinding.Source = ToolWindow._ModalContainerPanel;
					Binding hBinding = new Binding("ActualHeight");
					hBinding.Source = ToolWindow._ModalContainerPanel;

					Canvas fence = new Canvas();
					fence.Background = Brushes.Transparent;
					fence.SetBinding(Canvas.WidthProperty, wBinding);
					fence.SetBinding(Canvas.HeightProperty, hBinding);

					ToolWindow._ModalContainerPanel.Children.Add(fence);
					Canvas.SetZIndex(fence, ToolWindow._ModalContainerPanel.Children.Count);
				}
				else
				{
					throw new Exception("ToolWindow._ModalContainerPanel is not defined yet. In order to show a Modal ToolWindow you must set the static property ToolWindow.ModalContainerPanel.");
				}
			}
			else
			{
				int windowIndex = ToolWindow._ModalContainerPanel.Children.IndexOf(this);
				if (windowIndex > 0)
				{
					UIElement target = ToolWindow._ModalContainerPanel.Children[windowIndex - 1];

					if (target is Canvas)
					{
						ToolWindow._ModalContainerPanel.Children.Remove(target);
						GC.SuppressFinalize(target);
					}
				}

			}
        }

        #endregion

        #region CloseCommand_Executed

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
			this.Close();
        }

        #endregion

        #region MaximizeCommand_Executed

        private void MaximizeCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.IsMaximized = !this.IsMaximized;
        }

        #endregion

        #region OnRenderSizeChanged

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            if (!this._IsInitialized)
            {
                this.RefreshCalculatedVisualProperties();
                this._IsInitialized = true;
            }
        }

        #endregion

		#region CreatedEvent

		public event RoutedEventHandler Created
        {
			add { AddHandler(ToolWindow.CreatedEvent, value); }
			remove { RemoveHandler(ToolWindow.CreatedEvent, value); }
        }

		void RaiseCreatedEvent()
        {
			RoutedEventArgs e = new RoutedEventArgs(ToolWindow.CreatedEvent, this);
            this.RaiseEvent(e);
        }

        #endregion

        #region CloseEvent

        public event RoutedEventHandler Closed
        {
            add { AddHandler(ToolWindow.ClosedEvent, value); }
            remove { RemoveHandler(ToolWindow.ClosedEvent, value); }
        }

        void RaiseCloseEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ToolWindow.ClosedEvent, this);
            this.RaiseEvent(newEventArgs);
        }

        #endregion

        #region MaximizeEvent

        public event RoutedEventHandler Maximize
        {
            add { AddHandler(ToolWindow.MaximizeChangedEvent, value); }
            remove { RemoveHandler(ToolWindow.MaximizeChangedEvent, value); }
        }

        void RaiseMaximizeChangedEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ToolWindow.MaximizeChangedEvent, this);
			this.RaiseEvent(newEventArgs);
        }

        #endregion

        #region RefreshCalculatedVisualProperties

        private void RefreshCalculatedVisualProperties()
        {
            #region ToolWindowState

            switch (this.ToolWindowState)
            {
                case ToolWindowState.Maximized:
                    this.IsMaximized = true;
                    return;
                case ToolWindowState.Normal:
                    this.IsMaximized = false;
                    break;
            }

            #endregion

            #region StartPosition

            switch (this.StartPosition)
            {
                case ToolWindowStartPosition.CenterParent:

                    FrameworkElement parent;
                    parent = this.Parent as FrameworkElement;

                    double top = (parent.ActualHeight - this.ActualHeight) / 2;
                    double left = (parent.ActualWidth - this.ActualWidth) / 2;
                    this.Margin = new Thickness(left, top, 0, 0);

                    break;
                case ToolWindowStartPosition.Manual:
                    break;
                case ToolWindowStartPosition.WindowsDefaultLocation:

                    this.Margin = new Thickness(0);

                    break;
            }

            #endregion
        }

        #endregion

        # region ToolWindow_Activated

        private void ToolWindow_Activated(object sender, RoutedEventArgs e)
        {
            if (sender != this)
            {
                this.OnDeactivated();
            }
        }

        # endregion

        # region OnGotKeyboardFocus

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            this.OnActivated();
        }

        # endregion

        # region OnPreviewMouseDown

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            this.OnActivated();
        }

        # endregion

        # region MeasureOverride

        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }

        # endregion

        # region DragOrResizeStatus Enum

        private struct DragOrResizeStatus
        {
            private byte _Value;

            const byte _TopLeft = 9;
            const byte _TopCenter = 10;
            const byte _TopRight = 12;
            const byte _MiddleLeft = 17;
            const byte _Drag = 18;
            const byte _MiddleRight = 20;
            const byte _BottomLeft = 33;
            const byte _BottomCenter = 34;
            const byte _BottomRight = 36;

            const byte _Left = 1;
            const byte _HMiddle = 2;
            const byte _Right = 4;
            const byte _Top = 8;
            const byte _VCenter = 16;
            const byte _Bottom = 32;

            public static DragOrResizeStatus None = new DragOrResizeStatus { _Value = 0 };
            public static DragOrResizeStatus TopLeft = new DragOrResizeStatus { _Value = _TopLeft };
            public static DragOrResizeStatus TopCenter = new DragOrResizeStatus { _Value = _TopCenter };
            public static DragOrResizeStatus TopRight = new DragOrResizeStatus { _Value = _TopRight };
            public static DragOrResizeStatus MiddleLeft = new DragOrResizeStatus { _Value = _MiddleLeft };
            public static DragOrResizeStatus Drag = new DragOrResizeStatus { _Value = _Drag };
            public static DragOrResizeStatus MiddleRight = new DragOrResizeStatus { _Value = _MiddleRight };
            public static DragOrResizeStatus BottomLeft = new DragOrResizeStatus { _Value = _BottomLeft };
            public static DragOrResizeStatus BottomCenter = new DragOrResizeStatus { _Value = _BottomCenter };
            public static DragOrResizeStatus BottomRight = new DragOrResizeStatus { _Value = _BottomRight };

            public bool IsOnLeft { get { return (this._Value & _Left) == _Left; } }
            public bool IsOnHMiddle { get { return (this._Value & _HMiddle) == _HMiddle; } }
            public bool IsOnRight { get { return (this._Value & _Right) == _Right; } }
            public bool IsOnTop { get { return (this._Value & _Top) == _Top; } }
            public bool IsOnVCenter { get { return (this._Value & _VCenter) == _VCenter; } }
            public bool IsOnBottom { get { return (this._Value & _Bottom) == _Bottom; } }
            public bool IsOnTopLeftOrBottomRight { get { return this._Value == _TopLeft || this._Value == _BottomRight; } }
            public bool IsOnTopRightOrBottomLeft { get { return this._Value == _TopRight || this._Value == _BottomLeft; } }
            public bool IsOnTopCenterOrBottomCenter { get { return this._Value == _TopCenter || this._Value == _BottomCenter; } }
            public bool IsOnMiddleLeftOrMiddleRight { get { return this._Value == _MiddleLeft || this._Value == _MiddleRight; } }

            public override bool Equals(object obj)
            {
                if (obj == null || this.GetType() != obj.GetType())
                {
                    return false;
                }

                return ((DragOrResizeStatus)obj)._Value == this._Value;
            }

            public override int GetHashCode()
            {
                return this._Value.GetHashCode();
            }

            public static bool operator ==(DragOrResizeStatus x, DragOrResizeStatus y)
            {
                return x.Equals(y);
            }

            public static bool operator !=(DragOrResizeStatus x, DragOrResizeStatus y)
            {
                return !x.Equals(y);
            }
        }

        # endregion

		# region Drag and Resize

        # region OnApplyTemplate

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._PART_Dragger_Element = this.GetTemplateChild(ToolWindow.PART_Dragger) as UIElement;
            this._PART_ResizeGrip_Element = this.GetTemplateChild(ToolWindow.PART_ResizeGrip) as ResizeGrip;
        }

        # endregion

        # region GetDragOrResizeMode

        private DragOrResizeStatus GetDragOrResizeMode(Point position)
        {
            int resizeSideThichness = 4;
            int resizeCornerSize = 12;

            DragOrResizeStatus stat = DragOrResizeStatus.None;

            if (position.X <= resizeSideThichness) // left
            {
                if (position.Y <= resizeCornerSize)
                {
                    stat = DragOrResizeStatus.TopLeft;
                }
                else if (this.ActualHeight - position.Y <= resizeCornerSize)
                {
                    stat = DragOrResizeStatus.BottomLeft;
                }
                else
                {
                    stat = DragOrResizeStatus.MiddleLeft;
                }
            }
            else if (this.ActualWidth - position.X <= resizeSideThichness) // right
            {
                if (position.Y <= resizeCornerSize)
                {
                    stat = DragOrResizeStatus.TopRight;
                }
                else if (this.ActualHeight - position.Y <= resizeCornerSize)
                {
                    stat = DragOrResizeStatus.BottomRight;
                }
                else
                {
                    stat = DragOrResizeStatus.MiddleRight;
                }
            }
            else if (position.Y <= resizeSideThichness) // top
            {
                if (position.X <= resizeCornerSize)
                {
                    stat = DragOrResizeStatus.TopLeft;
                }
                else if (this.ActualWidth - position.X <= resizeCornerSize)
                {
                    stat = DragOrResizeStatus.TopRight;
                }
                else
                {
                    stat = DragOrResizeStatus.TopCenter;
                }
            }
            else if (this.ActualHeight - position.Y <= resizeSideThichness) // bottom
            {
                if (position.X <= resizeCornerSize)
                {
                    stat = DragOrResizeStatus.BottomLeft;
                }
                else if (this.ActualWidth - position.X <= resizeCornerSize)
                {
                    stat = DragOrResizeStatus.BottomRight;
                }
                else
                {
                    stat = DragOrResizeStatus.BottomCenter;
                }
            }
            else if (this._PART_Dragger_Element != null &&
                this._PART_Dragger_Element.IsMouseOver)
            {
                stat = DragOrResizeStatus.Drag;
            }
            else if (this._PART_ResizeGrip_Element != null &&
                this._PART_ResizeGrip_Element.IsMouseOver)
            {
                stat = DragOrResizeStatus.BottomRight;
            }

            return stat;
        }

        # endregion

        # region CancelDragOrResize

        private void CancelDragOrResize()
        {
			this.Cursor = Cursors.Arrow;
            this.ReleaseMouseCapture();
		    this._DragOrResizeStatus = DragOrResizeStatus.None;
        }

        # endregion

        # region SetResizeCursor

        private void SetResizeCursor(DragOrResizeStatus resizeStatus)
        {
            if (resizeStatus.IsOnTopLeftOrBottomRight)
            {
                this.Cursor = Cursors.SizeNWSE;
            }
            else if (resizeStatus.IsOnTopRightOrBottomLeft)
            {
                this.Cursor = Cursors.SizeNESW;
            }
            else if (resizeStatus.IsOnTopRightOrBottomLeft)
            {
                this.Cursor = Cursors.SizeNESW;
            }
            else if (resizeStatus.IsOnTopCenterOrBottomCenter)
            {
                this.Cursor = Cursors.SizeNS;
            }
            else if (resizeStatus.IsOnMiddleLeftOrMiddleRight)
            {
                this.Cursor = Cursors.SizeWE;
            }
            else
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        # endregion

        # region AdjustBounds

        private void AdjustBounds(Point actualPosition)
        {
            System.Windows.
            Thickness margin = new Thickness(this.Margin.Left, this.Margin.Top, this.Margin.Right, this.Margin.Bottom);
            Size size = this.RenderSize;

            Vector changeFromStart = Point.Subtract(actualPosition, this._StartMousePosition);
            Vector changeFromPrevious = Point.Subtract(actualPosition, this._PreviousMousePosition);

            if (this._DragOrResizeStatus == DragOrResizeStatus.Drag)
            {
                margin.Left += changeFromStart.X;
                margin.Top += changeFromStart.Y;
            }
            else
            {
                if (this._DragOrResizeStatus.IsOnRight)
                {
                    if (size.Width + changeFromPrevious.X > this.MinWidth)
                    {
                        size.Width += changeFromPrevious.X;
                    }
                }
                else if (this._DragOrResizeStatus.IsOnLeft)
                {
                    if (size.Width - changeFromStart.X > this.MinWidth)
                    {
                        margin.Left += changeFromStart.X;
                        size.Width -= changeFromStart.X;
                    }
                }

                if (this._DragOrResizeStatus.IsOnBottom)
                {
                    if (size.Height + changeFromPrevious.Y > this.MinHeight)
                    {
                        size.Height += changeFromPrevious.Y;
                    }
                }
                else if (this._DragOrResizeStatus.IsOnTop)
                {
                    if (size.Height - changeFromStart.Y > this.MinHeight)
                    {
                        margin.Top += changeFromStart.Y;
                        size.Height -= changeFromStart.Y;
                    }
                }
            }

            this.Margin = margin;
            this.Width = size.Width;
            this.Height = size.Height;
        }

        # endregion

        # region OnMouseLeftButtonDown

        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._DragOrResizeStatus == DragOrResizeStatus.None)
            {
                if (this._PreviewDragOrResizeStatus != DragOrResizeStatus.None)
                {
                    e.Handled = true;
                    this._DragOrResizeStatus = this._PreviewDragOrResizeStatus;
                    this._StartMousePosition = this._PreviousMousePosition = e.GetPosition(this);
                    this.Focus();
                    this.CaptureMouse();

                }

                //bool errorOccurred = true;

                //try
                //{
                //    DragStartedEventArgs args = new DragStartedEventArgs(this._OriginWindowPoint.X, this._OriginWindowPoint.Y);
                //    args.RoutedEvent = ChildWindow.DragStartedEvent;
                //    this.RaiseEvent(args);
                //    errorOccurred = false;
                //}
                //finally
                //{
                //    if (errorOccurred)
                //    {
                //        this.CancelDrag();
                //    }
                //}
            }
            base.OnMouseLeftButtonDown(e);
        }

        # endregion

        # region OnMouseMove

        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseMove(e);

            Point point = e.GetPosition(this);

            if (this._DragOrResizeStatus == DragOrResizeStatus.None)
            {
                Rect rect = new Rect(new Point(), this.RenderSize);

                if (!rect.Contains(point)) // modal mouse capture
                {
                    this.Cursor = Cursors.Arrow;
                    this._PreviewDragOrResizeStatus = DragOrResizeStatus.None;
                }
                else
                {
                    this._PreviewDragOrResizeStatus = this.GetDragOrResizeMode(point);

                    if (!this.CanDrag && this._PreviewDragOrResizeStatus == DragOrResizeStatus.Drag)
                    {
                        this._PreviewDragOrResizeStatus = DragOrResizeStatus.None;
                    }

                    if (!this.CanResize
                        && this._PreviewDragOrResizeStatus != DragOrResizeStatus.Drag
                        && this._PreviewDragOrResizeStatus != DragOrResizeStatus.None)
                    {
                        this._PreviewDragOrResizeStatus = DragOrResizeStatus.None;
                    }

                    this.SetResizeCursor(this._PreviewDragOrResizeStatus);
                }
            }
            else
            {
                if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
                {
                    if (point != this._PreviousMousePosition)
                    {
                        e.Handled = true;

                        //DragDeltaEventArgs args = new DragDeltaEventArgs(
                        //    point.X - this._OriginWindowPoint.X, point.Y - this._OriginWindowPoint.Y);
                        //args.RoutedEvent = ChildWindow.DragDeltaEvent;
                        //this.RaiseEvent(args);

                        this.AdjustBounds(point);

                        this._PreviousMousePosition = point;
                    }
                }
                else
                {
                    this.CancelDragOrResize();
                }
            }
        }

        # endregion

        # region // OnMouseLeave

        //protected override void OnMouseLeave(MouseEventArgs e)
        //{
        //    base.OnMouseLeave(e);

        //    this.Cursor = Cursors.Arrow;
        //}

        # endregion

        # region OnMouseLeftButtonUp

        protected override void OnMouseLeftButtonUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.IsMouseCaptured && this._DragOrResizeStatus != DragOrResizeStatus.None)
            {
                e.Handled = true;
                this.CancelDragOrResize();

                //Point point = e.MouseDevice.GetPosition(this._PART_Move_Element);

                //DragCompletedEventArgs args = new DragCompletedEventArgs(
                //    //point.X - this._OriginScreenCoordPosition.X, point.Y - this._OriginScreenCoordPosition.Y, false);
                //    point.X - this._OriginWindowPoint.X, point.Y - this._OriginWindowPoint.Y, false);
                //args.RoutedEvent = ChildWindow.DragCompletedEvent;
                //this.RaiseEvent(args);
            }
            base.OnMouseLeftButtonUp(e);
        }

        # endregion

        # endregion

        # region FromPage

        public static ToolWindow FromPage(Page page)
        {
            ToolWindow window = new ToolWindow();
            Frame frame = new Frame();
            frame.Content = page;
            window.Content = frame;
            window.Header = page.Title;
            window.Width = page.Width;
            window.Height = page.Height;
            return window;
        }

        # endregion

        #endregion
    } 

    #endregion

    #region enum ToolWindowState

    public enum ToolWindowState
    {
        Maximized,
        Normal
    }

    #endregion

    #region enum ToolWindowStartPosition

    public enum ToolWindowStartPosition
    {
        CenterParent,
        Manual,
        WindowsDefaultLocation
    }

    #endregion

    #region enum MaximizeBehavior

    public enum MaximizeBehavior
    {
        DefaultToBounds,
        Custom
    }

    #endregion
}
