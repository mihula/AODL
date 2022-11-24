using System;

namespace AODL.Document.Styles
{
	public class Border
	{
		public readonly static string NormalSolid;

		public readonly static string MiddleSolid;

		public readonly static string HeavySolid;

		static Border()
		{
			Border.NormalSolid = "0.002cm solid #000000";
			Border.MiddleSolid = "0.004cm solid #000000";
			Border.HeavySolid = "0.008cm solid #000000";
		}

		public Border()
		{
		}
	}
}