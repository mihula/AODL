using AODL.Document;
using AODL.Document.Content;
using AODL.Document.SpreadsheetDocuments;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Content.Tables
{
	public class Column : IContent
	{
		private AODL.Document.Content.Tables.Table _table;

		private IDocument _document;

		private IStyle _style;

		private XmlNode _node;

		public AODL.Document.Styles.ColumnStyle ColumnStyle
		{
			get
			{
				return (AODL.Document.Styles.ColumnStyle)this.Style;
			}
			set
			{
				this.StyleName = value.StyleName;
				this.Style = value;
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

		public string ParentCellStyleName
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@table:default-cell-style-name", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@table:default-cell-style-name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("default-cell-style-name", value, "table");
				}
				this._node.SelectSingleNode("@table:default-cell-style-name", this.Document.NamespaceManager).InnerText = value;
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

		public Column(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.Node = node;
		}

		public Column(AODL.Document.Content.Tables.Table table, string styleName)
		{
			this.Table = table;
			this.Document = table.Document;
			this.NewXmlNode(styleName);
			this.ColumnStyle = new AODL.Document.Styles.ColumnStyle(this.Document, styleName);
			this.Document.Styles.Add(this.ColumnStyle);
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
			xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		private void NewXmlNode(string styleName)
		{
			this.Node = this.Document.CreateNode("table-column", "table");
			XmlAttribute xmlAttribute = this.Document.CreateAttribute("style-name", "table");
			xmlAttribute.Value=(styleName);
			this.Node.Attributes.Append(xmlAttribute);
			if (this.Document is SpreadsheetDocument)
			{
				this.ParentCellStyleName = "Default";
			}
		}
	}
}