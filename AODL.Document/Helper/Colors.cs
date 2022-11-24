using System;
using System.Drawing;

namespace AODL.Document.Helper
{
	public class Colors
	{
		public Colors()
		{
		}

		public static string GetColor(Color color)
		{
			int argb = color.ToArgb();
			string str = string.Concat("#", argb.ToString("x").Substring(2));
			return str;
		}
	}
}