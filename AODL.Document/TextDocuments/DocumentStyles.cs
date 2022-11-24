using AODL.Document;
using AODL.Document.Content.Text;
using AODL.Document.Styles;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Xml;

namespace AODL.Document.TextDocuments
{
	public class DocumentStyles
	{
		public readonly static string FileName;

		private readonly static string OfficeStyles;

		private XmlDocument _styles;

		public XmlDocument Styles
		{
			get
			{
				return this._styles;
			}
			set
			{
				this._styles = value;
			}
		}

		static DocumentStyles()
		{
			DocumentStyles.FileName = "styles.xml";
			DocumentStyles.OfficeStyles = "/office:document-style/office:styles";
		}

		public DocumentStyles()
		{
		}

		private XmlNode CreateNode(string name, string prefix, TextDocument document)
		{
			XmlNode xmlNode;
			try
			{
				string namespaceUri = document.GetNamespaceUri(prefix);
				xmlNode = this.Styles.CreateElement(prefix, name, namespaceUri);
			}
			catch (Exception exception)
			{
				throw;
			}
			return xmlNode;
		}

		internal string GetHtmlFooter(TextDocument document)
		{
			string str = "";
			try
			{
				this.Styles.SelectSingleNode("//office:master-styles/style:master-page/style:footer", document.NamespaceManager);
			}
			catch (Exception exception)
			{
			}
			return str;
		}

		internal string GetHtmlHeader(TextDocument document)
		{
			string str = "";
			try
			{
				this.Styles.SelectSingleNode("//office:master-styles/style:master-page/style:header", document.NamespaceManager);
			}
			catch (Exception exception)
			{
			}
			return str;
		}

		internal void InsertFooter(Paragraph content, TextDocument document)
		{
			try
			{
				bool flag = true;
				XmlNode xmlNode = this._styles.SelectSingleNode("//office:master-styles/style:master-page/style:footer", document.NamespaceManager);
				if (xmlNode == null)
				{
					xmlNode = this.CreateNode("footer", "style", document);
					flag = false;
				}
				else
				{
					xmlNode.InnerXml=("");
				}
				XmlNode xmlNode1 = this.Styles.ImportNode(content.Node, true);
				xmlNode.AppendChild(xmlNode1);
				if (!flag)
				{
					this._styles.SelectSingleNode("//office:master-styles/style:master-page", document.NamespaceManager).AppendChild(xmlNode);
				}
				this.InsertParagraphStyle(content, document);
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		internal void InsertHeader(Paragraph content, TextDocument document)
		{
			try
			{
				bool flag = true;
				XmlNode xmlNode = this._styles.SelectSingleNode("//office:master-styles/style:master-page/style:header", document.NamespaceManager);
				if (xmlNode == null)
				{
					xmlNode = this.CreateNode("header", "style", document);
					flag = false;
				}
				else
				{
					xmlNode.InnerXml=("");
				}
				XmlNode xmlNode1 = this.Styles.ImportNode(content.Node, true);
				xmlNode.AppendChild(xmlNode1);
				if (!flag)
				{
					this._styles.SelectSingleNode("//office:master-styles/style:master-page", document.NamespaceManager).AppendChild(xmlNode);
				}
				this.InsertParagraphStyle(content, document);
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		public virtual void InsertOfficeStylesNode(XmlNode aStyleNode, IDocument document)
		{
			this.Styles.SelectSingleNode("//office:styles", document.NamespaceManager).AppendChild(aStyleNode);
		}

		private void InsertParagraphStyle(Paragraph content, TextDocument document)
		{
			try
			{
				if (content.Style != null)
				{
					XmlNode xmlNode = this.Styles.ImportNode(content.Style.Node, true);
					this.Styles.SelectSingleNode("//office:styles", document.NamespaceManager).AppendChild(xmlNode);
				}
				if (content.TextContent != null)
				{
					foreach (IText textContent in content.TextContent)
					{
						if (!(textContent is FormatedText))
						{
							continue;
						}
						XmlNode xmlNode1 = this.Styles.ImportNode(textContent.Style.Node, true);
						this.Styles.SelectSingleNode("//office:styles", document.NamespaceManager).AppendChild(xmlNode1);
					}
				}
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		public void LoadFromFile(string file)
		{
			try
			{
				this.Styles = new XmlDocument();
				this.Styles.Load(file);
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		public virtual void New()
		{
			try
			{
				Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AODL.Resources.OD.styles.xml");
				this.Styles = new XmlDocument();
				this.Styles.Load(manifestResourceStream);
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		public void SetOutlineStyle(int outlineLevel, string numFormat, TextDocument document)
		{
			try
			{
				XmlNode node = null;
				foreach (IStyle commonStyle in document.CommonStyles)
				{
					if (commonStyle.Node.Name != "text:outline-style")
					{
						continue;
					}
					node = commonStyle.Node;
				}
				XmlNode xmlNode = null;
				if (node != null)
				{
					xmlNode = node.SelectSingleNode(string.Concat("text:outline-level-style[@text:level='", outlineLevel.ToString(), "']"), document.NamespaceManager);
				}
				if (xmlNode != null)
				{
					XmlNode xmlNode1 = xmlNode.SelectSingleNode("@style:num-format", document.NamespaceManager);
					if (xmlNode1 != null)
					{
						xmlNode1.InnerText=(numFormat);
					}
					XmlAttribute xmlAttribute = document.CreateAttribute("num-suffix", "style");
					xmlAttribute.InnerText=(".");
					xmlNode.Attributes.Append(xmlAttribute);
					if (outlineLevel > 1)
					{
						xmlAttribute = document.CreateAttribute("display-levels", "text");
						xmlAttribute.InnerText=(outlineLevel.ToString());
						xmlNode.Attributes.Append(xmlAttribute);
					}
				}
			}
			catch (Exception exception)
			{
				throw;
			}
		}
	}
}