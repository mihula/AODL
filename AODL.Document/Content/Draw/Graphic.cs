using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Content;
using AODL.Document.Styles;
using AODL.Document.TextDocuments;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Content.Draw
{
	public class Graphic : IContent, IContentContainer
	{
		private string _graphicRealPath;

		private AODL.Document.Content.Draw.Frame _frame;

		private IDocument _document;

		private IStyle _style;

		private XmlNode _node;

		private IContentCollection _content;

		public string Actuate
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@xlink:actuate", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@xlink:actuate", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("actuate", value, "xlink");
				}
				this._node.SelectSingleNode("@xlink:actuate", this.Document.NamespaceManager).InnerText = value;
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

		public AODL.Document.Content.Draw.Frame Frame
		{
			get
			{
				return this._frame;
			}
			set
			{
				this._frame = value;
			}
		}

		public string GraphicRealPath
		{
			get
			{
				return this._graphicRealPath;
			}
			set
			{
				this._graphicRealPath = value;
			}
		}

		public string HRef
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@xlink:href", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@xlink:href", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("href", value, "xlink");
				}
				this._node.SelectSingleNode("@xlink:href", this.Document.NamespaceManager).InnerText = value;
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

		public string Show
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@xlink:show", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@xlink:show", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("show", value, "xlink");
				}
				this._node.SelectSingleNode("@xlink:show", this.Document.NamespaceManager).InnerText = value;
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
				XmlNode xmlNode = this._node.SelectSingleNode("@text:style-name", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@text:style-name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("style-name", value, "text");
				}
				this._node.SelectSingleNode("@text:style-name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string XLinkType
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@xlink:type", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@xlink:type", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("type", value, "xlink");
				}
				this._node.SelectSingleNode("@xlink:type", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public Graphic(IDocument document, AODL.Document.Content.Draw.Frame frame, string graphiclink)
		{
			this.Frame = frame;
			this.Document = document;
			this.NewXmlNode(string.Concat("Pictures/", graphiclink));
			this.InitStandards();
			this.Document.Graphics.Add(this);
			DocumentMetadata documentMetadata = this.Document.DocumentMetadata;
			documentMetadata.ImageCount = documentMetadata.ImageCount + 1;
		}

		private void Content_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IContent)value).Node);
		}

		private void Content_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IContent)value).Node);
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
			xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		private void InitStandards()
		{
			this.Content = new IContentCollection();
			this.Content.Inserted += new CollectionWithEvents.CollectionChange(this.Content_Inserted);
			this.Content.Removed += new CollectionWithEvents.CollectionChange(this.Content_Removed);
		}

		private void NewXmlNode(string graphiclink)
		{
			this.Node = this.Document.CreateNode("image", "draw");
			XmlAttribute xmlAttribute = this.Document.CreateAttribute("href", "xlink");
			xmlAttribute.Value=(graphiclink);
			this.Node.Attributes.Append(xmlAttribute);
			xmlAttribute = this.Document.CreateAttribute("type", "xlink");
			xmlAttribute.Value=("standard");
			this.Node.Attributes.Append(xmlAttribute);
			xmlAttribute = this.Document.CreateAttribute("show", "xlink");
			xmlAttribute.Value=("embed");
			this.Node.Attributes.Append(xmlAttribute);
			xmlAttribute = this.Document.CreateAttribute("actuate", "xlink");
			xmlAttribute.Value=("onLoad");
			this.Node.Attributes.Append(xmlAttribute);
		}
	}
}