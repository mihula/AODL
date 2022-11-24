using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Content;
using AODL.Document.SpreadsheetDocuments;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.Document.TextDocuments;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Xml;

namespace AODL.Document.Content.Tables
{
	public class Row : IContent, IHtml
	{
		private AODL.Document.Content.Tables.Table _table;

		private AODL.Document.Content.Tables.CellCollection _cellCollection;

		private AODL.Document.Content.Tables.CellSpanCollection _cellSpanCollection;

		private IDocument _document;

		private IStyle _style;

		private XmlNode _node;

		public AODL.Document.Content.Tables.CellCollection CellCollection
		{
			get
			{
				return this._cellCollection;
			}
			set
			{
				this._cellCollection = value;
			}
		}

		public AODL.Document.Content.Tables.CellSpanCollection CellSpanCollection
		{
			get
			{
				return this._cellSpanCollection;
			}
			set
			{
				this._cellSpanCollection = value;
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

		public AODL.Document.Styles.RowStyle RowStyle
		{
			get
			{
				return (AODL.Document.Styles.RowStyle)this.Style;
			}
			set
			{
				this.StyleName = value.StyleName;
				this.Style = value;
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

		public AODL.Document.Content.Tables.Table Table
		{
			get
			{
				return this._table;
			}
			set
			{
				this._table = value;
			}
		}

		public Row(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.Node = node;
			this.InitStandards();
		}

		public Row(AODL.Document.Content.Tables.Table table)
		{
			this.Table = table;
			this.Document = table.Document;
			this.NewXmlNode(null);
			this.InitStandards();
		}

		public Row(AODL.Document.Content.Tables.Table table, string styleName)
		{
			this.Table = table;
			this.Document = table.Document;
			this.NewXmlNode(styleName);
			this.RowStyle = new AODL.Document.Styles.RowStyle(this.Document, styleName);
			this.Document.Styles.Add(this.RowStyle);
			this.InitStandards();
		}

		private void CellCollection_Inserted(int index, object value)
		{
			if (!(this.Document is SpreadsheetDocument) || this.Document.IsLoadedFile)
			{
				this.Node.AppendChild(((Cell)value).Node);
			}
			else
			{
				if (this.Node.ChildNodes.Count != index)
				{
					XmlNode itemOf = this.Node.ChildNodes[index];
					this.Node.InsertAfter(((Cell)value).Node, itemOf);
				}
				else
				{
					this.Node.AppendChild(((Cell)value).Node);
				}
				if (Row.OnRowChanged != null)
				{
					Row.OnRowChanged(this.GetRowIndex(), this.CellCollection.Count);
				}
			}
		}

		private void CellCollection_Removed(int index, object value)
		{
			this.Node.RemoveChild(((Cell)value).Node);
		}

		private void CellSpanCollection_Inserted(int index, object value)
		{
			this.Node.AppendChild(((CellSpan)value).Node);
		}

		private void CellSpanCollection_Removed(int index, object value)
		{
			this.Node.RemoveChild(((CellSpan)value).Node);
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
			xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		public int GetCellIndex(Cell cell)
		{
			int num;
			if (cell != null && this.CellCollection != null)
			{
				int num1 = 0;
				while (num1 < this.CellCollection.Count)
				{
					if (!this.CellCollection[num1].Equals(cell))
					{
						num1++;
					}
					else
					{
						num = num1;
						return num;
					}
				}
			}
			num = -1;
			return num;
		}

		public string GetHtml()
		{
			string str = "<tr ";
			if (((AODL.Document.Styles.RowStyle)this.Style).RowProperties != null)
			{
				str = string.Concat(str, ((AODL.Document.Styles.RowStyle)this.Style).RowProperties.GetHtmlStyle());
			}
			str = string.Concat(str, ">\n");
			foreach (Cell cellCollection in this.CellCollection)
			{
				if (cellCollection == null)
				{
					continue;
				}
				str = string.Concat(str, cellCollection.GetHtml(), "\n");
			}
			str = string.Concat(str, "</tr>");
			return str;
		}

		private int GetRowIndex()
		{
			int count;
			int num = 0;
			while (true)
			{
				if (num >= this.Table.RowCollection.Count)
				{
					count = this.Table.RowCollection.Count;
					break;
				}
				else if ((object)this.Table.RowCollection[num] != (object)this)
				{
					num++;
				}
				else
				{
					count = num;
					break;
				}
			}
			return count;
		}

		private void InitStandards()
		{
			this.CellCollection = new AODL.Document.Content.Tables.CellCollection();
			this.CellCollection.Removed += new CollectionWithEvents.CollectionChange(this.CellCollection_Removed);
			this.CellCollection.Inserted += new CollectionWithEvents.CollectionChange(this.CellCollection_Inserted);
			this.CellSpanCollection = new AODL.Document.Content.Tables.CellSpanCollection();
			this.CellSpanCollection.Inserted += new CollectionWithEvents.CollectionChange(this.CellSpanCollection_Inserted);
			this.CellSpanCollection.Removed += new CollectionWithEvents.CollectionChange(this.CellSpanCollection_Removed);
		}

		public void InsertCellAt(int position, Cell cell)
		{
			if (this.CellCollection.Count <= position)
			{
				for (int i = 0; i < position - this.CellCollection.Count; i++)
				{
					this.CellCollection.Add(new Cell(this.Table));
				}
				this.CellCollection.Add(cell);
			}
			else if (this.CellCollection.Count + 1 != position)
			{
				this.CellCollection.Insert(position, cell);
			}
			else
			{
				this.CellCollection.Add(cell);
			}
		}

		public void MergeCells(TextDocument document, int cellStartIndex, int mergeCells, bool mergeContent)
		{
			try
			{
				this.CellCollection[cellStartIndex].ColumnRepeating = mergeCells.ToString();
				if (mergeContent)
				{
					for (int i = cellStartIndex + 1; i < cellStartIndex + mergeCells; i++)
					{
						foreach (IContent content in this.CellCollection[i].Content)
						{
							this.CellCollection[cellStartIndex].Content.Add(content);
						}
					}
				}
				for (int j = cellStartIndex + mergeCells - 1; j > cellStartIndex; j--)
				{
					this.CellCollection.RemoveAt(j);
					this.CellSpanCollection.Add(new CellSpan(this, (TextDocument)this.Document));
				}
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		private void NewXmlNode(string styleName)
		{
			this.Node = this.Document.CreateNode("table-row", "table");
			if (styleName != null)
			{
				XmlAttribute xmlAttribute = this.Document.CreateAttribute("style-name", "table");
				xmlAttribute.Value=(styleName);
				this.Node.Attributes.Append(xmlAttribute);
			}
		}

		public static event Row.RowChanged OnRowChanged;

		public delegate void RowChanged(int rowNumber, int cellCount);
	}
}