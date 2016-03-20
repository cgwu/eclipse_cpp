# region Using Directives

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Collections;
using Assergs.Windows.Media;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

# endregion

namespace Assergs.Windows.Tests
{
	public partial class ColorationWindow : ToolWindow
	{
        #region Declare

        DragCompletedEventHandler _DragCompletedEventHandler;
        Slider[] _Sliders; 

        #endregion

		# region Constructor

		public ColorationWindow()
		{
			this.SuspendLayout();

			this.InitializeComponent();

			this.Startup();
                        
			this.ResumeLayout();

            #region Manage Sliders

            this._DragCompletedEventHandler = new DragCompletedEventHandler(this.SlidersThumb_DragCompleted);

            this._Sliders = new Slider[4];
            this._Sliders[0] = this._HueSlider;
            this._Sliders[1] = this._HueConstraintSlider;
            this._Sliders[2] = this._SaturationSlider;
            this._Sliders[3] = this._BrightSlider;

            foreach (Slider slider in _Sliders)
            {
                slider.Style = this.GetSliderStyle();
                slider.Loaded += new RoutedEventHandler(Sliders_Loaded);
            } 

            #endregion
		}

		# endregion

		# region Startup

		private void Startup()
		{
			//ColorType hsl = ColorType.HSL;

			//this._PalletValues = new[] {
			//    this.GetResetedPallet(OfficeColorPallet.Background),
			//    this.GetResetedPallet(OfficeColorPallet.Foreground),
			//    this.GetResetedPallet(OfficeColorPallet.HighLight)
			//    };

			#region Linq

            //this._PalletCombo.ItemsSource =
            //        from
            //            p in OfficeColors.Pallets
            //        select
            //            p.Type.Name; 

	        #endregion

            foreach(OfficePallet op in OfficeColors.Pallets)
            {
                this._PalletCombo.Items.Add(op.Type.Name);
            }

			this._PalletCombo.SelectedIndex = 0;

			this.ArrangeSliders();
		}

		# endregion

		# region Hue

		private float Hue
		{
			get
			{
				return (float)this._HueSlider.Value;
			}
			set
			{
				this._HueSlider.Value = value;
			}
		}

		# endregion

		# region HueConstraint

		private float HueConstraint
		{
			get
			{
				return (float)this._HueConstraintSlider.Value;
			}
			set
			{
				this._HueConstraintSlider.Value = value;
			}
		}

		# endregion

		# region Saturation

		private float Saturation
		{
			get
			{
				return (float)this._SaturationSlider.Value;
			}
			set
			{
				this._SaturationSlider.Value = value;
			}
		}

		# endregion

		# region Brightness

		private float Brightness
		{
			get
			{
				return (float)this._BrightSlider.Value;
			}
			set
			{
				this._BrightSlider.Value = value;
			}
		}

		# endregion

		# region ActualPallet

		private OfficePallet ActualPallet
		{
			get
			{
				return OfficeColors.Pallets[this._PalletCombo.SelectedIndex];
			}
		}

		# endregion

		# region IsLayoutSuspended

		private bool _IsLayoutSuspended;

		private bool IsLayoutSuspended
		{
			get
			{
				return this._IsLayoutSuspended;
			}
			set
			{
				this._IsLayoutSuspended = value;
			}
		}

		# endregion

		# region SuspendLayout

		private void SuspendLayout()
		{
			this.IsLayoutSuspended = true;
		}

		# endregion

		# region ResumeLayout

		private void ResumeLayout()
		{
			this.IsLayoutSuspended = false;
		}

		# endregion

		# region _PalletCombo_SelectionChanged

		private void _PalletCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!this.IsLayoutSuspended)
			{
				this.SuspendLayout();
				this.ArrangeSliders();
				this.ResumeLayout();
			}
		}

		# endregion

		# region _CollorTypeCombo_SelectionChanged

		private void _CollorTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!this.IsLayoutSuspended)
			{
				this.ActualPallet.ColorType = (ColorType)this._ColorTypeCombo.SelectedIndex;

				this.ArrangeColors();
			}
		}

		# endregion

		# region _ResetButton_Click

		void _ResetButton_Click(object sender, RoutedEventArgs e)
		{
			this.ActualPallet.Reset();
			this.SuspendLayout();
			this.ArrangeSliders();
			this.ArrangeColors();
			this.ResumeLayout();
		}

		# endregion

		# region Sliders_Changed

        //private bool SliderIsMouseDown = false;

		void Sliders_Changed(object sender, RoutedEventArgs e)
		{
            if (!this.IsLayoutSuspended)
            {
                this.ActualPallet.Hue = this.Hue;
                this.ActualPallet.HueConstraint = this.HueConstraint;
                this.ActualPallet.Saturation = this.Saturation;
                this.ActualPallet.Brightness = this.Brightness;

                this.ArrangeColors();
            }
		}

		# endregion

        #region Sliders_Loaded

        void Sliders_Loaded(object sender, RoutedEventArgs e)
        {
            Slider slider = sender as Slider;
            Thumb sliderThumb = (Thumb)slider.Template.FindName("HorizontalThumb", slider);
            sliderThumb.Tag = slider;
            sliderThumb.DragCompleted += this._DragCompletedEventHandler;
        } 

        #endregion

        #region SlidersThumb_DragCompleted

        void SlidersThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            Thumb sliderThumb = sender as Thumb;
            Slider slider = sliderThumb.Tag as Slider;
            slider.Style = this.GetSliderStyle();
        } 

        #endregion

		# region ArrangeSliders

		private void ArrangeSliders()
		{
			this.Hue = this.ActualPallet.Hue;
			this.HueConstraint = this.ActualPallet.HueConstraint;
			this.Saturation = this.ActualPallet.Saturation;
			this.Brightness = this.ActualPallet.Brightness;
		}

		# endregion

		# region ArrangeColors

		private void ArrangeColors()
		{
			Application.Current.Resources = new OfficeStyle();

            #region Manage Sliders

            if (this._Sliders != null)
            {
                foreach (Slider slider in this._Sliders)
                {
                    Thumb sliderThumb = (Thumb)slider.Template.FindName("HorizontalThumb", slider);
                    if (sliderThumb != null)
                    {
                        sliderThumb.Tag = slider;
                        if (!sliderThumb.IsDragging)
                        {
                            slider.Style = this.GetSliderStyle();
                        }
                    }
                }
            }

            #endregion
		}

		# endregion

        #region GetSliderStyle

        private Style GetSliderStyle()
        {
            return (Style)Application.Current.FindResource(typeof(Slider));
        } 

        #endregion
	}
}