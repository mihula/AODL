using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Content;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.Styles;
using AODL.Document.TextDocuments;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Content.Text
{
	public class XLink : IText, IHtml, ITextContainer, ICloneable
	{
		private XmlNode _node;

		private IDocument _document;

		private IStyle _style;

		private string _styleName;

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

		public string OfficeName
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@office:name", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@office:name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("name", value, "office");
				}
				this._node.SelectSingleNode("@office:name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string Show
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@xlink:show", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@xlink:show", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("show", value, "xlink");
				}
				this._node.SelectSingleNode("@xlink:show", this.Document.NamespaceManager).InnerText = value;
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
				this._style = value;
			}
		}

		public string StyleName
		{
			get
			{
				return this._styleName;
			}
			set
			{
				this._styleName = value;
			}
		}

		public string TargetFrameName
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@office:target-frame-name", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@office:target-frame-name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("target-frame-name", value, "office");
				}
				this._node.SelectSingleNode("@office:target-frame-name", this.Document.NamespaceManager).InnerText = value;
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

		public XLink(IDocument document, string href, string name)
		{
			this.Document = document;
			this.NewXmlNode();
			this.InitStandards();
			this.Href = href;
			this.TextContent.Add(new SimpleText(this.Document, name));
		}

		public XLink(IDocument document)
		{
			this.Document = document;
			this.InitStandards();
		}

		public object Clone()
		{
			XLink xLink = null;
			if (this.Document != null && this.Node != null)
			{
				TextContentProcessor textContentProcessor = new TextContentProcessor();
				xLink = textContentProcessor.CreateXLink(this.Document, this.Node.CloneNode(true));
			}
			return xLink;
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
			xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		public string GetHtml()
		{
			string str = "<a href=\"";
			if (this.Href != null)
			{
				str = string.Concat(str, this.GetLink(), "\"");
			}
			if (this.TargetFrameName != null)
			{
				str = string.Concat(str, " target=\"", this.TargetFrameName, "\"");
			}
			if (this.Href == null)
			{
				str = "";
			}
			else
			{
				str = string.Concat(str, ">\n");
				str = string.Concat(str, this.Text);
				str = string.Concat(str, "</a>");
			}
			return str;
		}

		private string GetLink()
		{
			string href = this.Href;
			return this.Href;
		}

		private void InitStandards()
		{
			this.TextContent = new ITextCollection();
			this.TextContent.Inserted += new CollectionWithEvents.CollectionChange(this.TextContent_Inserted);
			this.TextContent.Removed += new CollectionWithEvents.CollectionChange(this.TextContent_Removed);
		}

		private void NewXmlNode()
		{
			this.Node = this.Document.CreateNode("a", "text");
		}

		private void TextContent_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IText)value).Node);
			if (((IText)value).Text != null)
			{
				try
				{
					if (this.Document is TextDocument)
					{
						string text = ((IText)value).Text;
						DocumentMetadata documentMetadata = this.Document.DocumentMetadata;
						documentMetadata.CharacterCount = documentMetadata.CharacterCount + text.Length;
						string[] strArray = text.Split(new char[] { ' ' });
						DocumentMetadata wordCount = this.Document.DocumentMetadata;
						wordCount.WordCount = wordCount.WordCount + (int)strArray.Length;
					}
				}
				catch (Exception exception)
				{
				}
			}
		}

		private void TextContent_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IText)value).Node);
		}
	}
}