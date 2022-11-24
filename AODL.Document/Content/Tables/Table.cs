using AODL.Document;
using AODL.Document.Content;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Content.Tables
{
	public class Table : IContent, IHtml
	{
		private AODL.Document.Content.Tables.RowHeader _rowHeader;

		private AODL.Document.Content.Tables.RowCollection _rowCollection;

		private AODL.Document.Content.Tables.ColumnCollection _columnCollection;

		private IDocument _document;

		private IStyle _style;

		private XmlNode _node;

		public AODL.Document.Content.Tables.ColumnCollection ColumnCollection
		{
			get
			{
				return this._columnCollection;
			}
			set
			{
				this._columnCollection = value;
			}
		}

		public IDocument Document
		{
			get
			{
				return this._document;
			}
			set
			{
				this._document = value;
			}
		}

		public XmlNode Node
		{
			get
			{
				return this._node;
			}
			set
			{
				this._node = value;
			}
		}

		public AODL.Document.Content.Tables.RowCollection RowCollection
		{
			get
			{
				return this._rowCollection;
			}
			set
			{
				this._rowCollection = value;
			}
		}

		public AODL.Document.Content.Tables.RowHeader RowHeader
		{
			get
			{
				return this._rowHeader;
			}
			set
			{
				this._rowHeader = value;
			}
		}

		public IStyle Style
		{
			get
			{
				return this._style;
			}
			set
			{
				this.StyleName = value.StyleName;
				this._style = value;
			}
		}

		public string StyleName
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@table:style-name", this.Document.NamespaceManager);
				if (xmlNode == null)
				{
					innerText = null;
				}
				else
				{
					innerText = xmlNode.InnerText;
				}
				return innerText;
			}
			set
			{
				if (this._node.SelectSingleNode("@table:style-name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("style-name", value, "table");
				}
				this._node.SelectSingleNode("@table:style-name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string TableName
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@table:name", this.Document.NamespaceManager);
				if (xmlNode == null)
				{
					innerText = null;
				}
				else
				{
					innerText = xmlNode.InnerText;
				}
				return innerText;
			}
			set
			{
				if (this._node.SelectSingleNode("@table:name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("name", value, "table");
				}
				this._node.SelectSingleNode("@table:name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public AODL.Document.Styles.TableStyle TableStyle
		{
			get
			{
				return (AODL.Document.Styles.TableStyle)this.Style;
			}
			set
			{
				this.Style = value;
			}
		}

		public Table(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.Node = node;
			this.RowCollection = new AODL.Document.Content.Tables.RowCollection();
			this.ColumnCollection = new AODL.Document.Content.Tables.ColumnCollection();
			Row.OnRowChanged += new Row.RowChanged(this.Row_OnRowChanged);
		}

		public Table(IDocument document, string name, string styleName)
		{
			this.Document = document;
			this.NewXmlNode(name, styleName);
			this.TableStyle = new AODL.Document.Styles.TableStyle(this.Document, styleName);
			this.Document.Styles.Add(this.TableStyle);
			this.RowCollection = new AODL.Document.Content.Tables.RowCollection();
			this.ColumnCollection = new AODL.Document.Content.Tables.ColumnCollection();
			Row.OnRowChanged += new Row.RowChanged(this.Row_OnRowChanged);
		}

		public XmlNode BuildNode()
		{
			foreach (Column columnCollection in this.ColumnCollection)
			{
				this.Node.AppendChild(columnCollection.Node);
			}
			if (this.RowHeader != null)
			{
				this.Node.AppendChild(this.RowHeader.Node);
			}
			foreach (Row rowCollection in this.RowCollection)
			{
				foreach (Cell cellCollection in rowCollection.CellCollection)
				{
					foreach (IContent content in cellCollection.Content)
					{
						if (!(content is Table))
						{
							continue;
						}
						((Table)content).BuildNode();
					}
				}
				this.Node.AppendChild(rowCollection.Node);
			}
			return this.Node;
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
            xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		public Cell CreateCell()
		{
			return new Cell(this);
		}

		public Cell CreateCell(string styleName)
		{
			return new Cell(this, styleName);
		}

		public Cell CreateCell(string styleName, string officeValueType)
		{
			return new Cell(this, styleName, officeValueType);
		}

		public string GetHtml()
		{
			bool flag = false;
			string str = "<table border=0 cellspacing=0 cellpadding=0 border=0 style=\"width: 16.55cm; \">\n\n<tr>\n<td align=right>\n";
			string str1 = "<table hspace=\"14\" vspace=\"14\" cellpadding=\"2\" cellspacing=\"2\" border=\"0\" bgcolor=\"#000000\" ";
			string str2 = "<table cellpadding=\"2\" cellspacing=\"2\" border=\"0\" bgcolor=\"#000000\" ";
			if (this.TableStyle.TableProperties != null)
			{
				if (this.TableStyle.TableProperties.Align != null)
				{
					string lower = this.TableStyle.TableProperties.Align.ToLower();
					if (lower == "right")
					{
						flag = true;
						str1 = str2;
					}
					else if (lower == "margin")
					{
						lower = "left";
					}
					str1 = string.Concat(str1, " align=\"", lower, "\" ");
				}
				str1 = string.Concat(str1, this.TableStyle.TableProperties.GetHtmlStyle());
			}
			str1 = string.Concat(str1, ">\n");
			if (this.RowHeader != null)
			{
				str1 = string.Concat(str1, this.RowHeader.GetHtml());
			}
			foreach (Row rowCollection in this.RowCollection)
			{
				str1 = string.Concat(str1, rowCollection.GetHtml(), "\n");
			}
			str1 = string.Concat(str1, "</table>\n");
			if (flag)
			{
				str1 = string.Concat(str, str1, "\n</td>\n</tr>\n</table>\n");
			}
			return str1;
		}

		public void InsertCellAt(int rowIndex, int columnIndex, Cell cell)
		{
			if (this.RowCollection.Count <= rowIndex)
			{
				for (int i = 0; i < rowIndex - this.RowCollection.Count; i++)
				{
					AODL.Document.Content.Tables.RowCollection rowCollection = this.RowCollection;
					int count = this.RowCollection.Count;
					rowCollection.Add(new Row(this, string.Concat("row", count.ToString(), i.ToString())));
				}
				Row row = new Row(this, this.RowCollection[this.RowCollection.Count - 1].StyleName);
				row.InsertCellAt(columnIndex, cell);
				cell.Row = row;
				this.RowCollection.Add(row);
			}
			else if (this.RowCollection.Count + 1 != rowIndex)
			{
				Row row1 = new Row(this, this.RowCollection[this.RowCollection.Count - 1].StyleName);
				row1.InsertCellAt(columnIndex, cell);
				cell.Row = row1;
				this.RowCollection.Insert(rowIndex, row1);
			}
			else
			{
				Row row2 = new Row(this, this.RowCollection[this.RowCollection.Count - 1].StyleName);
				row2.InsertCellAt(columnIndex, cell);
				cell.Row = row2;
				this.RowCollection.Add(row2);
			}
		}

		private void NewXmlNode(string name, string styleName)
		{
			this.Node = this.Document.CreateNode("table", "table");
			XmlAttribute xmlAttribute = this.Document.CreateAttribute("style-name", "table");
			xmlAttribute.Value=(styleName);
			this.Node.Attributes.Append(xmlAttribute);
			xmlAttribute = this.Document.CreateAttribute("name", "table");
			xmlAttribute.Value=(name);
			this.Node.Attributes.Append(xmlAttribute);
		}

		public void Reset()
		{
			string tableName = this.TableName;
			string styleName = this.StyleName;
			this.Node.RemoveAll();
			this.NewXmlNode(tableName, styleName);
		}

		private void Row_OnRowChanged(int rowNumber, int cellCount)
		{
			if (this.ColumnCollection.Count <= cellCount)
			{
				for (int i = 0; i <= cellCount - this.ColumnCollection.Count; i++)
				{
					AODL.Document.Content.Tables.ColumnCollection columnCollection = this.ColumnCollection;
					int count = this.ColumnCollection.Count;
					columnCollection.Add(new Column(this, string.Concat("col", count.ToString(), i.ToString())));
				}
			}
			for (int j = 0; j < rowNumber; j++)
			{
				if (this.RowCollection[j].CellCollection.Count < cellCount)
				{
					int num = cellCount - this.RowCollection[j].CellCollection.Count;
					while (num < cellCount)
					{
						num++;
					}
					this.RowCollection[j].CellCollection.Add(new Cell(this));
				}
			}
		}
	}
}