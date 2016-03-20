# region Using Directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

# endregion

namespace Assergs.Windows.Controls
{
	public class RibbonTabItemChrome : Decorator
	{
		# region Declarations

		private const double _BorderTickness = 1;
		private const double _CornerRadius = 3D;
		private static Size _ArcSize = new Size(_CornerRadius, _CornerRadius);
		private const double _HalfTickness = _BorderTickness / 2D;

		# endregion

		# region Static Constructor

		static RibbonTabItemChrome()
		{
			RibbonTabItemChrome.DefaultStyleKeyProperty.OverrideMetadata(
				typeof(RibbonTabItemChrome), new FrameworkPropertyMetadata(typeof(RibbonTabItemChrome)));

			//RibbonTabItemChrome.BackgroundProperty = Control.BackgroundProperty.AddOwner(
			//    typeof(RibbonTabItemChrome), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
		}

		# endregion

		# region Properties

		# region SelectedBackground

		internal static readonly DependencyProperty SelectedBackgroundProperty =
			DependencyProperty.Register("SelectedBackground", typeof(Brush),
			typeof(RibbonTabItemChrome), new FrameworkPropertyMetadata(null,
			new PropertyChangedCallback(RibbonTabItemChrome.OnSelectedBackgroundChanged)));

		internal Brush SelectedBackground
		{
			get
			{
				return (Brush)this.GetValue(RibbonTabItemChrome.SelectedBackgroundProperty);
			}
			set
			{
				this.SetValue(RibbonTabItemChrome.SelectedBackgroundProperty, value);
			}
		}

		private static void OnSelectedBackgroundChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			RibbonTabItemChrome chrome = (RibbonTabItemChrome)o;
			chrome.InvalidateSelectedDrawing();
		}

		# endregion

		# region KeyboardFocusedBackground

		internal static readonly DependencyProperty KeyboardFocusedBackgroundProperty =
			DependencyProperty.Register("KeyboardFocusedBackground", typeof(Brush),
			typeof(RibbonTabItemChrome), new FrameworkPropertyMetadata(null,
			new PropertyChangedCallback(RibbonTabItemChrome.OnKeyboardFocusedBackgroundChanged)));

		internal Brush KeyboardFocusedBackground
		{
			get
			{
				return (Brush)this.GetValue(RibbonTabItemChrome.KeyboardFocusedBackgroundProperty);
			}
			set
			{
				this.SetValue(RibbonTabItemChrome.KeyboardFocusedBackgroundProperty, value);
			}
		}

		private static void OnKeyboardFocusedBackgroundChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			RibbonTabItemChrome chrome = (RibbonTabItemChrome)o;
			chrome.InvalidateKeyboardFocusedDrawing();
		}

		# endregion

		# region BorderBrush

		public static readonly DependencyProperty BorderBrushProperty = Border.BorderBrushProperty.AddOwner(
			typeof(RibbonTabItemChrome), new FrameworkPropertyMetadata(null,
			FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(RibbonTabItemChrome.OnBorderBrushChanged)));

		public Brush BorderBrush
		{
			get
			{
				return (Brush)base.GetValue(RibbonTabItemChrome.BorderBrushProperty);
			}
			set
			{
				base.SetValue(RibbonTabItemChrome.BorderBrushProperty, value);
			}
		}

		private static void OnBorderBrushChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			RibbonTabItemChrome chrome = (RibbonTabItemChrome)o;
			chrome.InvalidateSelectedDrawing();
		}

		# endregion

		# region MouseOverBackground

		internal static readonly DependencyProperty MouseOverBackgroundProperty =
			DependencyProperty.Register("MouseOverBackground", typeof(Brush),
			typeof(RibbonTabItemChrome), new FrameworkPropertyMetadata(null,
			new PropertyChangedCallback(RibbonTabItemChrome.OnMouseOverBackgroundChanged)));

		internal Brush MouseOverBackground
		{
			get
			{
				return (Brush)base.GetValue(RibbonTabItemChrome.MouseOverBackgroundProperty);
			}
			set
			{
				base.SetValue(RibbonTabItemChrome.MouseOverBackgroundProperty, value);
			}
		}

		private static void OnMouseOverBackgroundChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			RibbonTabItemChrome chrome = (RibbonTabItemChrome)o;
			chrome.InvalidateMouseOverDrawing();
		}

		# endregion

		# region RenderSelected

		public static readonly DependencyProperty RenderSelectedProperty =
			DependencyProperty.Register("RenderSelected", typeof(bool),
			typeof(RibbonTabItemChrome), new FrameworkPropertyMetadata(false,
			new PropertyChangedCallback(RibbonTabItemChrome.OnRenderSelectedChanged)));

		public bool RenderSelected
		{
			get
			{
				return (bool)this.GetValue(RibbonTabItemChrome.RenderSelectedProperty);
			}
			set
			{
				this.SetValue(RibbonTabItemChrome.RenderSelectedProperty, value);
			}
		}

		private static void OnRenderSelectedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			RibbonTabItemChrome chrome = (RibbonTabItemChrome)o;
			chrome.InvalidateVisual();
		}

		# endregion

		# region RenderKeyboardFocused

		public static readonly DependencyProperty RenderKeyboardFocuseddProperty =
			DependencyProperty.Register("RenderKeyboardFocused", typeof(bool),
			typeof(RibbonTabItemChrome), new FrameworkPropertyMetadata(false,
			new PropertyChangedCallback(RibbonTabItemChrome.OnRenderKeyboardFocusedChanged)));

		public bool RenderKeyboardFocused
		{
			get
			{
				return (bool)this.GetValue(RibbonTabItemChrome.RenderKeyboardFocuseddProperty);
			}
			set
			{
				this.SetValue(RibbonTabItemChrome.RenderKeyboardFocuseddProperty, value);
			}
		}

		private static void OnRenderKeyboardFocusedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			RibbonTabItemChrome chrome = (RibbonTabItemChrome)o;
			chrome.InvalidateVisual();
		}

		# endregion

		# region SelectedFigure

		private PathFigure _SelectedFigure;
		private Size _SelectedSize = Size.Empty;

		private PathFigure SelectedFigure
		{
			get
			{
				if (this.RenderSize != this._SelectedSize)
				{
					this._SelectedFigure = new PathFigure(new Point(0, this.ActualHeight), new PathSegment[] {
						new LineSegment(new Point(0, this.ActualHeight - 1 - _HalfTickness), false),
						new LineSegment(new Point(_BorderTickness, this.ActualHeight - 1 - _HalfTickness), true),
						new	ArcSegment(new Point(_BorderTickness + _CornerRadius, this.ActualHeight - _CornerRadius - 1 - _HalfTickness), _ArcSize, 90, false, SweepDirection.Counterclockwise, true),
						new LineSegment(new Point(_BorderTickness + _CornerRadius, _CornerRadius + _HalfTickness), true),
						new ArcSegment(new Point(_BorderTickness + 2 * _CornerRadius, _HalfTickness), _ArcSize, 90, false, SweepDirection.Clockwise, true),
						new LineSegment(new Point(this.ActualWidth - (_BorderTickness + 2 * _CornerRadius), _HalfTickness), true),
						new ArcSegment(new Point(this.ActualWidth - (_BorderTickness + _CornerRadius), _CornerRadius + _HalfTickness), _ArcSize, 90, false, SweepDirection.Clockwise, true),
						new LineSegment(new Point(this.ActualWidth - (_BorderTickness + _CornerRadius), this.ActualHeight - _CornerRadius - 1 - _HalfTickness), true),
						new ArcSegment(new Point(this.ActualWidth - _BorderTickness, this.ActualHeight - 1 - _HalfTickness), _ArcSize, 90, false, SweepDirection.Counterclockwise, true),
						new LineSegment(new Point(this.ActualWidth, this.ActualHeight - 1 - _HalfTickness), true),
						new LineSegment(new Point(this.ActualWidth, this.ActualHeight), false)
						}, false);

					this._SelectedSize = this.RenderSize;
				}

				return this._SelectedFigure;
			}
		}

		# endregion

		# region SelectedDrawing

		private Drawing _SelectedDrawing = null;
		private bool _SelectedDrawingInvalidated = true;

		private void InvalidateSelectedDrawing()
		{
			this._SelectedDrawingInvalidated = true;
			this.InvalidateSelectedMouseOverDrawing();
		}

		private Drawing SelectedDrawing
		{
			get
			{
				if (this.RenderSize != this._SelectedSize || this._SelectedDrawingInvalidated)
				{					
					this._SelectedDrawing = new GeometryDrawing(this.SelectedBackground,
						new Pen(this.BorderBrush, _BorderTickness),
						new PathGeometry(new [] { this.SelectedFigure }));

					this._SelectedDrawingInvalidated = false;
				}

				return this._SelectedDrawing;
			}
		}

		# endregion

		# region KeyboardFocusedDrawing

		private Drawing _KeyboardFocusedDrawing = null;
		private bool _KeyboardFocusedDrawingInvalidated = true;

		private void InvalidateKeyboardFocusedDrawing()
		{
			this._KeyboardFocusedDrawingInvalidated = true;
		}

		private Drawing KeyboardFocusedDrawing
		{
			get
			{
				if (this.RenderSize != this._SelectedSize || this._KeyboardFocusedDrawingInvalidated)
				{					
					this._KeyboardFocusedDrawing = new GeometryDrawing(this.KeyboardFocusedBackground,
						new Pen(new SolidColorBrush(OfficeColors.HighLight.OfficeColor1), _BorderTickness),
						new PathGeometry(new [] { this.SelectedFigure }));

					this._KeyboardFocusedDrawingInvalidated = false;
				}

				return this._KeyboardFocusedDrawing;
			}
		}

		# endregion

		# region SelectedMouseOverDrawing

		private Drawing _SelectedMouseOverDrawing = null;
		private bool _SelectedMouseOverDrawingInvalidated = true;

		private void InvalidateSelectedMouseOverDrawing()
		{
			this._SelectedMouseOverDrawingInvalidated = true;
		}

		private Drawing SelectedMouseOverDrawing
		{
			get
			{
				if (this._SelectedMouseOverDrawingInvalidated)
				{
					DrawingGroup group = new DrawingGroup();

					group.Children.Add(this.SelectedDrawing);

					group.Children.Add(new GeometryDrawing(Brushes.Transparent,
						new Pen(new SolidColorBrush(OfficeColors.HighLight.OfficeColor2), _BorderTickness),
						new PathGeometry(new PathFigure[] { new PathFigure(
							new Point(_BorderTickness + _CornerRadius, this.ActualHeight - _CornerRadius - 1 - _HalfTickness), new PathSegment[] {
							new LineSegment(new Point(_BorderTickness + _CornerRadius, _CornerRadius + _HalfTickness), true),
							new LineSegment(new Point(_BorderTickness + 2 * _CornerRadius, _HalfTickness), false),
							new LineSegment(new Point(this.ActualWidth - (_BorderTickness + 2 * _CornerRadius), _HalfTickness), true),
							new LineSegment(new Point(this.ActualWidth - (_BorderTickness + _CornerRadius), _CornerRadius + _HalfTickness), false),
							new LineSegment(new Point(this.ActualWidth - (_BorderTickness + _CornerRadius), this.ActualHeight - _CornerRadius - 1 - _HalfTickness), true)
						}, false) })));

					group.Children.Add(new GeometryDrawing(Brushes.Transparent,
						new Pen(new SolidColorBrush { Color = OfficeColors.HighLight.OfficeColor2, Opacity = 0.2 }, 4),
						new PathGeometry(new PathFigure [] { new PathFigure(
							new Point(_BorderTickness + _CornerRadius, this.ActualHeight - _CornerRadius - 1 - _HalfTickness), new PathSegment[] {
							new LineSegment(new Point(_BorderTickness + _CornerRadius, _CornerRadius + _HalfTickness), true),
							new LineSegment(new Point(_BorderTickness + 2 * _CornerRadius, _HalfTickness + 1), false),
							new LineSegment(new Point(this.ActualWidth - (_BorderTickness + 2 * _CornerRadius), _HalfTickness + 1), true),
							new LineSegment(new Point(this.ActualWidth - (_BorderTickness + _CornerRadius), _CornerRadius + _HalfTickness), false),
							new LineSegment(new Point(this.ActualWidth - (_BorderTickness + _CornerRadius), _CornerRadius - _CornerRadius - 1 - _HalfTickness), true)
						}, false) })));

					this._SelectedMouseOverDrawing = group;

					this._SelectedMouseOverDrawingInvalidated = false;
				}

				return this._SelectedMouseOverDrawing;
			}
		}

		# endregion
		
		# region MouseOverDrawing

		private Drawing _MouseOverDrawing = null;
		private bool _MouseOverDrawingInvalidated = true;

		private void InvalidateMouseOverDrawing()
		{
			this._MouseOverDrawingInvalidated = true;
		}

		private Drawing MouseOverDrawing
		{
			get
			{
				if (this._MouseOverDrawingInvalidated)
				{					
					PathFigure figure = new PathFigure(new Point(_BorderTickness + _CornerRadius, this.ActualHeight), new PathSegment[] {
						new LineSegment(new Point(_BorderTickness + _CornerRadius, _CornerRadius + _HalfTickness), true),
						new ArcSegment(new Point(_BorderTickness + 2 * _CornerRadius, _HalfTickness), _ArcSize, 90, false, SweepDirection.Clockwise, true),
						new LineSegment(new Point(this.ActualWidth - (_BorderTickness + 2 * _CornerRadius), _HalfTickness), true),
						new ArcSegment(new Point(this.ActualWidth - (_BorderTickness + _CornerRadius), _CornerRadius + _HalfTickness), _ArcSize, 90, false, SweepDirection.Clockwise, true),
						new LineSegment(new Point(this.ActualWidth - (_BorderTickness + _CornerRadius), this.ActualHeight), true),
						}, false);

					this._MouseOverDrawing = new GeometryDrawing(this.MouseOverBackground,
						new Pen(new SolidColorBrush(OfficeColors.Background.OfficeColor26), _BorderTickness),
						new PathGeometry(new PathFigure[] { figure }));

					this._MouseOverDrawingInvalidated = false;
				}

				return this._MouseOverDrawing;
			}
		}

		# endregion

		# endregion

		# region Override Methods

		# region OnMouseEnter

		protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
		{
			base.OnMouseEnter(e);
			this.InvalidateVisual();
		}

		# endregion

		# region OnMouseLeave

		protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
		{
			base.OnMouseLeave(e);
			this.InvalidateVisual();
		}

		# endregion

		# region OnRender

		protected override void OnRender(DrawingContext drawingContext)
		{
			if (this.RenderSelected)
			{
				if (this.RenderKeyboardFocused)
				{
					drawingContext.DrawDrawing(this.KeyboardFocusedDrawing);
				}
				else
				{
					drawingContext.DrawDrawing(this.SelectedDrawing);
				}

				if (!this.RenderKeyboardFocused && this.IsMouseOver)
				{
					drawingContext.DrawDrawing(this.SelectedMouseOverDrawing);
				}
			}
			else if (this.IsMouseOver)
			{
				drawingContext.DrawDrawing(this.MouseOverDrawing);
			}
			else
			{
				// fill transparent to invoke mouseover
				drawingContext.DrawRectangle(Brushes.Transparent, null,
				    new Rect(
					4,
					0,
					Math.Max(0, this.ActualWidth - 8),
					Math.Max(0, this.ActualHeight)));				
			}
		}

		# endregion

		# region MeasureOverride

		protected override Size MeasureOverride(Size availableSize)
		{
			if (this.Child != null)
			{
				Size size2 = new Size();
				bool flag = availableSize.Width < 12;
				bool flag2 = availableSize.Height < 4;
				if (!flag)
				{
					size2.Width = availableSize.Width - 12;
				}
				if (!flag2)
				{
					size2.Height = availableSize.Height - 4;
				}
				this.Child.Measure(size2);
				Size desiredSize = this.Child.DesiredSize;
				if (!flag)
				{
					desiredSize.Width += 12;
				}
				if (!flag2)
				{
					desiredSize.Height += 4;
				}
				return desiredSize;
			}
			return new Size(Math.Min(12, availableSize.Width), Math.Min(4, availableSize.Height));
		}

		# endregion

		# region ArrangeOverride

		protected override Size ArrangeOverride(Size finalSize)
		{
			Rect finalRect = new Rect(
				6,
				3,
				Math.Max((double)0, (double)(finalSize.Width - 12)),
				Math.Max((double)0, (double)(finalSize.Height - 4)));

			if (this.Child != null)
			{
				this.Child.Arrange(finalRect);
			}
			return finalSize;
		}

		# endregion 

		# endregion
	}
}
