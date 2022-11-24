using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Content;
using AODL.Document.Styles;
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Xml;

namespace AODL.Document.Content.Draw
{
	public class Frame : IContent, IContentContainer
	{
		private string _realgraphicname;

		private string _graphicSourcePath;

		private IDocument _document;

		private IStyle _style;

		private XmlNode _node;

		private IContentCollection _content;

		public string AnchorType
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@text:anchor-type", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@text:anchor-type", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("anchor-type", value, "text");
				}
				this._node.SelectSingleNode("@text:anchor-type", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public IContentCollection Content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

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

		public string DrawName
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@draw:name", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@draw:name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("name", value, "draw");
				}
				this._node.SelectSingleNode("@draw:name", this.Document.NamespaceManager).InnerText = value;
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

		public string GraphicSourcePath
		{
			get
			{
				return this._graphicSourcePath;
			}
			set
			{
				this._graphicSourcePath = value;
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

		public string RealGraphicName
		{
			get
			{
				return this._realgraphicname;
			}
			set
			{
				this._realgraphicname = value;
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
				this.StyleName = value.StyleName;
				this._style = value;
			}
		}

		public string StyleName
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@draw:style-name", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@draw:style-name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("style-name", value, "draw");
				}
				this._node.SelectSingleNode("@draw:style-name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string SvgHeight
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@svg:height", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@svg:height", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("height", value, "svg");
				}
				this._node.SelectSingleNode("@svg:height", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string SvgWidth
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@svg:width", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@svg:width", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("width", value, "svg");
				}
				this._node.SelectSingleNode("@svg:width", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string SvgX
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@svg:x", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@svg:x", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("x", value, "svg");
				}
				this._node.SelectSingleNode("@svg:x", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string SvgY
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@svg:y", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@svg:y", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("y", value, "svg");
				}
				this._node.SelectSingleNode("@svg:y", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string ZIndex
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@draw:z-index", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@draw:z-index", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("z-index", value, "draw");
				}
				this._node.SelectSingleNode("@draw:z-index", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public Frame(IDocument document, string stylename)
		{
			this.Document = document;
			this.NewXmlNode();
			this.InitStandards();
			if (stylename != null)
			{
				this.Style = new AODL.Document.Styles.FrameStyle(this.Document, stylename);
				this.StyleName = stylename;
				this.Document.Styles.Add(this.Style);
			}
		}

		public Frame(IDocument document, string stylename, string drawName, string graphicfile)
		{
			this.Document = document;
			this.NewXmlNode();
			this.InitStandards();
			this.StyleName = stylename;
			this.DrawName = drawName;
			this.GraphicSourcePath = graphicfile;
			this._realgraphicname = this.LoadImageFromFile(graphicfile);
			Graphic graphic = new Graphic(this.Document, this, this._realgraphicname)
			{
				GraphicRealPath = this.GraphicSourcePath
			};
			this.Content.Add(graphic);
			this.Style = new AODL.Document.Styles.FrameStyle(this.Document, stylename);
			this.Document.Styles.Add(this.Style);
		}

		private void Content_Inserted(int index, object value)
		{
			if (value is Graphic && ((Graphic)value).Frame == null)
			{
				((Graphic)value).Frame = this;
				if (((Graphic)value).GraphicRealPath != null && this.GraphicSourcePath == null)
				{
					this.GraphicSourcePath = ((Graphic)value).GraphicRealPath;
				}
			}
			this.Node.AppendChild(((IContent)value).Node);
		}

		private void Content_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IContent)value).Node);
			if (value is Graphic && this.Document.Graphics.Contains(value as Graphic))
			{
				this.Document.Graphics.Remove(value as Graphic);
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
			this.AnchorType = "paragraph";
			this.Content = new IContentCollection();
			this.Content.Inserted += new CollectionWithEvents.CollectionChange(this.Content_Inserted);
			this.Content.Removed += new CollectionWithEvents.CollectionChange(this.Content_Removed);
		}

		private string LoadImageFromFile(string graphicfilename)
		{
			string name;
			try
			{
				double num = 37.7928;
				Image image = Image.FromFile(graphicfilename);
				double num1 = Convert.ToDouble(image.Height) / num;
				double num2 = Convert.ToDouble(image.Width) / num;
				this.SvgHeight = string.Concat(num1.ToString("F3").Replace(",", "."), "cm");
				this.SvgWidth = string.Concat(num2.ToString("F3").Replace(",", "."), "cm");
				image.Dispose();
				name = (new FileInfo(graphicfilename)).Name;
			}
			catch (Exception exception)
			{
				throw;
			}
			return name;
		}

		private void NewXmlNode()
		{
			this.Node = this.Document.CreateNode("frame", "draw");
		}
	}
}