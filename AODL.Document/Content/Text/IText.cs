using AODL.Document;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Content.Text
{
	public interface IText
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

		IStyle Style
		{
			get;
			set;
		}

		string StyleName
		{
			get;
			set;
		}

		string Text
		{
			get;
			set;
		}
	}
}