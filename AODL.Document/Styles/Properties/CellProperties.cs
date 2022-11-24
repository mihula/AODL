using AODL.Document;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Styles.Properties
{
	public class CellProperties : IProperty, IHtmlStyle
	{
		private XmlNode _node;

		private IStyle _style;

		public string BackgroundColor
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:background-color", this.CellStyle.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:background-color", this.CellStyle.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("background-color", value, "fo");
				}
				this._node.SelectSingleNode("@fo:background-color", this.CellStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		public string Border
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:border", this.CellStyle.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:border", this.CellStyle.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("border", value, "fo");
				}
				this._node.SelectSingleNode("@fo:border", this.CellStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		public string BorderBottom
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:border-bottom", this.CellStyle.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:border-bottom", this.CellStyle.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("border-bottom", value, "fo");
				}
				this._node.SelectSingleNode("@fo:border-bottom", this.CellStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		public string BorderLeft
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:border-left", this.CellStyle.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:border-left", this.CellStyle.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("border-left", value, "fo");
				}
				this._node.SelectSingleNode("@fo:border-left", this.CellStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		public string BorderRight
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:border-right", this.CellStyle.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:border-right", this.CellStyle.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("border-right", value, "fo");
				}
				this._node.SelectSingleNode("@fo:border-right", this.CellStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		public string BorderTop
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:border-top", this.CellStyle.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:border-top", this.CellStyle.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("border-top", value, "fo");
				}
				this._node.SelectSingleNode("@fo:border-top", this.CellStyle.Document.NamespaceManager).InnerText = value;
			}
		}

		public AODL.Document.Styles.CellStyle CellStyle
		{
			get
			{
				return (AODL.Document.Styles.CellStyle)this.Style;
			}
			set
			{
				this.Style = value;
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

		public string Padding
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:padding", this.CellStyle.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:padding", this.CellStyle.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("padding", value, "fo");
				}
				this._node.SelectSingleNode("@fo:padding", this.CellStyle.Document.NamespaceManager).InnerText = value;
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

		public CellProperties(AODL.Document.Styles.CellStyle cellstyle)
		{
			this.CellStyle = cellstyle;
			this.NewXmlNode();
			this.Padding = "0.097cm";
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.CellStyle.Document.CreateAttribute(name, prefix);
			xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		public string GetHtmlStyle()
		{
			string str = "style=\"";
			if (this.BackgroundColor == null)
			{
				str = string.Concat(str, "background-color: #FFFFFF; ");
			}
			else
			{
				str = (this.BackgroundColor.ToLower() == "transparent" ? string.Concat(str, "background-color: #FFFFFF; ") : string.Concat(str, "background-color: ", this.BackgroundColor, "; "));
			}
			str = (str.EndsWith("; ") ? string.Concat(str, "\"") : "");
			return str;
		}

		private void NewXmlNode()
		{
			this.Node = this.Style.Document.CreateNode("table-cell-properties", "style");
		}
	}
}