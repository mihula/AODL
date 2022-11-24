using AODL.Document;
using System;
using System.Xml;

namespace AODL.Document.Content.Text.Indexes
{
	public class TableOfContentsSource
	{
		private XmlNode _node;

		private AODL.Document.Content.Text.Indexes.TableOfContents _tableOfContents;

		private AODL.Document.Content.Text.Indexes.TableOfContentsIndexTemplateCollection _tableOfContensIndexTemplateCollection;

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

		public AODL.Document.Content.Text.Indexes.TableOfContents TableOfContents
		{
			get
			{
				return this._tableOfContents;
			}
			set
			{
				this._tableOfContents = value;
			}
		}

		public AODL.Document.Content.Text.Indexes.TableOfContentsIndexTemplateCollection TableOfContentsIndexTemplateCollection
		{
			get
			{
				return this._tableOfContensIndexTemplateCollection;
			}
			set
			{
				this._tableOfContensIndexTemplateCollection = value;
			}
		}

		public TableOfContentsSource(AODL.Document.Content.Text.Indexes.TableOfContents tableOfContents)
		{
			this._tableOfContents = tableOfContents;
			this.TableOfContentsIndexTemplateCollection = new AODL.Document.Content.Text.Indexes.TableOfContentsIndexTemplateCollection();
			this.NewXmlNode();
		}

		public void InitStandardTableOfContentStyle()
		{
			for (int i = 1; i <= 10; i++)
			{
				TableOfContentsIndexTemplate tableOfContentsIndexTemplate = new TableOfContentsIndexTemplate(this.TableOfContents, i, string.Concat("Contents_20_", i.ToString()));
				tableOfContentsIndexTemplate.InitStandardTemplate();
				this.Node.AppendChild(tableOfContentsIndexTemplate.Node);
				this.TableOfContentsIndexTemplateCollection.Add(tableOfContentsIndexTemplate);
			}
		}

		private void NewXmlNode()
		{
			this.Node = this.TableOfContents.Document.CreateNode("table-of-content-source", "text");
			XmlAttribute xmlAttribute = this.TableOfContents.Document.CreateAttribute("outline-level", "text");
			xmlAttribute.Value=("10");
			this.Node.Attributes.Append(xmlAttribute);
			XmlNode xmlNode = this.TableOfContents.Document.CreateNode("index-title-template", "text");
			xmlNode.InnerText=(this.TableOfContents.Title);
			xmlAttribute = this.TableOfContents.Document.CreateAttribute("style-name", "text");
			xmlAttribute.Value=("Contents_20_Heading");
			xmlNode.Attributes.Append(xmlAttribute);
			this.Node.AppendChild(xmlNode);
		}
	}
}