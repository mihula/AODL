using AODL.Document;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Styles.Properties
{
	public class RowProperties : IProperty
	{
		private XmlNode _node;

		private IStyle _style;

		public string BackgroundColor
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:background-color", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:background-color", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("background-color", value, "fo");
				}
				this._node.SelectSingleNode("@fo:background-color", this.Style.Document.NamespaceManager).InnerText = value;
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

		public RowProperties(IStyle style)
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
			if (this.BackgroundColor != null)
			{
				str = string.Concat(str, "background-color: ", this.BackgroundColor, "; ");
			}
			str = (str.EndsWith("; ") ? string.Concat(str, "\"") : "");
			return str;
		}

		private void NewXmlNode()
		{
			this.Node = this.Style.Document.CreateNode("table-row-properties", "style");
		}
	}
}