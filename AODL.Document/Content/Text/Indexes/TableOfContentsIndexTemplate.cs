using AODL.Document;
using System;
using System.Xml;

namespace AODL.Document.Content.Text.Indexes
{
	public class TableOfContentsIndexTemplate
	{
		private XmlNode _node;

		private AODL.Document.Content.Text.Indexes.TableOfContents _tableOfContents;

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

		public TableOfContentsIndexTemplate(AODL.Document.Content.Text.Indexes.TableOfContents tableOfContents, int outlineLevel, string styleName)
		{
			this.TableOfContents = tableOfContents;
			this.NewXmlNode(outlineLevel, styleName);
		}

		private void AddIndexEntryNode(string nodeName, IndexEntryTypes indexEntryType)
		{
			XmlNode xmlNode = this.TableOfContents.Document.CreateNode(nodeName, "text");
			if (indexEntryType == IndexEntryTypes.TabStop)
			{
				XmlAttribute xmlAttribute = this.TableOfContents.Document.CreateAttribute("type", "style");
				xmlAttribute.Value=("right");
				xmlNode.Attributes.Append(xmlAttribute);
				xmlAttribute = this.TableOfContents.Document.CreateAttribute("leader-char", "style");
				xmlAttribute.Value=(".");
				xmlNode.Attributes.Append(xmlAttribute);
			}
			this.Node.AppendChild(xmlNode);
		}

		public void InitStandardTemplate()
		{
			if (this.TableOfContents.UseHyperlinks)
			{
				this.InsertIndexEntry(IndexEntryTypes.HyperlinkStart);
			}
			this.InsertIndexEntry(IndexEntryTypes.Chapter);
			this.InsertIndexEntry(IndexEntryTypes.Text);
			if (this.TableOfContents.UseHyperlinks)
			{
				this.InsertIndexEntry(IndexEntryTypes.HyperlinkEnd);
			}
			this.InsertIndexEntry(IndexEntryTypes.TabStop);
			this.InsertIndexEntry(IndexEntryTypes.PageNumber);
		}

		public void InsertIndexEntry(IndexEntryTypes indexEntryType)
		{
			if (indexEntryType == IndexEntryTypes.Chapter)
			{
				this.AddIndexEntryNode("index-entry-chapter", indexEntryType);
			}
			else if (indexEntryType == IndexEntryTypes.Text)
			{
				this.AddIndexEntryNode("index-entry-text", indexEntryType);
			}
			else if (indexEntryType == IndexEntryTypes.TabStop)
			{
				this.AddIndexEntryNode("index-entry-tab-stop", indexEntryType);
			}
			else if (indexEntryType == IndexEntryTypes.PageNumber)
			{
				this.AddIndexEntryNode("index-entry-page-number", indexEntryType);
			}
			else if (indexEntryType == IndexEntryTypes.HyperlinkStart)
			{
				this.AddIndexEntryNode("index-entry-link-start", indexEntryType);
			}
			else if (indexEntryType == IndexEntryTypes.HyperlinkEnd)
			{
				this.AddIndexEntryNode("index-entry-link-end", indexEntryType);
			}
		}

		private void NewXmlNode(int outlineLevel, string styleName)
		{
			this.Node = this.TableOfContents.Document.CreateNode("table-of-content-entry-template", "text");
			XmlAttribute xmlAttribute = this.TableOfContents.Document.CreateAttribute("outline-level", "text");
			xmlAttribute.Value=(outlineLevel.ToString());
			this.Node.Attributes.Append(xmlAttribute);
			xmlAttribute = this.TableOfContents.Document.CreateAttribute("style-name", "text");
			xmlAttribute.Value=(styleName);
			this.Node.Attributes.Append(xmlAttribute);
		}
	}
}