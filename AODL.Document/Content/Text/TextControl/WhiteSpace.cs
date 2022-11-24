using AODL.Document;
using AODL.Document.Content.Text;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Content.Text.TextControl
{
	public class WhiteSpace : IText
	{
		private XmlNode _node;

		private IDocument _document;

		public string Count
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@text:c", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@text:c", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("c", value, "text");
				}
				this._node.SelectSingleNode("@text:c", this.Document.NamespaceManager).InnerText = value;
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
				return null;
			}
			set
			{
			}
		}

		public WhiteSpace(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.Node = node;
		}

		public WhiteSpace(IDocument document, int whiteSpacesCount)
		{
			this.Document = document;
			this.NewXmlNode();
			this.Count = whiteSpacesCount.ToString();
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
			xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		private void NewXmlNode()
		{
			this.Node = this.Document.CreateNode("s", "text");
		}
	}
}