using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Styles.Properties;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Styles
{
	public class ListStyle : IStyle
	{
		private ListLevelStyleCollection _listlevelcollection;

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

		public ListLevelStyleCollection ListlevelStyles
		{
			get
			{
				return this._listlevelcollection;
			}
			set
			{
				this._listlevelcollection = value;
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

		public ListStyle(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.Node = node;
			this.ListlevelStyles = new ListLevelStyleCollection();
		}

		public ListStyle(IDocument document, string styleName)
		{
			this.Document = document;
			this.NewXmlNode();
			this.InitStandards();
			this.ListlevelStyles = new ListLevelStyleCollection();
			this.StyleName = styleName;
		}

		public void AutomaticAddListLevelStyles(ListStyles typ)
		{
			if (this.Node.ChildNodes.Count != 0)
			{
				try
				{
					foreach (XmlNode childNode in this.Node.ChildNodes)
					{
						this.Node.RemoveChild(childNode);
					}
				}
				catch (Exception exception)
				{
					throw;
				}
			}
			this.ListlevelStyles.Clear();
			for (int i = 1; i <= 10; i++)
			{
				ListLevelStyle listLevelStyle = new ListLevelStyle(this.Document, this, typ, i);
				this.ListlevelStyles.Add(listLevelStyle);
			}
			foreach (ListLevelStyle listlevelStyle in this.ListlevelStyles)
			{
				this.Node.AppendChild(listlevelStyle.Node);
			}
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
			this.Node = this.Document.CreateNode("list-style", "text");
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