using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Content;
using AODL.Document.Styles;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;

namespace AODL.Document.Content.Tables
{
	public class RowHeader : IContent, IHtml
	{
		private AODL.Document.Content.Tables.Table _table;

		private AODL.Document.Content.Tables.RowCollection _rowCollection;

		private IDocument _document;

		private IStyle _style;

		private XmlNode _node;

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

		public RowHeader(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.Node = node;
			this.InitStandards();
		}

		public RowHeader(AODL.Document.Content.Tables.Table table)
		{
			this.Table = table;
			this.Document = table.Document;
			this.InitStandards();
			this.NewXmlNode();
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
			xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		public string GetHtml()
		{
			string str = "";
			foreach (Row rowCollection in this.RowCollection)
			{
				str = string.Concat(str, rowCollection.GetHtml(), "\n");
			}
			return this.HtmlCleaner(str);
		}

		private string HtmlCleaner(string text)
		{
			try
			{
				string str = "margin-top: \\d\\.\\d\\d\\w\\w;";
				string str1 = "margin-bottom: \\d\\.\\d\\d\\w\\w;";
				text = (new Regex(str, RegexOptions.IgnoreCase)).Replace(text, "");
				text = (new Regex(str1, RegexOptions.IgnoreCase)).Replace(text, "");
			}
			catch (Exception exception)
			{
			}
			return text;
		}

		private void InitStandards()
		{
			this.RowCollection = new AODL.Document.Content.Tables.RowCollection();
			this.RowCollection.Inserted += new CollectionWithEvents.CollectionChange(this.RowCollection_Inserted);
			this.RowCollection.Removed += new CollectionWithEvents.CollectionChange(this.RowCollection_Removed);
		}

		private void NewXmlNode()
		{
			this.Node = this.Document.CreateNode("table-header-rows", "table");
		}

		private void RowCollection_Inserted(int index, object value)
		{
			this.Node.AppendChild(((Row)value).Node);
		}

		private void RowCollection_Removed(int index, object value)
		{
			this.Node.RemoveChild(((Row)value).Node);
		}
	}
}