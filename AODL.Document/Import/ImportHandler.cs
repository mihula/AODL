using AODL.Document;
using AODL.Document.Exceptions;
using AODL.Document.Export;
using AODL.Document.Import.OpenDocument;
using AODL.Document.Import.PlainText;
using System;
using System.Collections;
using System.Diagnostics;

namespace AODL.Document.Import
{
	public class ImportHandler
	{
		public ImportHandler()
		{
		}

		public IImporter GetFirstImporter(DocumentTypes documentType, string loadPath)
		{
			IImporter importer;
			string extension = ExportHandler.GetExtension(loadPath);
			IEnumerator enumerator = this.LoadImporter().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					IImporter current = (IImporter)enumerator.Current;
					IEnumerator enumerator1 = current.DocumentSupportInfos.GetEnumerator();
					try
					{
						while (enumerator1.MoveNext())
						{
							DocumentSupportInfo documentSupportInfo = (DocumentSupportInfo)enumerator1.Current;
							if (!documentSupportInfo.Extension.ToLower().Equals(extension.ToLower()) || documentSupportInfo.DocumentType != documentType)
							{
								continue;
							}
							importer = current;
							return importer;
						}
					}
					finally
					{
						IDisposable disposable = enumerator1 as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
				}
				AODLException aODLException = new AODLException(string.Concat("No importer available for type ", documentType.ToString(), " and extension ", extension))
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true))
				};
				throw aODLException;
			}
			finally
			{
				IDisposable disposable1 = enumerator as IDisposable;
				if (disposable1 != null)
				{
					disposable1.Dispose();
				}
			}
			return importer;
		}

		private ArrayList LoadImporter()
		{
			ArrayList arrayList;
			try
			{
				ArrayList arrayList1 = new ArrayList();
				arrayList1.Add(new OpenDocumentImporter());
				arrayList1.Add(new PlainTextImporter());
				arrayList1.Add(new CsvImporter());
				arrayList = arrayList1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Error while trying to load the importer.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			return arrayList;
		}
	}
}