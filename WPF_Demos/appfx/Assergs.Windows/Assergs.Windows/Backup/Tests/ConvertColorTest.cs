# region Using Directives

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media;
using Assergs.Windows.Media;
using System.Reflection;
using Tests.EasyRGB;

# endregion

namespace Tests
{
	[TestClass]
	public class ConvertColorTest
	{
		# region Initializations

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		# endregion

		# region X11Test

		/// <summary>
		/// http://en.wikipedia.org/wiki/X11_color_names 
		/// </summary>
		[TestMethod]
		public void X11Test()
		{
			var colors =
			    from
			        p in typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static)
					//c in new Color[] { Colors.Crimson }
			    select
			        new
			        {
			            p.Name,
						Value = (Color)p.GetValue(null, null)
						//Name = "Crimson",
			            //Value = c
			        };

			foreach (var color in colors)
			{
				// RGB To HSL
				var expectedHSL = EasyRGBConvertColor.RGBToHSL(color.Value);
				var actualHSL = ConvertColor.RBGToHSL(color.Value);

				Assert.IsTrue(
					((actualHSL.Hue.HasValue && expectedHSL.Hue.ToString("G4") == actualHSL.Hue.Value.ToString("G4")) || (!actualHSL.Hue.HasValue && expectedHSL.Hue == 0F)) &&
					expectedHSL.Saturation.ToString("G4") == actualHSL.Saturation.ToString("G4") &&
					expectedHSL.Lightness.ToString("G4") == actualHSL.X.ToString("G4"),
					"\r\nRGB To HSL\r\n" +
					"----------\r\n" +
					"Color: {0} {1}\r\n" +
					"Expected: {2}\r\n" +
					"Actual: {3}"
					, color.Name, color.Value, expectedHSL, actualHSL);

				// HSL To RGB
				var expectedRGB = EasyRGBConvertColor.HSLToRGB(expectedHSL);
				var actualRGB = ConvertColor.HSLToRGB(actualHSL);

				//Assert.AreEqual<Color>(expectedRGB, actualRGB,
				Assert.IsTrue(expectedRGB == actualRGB &&
					expectedRGB.R == color.Value.R &&
					expectedRGB.G == color.Value.G &&
					expectedRGB.B == color.Value.B,
					"\r\nHSL To RGB\r\n" +
					"----------\r\n" +
					"Color: {0} {1}\r\n" +
					"Expected: {2}\r\n" +
					"Actual: {3}"
					, color.Name, color.Value, expectedRGB, actualRGB);				

				// RGB To HSV
				var expectedHSV = EasyRGBConvertColor.RGBToHSV(color.Value);
				var actualHSV = ConvertColor.RBGToHSV(color.Value);

				Assert.IsTrue(
					((actualHSV.Hue.HasValue && expectedHSV.Hue.ToString("G4") == actualHSV.Hue.Value.ToString("G4")) || (!actualHSV.Hue.HasValue && expectedHSV.Hue == 0F)) &&
					expectedHSV.Saturation.ToString("G4") == actualHSV.Saturation.ToString("G4") &&
					expectedHSV.Value.ToString("G4") == actualHSV.X.ToString("G4"),
					"\r\nRGB To HSV\r\n" +
					"----------\r\n" +
					"Color: {0} {1}\r\n" +
					"Expected: {2}\r\n" +
					"Actual: {3}"
					, color.Name, color.Value, expectedHSV, actualHSV);

				// HSV To RGB
				expectedRGB = EasyRGBConvertColor.HSVToRGB(expectedHSV);
				actualRGB = ConvertColor.HSVToRGB(actualHSV);

				//Assert.AreEqual<Color>(expectedRGB, actualRGB,
				Assert.IsTrue(expectedRGB == actualRGB &&
					expectedRGB.R == color.Value.R &&
					expectedRGB.G == color.Value.G &&
					expectedRGB.B == color.Value.B,
					"\r\nHSV To RGB\r\n" +
					"----------\r\n" +
					"Color: {0} {1}\r\n" +
					"Expected: {2}\r\n" +
					"Actual: {3}"
					, color.Name, color.Value, expectedRGB, actualRGB);
			}
		}

		# endregion

		# region // PalletTest

		/*
		# region ColorPallet

		struct ColorPallet
		{
			public string Name;
			public Color Color;
			public HSXColor HSLColor;
			public HSXColor HSVColor;

			# region Constructor

			public ColorPallet(string name, Color color, HSXColor hslColor, HSXColor hsvColor)
			{
				this.Name = name;
				this.Color = color;
				this.HSLColor = hslColor;
				this.HSVColor = hsvColor;
			}

			# endregion
		}

		# endregion

		[TestMethod]
		public void PalletTest()
		{
			// http://en.wikipedia.org/wiki/Color_list

			var pallet = new ColorPallet[] {
				new ColorPallet("AliceBlue", Colors.AliceBlue, new HSXColor(), new HSXColor(208 / 360, 0.06F, 1F)),
				new ColorPallet("Alizarin Crimson", Color.FromRgb(240, 248, 245), new HSXColor(), new HSXColor(355 / 360, 0.83F, 0.89F))
				};

			foreach (var colorPallet in pallet)
			{
				var hsl = ConvertColor.RBGToHSL(colorPallet.Color);
				Assert.IsTrue(
					colorPallet.HSLColor.Hue.Value.ToString("P0") == hsl.Hue.Value.ToString("P0") &&
					colorPallet.HSLColor.Saturation.ToString("P0") == hsl.Saturation.ToString("P0") &&
					colorPallet.HSLColor.X.ToString("P0") == hsl.X.ToString("P0"),
					"RGB To HSL {0}.", colorPallet.Name);

				var hsv = ConvertColor.RBGToHSV(colorPallet.Color);
				Assert.IsTrue(
					colorPallet.HSVColor.Hue.Value.ToString("P0") == hsv.Hue.Value.ToString("P0") &&
					colorPallet.HSVColor.Saturation.ToString("P0") == hsv.Saturation.ToString("P0") &&
					colorPallet.HSVColor.X.ToString("P0") == hsv.X.ToString("P0"),
					"RGB To HSV {0}.", colorPallet.Name);

				var rgb = ConvertColor.HSLToRGB(colorPallet.HSLColor);
				Assert.AreEqual<Color>(rgb, colorPallet.Color, "HSL To RGB {0}.", colorPallet.Name);
				
				rgb = ConvertColor.HSVToRGB(colorPallet.HSVColor);
				Assert.AreEqual<Color>(rgb, colorPallet.Color, "HSV To RGB {0}.", colorPallet.Name);
			}
		}
		*/

		# endregion
	}
}
