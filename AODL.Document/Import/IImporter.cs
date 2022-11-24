using AODL.Document;
using System;
using System.Collections;

namespace AODL.Document.Import
{
	public interface IImporter
	{
		ArrayList DocumentSupportInfos
		{
			get;
		}

		ArrayList ImportError
		{
			get;
		}

		bool NeedNewOpenDocument
		{
			get;
		}

		void Import(IDocument document, string filename);
	}
}