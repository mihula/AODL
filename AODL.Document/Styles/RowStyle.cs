using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Styles.Properties;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Styles
{
	public class RowStyle : IStyle
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

		public string FamilyStyle
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:family", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:family", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("family", value, "style");
				}
				this._node.SelectSingleNode("@style:family", this.Document.NamespaceManager).InnerText = value;
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

		public AODL.Document.Styles.Properties.RowProperties RowProperties
		{
			get
			{
				AODL.Document.Styles.Properties.RowProperties rowProperty;
				foreach (IProperty propertyCollection in this.PropertyCollection)
				{
					if (!(propertyCollection is AODL.Document.Styles.Properties.RowProperties))
					{
						continue;
					}
					rowProperty = (AODL.Document.Styles.Properties.RowProperties)propertyCollection;
					return rowProperty;
				}
				AODL.Document.Styles.Properties.RowProperties rowProperty1 = new AODL.Document.Styles.Properties.RowProperties(this);
				this.PropertyCollection.Add(rowProperty1);
				rowProperty = rowProperty1;
				return rowProperty;
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

		public RowStyle(IDocument document)
		{
			this.Document = document;
			this.InitStandards();
		}

		public RowStyle(IDocument document, string styleName)
		{
			this.Document = document;
			this.InitStandards();
			this.StyleName = styleName;
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
			xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		private void InitStandards()
		{
			this.NewXmlNode();
			this.PropertyCollection = new IPropertyCollection();
			this.PropertyCollection.Inserted += new CollectionWithEvents.CollectionChange(this.PropertyCollection_Inserted);
			this.PropertyCollection.Removed += new CollectionWithEvents.CollectionChange(this.PropertyCollection_Removed);
		}

		private void NewXmlNode()
		{
			this.Node = this.Document.CreateNode("style", "style");
			this.FamilyStyle = FamiliyStyles.TableRow;
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