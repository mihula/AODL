using System;
using System.Globalization;

namespace AODL.Document.Helper
{
	public class SizeConverter
	{
		public readonly static double pxtocm;

		public readonly static double intocm;

		static SizeConverter()
		{
			SizeConverter.pxtocm = 37.7928;
			SizeConverter.intocm = 2.41;
		}

		public SizeConverter()
		{
		}

		public static double CmToInch(double cm)
		{
			return SizeConverter.intocm * cm;
		}

		public static string CmToInchAsString(double cm)
		{
			int inch = (int)SizeConverter.CmToInch(cm);
			return string.Concat(inch.ToString(), "in");
		}

		public static double CmToPixel(double cm)
		{
			return SizeConverter.pxtocm * cm;
		}

		public static string CmToPixelAsString(double cm)
		{
			int pixel = (int)SizeConverter.CmToPixel(cm);
			return string.Concat(pixel.ToString(), "px");
		}

		public static double GetDoubleFromAnOfficeSizeValue(string aSizeValue)
		{
			double num;
			if (aSizeValue != null)
			{
				try
				{
					if (aSizeValue.EndsWith("cm"))
					{
						aSizeValue = aSizeValue.Replace("cm", "");
						num = Convert.ToDouble(aSizeValue, NumberFormatInfo.InvariantInfo);
					}
					else if (!aSizeValue.EndsWith("in"))
					{
						num = 0;
					}
					else
					{
						aSizeValue = aSizeValue.Replace("in", "");
						num = Convert.ToDouble(aSizeValue, NumberFormatInfo.InvariantInfo);
					}
				}
				catch (Exception exception)
				{
					num = 0;
				}
			}
			else
			{
				num = 0;
			}
			return num;
		}

		public static int GetPixelFromAnOfficeSizeValue(string aSizeValue)
		{
			int pixel;
			if (aSizeValue != null)
			{
				try
				{
					if (aSizeValue.EndsWith("cm"))
					{
						aSizeValue = aSizeValue.Replace("cm", "");
						pixel = (int)SizeConverter.CmToPixel(Convert.ToDouble(aSizeValue, NumberFormatInfo.InvariantInfo));
					}
					else if (!aSizeValue.EndsWith("in"))
					{
						pixel = 0;
					}
					else
					{
						aSizeValue = aSizeValue.Replace("in", "");
						pixel = (int)SizeConverter.InchToPixel(Convert.ToDouble(aSizeValue, NumberFormatInfo.InvariantInfo));
					}
				}
				catch (Exception exception)
				{
					pixel = 0;
				}
			}
			else
			{
				pixel = 0;
			}
			return pixel;
		}

		public static double InchToCm(double inch)
		{
			return inch / SizeConverter.intocm;
		}

		public static string InchToCmAsString(double inch)
		{
			double cm = SizeConverter.InchToCm(inch);
			string str = string.Concat(cm.ToString(NumberFormatInfo.InvariantInfo), "cm");
			return str;
		}

		public static double InchToPixel(double inch)
		{
			return inch * SizeConverter.pxtocm * SizeConverter.intocm;
		}

		public static string InchToPixelAsString(double inch)
		{
			double pixel = SizeConverter.InchToPixel(inch);
			string str = string.Concat(pixel.ToString(NumberFormatInfo.InvariantInfo), "px");
			return str;
		}
	}
}