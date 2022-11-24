using AODL.Document.SpreadsheetDocuments;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.Document.TextDocuments;
using System;

namespace AODL.Document.Content.Tables
{
	public class TableBuilder
	{
		public TableBuilder()
		{
		}

		public static Table CreateSpreadsheetTable(SpreadsheetDocument document, string tableName, string styleName)
		{
			return new Table(document, tableName, styleName);
		}

		public static Table CreateTextDocumentTable(TextDocument document, string tableName, string styleName, int rows, int columns, double width, bool useTableRowHeader, bool useBorder)
		{
			string str = document.DocumentMetadata.TableCount.ToString();
			Table table = new Table(document, tableName, styleName);
			table.TableStyle.TableProperties.Width = string.Concat(width.ToString().Replace(",", "."), "cm");
			for (int i = 0; i < columns; i++)
			{
				Column column = new Column(table, string.Concat("co", str, i.ToString()));
				column.ColumnStyle.ColumnProperties.Width = TableBuilder.GetColumnCellWidth(columns, width);
				table.ColumnCollection.Add(column);
			}
			if (useTableRowHeader)
			{
				rows--;
				RowHeader rowHeader = new RowHeader(table);
				Row row = new Row(table, string.Concat("roh1", str));
				for (int j = 0; j < columns; j++)
				{
					Cell cell = new Cell(table, string.Concat("rohce", str, j.ToString()));
					if (useBorder)
					{
						cell.CellStyle.CellProperties.Border = Border.NormalSolid;
					}
					row.CellCollection.Add(cell);
				}
				rowHeader.RowCollection.Add(row);
				table.RowHeader = rowHeader;
			}
			for (int k = 0; k < rows; k++)
			{
				Row row1 = new Row(table, string.Concat("ro", str, k.ToString()));
				for (int l = 0; l < columns; l++)
				{
					Cell normalSolid = new Cell(table, string.Concat("ce", str, k.ToString(), l.ToString()));
					if (useBorder)
					{
						normalSolid.CellStyle.CellProperties.Border = Border.NormalSolid;
					}
					row1.CellCollection.Add(normalSolid);
				}
				table.RowCollection.Add(row1);
			}
			return table;
		}

		private static string GetColumnCellWidth(int columns, double tableWith)
		{
			double num = (double)(tableWith / (double)columns);
			return num.ToString("F2").Replace(",", ".");
		}
	}
}