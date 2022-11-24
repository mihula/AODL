using AODL.Document;
using AODL.Document.Content.Draw;
using AODL.Document.Exceptions;
using AODL.Document.Export;
using AODL.Document.Import.OpenDocument;
using AODL.Document.SpreadsheetDocuments;
using AODL.Document.TextDocuments;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace AODL.Document.Export.OpenDocument
{
	public class OpenDocumentTextExporter : IExporter, IPublisherInfo
	{
		private readonly static string dir;

		private string[] _directories = new string[] { "Configurations2", "META-INF", "Pictures", "Thumbnails" };

		private IDocument _document = null;

		private ArrayList _supportedExtensions;

		private ArrayList _exportError;

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

		public ArrayList ExportError
		{
			get
			{
				return this._exportError;
			}
		}

		public string InfoUrl
		{
			get
			{
				return this._infoUrl;
			}
		}

		static OpenDocumentTextExporter()
		{
			OpenDocumentTextExporter.dir = string.Concat(Environment.CurrentDirectory, "\\aodlwrite\\");
		}

		public OpenDocumentTextExporter()
		{
			this._exportError = new ArrayList();
			this._supportedExtensions = new ArrayList();
			this._supportedExtensions.Add(new DocumentSupportInfo(".odt", DocumentTypes.TextDocument));
			this._supportedExtensions.Add(new DocumentSupportInfo(".ods", DocumentTypes.SpreadsheetDocument));
			this._author = "Lars Behrmann, lb@OpenDocument4all.com";
			this._infoUrl = "http://AODL.OpenDocument4all.com";
			this._description = "This the standard OpenDocument format exporter of the OpenDocument library AODL.";
		}

		internal static void CleanUpReadAndWriteDirectories()
		{
			try
			{
				if (Directory.Exists(OpenDocumentImporter.dir))
				{
					Directory.Delete(OpenDocumentImporter.dir, true);
				}
				if (Directory.Exists(OpenDocumentImporter.dir))
				{
					Directory.Delete(OpenDocumentImporter.dir, true);
				}
				if (Directory.Exists(OpenDocumentTextExporter.dir))
				{
					Directory.Delete(OpenDocumentTextExporter.dir, true);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLWarning aODLWarning = new AODLWarning("An exception ouccours while trying to remove the temp read directories.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw exception;
			}
		}

		private static void CopyGraphics(IDocument document, string directory)
		{
			try
			{
				string str = string.Concat(directory, "\\Pictures\\");
				foreach (Graphic graphic in document.Graphics)
				{
					if (graphic.GraphicRealPath == null)
					{
						continue;
					}
					FileInfo fileInfo = new FileInfo(graphic.GraphicRealPath);
					if (File.Exists(string.Concat(str, fileInfo.Name)))
					{
						continue;
					}
					File.Copy(graphic.GraphicRealPath, string.Concat(str, fileInfo.Name));
				}
				OpenDocumentTextExporter.MovePicturesIfLoaded(document, str);
			}
			catch (Exception exception)
			{
				Console.WriteLine("CopyGraphics: {0}", exception.Message);
				throw;
			}
		}

		private static void CreateOpenDocument(string filename, string directory)
		{
			try
			{
				FastZip fastZip = new FastZip()
				{
					CreateEmptyDirectories = true
				};
				fastZip.CreateZip(filename, directory, true, "");
				fastZip = null;
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		public void Export(IDocument document, string filename)
		{
			try
			{
				this._document = document;
				this.PrepareDirectory(OpenDocumentTextExporter.dir);
				if (!(document is TextDocument))
				{
					if (!(document is SpreadsheetDocument))
					{
						throw new Exception("Unsupported document type!");
					}
					this.WriteSingleFiles(((SpreadsheetDocument)document).DocumentManifest.Manifest, string.Concat(OpenDocumentTextExporter.dir, AODL.Document.TextDocuments.DocumentManifest.FolderName, "\\", AODL.Document.TextDocuments.DocumentManifest.FileName));
					this.WriteSingleFiles(((SpreadsheetDocument)document).DocumentMetadata.Meta, string.Concat(OpenDocumentTextExporter.dir, DocumentMetadata.FileName));
					this.WriteSingleFiles(((SpreadsheetDocument)document).DocumentSetting.Settings, string.Concat(OpenDocumentTextExporter.dir, AODL.Document.TextDocuments.DocumentSetting.FileName));
					this.WriteSingleFiles(((SpreadsheetDocument)document).DocumentStyles.Styles, string.Concat(OpenDocumentTextExporter.dir, AODL.Document.TextDocuments.DocumentStyles.FileName));
					this.WriteSingleFiles(((SpreadsheetDocument)document).XmlDoc, string.Concat(OpenDocumentTextExporter.dir, "content.xml"));
				}
				else
				{
					this.WriteSingleFiles(((TextDocument)document).DocumentManifest.Manifest, string.Concat(OpenDocumentTextExporter.dir, AODL.Document.TextDocuments.DocumentManifest.FolderName, "\\", AODL.Document.TextDocuments.DocumentManifest.FileName));
					this.WriteSingleFiles(((TextDocument)document).DocumentMetadata.Meta, string.Concat(OpenDocumentTextExporter.dir, DocumentMetadata.FileName));
					this.WriteSingleFiles(((TextDocument)document).DocumentSetting.Settings, string.Concat(OpenDocumentTextExporter.dir, AODL.Document.TextDocuments.DocumentSetting.FileName));
					this.WriteSingleFiles(((TextDocument)document).DocumentStyles.Styles, string.Concat(OpenDocumentTextExporter.dir, AODL.Document.TextDocuments.DocumentStyles.FileName));
					this.WriteSingleFiles(((TextDocument)document).XmlDoc, string.Concat(OpenDocumentTextExporter.dir, "content.xml"));
					OpenDocumentTextExporter.SaveGraphic((TextDocument)document, OpenDocumentTextExporter.dir);
				}
				this.WriteMimetypeFile(string.Concat(OpenDocumentTextExporter.dir, "\\mimetyp"));
				OpenDocumentTextExporter.CreateOpenDocument(filename, OpenDocumentTextExporter.dir);
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		private static void MovePicturesIfLoaded(IDocument document, string targetDir)
		{
		}

		private void PrepareDirectory(string directory)
		{
			try
			{
				if (Directory.Exists(directory))
				{
					Directory.Delete(directory, true);
				}
				string[] strArray = this._directories;
				for (int i = 0; i < (int)strArray.Length; i++)
				{
					string str = strArray[i];
					Directory.CreateDirectory(string.Concat(directory, "\\", str));
				}
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		private void SaveExistingGraphics(DocumentPictureCollection pictures, string folder)
		{
			try
			{
				foreach (DocumentPicture picture in pictures)
				{
					if (File.Exists(string.Concat(folder, picture.ImageName)))
					{
						continue;
					}
					picture.Image.Save(string.Concat(folder, picture.ImageName));
				}
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		internal static void SaveGraphic(IDocument document, string directory)
		{
			OpenDocumentTextExporter.CopyGraphics(document, directory);
		}

		private void WriteMimetypeFile(string file)
		{
			try
			{
				if (File.Exists(file))
				{
					File.Delete(file);
				}
				StreamWriter streamWriter = File.CreateText(file);
				if (this._document is TextDocument)
				{
					streamWriter.WriteLine("application/vnd.oasis.opendocument.text");
				}
				else if (this._document is SpreadsheetDocument)
				{
					streamWriter.WriteLine("application/vnd.oasis.opendocument.spreadsheet");
				}
				streamWriter.Close();
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		private void WriteSingleFiles(XmlDocument document, string filename)
		{
			try
			{
				XmlTextWriter xmlTextWriter = new XmlTextWriter(filename, Encoding.UTF8);
				xmlTextWriter.Formatting = Formatting.None;
				document.WriteContentTo(xmlTextWriter);
				xmlTextWriter.Flush();
				xmlTextWriter.Close();
			}
			catch (Exception exception)
			{
				throw;
			}
		}
	}
}