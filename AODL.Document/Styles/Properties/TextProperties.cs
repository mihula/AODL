using AODL.Document;
using AODL.Document.Styles;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Styles.Properties
{
	public class TextProperties : IProperty, IHtmlStyle
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

		public string Bold
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:font-weight", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:font-weight", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("font-weight", value, "fo");
				}
				this._node.SelectSingleNode("@fo:font-weight", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string FontColor
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:color", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:color", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("color", value, "fo");
				}
				this._node.SelectSingleNode("@fo:color", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string FontName
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:font-name", this.Style.Document.NamespaceManager);
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
				this.Style.Document.FontList.Add(value);
				if (this._node.SelectSingleNode("@style:font-name", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("font-name", value, "style");
				}
				this._node.SelectSingleNode("@style:font-name", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string FontSize
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:font-size", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:font-size", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("font-size", value, "fo");
				}
				this._node.SelectSingleNode("@fo:font-size", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string Italic
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:font-style", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:font-style", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("font-style", value, "fo");
				}
				this._node.SelectSingleNode("@fo:font-style", this.Style.Document.NamespaceManager).InnerText = value;
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

		public string Outline
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:text-outline", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:text-outline", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("text-outline", value, "style");
				}
				this._node.SelectSingleNode("@style:text-outline", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string Position
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:text-position", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:text-position", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("text-position", value, "style");
				}
				this._node.SelectSingleNode("@style:text-position", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string Shadow
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:text-shadow", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:text-shadow", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("text-shadow", value, "style");
				}
				this._node.SelectSingleNode("@style:text-shadow", this.Style.Document.NamespaceManager).InnerText = value;
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

		public string TextLineThrough
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:text-line-through-style", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:text-line-through-style", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("text-line-through-style", value, "style");
				}
				this._node.SelectSingleNode("@style:text-line-through-style", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string Underline
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:text-underline-style", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:text-underline-style", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("text-underline-style", value, "style");
				}
				this._node.SelectSingleNode("@style:text-underline-style", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string UnderlineColor
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:text-underline-color", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:text-underline-color", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("text-underline-color", value, "style");
				}
				this._node.SelectSingleNode("@style:text-underline-color", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string UnderlineWidth
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:text-underline-width", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:text-underline-width", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("text-underline-width", value, "style");
				}
				this._node.SelectSingleNode("@style:text-underline-width", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public TextProperties(IStyle style)
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
			if (this.Italic != null && this.Italic != "normal")
			{
				str = string.Concat(str, "font-style: italic; ");
			}
			if (this.Bold != null)
			{
				str = string.Concat(str, "font-weight: ", this.Bold, "; ");
			}
			if (this.Underline != null)
			{
				str = string.Concat(str, "text-decoration: underline; ");
			}
			if (this.TextLineThrough != null)
			{
				str = string.Concat(str, "text-decoration: line-through; ");
			}
			if (this.FontName != null)
			{
				str = string.Concat(str, "font-family: ", FontFamilies.HtmlFont(this.FontName), "; ");
			}
			if (this.FontSize != null)
			{
				str = string.Concat(str, "font-size: ", FontFamilies.PtToPx(this.FontSize), "; ");
			}
			if (this.FontColor != null)
			{
				str = string.Concat(str, "color: ", this.FontColor, "; ");
			}
			if (this.BackgroundColor != null)
			{
				str = string.Concat(str, "background-color: ", this.BackgroundColor, "; ");
			}
			str = (str.EndsWith("; ") ? string.Concat(str, "\"") : "");
			return str;
		}

		private void NewXmlNode()
		{
			this.Node = this.Style.Document.CreateNode("text-properties", "style");
		}

		public void SetUnderlineStyles(string style, string width, string color)
		{
			this.Underline = style;
			this.UnderlineWidth = width;
			this.UnderlineColor = color;
		}
	}
}