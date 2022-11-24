using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Styles.Properties;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Styles
{
	public class ListLevelStyle : IStyle
	{
		private AODL.Document.Styles.ListStyle _liststyle;

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

		public AODL.Document.Styles.Properties.ListLevelProperties ListLevelProperties
		{
			get
			{
				AODL.Document.Styles.Properties.ListLevelProperties listLevelProperty;
				foreach (IProperty propertyCollection in this.PropertyCollection)
				{
					if (!(propertyCollection is AODL.Document.Styles.Properties.ListLevelProperties))
					{
						continue;
					}
					listLevelProperty = (AODL.Document.Styles.Properties.ListLevelProperties)propertyCollection;
					return listLevelProperty;
				}
				AODL.Document.Styles.Properties.ListLevelProperties listLevelProperty1 = new AODL.Document.Styles.Properties.ListLevelProperties(this);
				this.PropertyCollection.Add(listLevelProperty1);
				listLevelProperty = listLevelProperty1;
				return listLevelProperty;
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

		public AODL.Document.Styles.ListStyle ListStyle
		{
			get
			{
				return this._liststyle;
			}
			set
			{
				this._liststyle = value;
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

		public ListLevelStyle(IDocument document, AODL.Document.Styles.ListStyle style, ListStyles typ, int level)
		{
			this.Document = document;
			this.ListStyle = style;
			this.InitStandards(level);
			this.NewXmlNode(typ, level);
			this.AddListLevel(level);
		}

		private void AddListLevel(int level)
		{
			this.ListLevelProperties = new AODL.Document.Styles.Properties.ListLevelProperties(this);
			double num = 0.635 * (double)level;
			string str = string.Concat(num.ToString().Replace(",", "."), "cm");
			this.ListLevelProperties.MinLabelWidth = "0.635cm";
			this.ListLevelProperties.SpaceBefore = str;
		}

		private void AddTextPropertie()
		{
			this.TextProperties = new AODL.Document.Styles.Properties.TextProperties(this)
			{
				FontName = "StarSymbol"
			};
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
			xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		private void InitStandards(int level)
		{
			this.PropertyCollection = new IPropertyCollection();
			this.PropertyCollection.Inserted += new CollectionWithEvents.CollectionChange(this.PropertyCollection_Inserted);
			this.PropertyCollection.Removed += new CollectionWithEvents.CollectionChange(this.PropertyCollection_Removed);
		}

		private void NewXmlNode(ListStyles typ, int level)
		{
			XmlAttribute xmlAttribute = null;
			if (typ != ListStyles.Bullet)
			{
				if (typ != ListStyles.Number)
				{
					throw new Exception("Unknown ListStyles typ");
				}
				this.Node = this.Document.CreateNode("list-level-style-number", "text");
				xmlAttribute = this.Document.CreateAttribute("style-name", "text");
				xmlAttribute.Value=("Numbering_20_Symbols");
				this.Node.Attributes.Append(xmlAttribute);
				xmlAttribute = this.Document.CreateAttribute("num-format", "style");
				xmlAttribute.Value=("1");
				this.Node.Attributes.Append(xmlAttribute);
			}
			else
			{
				this.Node = this.Document.CreateNode("list-level-style-bullet", "text");
				xmlAttribute = this.Document.CreateAttribute("style-name", "text");
				xmlAttribute.Value=("Bullet_20_Symbols");
				this.Node.Attributes.Append(xmlAttribute);
				xmlAttribute = this.Document.CreateAttribute("bullet-char", "text");
				xmlAttribute.Value=("•");
				this.Node.Attributes.Append(xmlAttribute);
				this.AddTextPropertie();
			}
			xmlAttribute = this.Document.CreateAttribute("level", "text");
			xmlAttribute.Value=(level.ToString());
			this.Node.Attributes.Append(xmlAttribute);
			xmlAttribute = this.Document.CreateAttribute("num-suffix", "style");
			xmlAttribute.Value=(".");
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