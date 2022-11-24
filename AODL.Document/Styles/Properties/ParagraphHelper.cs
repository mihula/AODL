using System;

namespace AODL.Document.Styles.Properties
{
	public class ParagraphHelper
	{
		public readonly static string LineSpacing15;

		public readonly static string LineDouble;

		public readonly static string LineSpacing3;

		static ParagraphHelper()
		{
			ParagraphHelper.LineSpacing15 = "150%";
			ParagraphHelper.LineDouble = "200%";
			ParagraphHelper.LineSpacing3 = "300%";
		}

		public ParagraphHelper()
		{
		}
	}
}