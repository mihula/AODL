using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Content;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Content.Text
{
	public class FormatedText : IHtml, IText, ITextContainer, ICloneable
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

		public AODL.Document.Styles.TextStyle TextStyle
		{
			get
			{
				return (AODL.Document.Styles.TextStyle)this.Style;
			}
			set
			{
				this.Style = value;
			}
		}

		public FormatedText(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.Node = node;
			this.InitStandards();
		}

		public FormatedText(IDocument document, string name, string text)
		{
			this.Document = document;
			this.NewXmlNode(name);
			this.InitStandards();
			this.Text = text;
			this.Style = new AODL.Document.Styles.TextStyle(this.Document, name);
			this.Document.Styles.Add(this.Style);
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

		public string GetHtml()
		{
			string htmlStyle = ((AODL.Document.Styles.TextStyle)this.Style).TextProperties.GetHtmlStyle();
			string str = "<span ";
			string textWithHtmlControl = this.GetTextWithHtmlControl();
			str = (htmlStyle.Length <= 0 ? string.Concat(str, ">\n") : string.Concat(str, htmlStyle, ">\n"));
			if (textWithHtmlControl.Length > 0)
			{
				str = string.Concat(str, textWithHtmlControl);
			}
			str = string.Concat(str, "</span>\n");
			str = string.Concat(this.GetSubOrSupStartTag(), str, this.GetSubOrSupEndTag());
			return str;
		}

		private string GetSubOrSupEndTag()
		{
			string str;
			if (((AODL.Document.Styles.TextStyle)this.Style).TextProperties.Position == null || ((AODL.Document.Styles.TextStyle)this.Style).TextProperties.Position.Length <= 0)
			{
				str = "";
			}
			else
			{
				str = (!((AODL.Document.Styles.TextStyle)this.Style).TextProperties.Position.ToLower().StartsWith("sub") ? "</sup>" : "</sub>");
			}
			return str;
		}

		private string GetSubOrSupStartTag()
		{
			string str;
			if (((AODL.Document.Styles.TextStyle)this.Style).TextProperties.Position == null || ((AODL.Document.Styles.TextStyle)this.Style).TextProperties.Position.Length <= 0)
			{
				str = "";
			}
			else
			{
				str = (!((AODL.Document.Styles.TextStyle)this.Style).TextProperties.Position.ToLower().StartsWith("sub") ? "<sup>" : "<sub>");
			}
			return str;
		}

		private string GetTextWithHtmlControl()
		{
			string str = "";
			foreach (XmlNode node in this.Node)
			{
				if (node.LocalName == "tab")
				{
					str = string.Concat(str, "&nbsp;&nbsp;&nbsp;");
				}
				else if (node.LocalName != "line-break")
				{
					if (node.InnerText.Length <= 0)
					{
						continue;
					}
					str = string.Concat(str, node.InnerText);
				}
				else
				{
					str = string.Concat(str, "<br>");
				}
			}
			return str;
		}

		private void InitStandards()
		{
			this.TextContent = new ITextCollection();
			this.TextContent.Inserted += new CollectionWithEvents.CollectionChange(this.TextContent_Inserted);
			this.TextContent.Removed += new CollectionWithEvents.CollectionChange(this.TextContent_Removed);
		}

		private void NewXmlNode(string stylename)
		{
			this.Node = this.Document.CreateNode("span", "text");
			XmlAttribute xmlAttribute = this.Document.CreateAttribute("style-name", "text");
			xmlAttribute.Value=(stylename);
			this.Node.Attributes.Append(xmlAttribute);
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