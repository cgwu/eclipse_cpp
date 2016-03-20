#region Using

using System;
using Assergs.Windows;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Collections.Generic;
using System.Reflection;

#endregion

namespace Assergs.Windows.Tests
{
    #region class ColorChooser

    public partial class ColorChooser : ToolWindow
    {
        List<ColorInfo> colors;

		#region class ColorInfo

		public class ColorInfo
		{
			#region Constructor

			public ColorInfo(string name, Color color, string palletName)
				: this(name, color, double.MaxValue, palletName)
			{ }

			public ColorInfo(string name, Color color, double distance, string palletName)
			{
				this._Name = name;
				this._Color = color;
				this._Distance = distance;
				this._PalletName = palletName;
			}

			#endregion

			#region Properties

			#region Name

			private string _Name;
			public string Name
			{
				get { return this._Name; }
			}

			#endregion

			#region Color

			private Color _Color;
			public Color Color
			{
				get { return this._Color; }
			}

			#endregion

			#region Distance

			private double _Distance = Double.MaxValue;
			public double Distance
			{
				get
				{
					return this._Distance;
				}
				set
				{
					this._Distance = value;
				}
			}

			#endregion

			#region PalletName

			private string _PalletName;
			public string PalletName
			{
				get
				{
					return this._PalletName;
				}
				set
				{
					this._PalletName = value;
				}
			}

			#endregion

			#endregion

			#region ToString

			public override string ToString()
			{
				return String.Format("{0}.{1} = {2}", this._PalletName, this.Name, this.Color);
			}

			#endregion
		}

		#endregion

        #region Constructor

        public ColorChooser()
        {
            InitializeComponent();
            colors = new List<ColorInfo>();

            this.FillColors();

            colorsList.SelectionChanged += new SelectionChangedEventHandler(colorsList_SelectionChanged);
        }

        void colorsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (colorsList.SelectedItems.Count > 0)
            {
                txtSearch.Text = ((ColorInfo)((FrameworkElement)colorsList.SelectedItem).Tag).Color.ToString().Replace("#FF", String.Empty);
            }
        }

        #endregion

        #region FillColors

        private void FillColors()
        {
            this.colors.Clear();
            Type officeColors = typeof(OfficeColors);
            object auxObj = new object();
            Color auxColor = new Color();

            #region Fill Colors

            int oldSelectedIndex = cboPallets.SelectedIndex;

            cboPallets.Items.Clear();
            cboPallets.Items.Add("All");


            MemberInfo[] palletMembers = officeColors.FindMembers(MemberTypes.NestedType, BindingFlags.Public | System.Reflection.BindingFlags.Static, null, null);
            foreach (MemberInfo mi in palletMembers)
            {
                cboPallets.Items.Add(mi.Name);

                Type pallet = mi as Type;
                FieldInfo[] colorFields = pallet.GetFields(BindingFlags.Static | BindingFlags.Public);

                foreach (FieldInfo fi in colorFields)
                {
                    auxColor = (Color)fi.GetValue(auxObj);
                    colors.Add(new ColorInfo(fi.Name, auxColor, pallet.Name));
                }
            }

            if (oldSelectedIndex == -1)
            {
                cboPallets.SelectedIndex = 0;
            }
            else
            {
                if (cboPallets.Items.Count > oldSelectedIndex)
                {
                    cboPallets.SelectedIndex = oldSelectedIndex;
                }
            }

            #endregion
        }

        #endregion

        #region btnSearch_Click

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.searchedColor = (Color)ColorConverter.ConvertFromString(String.Format("#{0}", txtSearch.Text.PadRight(6, '0')));

                foreach (ColorInfo ci in this.colors)
                {
                    ci.Distance = this.CalculateEuclideanDistanceBetweenColors(ci.Color, this.searchedColor);
                }
            }
            catch
            {
                this.searchedColor = Colors.Black;
            }

            this.FillList();
        }

        #endregion

        #region sliderDistanceAllowed_DragLeave

        void sliderDistanceAllowed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.colors != null)
                this.FillList();
        }

        #endregion

        #region ColorInfoComparer

        private int ColorInfoComparerByDistance(ColorInfo x, ColorInfo y)
        {
            if (x.Distance == y.Distance)
            {
                return 0;
            }
            else if (x.Distance > y.Distance)
            {
                return 1;
            }
            else if (x.Distance < y.Distance)
            {
                return -1;
            }

            return 0;
        }

        #endregion

        #region FillList

        private void FillList()
        {
            bool hasColorSearch = (txtSearch.Text.Trim() != String.Empty);

            if (hasColorSearch)
            {
                this.colors.Sort(ColorInfoComparerByDistance);
                this.sliderDistanceAllowed.IsEnabled = true;
            }
            else
            {
                this.FillColors();
                this.sliderDistanceAllowed.IsEnabled = false;
            }

            if (colors == null) return;

            colorsList.Items.Clear();
            foreach (ColorInfo colorInfo in this.colors)
            {
                if (hasColorSearch && (colorInfo.Distance > sliderDistanceAllowed.Value))
                {
                    continue;
                }
                else if (cboPallets.SelectedIndex != 0)
                {
                    if (colorInfo.PalletName != cboPallets.SelectedItem.ToString())
                    {
                        continue;
                    }
                }

                Border itemColor = new Border();
                if (hasColorSearch)
                {
                    itemColor = new Border();
                    itemColor.Width = itemColor.Height = 40;
                    itemColor.Background = new SolidColorBrush(this.searchedColor);
                    itemColor.Margin = new Thickness(1);
                }

                Border sampleColor = new Border();
                sampleColor.Width = sampleColor.Height = 40;
                sampleColor.Background = new SolidColorBrush(colorInfo.Color);
                sampleColor.Margin = new Thickness(1);

                TextBlock colorName = new TextBlock();
                if (hasColorSearch)
                {
                    colorName.Text = String.Format("{0} - {1}", colorInfo.Distance.ToString("N2"), colorInfo.ToString());
                }
                else
                {
                    colorName.Text = colorInfo.ToString();
                }


                colorName.FontSize = 14;
                colorName.Margin = new Thickness(1);
                colorName.VerticalAlignment = VerticalAlignment.Center;

                DockPanel pnl = new DockPanel();
                if (hasColorSearch) pnl.Children.Add(itemColor);
                pnl.Children.Add(sampleColor);
                pnl.Children.Add(colorName);
                pnl.Tag = colorInfo;

                colorsList.Items.Add(pnl);
            }
        }

        #endregion

        #region CalculateEuclideanDistanceBetweenColors

        private double CalculateEuclideanDistanceBetweenColors(Color color1, Color color2)
        {
            double rDiference = Math.Pow((double)color1.R - (double)color2.R, 2);
            double gDiference = Math.Pow((double)color1.G - (double)color2.G, 2);
            double bDiference = Math.Pow((double)color1.B - (double)color2.B, 2);

            return Math.Sqrt(rDiference + gDiference + bDiference);
        }

        #endregion

        #region searchedColor

        public Color searchedColor
        {
            get { return (Color)GetValue(searchedColorProperty); }
            set { SetValue(searchedColorProperty, value); }
        }

        public static readonly DependencyProperty searchedColorProperty =
            DependencyProperty.Register("searchedColor", typeof(Color), typeof(ColorChooser), new FrameworkPropertyMetadata(Color.FromRgb(0, 0, 0)));

        #endregion
    } 

    #endregion
}