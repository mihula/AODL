using AODL.Document;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Styles.Properties
{
	public class GraphicProperties : IProperty
	{
		private XmlNode _node;

		private IStyle _style;

		public string Clip
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@fo:clip", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@fo:clip", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("clip", value, "fo");
				}
				this._node.SelectSingleNode("@fo:clip", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public bool ColorInversion
		{
			get
			{
				bool flag;
				XmlNode xmlNode = this._node.SelectSingleNode("@draw:color-inversion", this.Style.Document.NamespaceManager);
				flag = (xmlNode == null ? false : Convert.ToBoolean(xmlNode.InnerText));
				return flag;
			}
			set
			{
				string str = (value ? "true" : "false");
				if (this._node.SelectSingleNode("@draw:color-inversion", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("color-inversion", str, "draw");
				}
				this._node.SelectSingleNode("@draw:color-inversion", this.Style.Document.NamespaceManager).InnerText=(str);
			}
		}

		public string ColorMode
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@draw:color-mode", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@draw:color-mode", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("color-mode", value, "draw");
				}
				this._node.SelectSingleNode("@draw:color-mode", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string ContrastInProcent
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@draw:contrast", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@draw:contrast", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("contrast", value, "draw");
				}
				this._node.SelectSingleNode("@draw:contrast", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string DrawBlueInProcent
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@draw:blue", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@draw:blue", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("blue", value, "draw");
				}
				this._node.SelectSingleNode("@draw:blue", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string DrawGammaInProcent
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@draw:gamma", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@draw:gamma", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("gamma", value, "draw");
				}
				this._node.SelectSingleNode("@draw:gamma", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string DrawGreenInProcent
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@draw:green", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@draw:green", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("green", value, "draw");
				}
				this._node.SelectSingleNode("@draw:green", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string DrawRedInProcent
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@draw:red", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@draw:red", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("red", value, "draw");
				}
				this._node.SelectSingleNode("@draw:red", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public AODL.Document.Styles.FrameStyle FrameStyle
		{
			get
			{
				return (AODL.Document.Styles.FrameStyle)this.Style;
			}
			set
			{
				this.Style = value;
			}
		}

		public string HorizontalPosition
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:horizontal-pos", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:horizontal-pos", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("horizontal-pos", value, "style");
				}
				this._node.SelectSingleNode("@style:horizontal-pos", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string HorizontalRelative
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:horizontal-rel", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:horizontal-rel", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("horizontal-rel", value, "style");
				}
				this._node.SelectSingleNode("@style:horizontal-rel", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string ImageOpacityInProcent
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@draw:image-opacity", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@draw:image-opacity", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("image-opacity", value, "draw");
				}
				this._node.SelectSingleNode("@draw:image-opacity", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string LuminanceInProcent
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@draw:luminance", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@draw:luminance", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("luminance", value, "draw");
				}
				this._node.SelectSingleNode("@draw:luminance", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string Mirror
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:mirror", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:mirror", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("mirror", value, "style");
				}
				this._node.SelectSingleNode("@style:mirror", this.Style.Document.NamespaceManager).InnerText = value;
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

		public string VerticalPosition
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:vertical-pos", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:vertical-pos", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("vertical-pos", value, "style");
				}
				this._node.SelectSingleNode("@style:vertical-pos", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public string VerticalRelative
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:vertical-rel", this.Style.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:vertical-rel", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("vertical-rel", value, "style");
				}
				this._node.SelectSingleNode("@style:vertical-rel", this.Style.Document.NamespaceManager).InnerText = value;
			}
		}

		public GraphicProperties(IStyle style)
		{
			this.Style = style;
			this.NewXmlNode();
			this.InitStandardImplemenation();
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Style.Document.CreateAttribute(name, prefix);
			xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		private void InitStandardImplemenation()
		{
			this.Clip = "rect(0cm 0cm 0cm 0cm)";
			this.ColorInversion = false;
			this.ColorMode = "standard";
			this.ContrastInProcent = "0%";
			this.DrawBlueInProcent = "0%";
			this.DrawGammaInProcent = "100%";
			this.DrawGreenInProcent = "0%";
			this.DrawRedInProcent = "0%";
			this.HorizontalPosition = "center";
			this.HorizontalRelative = "paragraph";
			this.ImageOpacityInProcent = "100%";
			this.LuminanceInProcent = "0%";
			this.Mirror = "none";
		}

		private void NewXmlNode()
		{
			this.Node = this.Style.Document.CreateNode("graphic-properties", "style");
		}
	}
}