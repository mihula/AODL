using AODL.Document;
using AODL.Document.Styles.Properties;
using System;
using System.Xml;

namespace AODL.Document.Styles
{
	public class UnknownStyle : IStyle
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

		public IPropertyCollection PropertyCollection
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
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:name", this.Document.NamespaceManager);
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
			}
		}

		public UnknownStyle(IDocument document, XmlNode unknownNode)
		{
			this.Document = document;
			this.Node = unknownNode;
		}
	}
}