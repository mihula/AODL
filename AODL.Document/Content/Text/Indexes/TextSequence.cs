using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Content.Text;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.Styles;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Content.Text.Indexes
{
	public class TextSequence : IText, ICloneable
	{
		private XmlNode _node;

		private IDocument _document;

		private IStyle _style;

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

		public string Formula
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@text:formula", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@text:formula", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("formula", value, "text");
				}
				this._node.SelectSingleNode("@text:formula", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string Name
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

		public string NumFormat
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:num-format", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:num-format", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("num-format", value, "style");
				}
				this._node.SelectSingleNode("@style:num-format", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string RefName
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@text:ref-name", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@text:ref-name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("ref-name", value, "text");
				}
				this._node.SelectSingleNode("@text:ref-name", this.Document.NamespaceManager).InnerText = value;
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

		public TextSequence()
		{
		}

		public TextSequence(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.Node = node;
			this.InitStandards();
		}

		public TextSequence(IDocument document)
		{
			this.Document = document;
			this.NewXmlNode();
			this.InitStandards();
		}

		public object Clone()
		{
			FormatedText formatedText = null;
			if (this.Document != null && this.Node != null)
			{
				TextContentProcessor textContentProcessor = new TextContentProcessor();
				formatedText = textContentProcessor.CreateFormatedText(this.Document, this.Node.CloneNode(true));
			}
			return formatedText;
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
            xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		private void InitStandards()
		{
			this.TextContent = new ITextCollection();
			this.TextContent.Inserted += new CollectionWithEvents.CollectionChange(this.TextContent_Inserted);
			this.TextContent.Removed += new CollectionWithEvents.CollectionChange(this.TextContent_Removed);
		}

		private void NewXmlNode()
		{
			this.Node = this.Document.CreateNode("sequence", "text");
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