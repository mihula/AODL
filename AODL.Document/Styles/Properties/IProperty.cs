using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Styles.Properties
{
	public interface IProperty
	{
		XmlNode Node
		{
			get;
			set;
		}

		IStyle Style
		{
			get;
			set;
		}
	}
}