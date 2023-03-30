using AODL.Document.TextDocuments;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace AODL.Document.SpreadsheetDocuments
{
	public class DocumentStyles : AODL.Document.TextDocuments.DocumentStyles
	{
		public DocumentStyles()
		{
		}

		public override void New()
		{
			try
			{
				Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AODL.AODL.Resources.OD.spreadsheetstyles.xml");
				base.Styles = new XmlDocument();
				base.Styles.Load(manifestResourceStream);
			}
			catch (Exception exception)
			{
				throw;
			}
		}
	}
}