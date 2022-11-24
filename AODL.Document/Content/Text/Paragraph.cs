using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Content;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.Document.TextDocuments;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Content.Text
{
	public class Paragraph : IContent, IContentContainer, IHtml, ITextContainer, ICloneable
	{
		private ArrayList _mixedContent;

		private ParentStyles _parentStyle;

		private IDocument _document;

		private IStyle _style;

		private XmlNode _node;

		private IContentCollection _content;

		private ITextCollection _textContent;

		public IContentCollection Content
		{
			get
			{
				return this._content;
			}
			set
			{
				if (this._content != null)
				{
					foreach (IContent content in this._content)
					{
						this.Node.RemoveChild(content.Node);
					}
				}
				this._content = value;
				if (this._content != null)
				{
					foreach (IContent content1 in this._content)
					{
						this.Node.AppendChild(content1.Node);
					}
				}
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

		public ArrayList MixedContent
		{
			get
			{
				return this._mixedContent;
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
				return (AODL.Document.Styles.ParagraphStyle)this.Style;
			}
			set
			{
				this.Style = value;
			}
		}

		public ParentStyles ParentStyle
		{
			get
			{
				return this._parentStyle;
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

		public Paragraph(IDocument document)
		{
			this.Document = document;
			this.NewXmlNode();
			this.InitStandards();
		}

		public Paragraph(IDocument document, string styleName)
		{
			this.Document = document;
			this.NewXmlNode();
			this.Init(styleName);
		}

		public Paragraph(IDocument document, ParentStyles style, string simpletext)
		{
			this.Document = document;
			this.NewXmlNode();
			if (style == ParentStyles.Standard)
			{
				this.Init(ParentStyles.Standard.ToString());
			}
			else if (style == ParentStyles.Table)
			{
				this.Init(ParentStyles.Table.ToString());
			}
			else if (style == ParentStyles.Text_20_body)
			{
				this.Init(ParentStyles.Text_20_body.ToString());
			}
			if (simpletext != null)
			{
				this.TextContent.Add(new SimpleText(this.Document, simpletext));
			}
			this._parentStyle = style;
		}

		internal Paragraph(XmlNode node, IDocument document)
		{
			this.Document = document;
			this.Node = node;
			this.InitStandards();
		}

		public object Clone()
		{
			Paragraph paragraph = null;
			if (this.Document != null && this.Node != null)
			{
				MainContentProcessor mainContentProcessor = new MainContentProcessor(this.Document);
				paragraph = mainContentProcessor.CreateParagraph(this.Node.CloneNode(true));
			}
			return paragraph;
		}

		private void Content_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IContent)value).Node);
			this._mixedContent.Add(value);
		}

		private void Content_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IContent)value).Node);
			this.RemoveMixedContent(value);
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
			xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		private string GetContentHtmlContent()
		{
			string str = "";
			foreach (IContent content in this.Content)
			{
				if (!(content is IHtml))
				{
					continue;
				}
				str = string.Concat(str, ((IHtml)content).GetHtml());
			}
			return str;
		}

		private string GetGlobalStyleElement(XmlNode node, string style)
		{
			string innerText;
			try
			{
				XmlNode xmlNode = node.SelectSingleNode(style, this.Document.NamespaceManager);
				if (xmlNode != null && xmlNode.InnerText != null)
				{
					innerText = xmlNode.InnerText;
					return innerText;
				}
			}
			catch (Exception exception)
			{
			}
			innerText = null;
			return innerText;
		}

		public string GetHtml()
		{
			string str;
			string str1 = "<p ";
			string htmlStyle = null;
			bool flag = false;
			bool flag1 = false;
			if (this.Style == null)
			{
				flag1 = true;
			}
			else if (!(((AODL.Document.Styles.ParagraphStyle)this.Style).ParentStyle == "Heading") || this.ParagraphStyle.ParagraphProperties != null || this.ParagraphStyle.TextProperties != null)
			{
				if (this.ParagraphStyle.ParagraphProperties != null)
				{
					str1 = string.Concat(str1, this.ParagraphStyle.ParagraphProperties.GetHtmlStyle());
				}
				if (this.ParagraphStyle.TextProperties != null)
				{
					htmlStyle = this.ParagraphStyle.TextProperties.GetHtmlStyle();
					if (htmlStyle.Length > 0)
					{
						str1 = string.Concat(str1, "<span ", htmlStyle);
						flag = true;
					}
				}
			}
			else
			{
				flag1 = true;
			}
			if (flag1)
			{
				string htmlStyleFromGlobalStyles = this.GetHtmlStyleFromGlobalStyles();
				if (htmlStyleFromGlobalStyles.Length > 0)
				{
					str1 = string.Concat(str1, htmlStyleFromGlobalStyles);
				}
			}
			str1 = string.Concat(str1, ">\n");
			foreach (object obj in this._mixedContent)
			{
				if (!(obj is IHtml))
				{
					continue;
				}
				str1 = string.Concat(str1, ((IHtml)obj).GetHtml());
			}
			str = (!flag ? string.Concat(str1, "&nbsp;</p>\n") : string.Concat(str1, "</span>&nbsp;</p>\n"));
			return str;
		}

		private string GetHtmlStyleFromGlobalStyles()
		{
			string str;
			try
			{
				string str1 = "style=\"";
				if (this.Document is TextDocument)
				{
					XmlNode xmlNode = ((TextDocument)this.Document).DocumentStyles.Styles.SelectSingleNode(string.Concat("//office:styles/style:style[@style:name='", this.StyleName, "']"), this.Document.NamespaceManager) ?? ((TextDocument)this.Document).DocumentStyles.Styles.SelectSingleNode(string.Concat("//office:styles/style:style[@style:name='", ((AODL.Document.Styles.ParagraphStyle)this.Style).ParentStyle, "']"), this.Document.NamespaceManager);
					if (xmlNode != null)
					{
						XmlNode xmlNode1 = xmlNode.SelectSingleNode("style:paragraph-properties", this.Document.NamespaceManager);
						XmlNode xmlNode2 = xmlNode.SelectSingleNode("@style:parent-style-name", this.Document.NamespaceManager);
						XmlNode xmlNode3 = null;
						XmlNode xmlNode4 = null;
						if (xmlNode2 != null && xmlNode2.InnerText != null)
						{
							xmlNode4 = ((TextDocument)this.Document).DocumentStyles.Styles.SelectSingleNode(string.Concat("//office:styles/style:style[@style:name='", xmlNode2.InnerText, "']"), this.Document.NamespaceManager);
							if (xmlNode4 != null)
							{
								xmlNode3 = xmlNode4.SelectSingleNode("style:paragraph-properties", this.Document.NamespaceManager);
							}
						}
						if (xmlNode3 != null)
						{
							string globalStyleElement = this.GetGlobalStyleElement(xmlNode3, "@fo:text-align");
							if (globalStyleElement != null)
							{
								globalStyleElement = globalStyleElement.ToLower().Replace("end", "right");
								if (globalStyleElement.ToLower() == "center" || globalStyleElement.ToLower() == "right")
								{
									str1 = string.Concat(str1, "text-align: ", globalStyleElement, "; ");
								}
							}
							string globalStyleElement1 = this.GetGlobalStyleElement(xmlNode3, "@fo:line-height");
							if (globalStyleElement1 != null)
							{
								str1 = string.Concat(str1, "line-height: ", globalStyleElement1, "; ");
							}
							string globalStyleElement2 = this.GetGlobalStyleElement(xmlNode3, "@fo:margin-top");
							if (globalStyleElement2 != null)
							{
								str1 = string.Concat(str1, "margin-top: ", globalStyleElement2, "; ");
							}
							string str2 = this.GetGlobalStyleElement(xmlNode3, "@fo:margin-bottom");
							if (str2 != null)
							{
								str1 = string.Concat(str1, "margin-bottom: ", str2, "; ");
							}
							string globalStyleElement3 = this.GetGlobalStyleElement(xmlNode3, "@fo:margin-left");
							if (globalStyleElement3 != null)
							{
								str1 = string.Concat(str1, "margin-left: ", globalStyleElement3, "; ");
							}
							string str3 = this.GetGlobalStyleElement(xmlNode3, "@fo:margin-right");
							if (str3 != null)
							{
								str1 = string.Concat(str1, "margin-right: ", str3, "; ");
							}
						}
						if (xmlNode1 != null)
						{
							string globalStyleElement4 = this.GetGlobalStyleElement(xmlNode1, "@fo:text-align");
							if (globalStyleElement4 != null)
							{
								globalStyleElement4 = globalStyleElement4.ToLower().Replace("end", "right");
								if (globalStyleElement4.ToLower() == "center" || globalStyleElement4.ToLower() == "right")
								{
									str1 = string.Concat(str1, "text-align: ", globalStyleElement4, "; ");
								}
							}
							string str4 = this.GetGlobalStyleElement(xmlNode1, "@fo:line-height");
							if (str4 != null)
							{
								str1 = string.Concat(str1, "line-height: ", str4, "; ");
							}
							string globalStyleElement5 = this.GetGlobalStyleElement(xmlNode1, "@fo:margin-top");
							if (globalStyleElement5 != null)
							{
								str1 = string.Concat(str1, "margin-top: ", globalStyleElement5, "; ");
							}
							string str5 = this.GetGlobalStyleElement(xmlNode1, "@fo:margin-bottom");
							if (str5 != null)
							{
								str1 = string.Concat(str1, "margin-bottom: ", str5, "; ");
							}
							string globalStyleElement6 = this.GetGlobalStyleElement(xmlNode1, "@fo:margin-left");
							if (globalStyleElement6 != null)
							{
								str1 = string.Concat(str1, "margin-left: ", globalStyleElement6, "; ");
							}
							string str6 = this.GetGlobalStyleElement(xmlNode1, "@fo:margin-right");
							if (str6 != null)
							{
								str1 = string.Concat(str1, "margin-right: ", str6, "; ");
							}
						}
						XmlNode xmlNode5 = xmlNode.SelectSingleNode("style:text-properties", this.Document.NamespaceManager);
						XmlNode xmlNode6 = null;
						if (xmlNode4 != null)
						{
							xmlNode6 = xmlNode4.SelectSingleNode("style:text-properties", this.Document.NamespaceManager);
						}
						if (xmlNode6 != null)
						{
							string globalStyleElement7 = this.GetGlobalStyleElement(xmlNode6, "@fo:font-size");
							if (globalStyleElement7 != null)
							{
								str1 = string.Concat(str1, "font-size: ", FontFamilies.PtToPx(globalStyleElement7), "; ");
							}
							if (this.GetGlobalStyleElement(xmlNode6, "@fo:font-style") != null)
							{
								str1 = string.Concat(str1, "font-size: italic; ");
							}
							if (this.GetGlobalStyleElement(xmlNode6, "@fo:font-weight") != null)
							{
								str1 = string.Concat(str1, "font-weight: bold; ");
							}
							if (this.GetGlobalStyleElement(xmlNode6, "@style:text-underline-style") != null)
							{
								str1 = string.Concat(str1, "text-decoration: underline; ");
							}
							string str7 = this.GetGlobalStyleElement(xmlNode6, "@style:font-name");
							if (str7 != null)
							{
								str1 = string.Concat(str1, "font-family: ", FontFamilies.HtmlFont(str7), "; ");
							}
							string globalStyleElement8 = this.GetGlobalStyleElement(xmlNode6, "@fo:color");
							if (globalStyleElement8 != null)
							{
								str1 = string.Concat(str1, "color: ", globalStyleElement8, "; ");
							}
						}
						if (xmlNode5 != null)
						{
							string str8 = this.GetGlobalStyleElement(xmlNode5, "@fo:font-size");
							if (str8 != null)
							{
								str1 = string.Concat(str1, "font-size: ", FontFamilies.PtToPx(str8), "; ");
							}
							if (this.GetGlobalStyleElement(xmlNode5, "@fo:font-style") != null)
							{
								str1 = string.Concat(str1, "font-size: italic; ");
							}
							if (this.GetGlobalStyleElement(xmlNode5, "@fo:font-weight") != null)
							{
								str1 = string.Concat(str1, "font-weight: bold; ");
							}
							if (this.GetGlobalStyleElement(xmlNode5, "@style:text-underline-style") != null)
							{
								str1 = string.Concat(str1, "text-decoration: underline; ");
							}
							string globalStyleElement9 = this.GetGlobalStyleElement(xmlNode5, "@style:font-name");
							if (globalStyleElement9 != null)
							{
								str1 = string.Concat(str1, "font-family: ", FontFamilies.HtmlFont(globalStyleElement9), "; ");
							}
							string str9 = this.GetGlobalStyleElement(xmlNode5, "@fo:color");
							if (str9 != null)
							{
								str1 = string.Concat(str1, "color: ", str9, "; ");
							}
						}
					}
				}
				str1 = (str1.EndsWith("; ") ? string.Concat(str1, "\"") : "");
				str = str1;
				return str;
			}
			catch (Exception exception)
			{
			}
			str = "";
			return str;
		}

		private string GetTextHtmlContent()
		{
			string str = "";
			foreach (IText textContent in this.TextContent)
			{
				if (!(textContent is IHtml))
				{
					continue;
				}
				str = string.Concat(str, ((IHtml)textContent).GetHtml(), "\n");
			}
			return str;
		}

		private void Init(string styleName)
		{
			if (styleName != "Standard" && styleName != "Table_20_Contents" && styleName != "Text_20_body")
			{
				this.Style = new AODL.Document.Styles.ParagraphStyle(this.Document, styleName);
				this.Document.Styles.Add(this.Style);
			}
			this.InitStandards();
			this.StyleName = styleName;
		}

		private void InitStandards()
		{
			this.TextContent = new ITextCollection();
			this.Content = new IContentCollection();
			this._mixedContent = new ArrayList();
			if (this.Document is TextDocument)
			{
				DocumentMetadata documentMetadata = this.Document.DocumentMetadata;
				documentMetadata.ParagraphCount = documentMetadata.ParagraphCount + 1;
			}
			this.TextContent.Inserted += new CollectionWithEvents.CollectionChange(this.TextContent_Inserted);
			this.Content.Inserted += new CollectionWithEvents.CollectionChange(this.Content_Inserted);
			this.TextContent.Removed += new CollectionWithEvents.CollectionChange(this.TextContent_Removed);
			this.Content.Removed += new CollectionWithEvents.CollectionChange(this.Content_Removed);
		}

		private void NewXmlNode()
		{
			this.Node = this.Document.CreateNode("p", "text");
		}

		private void RemoveMixedContent(object value)
		{
			if (this._mixedContent.Contains(value))
			{
				this._mixedContent.Remove(value);
			}
		}

		private void TextContent_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IText)value).Node);
			this._mixedContent.Add(value);
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
			this.RemoveMixedContent(value);
		}
	}
}