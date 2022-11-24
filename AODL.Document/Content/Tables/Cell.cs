using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Content;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Content.Tables
{
	public class Cell : IContent, IContentContainer, IHtml
	{
		private AODL.Document.Content.Tables.Table _table;

		private AODL.Document.Content.Tables.Row _row;

		private IDocument _document;

		private IStyle _style;

		private XmlNode _node;

		private IContentCollection _content;

		public AODL.Document.Styles.CellStyle CellStyle
		{
			get
			{
				return (AODL.Document.Styles.CellStyle)this.Style;
			}
			set
			{
				this.StyleName = value.StyleName;
				this.Style = value;
			}
		}

		public string ColumnRepeating
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@table:number-columns-spanned", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@table:number-columns-spanned", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("number-columns-spanned", value, "table");
				}
				this._node.SelectSingleNode("@table:number-columns-spanned", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public IContentCollection Content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
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

		public string Formula
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@table:formula", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@table:formula", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("formula", value, "table");
				}
				this._node.SelectSingleNode("@table:formula", this.Document.NamespaceManager).InnerText = value;
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

		public string OfficeValue
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@office:value", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@office:value", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("value", value, "office");
				}
				this._node.SelectSingleNode("@office:value", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string OfficeValueType
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@office:value-type", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@office:value-type", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("value-type", value, "office");
				}
				this._node.SelectSingleNode("@office:value-type", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public AODL.Document.Content.Tables.Row Row
		{
			get
			{
				return this._row;
			}
			set
			{
				this._row = value;
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

		public Cell(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.Node = node;
			this.InitStandards();
		}

		public Cell(AODL.Document.Content.Tables.Table table)
		{
			this.Table = table;
			this.Document = table.Document;
			this.NewXmlNode(null);
			this.InitStandards();
		}

		public Cell(AODL.Document.Content.Tables.Table table, string styleName, string officeValueTyp)
		{
			this.Table = table;
			this.Document = table.Document;
			this.InitStandards();
			this.NewXmlNode(styleName);
			this.StyleName = styleName;
			this.CellStyle = new AODL.Document.Styles.CellStyle(this.Document, styleName);
			this.Document.Styles.Add(this.CellStyle);
			if (officeValueTyp != null)
			{
				this.OfficeValue = officeValueTyp;
			}
		}

		public Cell(AODL.Document.Content.Tables.Table table, string styleName)
		{
			this.Table = table;
			this.Document = table.Document;
			this.NewXmlNode(null);
			this.InitStandards();
			if (styleName != null)
			{
				this.StyleName = styleName;
				this.CellStyle = new AODL.Document.Styles.CellStyle(this.Document, styleName);
				this.Document.Styles.Add(this.CellStyle);
			}
		}

		private void Content_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IContent)value).Node);
		}

		private void Content_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IContent)value).Node);
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
			xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		public string GetHtml()
		{
			string str = "<td ";
			if (this.ColumnRepeating != null)
			{
				str = string.Concat(str, "colspan=", this.ColumnRepeating, " ");
			}
			if (((AODL.Document.Styles.CellStyle)this.Style).CellProperties != null)
			{
				str = string.Concat(str, ((AODL.Document.Styles.CellStyle)this.Style).CellProperties.GetHtmlStyle());
			}
			string htmlWidth = this.GetHtmlWidth();
			if (htmlWidth != null)
			{
				str = (str.IndexOf("style=") != -1 ? string.Concat(str.Substring(0, str.Length - 1), htmlWidth, "\"") : string.Concat(str, "style=\"", htmlWidth, "\""));
			}
			str = string.Concat(str, ">\n");
			foreach (IContent content in this.Content)
			{
				if (!(content is IHtml))
				{
					continue;
				}
				str = string.Concat(str, ((IHtml)content).GetHtml());
			}
			if (this.Content != null && this.Content.Count == 0)
			{
				str = string.Concat(str, "&nbsp;");
			}
			str = string.Concat(str, "\n</td>\n");
			return str;
		}

		private string GetHtmlWidth()
		{
			string str;
			try
			{
				int num = 0;
				foreach (Cell cellCollection in this.Row.CellCollection)
				{
					if ((object)cellCollection == (object)this && this.Row.Table.ColumnCollection != null && num <= this.Row.Table.ColumnCollection.Count)
					{
						Column item = this.Row.Table.ColumnCollection[num];
						if (item != null && item.ColumnStyle.ColumnProperties.Width != null)
						{
							str = string.Concat(" width: ", item.ColumnStyle.ColumnProperties.Width.Replace(",", "."), "; ");
							return str;
						}
					}
					num++;
				}
			}
			catch (Exception exception)
			{
			}
			str = "";
			return str;
		}

		private void InitStandards()
		{
			this.Content = new IContentCollection();
			this.Content.Inserted += new CollectionWithEvents.CollectionChange(this.Content_Inserted);
			this.Content.Removed += new CollectionWithEvents.CollectionChange(this.Content_Removed);
		}

		private void NewXmlNode(string styleName)
		{
			this.Node = this.Document.CreateNode("table-cell", "table");
		}
	}
}