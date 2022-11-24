using AODL.Document;
using AODL.Document.Styles.Properties;
using System;
using System.Xml;

namespace AODL.Document.Styles
{
	public interface IStyle
	{
		IDocument Document
		{
			get;
			set;
		}

		XmlNode Node
		{
			get;
			set;
		}

		IPropertyCollection PropertyCollection
		{
			get;
			set;
		}

		string StyleName
		{
			get;
			set;
		}
	}
}