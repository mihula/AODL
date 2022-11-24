using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Content;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Content.Draw
{
	public class DrawTextBox : IContent, IContentContainer
	{
		private IDocument _document;

		private IStyle _style;

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

		public string CornerRadius
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@draw:corner-radius", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@draw:corner-radius", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("corner-radius", value, "draw");
				}
				this._node.SelectSingleNode("@draw:corner-radius", this.Document.NamespaceManager).InnerText = value;
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

		public string Chain
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@draw:chain-next-name", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@draw:chain-next-name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("chain-next-name", value, "draw");
				}
				this._node.SelectSingleNode("@draw:chain-next-name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string MaxHeight
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:max-height", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:max-height", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("max-height", value, "fo");
				}
				this._node.SelectSingleNode("@fo:max-height", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string MaxWidth
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:max-width", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:max-width", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("max-width", value, "fo");
				}
				this._node.SelectSingleNode("@fo:max-width", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string MinHeight
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:min-height", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:min-height", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("min-height", value, "fo");
				}
				this._node.SelectSingleNode("@fo:min-height", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string MinWidth
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:min-width", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:min-width", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("min-width", value, "fo");
				}
				this._node.SelectSingleNode("@fo:min-width", this.Document.NamespaceManager).InnerText = value;
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
				XmlNode xmlNode = this._node.SelectSingleNode("@draw:style-name", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@draw:style-name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("style-name", value, "draw");
				}
				this._node.SelectSingleNode("@draw:style-name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public DrawTextBox(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.InitStandards();
			this.Node = node;
		}

		public DrawTextBox(IDocument document)
		{
			this.Document = document;
			this.InitStandards();
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

		private void InitStandards()
		{
			this.Content = new IContentCollection();
			this.Content.Inserted += new CollectionWithEvents.CollectionChange(this.Content_Inserted);
			this.Content.Removed += new CollectionWithEvents.CollectionChange(this.Content_Removed);
		}

		private void NewXmlNode()
		{
			this.Node = this.Document.CreateNode("text-box", "draw");
		}
	}
}