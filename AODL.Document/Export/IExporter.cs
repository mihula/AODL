using AODL.Document;
using System;
using System.Collections;

namespace AODL.Document.Export
{
	public interface IExporter
	{
		ArrayList DocumentSupportInfos
		{
			get;
		}

		ArrayList ExportError
		{
			get;
		}

		void Export(IDocument document, string filename);
	}
}