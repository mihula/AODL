using AODL.Document;
using AODL.Document.Content.Text;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Content.Text.TextControl
{
	public class TabStop : IText
	{
		private XmlNode _node;

		private IDocument _document;

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
				return null;
			}
			set
			{
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
				return null;
			}
			set
			{
			}
		}

		public TabStop(IDocument document)
		{
			this.Document = document;
			this.NewXmlNode();
		}

		private void NewXmlNode()
		{
			this.Node = this.Document.CreateNode("tab", "text");
		}
	}
}