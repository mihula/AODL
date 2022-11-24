using System;

namespace AODL.Document.Styles.Properties
{
	public class TextPropertieHelper
	{
		public readonly static string Subscript;

		public readonly static string Superscript;

		public readonly static string Shadowlight;

		public readonly static string Shadowmidlle;

		public readonly static string Shadowheavy;

		static TextPropertieHelper()
		{
			TextPropertieHelper.Subscript = "sub 58%";
			TextPropertieHelper.Superscript = "super 58%";
			TextPropertieHelper.Shadowlight = "1pt 1pt";
			TextPropertieHelper.Shadowmidlle = "3pt 3pt";
			TextPropertieHelper.Shadowheavy = "6pt 6pt";
		}

		public TextPropertieHelper()
		{
		}
	}
}