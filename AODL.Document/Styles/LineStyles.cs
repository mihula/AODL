using System;

namespace AODL.Document.Styles
{
	public class LineStyles
	{
		public readonly static string longdash;

		public readonly static string dotdash;

		public readonly static string dotdotdash;

		public readonly static string none;

		public readonly static string solid;

		public readonly static string dotted;

		public readonly static string dash;

		public readonly static string wave;

		static LineStyles()
		{
			LineStyles.longdash = "long-dash";
			LineStyles.dotdash = "dot-dash";
			LineStyles.dotdotdash = "dot-dot-dash";
			LineStyles.none = "none";
			LineStyles.solid = "solid";
			LineStyles.dotted = "dotted";
			LineStyles.dash = "dash";
			LineStyles.wave = "wave";
		}

		public LineStyles()
		{
		}
	}
}