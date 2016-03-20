using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Tests.EasyRGB
{
	struct HSLColor
	{
		public float Saturation;
		public float Lightness;

		public HSLColor(float hue, float saturation, float lightness)
		{
			this._Hue = 0F;
			this.Saturation = saturation;
			this.Lightness = lightness;

			this.Hue = hue;
		}

		# region Hue

		private float _Hue;

		public float Hue
		{
			get
			{
				return this._Hue;
			}
			set
			{
				if (this._Hue != value)
				{
					if (value < 0)
					{
						this._Hue = value % 1F + 1;
					}
					else
					{
						this._Hue = value % 1F;
					}
				}
			}
		}

		# endregion

		public override string ToString()
		{
			return string.Format("Hue: {0:P}, Saturation: {1:P}, Lightness: {2:P}",
				this.Hue, this.Saturation, this.Lightness);
		}
	}

	struct HSVColor
	{
		public float Saturation;
		public float Value;

		public HSVColor(float hue, float saturation, float value)
		{
			this._Hue = 0F;
			this.Saturation = saturation;
			this.Value = value;

			this.Hue = hue;
		}

		# region Hue

		private float _Hue;

		public float Hue
		{
			get
			{
				return this._Hue;
			}
			set
			{
				if (this._Hue != value)
				{
					if (value < 0)
					{
						this._Hue = value % 1F + 1;
					}
					else
					{
						this._Hue = value % 1F;
					}
				}
			}
		}

		# endregion

		public override string ToString()
		{
			return string.Format("Hue: {0:P}, Saturation: {1:P}, Value: {2:P}",
				this.Hue, this.Saturation, this.Value);
		}
	}

	/// <summary>
	/// http://www.easyrgb.com/math.php
	/// </summary>
	class EasyRGBConvertColor
	{		
		# region RGBToHSL

		public static HSLColor RGBToHSL(Color rgb)
		{
			return RGBToHSL(rgb.R, rgb.G, rgb.B);
		}
		public static HSLColor RGBToHSL(byte r, byte g, byte b)
		{
			return RGBToHSL(r / 255F, g / 255F, b / 255F);
		}
		public static HSLColor RGBToHSL(float r, float g, float b)
		{
			var min = Math.Min(r, Math.Min(g, b));
			var max = Math.Max(r, Math.Max(g, b));
			var delta = max - min;

			float h = 0F;
			float s = 0F;
			var l = (max + min) / 2F;

			if (delta != 0)
			{
				if (l < 0.5F)
				{
					s = delta / (max + min);
				}
				else
				{
					s = delta / (2F - max - min);
				}

				var del_R = (((max - r) / 6F) + (delta / 2F)) / delta;
				var del_G = (((max - g) / 6F) + (delta / 2F)) / delta;
				var del_B = (((max - b) / 6F) + (delta / 2F)) / delta;

				if (r == max)
				{
					h = del_B - del_G;
				}
				else if (g == max)
				{
					h = (1F / 3F) + del_R - del_B;
				}
				else if (b == max)
				{
					h = (2F / 3F) + del_G - del_R;
				}

				if (h < 0F) h += 1F;
				if (h > 1F) h -= 1F;
			}

			return new HSLColor(h, s, l);
		}

		# endregion

		# region HSLToRGB

		public static Color HSLToRGB(HSLColor hsl)
		{
			return HSLToRGB(hsl.Hue, hsl.Saturation, hsl.Lightness);
		}
		public static Color HSLToRGB(float h, float s, float l)
		{
			float r, g, b;

			if (s == 0F)
			{
				r = g = b = l;
			}
			else
			{
				float var_2;
				if (l < 0.5F)
				{
					var_2 = l * (1F + s);
				}
				else
				{
					var_2 = (l + s) - (s * l);
				}

				var var_1 = 2F * l - var_2;

				r = Hue_2_RGB(var_1, var_2, h + (1F / 3F));
				g = Hue_2_RGB(var_1, var_2, h);
				b = Hue_2_RGB(var_1, var_2, h - (1F / 3F));
			}

			return Color.FromRgb(Convert.ToByte(r * 255F), Convert.ToByte(g * 255F), Convert.ToByte(b * 255F));
		}
		private static float Hue_2_RGB(float v1, float v2, float vH)
		{
			if (vH < 0F) vH += 1F;
			if (vH > 1F) vH -= 1F;
			if ((6F * vH) < 1F) return (v1 + (v2 - v1) * 6F * vH);
			if ((2F * vH) < 1F) return (v2);
			if ((3F * vH) < 2F) return (v1 + (v2 - v1) * ((2F / 3F) - vH) * 6F);
			return (v1);
		}

		# endregion

		# region RGBToHSV

		public static HSVColor RGBToHSV(Color rgb)
		{
			return RGBToHSV(rgb.R, rgb.G, rgb.B);
		}
		public static HSVColor RGBToHSV(byte r, byte g, byte b)
		{
			return RGBToHSV(r / 255F, g / 255F, b / 255F);
		}
		public static HSVColor RGBToHSV(float r, float g, float b)
		{
			var min = Math.Min(r, Math.Min(g, b));
			var max = Math.Max(r, Math.Max(g, b));
			var delta = max - min;

			float h = 0F;
			float s = 0F;
			var v = max;

			if (delta != 0)
			{
				s = delta / max;

				var del_R = (((max - r) / 6F) + (delta / 2F)) / delta;
				var del_G = (((max - g) / 6F) + (delta / 2F)) / delta;
				var del_B = (((max - b) / 6F) + (delta / 2F)) / delta;

				if (r == max)
				{
					h = del_B - del_G;
				}
				else if (g == max)
				{
					h = (1F / 3F) + del_R - del_B;
				}
				else if (b == max)
				{
					h = (2F / 3F) + del_G - del_R;
				}

				if (h < 0F) h += 1F;
				if (h > 1F) h -= 1F;
			}

			return new HSVColor(h, s, v);
		}

		# endregion

		# region HSVToRGB

		public static Color HSVToRGB(HSVColor hsv)
		{
			return HSVToRGB(hsv.Hue, hsv.Saturation, hsv.Value);
		}
		public static Color HSVToRGB(float h, float s, float v)
		{
			float r, g, b;

			if (s == 0F)
			{
				r = g = b = v;
			}
			else
			{
				var var_h = h * 6F;

				if (var_h == 6F)
				{
					var_h = 0F;
				}

				var var_i = (int)var_h;

				var var_1 = v * (1F - s);
				var var_2 = v * (1F - s * (var_h - var_i));
				var var_3 = v * (1F - s * (1F - (var_h - var_i)));

				if (var_i == 0)
				{
					r = v;
					g = var_3;
					b = var_1;
				}
				else if (var_i == 1)
				{
					r = var_2;
					g = v;
					b = var_1;
				}
				else if (var_i == 2)
				{
					r = var_1;
					g = v;
					b = var_3;
				}
				else if (var_i == 3)
				{
					r = var_1;
					g = var_2;
					b = v;
				}
				else if (var_i == 4)
				{
					r = var_3;
					g = var_1;
					b = v;
				}
				else
				{
					r = v;
					g = var_1;
					b = var_2;
				}
			}

			return Color.FromRgb(Convert.ToByte(r * 255F), Convert.ToByte(g * 255F), Convert.ToByte(b * 255F));
		}

		# endregion
	}
}
