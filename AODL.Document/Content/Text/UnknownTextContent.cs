using AODL.Document;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Content.Text
{
	public class UnknownTextContent : IText
	{
		private IDocument _document;

		private IStyle _style;

		private XmlNode _node;

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

		public string GetElementName
		{
			get
			{
				string name;
				if (this.Node == null)
				{
					name = null;
				}
				else
				{
					name = this.Node.Name;
				}
				return name;
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
				this.StyleName = value.StyleName;
				this._style = value;
			}
		}

		public string StyleName
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string Text
		{
			get
			{
				return this.Node.InnerText;
			}
			set
			{
				this.Node.InnerText = value;
			}
		}

		public UnknownTextContent(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.Node = node;
		}
	}
}