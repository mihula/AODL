using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Content;
using AODL.Document.Content.Text;
using AODL.Document.Content.Text.TextControl;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.Document.TextDocuments;
using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace AODL.Document.Content.Text.Indexes
{
	public class TableOfContents : IContent, IContentContainer, IHtml
	{
		internal XmlNode _indexBodyNode;

		private string _contentStyleName = "Contents_20_";

		private string _contentStyleDisplayName = "Contents ";

		private bool _isNew;

		private bool _useHyperlinks;

		private Paragraph _titleParagraph;

		private AODL.Document.Content.Text.Indexes.TableOfContentsSource _tableOfContentsSource;

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

		public AODL.Document.Content.Text.Indexes.TableOfContentsSource TableOfContentsSource
		{
			get
			{
				return this._tableOfContentsSource;
			}
			set
			{
				this._tableOfContentsSource = value;
			}
		}

		public string Title
		{
			get
			{
				string str;
				XmlNode xmlNode = this._node.SelectSingleNode("@text:name", ((TextDocument)this.Document).NamespaceManager);
				if (xmlNode == null)
				{
					str = null;
				}
				else
				{
					str = xmlNode.InnerText.Substring(0, xmlNode.InnerText.Length - 1);
				}
				return str;
			}
		}

		public Paragraph TitleParagraph
		{
			get
			{
				return this._titleParagraph;
			}
			set
			{
				this._titleParagraph = value;
			}
		}

		public bool UseHyperlinks
		{
			get
			{
				return this._useHyperlinks;
			}
			set
			{
				this._useHyperlinks = value;
			}
		}

		public TableOfContents(TextDocument textDocument, string styleName, bool useHyperlinks, bool protectChanges, string textName)
		{
			this.Document = textDocument;
			this.UseHyperlinks = useHyperlinks;
			this._isNew = true;
			this.NewXmlNode(styleName, protectChanges, textName);
			this.Style = new SectionStyle(this, styleName);
			this.Document.Styles.Add(this.Style);
			this.TableOfContentsSource = new AODL.Document.Content.Text.Indexes.TableOfContentsSource(this);
			this.TableOfContentsSource.InitStandardTableOfContentStyle();
			this.Node.AppendChild(this.TableOfContentsSource.Node);
			this.CreateIndexBody();
			this.CreateTitlePargraph();
			this.InsertContentStyle();
			this.SetOutlineStyle();
			this.RegisterEvents();
		}

		internal TableOfContents(TextDocument textDocument, XmlNode tocNode)
		{
			this.Document = textDocument;
			this.Node = tocNode;
			this.RegisterEvents();
		}

		private void Content_Inserted(int index, object value)
		{
			this._indexBodyNode.AppendChild(((IContent)value).Node);
		}

		private void Content_Removed(int index, object value)
		{
			this._indexBodyNode.RemoveChild(((IContent)value).Node);
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
            xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		private void CreateIndexBody()
		{
			this._indexBodyNode = ((TextDocument)this.Document).CreateNode("index-body", "text");
			XmlNode xmlNode = ((TextDocument)this.Document).CreateNode("index-title", "text");
			XmlAttribute xmlAttribute = ((TextDocument)this.Document).CreateAttribute("style-name", "text");
			xmlAttribute.Value=(this.StyleName);
			xmlNode.Attributes.Append(xmlAttribute);
			xmlAttribute = ((TextDocument)this.Document).CreateAttribute("name", "text");
			xmlAttribute.Value=(string.Concat(this.Title, "_Head"));
			xmlNode.Attributes.Append(xmlAttribute);
			this._indexBodyNode.AppendChild(xmlNode);
			this.Node.AppendChild(this._indexBodyNode);
		}

		private void CreateTitlePargraph()
		{
			this.TitleParagraph = new Paragraph((TextDocument)this.Document, "Table_Of_Contents_Title");
			this.TitleParagraph.TextContent.Add(new SimpleText(this.Document, this.Title));
			this.TitleParagraph.ParagraphStyle.TextProperties.Bold = "bold";
			this.TitleParagraph.ParagraphStyle.TextProperties.FontName = FontFamilies.Arial;
			this.TitleParagraph.ParagraphStyle.TextProperties.FontSize = "20pt";
			this._indexBodyNode.ChildNodes[0].AppendChild(this.TitleParagraph.Node);
		}

		public string GetHtml()
		{
			string str = "<br>&nbsp;\n";
			try
			{
				foreach (IContent content in this.Content)
				{
					if (!(content is IHtml))
					{
						continue;
					}
					str = string.Concat(str, ((IHtml)content).GetHtml(), "\n");
				}
			}
			catch (Exception exception)
			{
			}
			return str;
		}

		public TabStopStyleCollection GetTabStopStyle(string leaderStyle, string leadingChar, double position)
		{
			TabStopStyleCollection tabStopStyleCollection = new TabStopStyleCollection((TextDocument)this.Document);
			TabStopStyle tabStopStyle = new TabStopStyle((TextDocument)this.Document, position)
			{
				LeaderStyle = leaderStyle,
				LeaderText = leadingChar,
				Type = TabStopTypes.Center
			};
			tabStopStyleCollection.Add(tabStopStyle);
			return tabStopStyleCollection;
		}

		private void InsertContentStyle()
		{
			for (int i = 1; i <= 10; i++)
			{
				XmlNode xmlNode = this.Document.CreateNode("style", "style");
				XmlAttribute xmlAttribute = this.Document.CreateAttribute("name", "style");
				xmlAttribute.InnerText=(string.Concat(this._contentStyleName, i.ToString()));
				xmlNode.Attributes.Append(xmlAttribute);
				xmlAttribute = this.Document.CreateAttribute("display-name", "style");
				xmlAttribute.InnerText=(string.Concat(this._contentStyleDisplayName, i.ToString()));
				xmlNode.Attributes.Append(xmlAttribute);
				xmlAttribute = this.Document.CreateAttribute("parent-style-name", "style");
				xmlAttribute.InnerText=("Index");
				xmlNode.Attributes.Append(xmlAttribute);
				xmlAttribute = this.Document.CreateAttribute("family", "style");
				xmlAttribute.InnerText=("paragraph");
				xmlNode.Attributes.Append(xmlAttribute);
				xmlAttribute = this.Document.CreateAttribute("class", "style");
				xmlAttribute.InnerText=("index");
				xmlNode.Attributes.Append(xmlAttribute);
				XmlNode xmlNode1 = this.Document.CreateNode("paragraph-properties", "style");
				xmlAttribute = this.Document.CreateAttribute("margin-left", "fo");
				double num = 0.499 * (double)(i - 1);
				xmlAttribute.InnerText=(string.Concat(num.ToString("F3").Replace(",", "."), "cm"));
				xmlNode1.Attributes.Append(xmlAttribute);
				xmlAttribute = this.Document.CreateAttribute("margin-right", "fo");
				xmlAttribute.InnerText=("0cm");
				xmlNode1.Attributes.Append(xmlAttribute);
				xmlAttribute = this.Document.CreateAttribute("text-indent", "fo");
				xmlAttribute.InnerText=("0cm");
				xmlNode1.Attributes.Append(xmlAttribute);
				xmlAttribute = this.Document.CreateAttribute("auto-text-indent", "fo");
				xmlAttribute.InnerText=("0cm");
				xmlNode1.Attributes.Append(xmlAttribute);
				XmlNode xmlNode2 = this.Document.CreateNode("tab-stops", "style");
				XmlNode xmlNode3 = this.Document.CreateNode("tab-stop", "style");
				xmlAttribute = this.Document.CreateAttribute("position", "style");
				double num1 = 16.999 - (double)i * 0.499;
				xmlAttribute.InnerText=(string.Concat(num1.ToString("F3").Replace(",", "."), "cm"));
				xmlNode3.Attributes.Append(xmlAttribute);
				xmlAttribute = this.Document.CreateAttribute("type", "style");
				xmlAttribute.InnerText=("right");
				xmlNode3.Attributes.Append(xmlAttribute);
				xmlAttribute = this.Document.CreateAttribute("leader-style", "style");
				xmlAttribute.InnerText=("dotted");
				xmlNode3.Attributes.Append(xmlAttribute);
				xmlAttribute = this.Document.CreateAttribute("leader-text", "style");
				xmlAttribute.InnerText=(".");
				xmlNode3.Attributes.Append(xmlAttribute);
				xmlNode2.AppendChild(xmlNode3);
				xmlNode1.AppendChild(xmlNode2);
				xmlNode.AppendChild(xmlNode1);
				IStyle unknownStyle = new UnknownStyle(this.Document, xmlNode);
				this.Document.CommonStyles.Add(unknownStyle);
			}
		}

		public void InsertEntry(string textEntry, int outLineLevel)
		{
			Paragraph paragraph = new Paragraph((TextDocument)this.Document, string.Concat("P1_Toc_Entry", outLineLevel.ToString()));
			((ParagraphStyle)paragraph.Style).ParentStyle = string.Concat(this._contentStyleName, outLineLevel.ToString());
			if (!this.UseHyperlinks)
			{
				paragraph.TextContent.Add(new SimpleText(this.Document, textEntry));
				paragraph.TextContent.Add(new TabStop(this.Document));
				paragraph.TextContent.Add(new SimpleText(this.Document, "1"));
			}
			else
			{
				int num = textEntry.IndexOf(" ");
				StringBuilder stringBuilder = (new StringBuilder(textEntry)).Remove(num, 1);
				string str = string.Concat("#", stringBuilder.ToString(), "|outline");
				XLink xLink = new XLink(this.Document, str, textEntry)
				{
					XLinkType = "simple"
				};
				paragraph.TextContent.Add(xLink);
				paragraph.TextContent.Add(new TabStop(this.Document));
				paragraph.TextContent.Add(new SimpleText(this.Document, "1"));
			}
			paragraph.ParagraphStyle.ParagraphProperties.TabStopStyleCollection = this.GetTabStopStyle(TabStopLeaderStyles.Dotted, ".", 16.999);
			this.Content.Add(paragraph);
		}

		private void NewXmlNode(string stylename, bool protectChanges, string textName)
		{
			this.Node = ((TextDocument)this.Document).CreateNode("table-of-content", "text");
			XmlAttribute xmlAttribute = ((TextDocument)this.Document).CreateAttribute("style-name", "text");
			xmlAttribute.Value=(stylename);
			this.Node.Attributes.Append(xmlAttribute);
			xmlAttribute = ((TextDocument)this.Document).CreateAttribute("protected", "text");
			xmlAttribute.Value=(protectChanges.ToString().ToLower());
			this.Node.Attributes.Append(xmlAttribute);
			xmlAttribute = ((TextDocument)this.Document).CreateAttribute("use-outline-level", "text");
			xmlAttribute.Value=("true");
			this.Node.Attributes.Append(xmlAttribute);
			xmlAttribute = ((TextDocument)this.Document).CreateAttribute("name", "text");
			xmlAttribute.Value=((textName != null ? textName : "Table of Contents1"));
			this.Node.Attributes.Append(xmlAttribute);
		}

		private void RegisterEvents()
		{
			this.Content = new IContentCollection();
			this.Content.Inserted += new CollectionWithEvents.CollectionChange(this.Content_Inserted);
			this.Content.Removed += new CollectionWithEvents.CollectionChange(this.Content_Removed);
		}

		private void SetOutlineStyle()
		{
			try
			{
				for (int i = 1; i <= 10; i++)
				{
					((TextDocument)this.Document).DocumentStyles.SetOutlineStyle(i, "1", (TextDocument)this.Document);
				}
			}
			catch (Exception exception)
			{
				throw;
			}
		}
	}
}