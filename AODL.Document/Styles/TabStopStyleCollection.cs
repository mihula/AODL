using AODL.Document;
using AODL.Document.Collections;
using System;
using System.Collections;
using System.Reflection;
using System.Xml;

namespace AODL.Document.Styles
{
	public class TabStopStyleCollection : CollectionWithEvents
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

		public TabStopStyle this[int index]
		{
			get
			{
				return base.List[index] as TabStopStyle;
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

		public TabStopStyleCollection(IDocument document)
		{
			this.Document = document;
			this.Node = this.Document.CreateNode("tab-stops", "style");
		}

		public int Add(TabStopStyle value)
		{
			this.Node.AppendChild(value.Node);
			return base.List.Add(value);
		}

		public bool Contains(TabStopStyle value)
		{
			return base.List.Contains(value);
		}

		public void Insert(int index, TabStopStyle value)
		{
			this.Node.AppendChild(value.Node);
			base.List.Insert(index, value);
		}

		public void Remove(TabStopStyle value)
		{
			this.Node.RemoveChild(value.Node);
			base.List.Remove(value);
		}
	}
}