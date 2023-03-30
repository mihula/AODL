using AODL.Document;
using AODL.Document.Content;
using AODL.Document.Content.Tables;
using AODL.Document.Exceptions;
using AODL.Document.Export;
using AODL.Document.Export.OpenDocument;
using AODL.Document.Import;
using AODL.Document.Import.OpenDocument;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.Styles;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Xml;

namespace AODL.Document.TextDocuments
{
	public class TextDocument : IDisposable, IDocument
	{
		private int _tableOfContentsCount = 0;

		private int _tableCount = 0;

		private XmlDocument _xmldoc;

		private bool _isLoadedFile;

		private ArrayList _graphics;

		private AODL.Document.TextDocuments.DocumentStyles _documentStyles;

		private AODL.Document.TextDocuments.DocumentSetting _documentSetting;

		private AODL.Document.TextDocuments.DocumentMetadata _documentMetadata;

		private AODL.Document.TextDocuments.DocumentManifest _documentManifest;

		private DocumentConfiguration2 _documentConfigurations2;

		private DocumentPictureCollection _documentPictures;

		private DocumentPictureCollection _documentThumbnails;

		private string _mimeTyp = "application/vnd.oasis.opendocument.text";

		private XmlNamespaceManager _namespacemanager;

		private IContentCollection _content;

		private IStyleCollection _styles;

		private IStyleCollection _commonStyles;

		private ArrayList _fontList;

		private bool _disposed = false;

		public IStyleCollection CommonStyles
		{
			get
			{
				return this._commonStyles;
			}
			set
			{
				this._commonStyles = value;
			}
		}

		public IContentCollection Content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		public DocumentConfiguration2 DocumentConfigurations2
		{
			get
			{
				return this._documentConfigurations2;
			}
			set
			{
				this._documentConfigurations2 = value;
			}
		}

		public AODL.Document.TextDocuments.DocumentManifest DocumentManifest
		{
			get
			{
				return this._documentManifest;
			}
			set
			{
				this._documentManifest = value;
			}
		}

		public AODL.Document.TextDocuments.DocumentMetadata DocumentMetadata
		{
			get
			{
				return this._documentMetadata;
			}
			set
			{
				this._documentMetadata = value;
			}
		}

		public DocumentPictureCollection DocumentPictures
		{
			get
			{
				return this._documentPictures;
			}
			set
			{
				this._documentPictures = value;
			}
		}

		public AODL.Document.TextDocuments.DocumentSetting DocumentSetting
		{
			get
			{
				return this._documentSetting;
			}
			set
			{
				this._documentSetting = value;
			}
		}

		public AODL.Document.TextDocuments.DocumentStyles DocumentStyles
		{
			get
			{
				return this._documentStyles;
			}
			set
			{
				this._documentStyles = value;
			}
		}

		public DocumentPictureCollection DocumentThumbnails
		{
			get
			{
				return this._documentThumbnails;
			}
			set
			{
				this._documentThumbnails = value;
			}
		}

		public ArrayList FontList
		{
			get
			{
				return this._fontList;
			}
			set
			{
				this._fontList = value;
			}
		}

		public ArrayList Graphics
		{
			get
			{
				return this._graphics;
			}
		}

		public bool IsLoadedFile
		{
			get
			{
				return this._isLoadedFile;
			}
		}

		public string MimeTyp
		{
			get
			{
				return this._mimeTyp;
			}
		}

		public XmlNamespaceManager NamespaceManager
		{
			get
			{
				return this._namespacemanager;
			}
			set
			{
				this._namespacemanager = value;
			}
		}

		public IStyleCollection Styles
		{
			get
			{
				return this._styles;
			}
			set
			{
				this._styles = value;
			}
		}

		public int TableCount
		{
			get
			{
				return this._tableCount;
			}
		}

		public int TableofContentsCount
		{
			get
			{
				return this._tableOfContentsCount;
			}
		}

		public XmlDocument XmlDoc
		{
			get
			{
				return this._xmldoc;
			}
			set
			{
				this._xmldoc = value;
			}
		}

		public TextDocument()
		{
			this.Content = new IContentCollection();
			this.Styles = new IStyleCollection();
			this.CommonStyles = new IStyleCollection();
			this.FontList = new ArrayList();
			this._graphics = new ArrayList();
		}

        ~TextDocument()
        {
            this.Dispose();
        }

        private void AddFont(string fontname)
		{
			try
			{
				Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AODL.AODL.Resources.OD.fonts.xml");
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(manifestResourceStream);
				if (this.XmlDoc.SelectSingleNode(string.Concat("/office:document-content/office:font-face-decls/style:font-face[@style:name='", fontname, "']"), this.NamespaceManager) == null)
				{
					XmlNode xmlNode = xmlDocument.SelectSingleNode(string.Concat("/office:document-content/office:font-face-decls/style:font-face[@style:name='", fontname, "']"), this.NamespaceManager);
					if (xmlNode != null)
					{
						XmlNode xmlNode1 = this.XmlDoc.SelectSingleNode("/office:document-content", this.NamespaceManager);
						if (xmlNode1 != null)
						{
							foreach (XmlNode xmlNode2 in xmlNode1)
							{
								if (xmlNode2.Name != "office:font-face-decls")
								{
									continue;
								}
								XmlNode xmlNode3 = this.CreateNode("font-face", "style");
								foreach (XmlAttribute attribute in xmlNode.Attributes)
								{
									XmlAttribute xmlAttribute = this.CreateAttribute(attribute.LocalName, attribute.Prefix);
									xmlAttribute.Value=(attribute.Value);
									xmlNode3.Attributes.Append(xmlAttribute);
								}
								xmlNode2.AppendChild(xmlNode3);
								break;
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		public XmlAttribute CreateAttribute(string name, string prefix)
		{
			if (this.XmlDoc == null)
			{
				string[] strArray = new string[] { "There is no XmlDocument loaded. Couldn't create Attribue ", name, " with Prefix ", prefix, ". ", this.GetType().ToString() };
				throw new NullReferenceException(string.Concat(strArray));
			}
			string namespaceUri = this.GetNamespaceUri(prefix);
			return this.XmlDoc.CreateAttribute(prefix, name, namespaceUri);
		}

		private void CreateCommonStyleContent()
		{
			XmlNode xmlNode = this.DocumentStyles.Styles.SelectSingleNode("office:document-styles/office:styles", this.NamespaceManager);
			xmlNode.InnerXml=("");
			foreach (IStyle commonStyle in this.CommonStyles)
			{
				XmlNode xmlNode1 = this.DocumentStyles.Styles.ImportNode(commonStyle.Node, true);
				xmlNode.AppendChild(xmlNode1);
			}
			xmlNode = this.XmlDoc.SelectSingleNode("office:document-content/office:styles", this.NamespaceManager);
			if (xmlNode != null)
			{
				this.XmlDoc.SelectSingleNode("office:document-content", this.NamespaceManager).RemoveChild(xmlNode);
			}
		}

		private void CreateContentBody()
		{
			XmlNode xmlNode = this.XmlDoc.SelectSingleNode(TextDocumentHelper.OfficeTextPath, this.NamespaceManager);
			foreach (IContent content in this.Content)
			{
				if (content is Table)
				{
					((Table)content).BuildNode();
				}
				xmlNode.AppendChild(content.Node);
			}
			this.CreateLocalStyleContent();
			this.CreateCommonStyleContent();
		}

		private void CreateLocalStyleContent()
		{
			XmlNode xmlNode = this.XmlDoc.SelectSingleNode(TextDocumentHelper.AutomaticStylePath, this.NamespaceManager);
			foreach (IStyle style in this.Styles)
			{
				bool flag = false;
				if (style.StyleName != null && xmlNode.SelectSingleNode(string.Concat("style:style[@style:name='", style.StyleName, "']"), this.NamespaceManager) != null)
				{
					flag = true;
				}
				if (flag)
				{
					continue;
				}
				xmlNode.AppendChild(style.Node);
			}
		}

		public XmlNode CreateNode(string name, string prefix)
		{
			if (this.XmlDoc == null)
			{
				string[] strArray = new string[] { "There is no XmlDocument loaded. Couldn't create Node ", name, " with Prefix ", prefix, ". ", this.GetType().ToString() };
				throw new NullReferenceException(string.Concat(strArray));
			}
			string namespaceUri = this.GetNamespaceUri(prefix);
			return this.XmlDoc.CreateElement(prefix, name, namespaceUri);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!this._disposed && disposing)
			{
				try
				{
					OpenDocumentTextExporter.CleanUpReadAndWriteDirectories();
				}
				catch (Exception exception)
				{
					throw exception;
				}
			}
			this._disposed = true;
		}

		internal string GetNamespaceUri(string prefix)
		{
			string str;
			foreach (string namespaceManager in this.NamespaceManager)
			{
				if (prefix != namespaceManager)
				{
					continue;
				}
				str = this.NamespaceManager.LookupNamespace(namespaceManager);
				return str;
			}
			str = null;
			return str;
		}

		public void Load(string file)
		{
			try
			{
				this._isLoadedFile = true;
				this._xmldoc = new XmlDocument();
				this._xmldoc.LoadXml(TextDocumentHelper.GetBlankDocument());
				this.NamespaceManager = TextDocumentHelper.NameSpace(this._xmldoc.NameTable);
				IImporter firstImporter = (new ImportHandler()).GetFirstImporter(DocumentTypes.TextDocument, file);
				if (firstImporter != null)
				{
					if (firstImporter.NeedNewOpenDocument)
					{
						this.New();
					}
					firstImporter.Import(this, file);
					if (firstImporter.ImportError != null && firstImporter.ImportError.Count > 0)
					{
						foreach (object importError in firstImporter.ImportError)
						{
							if (!(importError is AODLWarning))
							{
								continue;
							}
							if (((AODLWarning)importError).Message != null)
							{
								Console.WriteLine("Err: {0}", ((AODLWarning)importError).Message);
							}
							if (((AODLWarning)importError).Node == null)
							{
								continue;
							}
							XmlTextWriter xmlTextWriter = new XmlTextWriter(Console.Out);
							xmlTextWriter.Formatting = Formatting.Indented;
							((AODLWarning)importError).Node.WriteContentTo(xmlTextWriter);
						}
					}
				}
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		public void New()
		{
			this._xmldoc = new XmlDocument();
			this._xmldoc.LoadXml(TextDocumentHelper.GetBlankDocument());
			this.NamespaceManager = TextDocumentHelper.NameSpace(this._xmldoc.NameTable);
			this.DocumentConfigurations2 = new DocumentConfiguration2();
			this.DocumentManifest = new AODL.Document.TextDocuments.DocumentManifest();
			this.DocumentManifest.New();
			this.DocumentMetadata = new AODL.Document.TextDocuments.DocumentMetadata(this);
			this.DocumentMetadata.New();
			this.DocumentPictures = new DocumentPictureCollection();
			this.DocumentSetting = new AODL.Document.TextDocuments.DocumentSetting();
			this.DocumentSetting.New();
			this.DocumentStyles = new AODL.Document.TextDocuments.DocumentStyles();
			this.DocumentStyles.New();
			this.ReadCommonStyles();
			this.DocumentThumbnails = new DocumentPictureCollection();
		}

		private void ReadCommonStyles()
		{
			(new OpenDocumentImporter()
			{
				_document = this
			}).ImportCommonStyles();
			(new LocalStyleProcessor(this, true)).ReadStyles();
		}

		public void SaveTo(string filename)
		{
			try
			{
				foreach (string fontList in this.FontList)
				{
					this.AddFont(fontList);
				}
				this.CreateContentBody();
				(new ExportHandler()).GetFirstExporter(DocumentTypes.TextDocument, filename).Export(this, filename);
			}
			catch (Exception exception)
			{
				throw;
			}
		}
	}
}