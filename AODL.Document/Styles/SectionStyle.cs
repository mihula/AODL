using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Content;
using AODL.Document.Styles.Properties;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Styles
{
	public class SectionStyle : IStyle
	{
		private IContent _content;

		private XmlNode _node;

		private IDocument _document;

		private IPropertyCollection _propertyCollection;

		public IContent Content
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

		public IPropertyCollection PropertyCollection
		{
			get
			{
				return this._propertyCollection;
			}
			set
			{
				this._propertyCollection = value;
			}
		}

		public AODL.Document.Styles.Properties.SectionProperties SectionProperties
		{
			get
			{
				AODL.Document.Styles.Properties.SectionProperties sectionProperty;
				foreach (IProperty propertyCollection in this.PropertyCollection)
				{
					if (!(propertyCollection is AODL.Document.Styles.Properties.SectionProperties))
					{
						continue;
					}
					sectionProperty = (AODL.Document.Styles.Properties.SectionProperties)propertyCollection;
					return sectionProperty;
				}
				AODL.Document.Styles.Properties.SectionProperties sectionProperty1 = new AODL.Document.Styles.Properties.SectionProperties(this);
				this.PropertyCollection.Add(sectionProperty1);
				sectionProperty = sectionProperty1;
				return sectionProperty;
			}
			set
			{
				if (this.PropertyCollection.Contains(value))
				{
					this.PropertyCollection.Remove(value);
				}
				this.PropertyCollection.Add(value);
			}
		}

		public string StyleName
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:name", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("name", value, "style");
				}
				this._node.SelectSingleNode("@style:name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public SectionStyle(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.Node = node;
			this.InitStandards();
			this.SectionProperties = new AODL.Document.Styles.Properties.SectionProperties(this);
		}

		public SectionStyle(IContent content, string styleName)
		{
			this.Content = content;
			this.Document = content.Document;
			this.NewXmlNode(styleName);
			this.InitStandards();
			this.SectionProperties = new AODL.Document.Styles.Properties.SectionProperties(this);
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
			xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		private void InitStandards()
		{
			this.PropertyCollection = new IPropertyCollection();
			this.PropertyCollection.Inserted += new CollectionWithEvents.CollectionChange(this.PropertyCollection_Inserted);
			this.PropertyCollection.Removed += new CollectionWithEvents.CollectionChange(this.PropertyCollection_Removed);
			this.Document.Styles.Add(this);
		}

		private void NewXmlNode(string stylename)
		{
			this.Node = this.Document.CreateNode("style", "style");
			XmlAttribute xmlAttribute = this.Document.CreateAttribute("name", "style");
			xmlAttribute.Value=(stylename);
			this.Node.Attributes.Append(xmlAttribute);
			xmlAttribute = this.Document.CreateAttribute("family", "style");
			xmlAttribute.Value=("section");
			this.Node.Attributes.Append(xmlAttribute);
		}

		private void PropertyCollection_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IProperty)value).Node);
		}

		private void PropertyCollection_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IProperty)value).Node);
		}
	}
}