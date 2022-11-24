using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Styles.Properties;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Styles
{
	public class ParagraphStyle : IStyle
	{
		private XmlNode _node;

		private IDocument _document;

		private IPropertyCollection _propertyCollection;

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

		public string Family
		{
			get
			{
				string innerText = this._node.SelectSingleNode("@style:family", this.Document.NamespaceManager).InnerText;
				return innerText;
			}
			set
			{
				this._node.SelectSingleNode("@style:family", this.Document.NamespaceManager).InnerText=(value.ToString());
			}
		}

		public string ListStyleName
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:list-style-name", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:list-style-name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("list-style-name", value, "style");
				}
				this._node.SelectSingleNode("@style:list-style-name", this.Document.NamespaceManager).InnerText = value;
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

		public AODL.Document.Styles.Properties.ParagraphProperties ParagraphProperties
		{
			get
			{
				AODL.Document.Styles.Properties.ParagraphProperties paragraphProperty;
				foreach (IProperty propertyCollection in this.PropertyCollection)
				{
					if (!(propertyCollection is AODL.Document.Styles.Properties.ParagraphProperties))
					{
						continue;
					}
					paragraphProperty = (AODL.Document.Styles.Properties.ParagraphProperties)propertyCollection;
					return paragraphProperty;
				}
				AODL.Document.Styles.Properties.ParagraphProperties paragraphProperty1 = new AODL.Document.Styles.Properties.ParagraphProperties(this);
				this.PropertyCollection.Add(paragraphProperty1);
				paragraphProperty = paragraphProperty1;
				return paragraphProperty;
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

		public string ParentStyle
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:parent-style-name", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:parent-style-name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("parent-style-name", value, "style");
				}
				this._node.SelectSingleNode("@style:parent-style-name", this.Document.NamespaceManager).InnerText = value;
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

		public AODL.Document.Styles.Properties.TextProperties TextProperties
		{
			get
			{
				AODL.Document.Styles.Properties.TextProperties textProperty;
				foreach (IProperty propertyCollection in this.PropertyCollection)
				{
					if (!(propertyCollection is AODL.Document.Styles.Properties.TextProperties))
					{
						continue;
					}
					textProperty = (AODL.Document.Styles.Properties.TextProperties)propertyCollection;
					return textProperty;
				}
				AODL.Document.Styles.Properties.TextProperties textProperty1 = new AODL.Document.Styles.Properties.TextProperties(this);
				this.PropertyCollection.Add(textProperty1);
				textProperty = textProperty1;
				return textProperty;
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

		public ParagraphStyle(IDocument document, string styleName)
		{
			this.Document = document;
			this.NewXmlNode(styleName);
			this.InitStandards();
		}

		public ParagraphStyle(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.Node = node;
			this.InitStandards();
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
		}

		private void NewXmlNode(string name)
		{
			this.Node = this.Document.CreateNode("style", "style");
			XmlAttribute xmlAttribute = this.Document.CreateAttribute("name", "style");
			xmlAttribute.Value = name;
			this.Node.Attributes.Append(xmlAttribute);
			xmlAttribute = this.Document.CreateAttribute("family", "style");
			xmlAttribute.Value=(FamiliyStyles.Paragraph);
			this.Node.Attributes.Append(xmlAttribute);
			xmlAttribute = this.Document.CreateAttribute("parent-style-name", "style");
			xmlAttribute.Value=(ParentStyles.Standard.ToString());
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