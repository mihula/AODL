using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Styles.Properties;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Styles
{
	public class FrameStyle : IStyle
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

		public AODL.Document.Styles.Properties.GraphicProperties GraphicProperties
		{
			get
			{
				AODL.Document.Styles.Properties.GraphicProperties graphicProperty;
				foreach (IProperty propertyCollection in this.PropertyCollection)
				{
					if (!(propertyCollection is AODL.Document.Styles.Properties.GraphicProperties))
					{
						continue;
					}
					graphicProperty = (AODL.Document.Styles.Properties.GraphicProperties)propertyCollection;
					return graphicProperty;
				}
				AODL.Document.Styles.Properties.GraphicProperties graphicProperty1 = new AODL.Document.Styles.Properties.GraphicProperties(this);
				this.PropertyCollection.Add(graphicProperty1);
				graphicProperty = graphicProperty1;
				return graphicProperty;
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

		public FrameStyle(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.Node = node;
			this.InitStandards();
		}

		public FrameStyle(IDocument document, string styleName)
		{
			this.Document = document;
			this.InitStandards();
			this.NewXmlNode();
			this.StyleName = styleName;
			this.GraphicProperties = new AODL.Document.Styles.Properties.GraphicProperties(this);
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

		private void NewXmlNode()
		{
			this.Node = this.Document.CreateNode("style", "style");
			XmlAttribute xmlAttribute = this.Document.CreateAttribute("family", "style");
			xmlAttribute.Value=(FamiliyStyles.Graphic);
			this.Node.Attributes.Append(xmlAttribute);
			xmlAttribute = this.Document.CreateAttribute("parent-style-name", "style");
			xmlAttribute.Value=("Graphics");
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