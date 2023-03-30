using AODL.Document.TextDocuments;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace AODL.Document.SpreadsheetDocuments
{
	public class DocumentSetting : AODL.Document.TextDocuments.DocumentSetting
	{
		public DocumentSetting()
		{
		}

		public override void New()
		{
			try
			{
				Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AODL.AODL.Resources.OD.spreadsheetsettings.xml");
				base.Settings = new XmlDocument();
				base.Settings.Load(manifestResourceStream);
			}
			catch (Exception exception)
			{
				throw;
			}
		}
	}
}