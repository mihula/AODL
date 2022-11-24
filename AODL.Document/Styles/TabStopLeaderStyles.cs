using System;

namespace AODL.Document.Styles
{
	public class TabStopLeaderStyles
	{
		public readonly static string Dotted;

		public readonly static string Solid;

		static TabStopLeaderStyles()
		{
			TabStopLeaderStyles.Dotted = "dotted";
			TabStopLeaderStyles.Solid = "solid";
		}

		public TabStopLeaderStyles()
		{
		}
	}
}