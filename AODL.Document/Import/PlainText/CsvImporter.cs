using AODL.Document;
using AODL.Document.Content;
using AODL.Document.Content.Tables;
using AODL.Document.Content.Text;
using AODL.Document.Exceptions;
using AODL.Document.Export;
using AODL.Document.Import;
using AODL.Document.SpreadsheetDocuments;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace AODL.Document.Import.PlainText
{
	public class CsvImporter : IImporter, IPublisherInfo
	{
		internal IDocument _document;

		private ArrayList _supportedExtensions;

		private ArrayList _importError;

		private string _author;

		private string _infoUrl;

		private string _description;

		public string Author
		{
			get
			{
				return this._author;
			}
		}

		public string Description
		{
			get
			{
				return this._description;
			}
		}

		public ArrayList DocumentSupportInfos
		{
			get
			{
				return this._supportedExtensions;
			}
		}

		public ArrayList ImportError
		{
			get
			{
				return this._importError;
			}
		}

		public string InfoUrl
		{
			get
			{
				return this._infoUrl;
			}
		}

		public bool NeedNewOpenDocument
		{
			get
			{
				return true;
			}
		}

		public CsvImporter()
		{
			this._importError = new ArrayList();
			this._supportedExtensions = new ArrayList();
			this._supportedExtensions.Add(new DocumentSupportInfo(".csv", DocumentTypes.SpreadsheetDocument));
			this._author = "Lars Behrmann, lb@OpenDocument4all.com";
			this._infoUrl = "http://AODL.OpenDocument4all.com";
			this._description = "This the standard importer for comma seperated text files of the OpenDocument library AODL.";
		}

		private void CreateTables(ArrayList lines)
		{
			string str = "¿";
			if (lines != null)
			{
				Table table = TableBuilder.CreateSpreadsheetTable((SpreadsheetDocument)this._document, "Table1", "table1");
				string item = lines[0] as string;
				lines.RemoveAt(0);
				try
				{
					foreach (string line in lines)
					{
						string str1 = line.Replace(item, str);
						string[] strArray = str1.Split(str.ToCharArray());
						Row row = new Row(table);
						string[] strArray1 = strArray;
						for (int i = 0; i < (int)strArray1.Length; i++)
						{
							string str2 = strArray1[i];
							Cell cell = new Cell(table);
							Paragraph paragraph = ParagraphBuilder.CreateSpreadsheetParagraph(this._document);
							paragraph.TextContent.Add(new SimpleText(this._document, str2));
							cell.Content.Add(paragraph);
							row.InsertCellAt(row.CellCollection.Count, cell);
						}
						table.RowCollection.Add(row);
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					AODLException aODLException = new AODLException("Error while proccessing the csv file.")
					{
						InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
						OriginalException = exception
					};
					throw aODLException;
				}
				this._document.Content.Add(table);
			}
		}

		private ArrayList GetFileContent(string fileName)
		{
			ArrayList arrayList = new ArrayList();
			try
			{
				StreamReader streamReader = File.OpenText(fileName);
				string str = null;
				while (true)
				{
					string str1 = streamReader.ReadLine();
					str = str1;
					if (str1 == null)
					{
						break;
					}
					arrayList.Add(str);
				}
				streamReader.Close();
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return arrayList;
		}

		public void Import(IDocument document, string filename)
		{
			try
			{
				this._document = document;
				ArrayList fileContent = this.GetFileContent(filename);
				if (fileContent.Count <= 0)
				{
					AODLWarning aODLWarning = new AODLWarning(string.Concat("Empty file. [", filename, "]"));
					this.ImportError.Add(aODLWarning);
				}
				else
				{
					this.CreateTables(fileContent);
				}
			}
			catch (Exception exception)
			{
				throw;
			}
		}
	}
}