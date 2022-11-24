using AODL.Document;
using AODL.Document.Exceptions;
using AODL.Document.Export;
using AODL.Document.Import;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.SpreadsheetDocuments;
using AODL.Document.TextDocuments;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace AODL.Document.Import.OpenDocument
{
	public class OpenDocumentImporter : IImporter, IPublisherInfo
	{
		internal readonly static string dir;

		internal readonly static string dirpics;

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
				return false;
			}
		}

		static OpenDocumentImporter()
		{
			OpenDocumentImporter.dir = string.Concat(Environment.CurrentDirectory, "\\aodlread\\");
			OpenDocumentImporter.dirpics = string.Concat(Environment.CurrentDirectory, "\\PicturesRead\\");
		}

		public OpenDocumentImporter()
		{
			this._importError = new ArrayList();
			this._supportedExtensions = new ArrayList();
			this._supportedExtensions.Add(new DocumentSupportInfo(".odt", DocumentTypes.TextDocument));
			this._supportedExtensions.Add(new DocumentSupportInfo(".ods", DocumentTypes.SpreadsheetDocument));
			this._author = "Lars Behrmann, lb@OpenDocument4all.com";
			this._infoUrl = "http://AODL.OpenDocument4all.com";
			this._description = "This the standard importer of the OpenDocument library AODL.";
		}

		public void Import(IDocument document, string filename)
		{
			try
			{
				this._document = document;
				this.UnpackFiles(filename);
				this.ReadContent();
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		public void ImportCommonStyles()
		{
			string str = "office:document-styles/office:styles";
			string str1 = "office:document-content";
			try
			{
				XmlNode xmlNode = null;
				XmlNode xmlNode1 = null;
				if (this._document is TextDocument)
				{
					xmlNode = ((TextDocument)this._document).DocumentStyles.Styles.SelectSingleNode(str, this._document.NamespaceManager);
				}
				else if (this._document is SpreadsheetDocument)
				{
					xmlNode = ((SpreadsheetDocument)this._document).DocumentStyles.Styles.SelectSingleNode(str, this._document.NamespaceManager);
				}
				xmlNode1 = this._document.XmlDoc.SelectSingleNode(str1, this._document.NamespaceManager);
				if (xmlNode1 != null && xmlNode != null)
				{
					xmlNode = this._document.XmlDoc.ImportNode(xmlNode, true);
					xmlNode1.AppendChild(xmlNode);
				}
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		private void InitMetaData()
		{
			try
			{
				this._document.DocumentMetadata.ImageCount = 0;
				this._document.DocumentMetadata.ObjectCount = 0;
				this._document.DocumentMetadata.ParagraphCount = 0;
				this._document.DocumentMetadata.TableCount = 0;
				this._document.DocumentMetadata.WordCount = 0;
				this._document.DocumentMetadata.CharacterCount = 0;
				this._document.DocumentMetadata.LastModified = DateTime.Now.ToString("s");
			}
			catch (Exception exception)
			{
			}
		}

		private void mcp_OnWarning(AODLWarning warning)
		{
			this._importError.Add(warning);
		}

		private void MovePictures()
		{
		}

		private void ReadContent()
		{
			try
			{
				this._document.XmlDoc = new XmlDocument();
				this._document.XmlDoc.Load(string.Concat(OpenDocumentImporter.dir, "\\content.xml"));
				(new LocalStyleProcessor(this._document, false)).ReadStyles();
				this.ImportCommonStyles();
				(new LocalStyleProcessor(this._document, true)).ReadStyles();
				MainContentProcessor mainContentProcessor = new MainContentProcessor(this._document);
				mainContentProcessor.OnWarning += new MainContentProcessor.Warning(this.mcp_OnWarning);
				TextContentProcessor.OnWarning += new TextContentProcessor.Warning(this.TextContentProcessor_OnWarning);
				mainContentProcessor.ReadContentNodes();
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		private void ReadDocumentConfigurations2()
		{
			try
			{
				if (Directory.Exists(string.Concat(OpenDocumentImporter.dir, DocumentConfiguration2.FolderName)))
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(string.Concat(OpenDocumentImporter.dir, DocumentConfiguration2.FolderName));
					FileInfo[] files = directoryInfo.GetFiles();
					int num = 0;
					if (num < (int)files.Length)
					{
						FileInfo fileInfo = files[num];
						this._document.DocumentConfigurations2.FileName = fileInfo.Name;
						string str = null;
						StreamReader streamReader = new StreamReader(fileInfo.FullName);
						while (true)
						{
							string str1 = streamReader.ReadLine();
							str = str1;
							if (str1 == null)
							{
								break;
							}
							DocumentConfiguration2 documentConfigurations2 = this._document.DocumentConfigurations2;
							documentConfigurations2.Configurations2Content = string.Concat(documentConfigurations2.Configurations2Content, str);
						}
						streamReader.Close();
					}
				}
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		private DocumentPictureCollection ReadImageResources(string folder)
		{
			DocumentPictureCollection documentPictureCollection;
			DocumentPictureCollection documentPictureCollection1 = new DocumentPictureCollection();
			try
			{
				if (Directory.Exists(folder))
				{
					FileInfo[] files = (new DirectoryInfo(folder)).GetFiles();
					for (int i = 0; i < (int)files.Length; i++)
					{
						FileInfo fileInfo = files[i];
						documentPictureCollection1.Add(new DocumentPicture(fileInfo.FullName));
					}
				}
				else
				{
					documentPictureCollection = documentPictureCollection1;
					return documentPictureCollection;
				}
			}
			catch (Exception exception)
			{
				throw;
			}
			documentPictureCollection = documentPictureCollection1;
			return documentPictureCollection;
		}

		private void ReadResources()
		{
			try
			{
				this._document.DocumentConfigurations2 = new DocumentConfiguration2();
				this.ReadDocumentConfigurations2();
				this._document.DocumentMetadata = new DocumentMetadata(this._document);
				this._document.DocumentMetadata.LoadFromFile(string.Concat(OpenDocumentImporter.dir, DocumentMetadata.FileName));
				if (this._document is TextDocument)
				{
					((TextDocument)this._document).DocumentSetting = new AODL.Document.TextDocuments.DocumentSetting();
					string fileName = AODL.Document.TextDocuments.DocumentSetting.FileName;
					((TextDocument)this._document).DocumentSetting.LoadFromFile(string.Concat(OpenDocumentImporter.dir, fileName));
					((TextDocument)this._document).DocumentManifest = new AODL.Document.TextDocuments.DocumentManifest();
					string folderName = AODL.Document.TextDocuments.DocumentManifest.FolderName;
					fileName = AODL.Document.TextDocuments.DocumentManifest.FileName;
					((TextDocument)this._document).DocumentManifest.LoadFromFile(string.Concat(OpenDocumentImporter.dir, folderName, "\\", fileName));
					((TextDocument)this._document).DocumentStyles = new AODL.Document.TextDocuments.DocumentStyles();
					fileName = AODL.Document.TextDocuments.DocumentStyles.FileName;
					((TextDocument)this._document).DocumentStyles.LoadFromFile(string.Concat(OpenDocumentImporter.dir, fileName));
				}
				else if (this._document is SpreadsheetDocument)
				{
					((SpreadsheetDocument)this._document).DocumentSetting = new AODL.Document.SpreadsheetDocuments.DocumentSetting();
					string str = AODL.Document.TextDocuments.DocumentSetting.FileName;
					((SpreadsheetDocument)this._document).DocumentSetting.LoadFromFile(string.Concat(OpenDocumentImporter.dir, str));
					((SpreadsheetDocument)this._document).DocumentManifest = new AODL.Document.SpreadsheetDocuments.DocumentManifest();
					string folderName1 = AODL.Document.TextDocuments.DocumentManifest.FolderName;
					str = AODL.Document.TextDocuments.DocumentManifest.FileName;
					((SpreadsheetDocument)this._document).DocumentManifest.LoadFromFile(string.Concat(OpenDocumentImporter.dir, folderName1, "\\", str));
					((SpreadsheetDocument)this._document).DocumentStyles = new AODL.Document.SpreadsheetDocuments.DocumentStyles();
					str = AODL.Document.TextDocuments.DocumentStyles.FileName;
					((SpreadsheetDocument)this._document).DocumentStyles.LoadFromFile(string.Concat(OpenDocumentImporter.dir, str));
				}
				this._document.DocumentPictures = this.ReadImageResources(string.Concat(OpenDocumentImporter.dir, "Pictures"));
				this._document.DocumentThumbnails = this.ReadImageResources(string.Concat(OpenDocumentImporter.dir, "Thumbnails"));
				this.InitMetaData();
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		private void TextContentProcessor_OnWarning(AODLWarning warning)
		{
			this._importError.Add(warning);
		}

		private void UnpackFiles(string file)
		{
			try
			{
				if (!Directory.Exists(OpenDocumentImporter.dir))
				{
					Directory.CreateDirectory(OpenDocumentImporter.dir);
				}
				ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(file));
				while (true)
				{
					ZipEntry nextEntry = zipInputStream.GetNextEntry();
					ZipEntry zipEntry = nextEntry;
					if (nextEntry == null)
					{
						break;
					}
					string directoryName = Path.GetDirectoryName(zipEntry.Name);
					string fileName = Path.GetFileName(zipEntry.Name);
					if (directoryName != string.Empty)
					{
						Directory.CreateDirectory(string.Concat(OpenDocumentImporter.dir, directoryName));
					}
					if (fileName != string.Empty)
					{
						FileStream fileStream = File.Create(string.Concat(OpenDocumentImporter.dir, zipEntry.Name));
						int num = 2048;
						byte[] numArray = new byte[2048];
						while (true)
						{
							num = zipInputStream.Read(numArray, 0, (int)numArray.Length);
							if (num <= 0)
							{
								break;
							}
							fileStream.Write(numArray, 0, num);
						}
						fileStream.Close();
					}
				}
				zipInputStream.Close();
				this.MovePictures();
				this.ReadResources();
			}
			catch (Exception exception)
			{
				throw;
			}
		}
	}
}