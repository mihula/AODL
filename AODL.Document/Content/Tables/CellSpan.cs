using AODL.Document;
using AODL.Document.Content;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Content.Tables
{
	public class CellSpan : IContent
	{
		private AODL.Document.Content.Tables.Row _row;

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

		public AODL.Document.Content.Tables.Row Row
		{
			get
			{
				return this._row;
			}
			set
			{
				this._row = value;
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

		public CellSpan(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.Node = node;
		}

		public CellSpan(AODL.Document.Content.Tables.Row row, IDocument document)
		{
			this.Document = document;
			this.NewXmlNode();
		}

		private void NewXmlNode()
		{
			this.Node = this.Document.CreateNode("covered-table-cell", "table");
		}
	}
}