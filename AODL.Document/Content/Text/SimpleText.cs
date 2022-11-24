using AODL.Document;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Content.Text
{
	public class SimpleText : IText, ICloneable
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
				return this.Node.InnerText;
			}
			set
			{
				this.Node.InnerText = value;
			}
		}

		public SimpleText(IDocument document, string simpleText)
		{
			this.Document = document;
			this.NewNode(simpleText);
		}

		public object Clone()
		{
			SimpleText simpleText = null;
			if (this.Document != null && this.Node != null)
			{
				TextContentProcessor textContentProcessor = new TextContentProcessor();
				simpleText = (SimpleText)textContentProcessor.CreateTextObject(this.Document, this.Node.CloneNode(true));
			}
			return simpleText;
		}

		private void NewNode(string simpleText)
		{
			this.Node = this.Document.XmlDoc.CreateTextNode(simpleText);
		}
	}
}