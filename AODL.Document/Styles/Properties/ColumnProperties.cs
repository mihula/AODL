using AODL.Document;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Styles.Properties
{
	public class ColumnProperties : IProperty
	{
		private XmlNode _node;

		private IStyle _style;

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

		public string RelativeWidth
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:rel-column-width", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:rel-column-width", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("rel-column-width", value, "style");
				}
				this._node.SelectSingleNode("@style:rel-column-width", this.Style.Document.NamespaceManager).InnerText = value;
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
				this._style = value;
			}
		}

		public string Width
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:column-width", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:column-width", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("column-width", value, "style");
				}
				this._node.SelectSingleNode("@style:column-width", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public ColumnProperties(IStyle style)
		{
			this.Style = style;
			this.NewXmlNode();
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Style.Document.CreateAttribute(name, prefix);
			xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		private void NewXmlNode()
		{
			this.Node = this.Style.Document.CreateNode("table-column-properties", "style");
		}
	}
}