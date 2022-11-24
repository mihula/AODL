using AODL.Document;
using AODL.Document.Content;
using AODL.Document.Content.Text;
using AODL.Document.Exceptions;
using AODL.Document.Export;
using AODL.Document.Import;
using System;
using System.Collections;
using System.IO;

namespace AODL.Document.Import.PlainText
{
	public class PlainTextImporter : IImporter, IPublisherInfo
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

		public PlainTextImporter()
		{
			this._importError = new ArrayList();
			this._supportedExtensions = new ArrayList();
			this._supportedExtensions.Add(new DocumentSupportInfo(".txt", DocumentTypes.TextDocument));
			this._author = "Lars Behrmann, lb@OpenDocument4all.com";
			this._infoUrl = "http://AODL.OpenDocument4all.com";
			this._description = "This the standard importer for plain text files of the OpenDocument library AODL.";
		}

		public void Import(IDocument document, string filename)
		{
			try
			{
				this._document = document;
				string str = this.ReadContentFromFile(filename);
				if (str.Length <= 0)
				{
					AODLWarning aODLWarning = new AODLWarning(string.Concat("Empty file. [", filename, "]"));
					this.ImportError.Add(aODLWarning);
				}
				else
				{
					this.ReadTextToDocument(str);
				}
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		private string ReadContentFromFile(string fileName)
		{
			string end = "";
			try
			{
				StreamReader streamReader = File.OpenText(fileName);
				end = streamReader.ReadToEnd();
				streamReader.Close();
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return this.SetConformLineBreaks(end);
		}

		private void ReadTextToDocument(string text)
		{
			ParagraphCollection paragraphCollection = ParagraphBuilder.CreateParagraphCollection(this._document, text, false, ParagraphBuilder.ParagraphSeperator);
			if (paragraphCollection != null)
			{
				foreach (Paragraph paragraph in paragraphCollection)
				{
					this._document.Content.Add(paragraph);
				}
			}
		}

		private string SetConformLineBreaks(string text)
		{
			return text.Replace(ParagraphBuilder.ParagraphSeperator2, ParagraphBuilder.ParagraphSeperator);
		}
	}
}