using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Content;
using AODL.Document.Styles;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Content.Text
{
	public class Footnote : IText, IHtml, ITextContainer
	{
		private XmlNode _node;

		private IDocument _document;

		private ITextCollection _textContent;

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

		public string Id
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("//@text:note-citation", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("//@text:note-citation", this.Document.NamespaceManager) != null)
				{
					this._node.SelectSingleNode("//@text:note-citation", this.Document.NamespaceManager).InnerText = value;
					this._node.SelectSingleNode("//@text:id", this.Document.NamespaceManager).InnerText=(string.Concat("ftn", value));
				}
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
				return this.Node.InnerText;
			}
			set
			{
				this.Node.InnerText = value;
			}
		}

		public ITextCollection TextContent
		{
			get
			{
				return this._textContent;
			}
			set
			{
				if (this._textContent != null)
				{
					foreach (IText text in this._textContent)
					{
						this.Node.RemoveChild(text.Node);
					}
				}
				this._textContent = value;
				if (this._textContent != null)
				{
					foreach (IText text1 in this._textContent)
					{
						this.Node.AppendChild(text1.Node);
					}
				}
			}
		}

		public Footnote(IDocument document, string notetext, string id, FootnoteType type)
		{
			this.Document = document;
			this.NewXmlNode(id, notetext, type);
			this.InitStandards();
		}

		public Footnote(IDocument document)
		{
			this.Document = document;
			this.InitStandards();
		}

		public string GetHtml()
		{
			string str = string.Concat("<sup>(", this.Id);
			str = string.Concat(str, ". ", this.Text);
			return string.Concat(str, ")</sup>");
		}

		private void InitStandards()
		{
			this.TextContent = new ITextCollection();
			this.TextContent.Inserted += new CollectionWithEvents.CollectionChange(this.TextContent_Inserted);
			this.TextContent.Removed += new CollectionWithEvents.CollectionChange(this.TextContent_Removed);
		}

		private void NewXmlNode(string id, string notetext, FootnoteType type)
		{
			this.Node = this.Document.CreateNode("note", "text");
			XmlAttribute xmlAttribute = this.Document.CreateAttribute("id", "text");
			xmlAttribute.Value=(string.Concat("ftn", id));
			this.Node.Attributes.Append(xmlAttribute);
			xmlAttribute = this.Document.CreateAttribute("note-class", "text");
			xmlAttribute.Value=(type.ToString());
			this.Node.Attributes.Append(xmlAttribute);
			XmlNode xmlNode = this.Document.CreateNode("not-citation", "text");
			xmlNode.InnerText=(id);
			this._node.AppendChild(xmlNode);
			XmlNode xmlNode1 = this.Document.CreateNode("note-body", "text");
			xmlNode = this.Document.CreateNode("p", "text");
			xmlNode.InnerXml=(notetext);
			xmlAttribute = this.Document.CreateAttribute("style-name", "text");
			xmlAttribute.Value=((type == FootnoteType.footnode ? "Footnote" : "Endnote"));
			xmlNode.Attributes.Append(xmlAttribute);
			xmlNode1.AppendChild(xmlNode);
			this._node.AppendChild(xmlNode1);
		}

		private void TextContent_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IText)value).Node);
		}

		private void TextContent_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IText)value).Node);
		}
	}
}