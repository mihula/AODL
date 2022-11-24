using AODL.Document.Content;
using AODL.Document.Styles;
using AODL.Document.TextDocuments;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document
{
	public interface IDocument
	{
		IStyleCollection CommonStyles
		{
			get;
			set;
		}

		IContentCollection Content
		{
			get;
			set;
		}

		DocumentConfiguration2 DocumentConfigurations2
		{
			get;
			set;
		}

		AODL.Document.TextDocuments.DocumentMetadata DocumentMetadata
		{
			get;
			set;
		}

		DocumentPictureCollection DocumentPictures
		{
			get;
			set;
		}

		DocumentPictureCollection DocumentThumbnails
		{
			get;
			set;
		}

		ArrayList FontList
		{
			get;
			set;
		}

		ArrayList Graphics
		{
			get;
		}

		bool IsLoadedFile
		{
			get;
		}

		XmlNamespaceManager NamespaceManager
		{
			get;
			set;
		}

		IStyleCollection Styles
		{
			get;
			set;
		}

		XmlDocument XmlDoc
		{
			get;
			set;
		}

		XmlAttribute CreateAttribute(string name, string prefix);

		XmlNode CreateNode(string name, string prefix);

		void Load(string file);

		void SaveTo(string filename);
	}
}