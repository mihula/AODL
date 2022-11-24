using System;

namespace AODL.Document.Styles
{
	public class TabStopTypes
	{
		public readonly static string Right;

		public readonly static string Center;

		static TabStopTypes()
		{
			TabStopTypes.Right = "right";
			TabStopTypes.Center = "center";
		}

		public TabStopTypes()
		{
		}
	}
}