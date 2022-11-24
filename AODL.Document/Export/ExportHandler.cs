using AODL.Document;
using AODL.Document.Exceptions;
using AODL.Document.Export.Html;
using AODL.Document.Export.OpenDocument;
using System;
using System.Collections;
using System.Diagnostics;

namespace AODL.Document.Export
{
	public class ExportHandler
	{
		public ExportHandler()
		{
		}

		public static string GetExtension(string aFullPathOrFileName)
		{
			return aFullPathOrFileName.Substring(aFullPathOrFileName.LastIndexOf("."));
		}

		public IExporter GetFirstExporter(DocumentTypes documentType, string savePath)
		{
			IExporter exporter;
			string extension = ExportHandler.GetExtension(savePath);
			IEnumerator enumerator = this.LoadExporter().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					IExporter current = (IExporter)enumerator.Current;
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
							exporter = current;
							return exporter;
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
				AODLException aODLException = new AODLException(string.Concat("No exporter available for type ", documentType.ToString(), " and extension ", extension))
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
			return exporter;
		}

		private ArrayList LoadExporter()
		{
			ArrayList arrayList;
			try
			{
				ArrayList arrayList1 = new ArrayList();
				arrayList1.Add(new OpenDocumentTextExporter());
				arrayList1.Add(new OpenDocumentHtmlExporter());
				arrayList = arrayList1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Error while trying to load the exporter.")
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