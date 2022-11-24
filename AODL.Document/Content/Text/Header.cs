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
	public class Header : IContent, IHtml, ITextContainer, ICloneable
	{
		private IDocument _document;

		private IStyle _style;

		private XmlNode _node;

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

		public string OutLineLevel
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@text:outline-level", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@text:outline-level", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("outline-level", value, "text");
				}
				this._node.SelectSingleNode("@text:outline-level", this.Document.NamespaceManager).InnerText = value;
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

		public Header(IDocument document, Headings heading)
		{
			this.Document = document;
			this.NewXmlNode();
			this.StyleName = this.GetHeading(heading);
			this.InitStandards();
		}

		internal Header(XmlNode headernode, IDocument document)
		{
			this.Document = document;
			this.Node = headernode;
			this.InitStandards();
		}

		public object Clone()
		{
			Header header = null;
			if (this.Document != null && this.Node != null)
			{
				MainContentProcessor mainContentProcessor = new MainContentProcessor(this.Document);
				header = mainContentProcessor.CreateHeader(this.Node.CloneNode(true));
			}
			return header;
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
            xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		private string GetAnchor()
		{
			return "";
		}

		private string GetHeading(Headings heading)
		{
			string str;
			if (heading == Headings.Heading)
			{
				str = "Heading";
			}
			else if (heading == Headings.Heading_20_1)
			{
				str = "Heading_20_1";
			}
			else if (heading == Headings.Heading_20_2)
			{
				str = "Heading_20_2";
			}
			else if (heading == Headings.Heading_20_3)
			{
				str = "Heading_20_3";
			}
			else if (heading == Headings.Heading_20_4)
			{
				str = "Heading_20_4";
			}
			else if (heading == Headings.Heading_20_5)
			{
				str = "Heading_20_5";
			}
			else if (heading == Headings.Heading_20_6)
			{
				str = "Heading_20_6";
			}
			else if (heading == Headings.Heading_20_7)
			{
				str = "Heading_20_7";
			}
			else if (heading == Headings.Heading_20_8)
			{
				str = "Heading_20_8";
			}
			else if (heading != Headings.Heading_20_9)
			{
				str = (heading != Headings.Heading_20_10 ? "Heading" : "Heading_20_10");
			}
			else
			{
				str = "Heading_20_9";
			}
			return str;
		}

		private string GetHeadingNumber()
		{
			string str;
			try
			{
				int num = 0;
				int num1 = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				foreach (IContent content in this.Document.Content)
				{
					if (!(content is Header) || ((Header)content).OutLineLevel == null)
					{
						continue;
					}
					int num6 = Convert.ToInt32(((Header)content).OutLineLevel);
					if (num6 == 1)
					{
						num++;
						num1 = 0;
						num2 = 0;
						num3 = 0;
						num4 = 0;
						num5 = 0;
					}
					else if (num6 == 2)
					{
						num1++;
					}
					else if (num6 == 3)
					{
						num2++;
					}
					else if (num6 == 4)
					{
						num3++;
					}
					else if (num6 == 5)
					{
						num4++;
					}
					else if (num6 == 6)
					{
						num5++;
					}
					if (content != this)
					{
						continue;
					}
					string str1 = string.Concat(num.ToString(), ".");
					string str2 = "";
					if (num5 != 0)
					{
						str2 = string.Concat(".", num5.ToString(), ".");
					}
					if (num4 != 0)
					{
						str2 = string.Concat(str2, ".", num4.ToString(), ".");
					}
					if (num3 != 0)
					{
						str2 = string.Concat(str2, ".", num3.ToString(), ".");
					}
					if (num2 != 0)
					{
						str2 = string.Concat(str2, ".", num2.ToString(), ".");
					}
					if (num1 != 0)
					{
						str2 = string.Concat(str2, ".", num1.ToString(), ".");
					}
					str1 = string.Concat(str1, str2);
					str = str1.Replace("..", ".");
					return str;
				}
			}
			catch (Exception exception)
			{
			}
			str = "";
			return str;
		}

		public string GetHtml()
		{
			string str;
			try
			{
				string str1 = string.Concat(this.GetAnchor(), "<p ");
				string htmlStyle = this.GetHtmlStyle(this.StyleName);
				if (htmlStyle.Length > 0)
				{
					str1 = string.Concat(str1, htmlStyle);
				}
				str1 = string.Concat(str1, ">\n");
				string headingNumber = this.GetHeadingNumber();
				if (headingNumber.Length > 0)
				{
					str1 = string.Concat(str1, headingNumber, "&nbsp;&nbsp;");
				}
				foreach (IText textContent in this.TextContent)
				{
					if (!(textContent is IHtml))
					{
						continue;
					}
					str1 = string.Concat(str1, ((IHtml)textContent).GetHtml());
				}
				str1 = string.Concat(str1, "</p>\n");
				str = str1;
				return str;
			}
			catch (Exception exception)
			{
			}
			str = "";
			return str;
		}

		private string GetHtmlStyle(string headingname)
		{
			string str;
			try
			{
				string str1 = "style=\"margin-left: 0.5cm; margin-top: 0.5cm; margin-bottom: 0.5cm; ";
				string str2 = "";
				string str3 = "";
				string str4 = "";
				string str5 = "";
				if (this.Document is TextDocument)
				{
					TextDocument document = (TextDocument)this.Document;
					XmlNode xmlNode = document.DocumentStyles.Styles.SelectSingleNode(string.Concat("//style:style[@style:name='", headingname, "']"), this.Document.NamespaceManager);
					XmlNode xmlNode1 = null;
					if (xmlNode != null)
					{
						xmlNode1 = xmlNode.SelectSingleNode("style:text-properties", this.Document.NamespaceManager);
					}
					if (xmlNode1 != null)
					{
						XmlNode xmlNode2 = xmlNode1.SelectSingleNode("@fo:font-name", this.Document.NamespaceManager);
						if (xmlNode2 != null)
						{
							str2 = string.Concat("font-family:", xmlNode2.InnerText, "; ");
						}
						xmlNode2 = xmlNode1.SelectSingleNode("@fo:font-size", this.Document.NamespaceManager);
						if (xmlNode2 != null)
						{
							str3 = string.Concat("font-size:", xmlNode2.InnerText, "; ");
						}
						if (xmlNode1.OuterXml.IndexOf("bold") != -1)
						{
							str4 = "font-weight: bold; ";
						}
						if (xmlNode1.OuterXml.IndexOf("italic") != -1)
						{
							str5 = "font-style: italic; ";
						}
					}
					if (str2.Length > 0)
					{
						str1 = string.Concat(str1, str2);
					}
					if (str3.Length > 0)
					{
						str1 = string.Concat(str1, str3);
					}
					if (str4.Length > 0)
					{
						str1 = string.Concat(str1, str4);
					}
					if (str5.Length > 0)
					{
						str1 = string.Concat(str1, str5);
					}
					str1 = (!str1.EndsWith(" ") ? "" : string.Concat(str1, "\""));
					str = str1;
					return str;
				}
			}
			catch (Exception exception)
			{
				//exception.Message;
			}
			str = "";
			return str;
		}

		private void InitStandards()
		{
			this.TextContent = new ITextCollection();
			this.TextContent.Inserted += new CollectionWithEvents.CollectionChange(this.TextContent_Inserted);
			this.TextContent.Removed += new CollectionWithEvents.CollectionChange(this.TextContent_Removed);
		}

		private void NewXmlNode()
		{
			this.Node = this.Document.CreateNode("h", "text");
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