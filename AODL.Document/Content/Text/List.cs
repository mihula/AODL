using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Content;
using AODL.Document.Styles;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Content.Text
{
	public class List : IContent, IContentContainer, IHtml
	{
		private AODL.Document.Styles.ParagraphStyle _paragraphstyle;

		private IContentCollection _content;

		private ListStyles _type;

		private IDocument _document;

		private IStyle _style;

		private XmlNode _node;

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

		public AODL.Document.Styles.ListStyle ListStyle
		{
			get
			{
				return (AODL.Document.Styles.ListStyle)this.Style;
			}
			set
			{
				this.Style = value;
			}
		}

		public ListStyles ListType
		{
			get
			{
				return this._type;
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

		public AODL.Document.Styles.ParagraphStyle ParagraphStyle
		{
			get
			{
				return this._paragraphstyle;
			}
			set
			{
				this._paragraphstyle = value;
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
				XmlNode xmlNode = this._node.SelectSingleNode("@text:style-name", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@text:style-name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("style-name", value, "text");
				}
				this._node.SelectSingleNode("@text:style-name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public List(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.Node = node;
			this.InitStandards();
		}

		public List(IDocument document, string styleName, ListStyles typ, string paragraphStyleName)
		{
			this.Document = document;
			this.NewXmlNode();
			this.InitStandards();
			this.Style = new AODL.Document.Styles.ListStyle(this.Document, styleName);
			this.ParagraphStyle = new AODL.Document.Styles.ParagraphStyle(this.Document, paragraphStyleName);
			this.Document.Styles.Add(this.Style);
			this.Document.Styles.Add(this.ParagraphStyle);
			this.ParagraphStyle.ListStyleName = styleName;
			this._type = typ;
			((AODL.Document.Styles.ListStyle)this.Style).AutomaticAddListLevelStyles(typ);
		}

		public List(IDocument document, List outerlist)
		{
			this.Document = document;
			this.ParagraphStyle = outerlist.ParagraphStyle;
			this.InitStandards();
			this._type = outerlist.ListType;
			this.NewXmlNode();
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
			string str = null;
			if (this.ListType == ListStyles.Bullet)
			{
				str = "<ul>\n";
			}
			else if (this.ListType == ListStyles.Number)
			{
				str = "<ol>\n";
			}
			foreach (IContent content in this.Content)
			{
				if (!(content is IHtml))
				{
					continue;
				}
				str = string.Concat(str, ((IHtml)content).GetHtml());
			}
			if (this.ListType == ListStyles.Bullet)
			{
				str = string.Concat(str, "</ul>\n");
			}
			else if (this.ListType == ListStyles.Number)
			{
				str = string.Concat(str, "</ol>\n");
			}
			return str;
		}

		private void InitStandards()
		{
			this.Content = new IContentCollection();
			this.Content.Inserted += new CollectionWithEvents.CollectionChange(this.Content_Inserted);
			this.Content.Removed += new CollectionWithEvents.CollectionChange(this.Content_Removed);
		}

		private void NewXmlNode()
		{
			this.Node = this.Document.CreateNode("list", "text");
		}
	}
}