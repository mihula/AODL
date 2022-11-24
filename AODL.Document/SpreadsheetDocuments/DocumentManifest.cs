using AODL.Document.TextDocuments;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace AODL.Document.SpreadsheetDocuments
{
	public class DocumentManifest : AODL.Document.TextDocuments.DocumentManifest
	{
		public DocumentManifest()
		{
		}

		public override void New()
		{
			try
			{
				Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AODL.Resources.OD.spreadsheetmanifest.xml");
				base.Manifest = new XmlDocument();
				base.Manifest.Load(manifestResourceStream);
			}
			catch (Exception exception)
			{
				throw;
			}
		}
	}
}