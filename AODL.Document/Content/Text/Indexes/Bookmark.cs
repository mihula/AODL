using AODL.Document;
using AODL.Document.Content.Text;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Content.Text.Indexes
{
	public class Bookmark : IText, ICloneable
	{
		private XmlNode _node;

		private IDocument _document;

		public string BookmarkName
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@text:name", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@text:name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("name", value, "text");
				}
				this._node.SelectSingleNode("@text:name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public AODL.Document.Content.Text.Indexes.BookmarkType BookmarkType
		{
			get
			{
				AODL.Document.Content.Text.Indexes.BookmarkType bookmarkType;
				if (this.Node.Name != "text:bookmark-start")
				{
					bookmarkType = (this.Node.Name != "text:bookmark-end" ? AODL.Document.Content.Text.Indexes.BookmarkType.Standard : AODL.Document.Content.Text.Indexes.BookmarkType.End);
				}
				else
				{
					bookmarkType = AODL.Document.Content.Text.Indexes.BookmarkType.Start;
				}
				return bookmarkType;
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

		public IStyle Style
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string StyleName
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string Text
		{
			get
			{
				return this.BookmarkName;
			}
			set
			{
				this.BookmarkName = value;
			}
		}

		public Bookmark(IDocument document, AODL.Document.Content.Text.Indexes.BookmarkType type, string bookmarkname)
		{
			this.Document = document;
			this.NewXmlNode(type, bookmarkname);
		}

		public object Clone()
		{
			Bookmark bookmark = null;
			if (this.Document != null && this.Node != null)
			{
				TextContentProcessor textContentProcessor = new TextContentProcessor();
				bookmark = textContentProcessor.CreateBookmark(this.Document, this.Node.CloneNode(true), this.BookmarkType);
			}
			return bookmark;
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
            xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		private void NewXmlNode(AODL.Document.Content.Text.Indexes.BookmarkType type, string bookmarkname)
		{
			if (type == AODL.Document.Content.Text.Indexes.BookmarkType.Start)
			{
				this.Node = this.Document.CreateNode("bookmark-start", "text");
			}
			else if (type != AODL.Document.Content.Text.Indexes.BookmarkType.End)
			{
				this.Node = this.Document.CreateNode("bookmark", "text");
			}
			else
			{
				this.Node = this.Document.CreateNode("bookmark-end", "text");
			}
			XmlAttribute xmlAttribute = this.Document.CreateAttribute("name", "text");
			xmlAttribute.Value=(bookmarkname);
			this.Node.Attributes.Append(xmlAttribute);
		}
	}
}