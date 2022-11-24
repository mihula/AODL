using System;

namespace AODL.Document.Styles
{
	public class FamiliyStyles
	{
		public readonly static string Table;

		public readonly static string TableColumn;

		public readonly static string TableCell;

		public readonly static string TableRow;

		public readonly static string Paragraph;

		public readonly static string Text;

		public readonly static string Graphic;

		static FamiliyStyles()
		{
			FamiliyStyles.Table = "table";
			FamiliyStyles.TableColumn = "table-column";
			FamiliyStyles.TableCell = "table-cell";
			FamiliyStyles.TableRow = "table-row";
			FamiliyStyles.Paragraph = "paragraph";
			FamiliyStyles.Text = "text";
			FamiliyStyles.Graphic = "graphic";
		}

		public FamiliyStyles()
		{
		}
	}
}