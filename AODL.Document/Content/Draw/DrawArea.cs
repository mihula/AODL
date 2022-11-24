using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Content;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Content.Draw
{
	public abstract class DrawArea : IContent, IContentContainer
	{
		private IDocument _document;

		private XmlNode _node;

		private IContentCollection _content;

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

		public string Description
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@draw:desc", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@draw:desc", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("desc", value, "draw");
				}
				this._node.SelectSingleNode("@draw:desc", this.Document.NamespaceManager).InnerText = value;
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

		public string Href
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@xlink:href", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@xlink:href", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("href", value, "xlink");
				}
				this._node.SelectSingleNode("@xlink:href", this.Document.NamespaceManager).InnerText = value;
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

		public string XLinkType
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@xlink:type", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@xlink:type", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("type", value, "xlink");
				}
				this._node.SelectSingleNode("@xlink:type", this.Document.NamespaceManager).InnerText = value;
			}
		}

		protected DrawArea(IDocument document)
		{
			this.Document = document;
			this.InitStandards();
			this.NewXmlNode();
		}

		protected void Content_Inserted(int index, object value)
		{
			if (this.Node != null)
			{
				this.Node.AppendChild(((IContent)value).Node);
			}
		}

		protected void Content_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IContent)value).Node);
		}

		protected void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
			xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		private void InitStandards()
		{
			this.Content = new IContentCollection();
			this.Content.Inserted += new CollectionWithEvents.CollectionChange(this.Content_Inserted);
			this.Content.Removed += new CollectionWithEvents.CollectionChange(this.Content_Removed);
		}

		protected abstract void NewXmlNode();
	}
}