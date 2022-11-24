using AODL.Document;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Styles.Properties
{
	public class ParagraphProperties : IProperty, IHtmlStyle
	{
		private AODL.Document.Styles.TabStopStyleCollection _tabstopstylecollection;

		private XmlNode _node;

		private IStyle _style;

		public string Alignment
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:text-align", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:text-align", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("text-align", value, "fo");
				}
				this._node.SelectSingleNode("@fo:text-align", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

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

		public string Border
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:border", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:border", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("border", value, "fo");
				}
				this._node.SelectSingleNode("@fo:border", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string BreakBefore
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:break-before", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:break-before", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("break-before", value, "fo");
				}
				this._node.SelectSingleNode("@fo:break-before", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string LineSpacing
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:line-height", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:line-height", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("line-height", value, "fo");
				}
				this._node.SelectSingleNode("@fo:line-height", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string MarginLeft
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:margin-left", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:margin-left", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("margin-left", value, "fo");
				}
				this._node.SelectSingleNode("@fo:margin-left", this.Style.Document.NamespaceManager).InnerText = value;
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
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:padding", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:padding", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("padding", value, "fo");
				}
				this._node.SelectSingleNode("@fo:padding", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public ParagraphStyle Paragraphstyle
		{
			get
			{
				return (ParagraphStyle)this.Style;
			}
			set
			{
				this.Style = value;
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

		public AODL.Document.Styles.TabStopStyleCollection TabStopStyleCollection
		{
			get
			{
				return this._tabstopstylecollection;
			}
			set
			{
				if (this.Style.StyleName != "Standard")
				{
					if (this._tabstopstylecollection != null)
					{
						this.Node.RemoveChild(this._tabstopstylecollection.Node);
						this._tabstopstylecollection = null;
					}
					this._tabstopstylecollection = value;
					if (this.Node.SelectSingleNode("style:tab-stops", this.Style.Document.NamespaceManager) == null)
					{
						this.Node.AppendChild(this._tabstopstylecollection.Node);
					}
				}
			}
		}

		public ParagraphProperties(IStyle style)
		{
			this.Style = style;
			this.NewXmlNode();
		}

		public ParagraphProperties(IStyle style, XmlNode node)
		{
			this.Style = style;
			this.Node = node;
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
			if (this.Alignment != null)
			{
				str = string.Concat(str, "text-align: ", this.Alignment, "; ");
			}
			if (this.MarginLeft != null)
			{
				str = string.Concat(str, "text-indent: ", this.MarginLeft, "; ");
			}
			if (this.LineSpacing != null)
			{
				str = string.Concat(str, "line-height: ", this.LineSpacing, "; ");
			}
			if (this.Border != null && this.Padding == null)
			{
				str = string.Concat(str, "border-width:1px; border-style:solid; padding: 0.5cm; ");
			}
			if (this.Border != null && this.Padding != null)
			{
				str = string.Concat(str, "border-width:1px; border-style:solid; padding:", this.Padding, "; ");
			}
			str = (str.EndsWith("; ") ? string.Concat(str, "\"") : "");
			return str;
		}

		private void NewXmlNode()
		{
			this.Node = this.Style.Document.CreateNode("paragraph-properties", "style");
		}
	}
}