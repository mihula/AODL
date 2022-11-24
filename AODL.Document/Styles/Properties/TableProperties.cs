using AODL.Document;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Styles.Properties
{
	public class TableProperties : IProperty, IHtmlStyle
	{
		private XmlNode _node;

		private IStyle _style;

		public string Align
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@table:align", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@table:align", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("align", value, "table");
				}
				this._node.SelectSingleNode("@table:align", this.Style.Document.NamespaceManager).InnerText = value;
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

		public string Shadow
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:shadow", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:shadow", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("shadow", value, "style");
				}
				this._node.SelectSingleNode("@style:shadow", this.Style.Document.NamespaceManager).InnerText = value;
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
				XmlNode xmlNode = this._node.SelectSingleNode("@style:width", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:width", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("width", value, "style");
				}
				this._node.SelectSingleNode("@style:width", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public TableProperties(IStyle style)
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

		public string GetHtmlStyle()
		{
			string str = "style=\"";
			if (this.Width != null)
			{
				str = string.Concat(str, "width: ", this.Width.Replace(",", "."), "; ");
			}
			str = (str.EndsWith("; ") ? string.Concat(str, "\"") : "");
			return str;
		}

		private void NewXmlNode()
		{
			this.Node = this.Style.Document.CreateNode("table-properties", "style");
			this.Width = "16.99cm";
			this.Align = "margin";
			this.Shadow = "none";
		}
	}
}