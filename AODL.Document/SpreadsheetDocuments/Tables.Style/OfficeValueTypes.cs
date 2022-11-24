using System;

namespace AODL.Document.SpreadsheetDocuments.Tables.Style
{
	public class OfficeValueTypes
	{
		public static string Float;

		public static string String;

		static OfficeValueTypes()
		{
			OfficeValueTypes.Float = "float";
			OfficeValueTypes.String = "string";
		}

		public OfficeValueTypes()
		{
		}
	}
}