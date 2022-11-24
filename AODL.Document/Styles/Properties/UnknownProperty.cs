using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Styles.Properties
{
	public class UnknownProperty : IProperty
	{
		private XmlNode _node;

		private IStyle _style;

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

		public UnknownProperty(IStyle style, XmlNode node)
		{
			this.Style = style;
			this.Node = node;
		}
	}
}