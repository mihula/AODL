using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Content;
using AODL.Document.Content.Tables;
using AODL.Document.Exceptions;
using AODL.Document.Export;
using AODL.Document.Export.OpenDocument;
using AODL.Document.Import;
using AODL.Document.Import.OpenDocument;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.Styles;
using AODL.Document.TextDocuments;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Xml;

namespace AODL.Document.SpreadsheetDocuments
{
    public class SpreadsheetDocument : IDocument, IDisposable
    {
        private int _tableCount = 0;

        private XmlDocument _xmldoc;

        private bool _isLoadedFile;

        private ArrayList _fontList;

        private IStyleCollection _styles;

        private IStyleCollection _commonStyles;

        private IContentCollection _contents;

        private AODL.Document.Content.Tables.TableCollection _tableCollection;

        private AODL.Document.SpreadsheetDocuments.DocumentStyles _documentStyles;

        private AODL.Document.SpreadsheetDocuments.DocumentSetting _documentSetting;

        private AODL.Document.TextDocuments.DocumentMetadata _documentMetadata;

        private DocumentConfiguration2 _documentConfiguations2;

        private AODL.Document.SpreadsheetDocuments.DocumentManifest _documentManifest;

        private DocumentPictureCollection _documentPictures;

        private DocumentPictureCollection _documentThumbnails;

        private ArrayList _graphics;

        private string _mimeTyp = "application/vnd.oasis.opendocument.spreadsheet";

        private XmlNamespaceManager _namespacemanager;

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
                return this._contents;
            }
            set
            {
                this._contents = value;
            }
        }

        public DocumentConfiguration2 DocumentConfigurations2
        {
            get
            {
                return this._documentConfiguations2;
            }
            set
            {
                this._documentConfiguations2 = value;
            }
        }

        public AODL.Document.SpreadsheetDocuments.DocumentManifest DocumentManifest
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

        public AODL.Document.SpreadsheetDocuments.DocumentSetting DocumentSetting
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

        public AODL.Document.SpreadsheetDocuments.DocumentStyles DocumentStyles
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

        public AODL.Document.Content.Tables.TableCollection TableCollection
        {
            get
            {
                return this._tableCollection;
            }
            set
            {
                this._tableCollection = value;
            }
        }

        public int TableCount
        {
            get
            {
                return this._tableCount;
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

        public SpreadsheetDocument()
        {
            this.TableCollection = new AODL.Document.Content.Tables.TableCollection();
            this.Styles = new IStyleCollection();
            this.CommonStyles = new IStyleCollection();
            this.Content = new IContentCollection();
            this._graphics = new ArrayList();
            this.FontList = new ArrayList();
            this.TableCollection.Inserted += new CollectionWithEvents.CollectionChange(this.TableCollection_Inserted);
            this.TableCollection.Removed += new CollectionWithEvents.CollectionChange(this.TableCollection_Removed);
            this.Content.Inserted += new CollectionWithEvents.CollectionChange(this.Content_Inserted);
            this.Content.Removed += new CollectionWithEvents.CollectionChange(this.Content_Removed);
        }

        ~SpreadsheetDocument()
        {
            this.Dispose();
        }

        private void Content_Inserted(int index, object value)
        {
            if (value is Table && !this.TableCollection.Contains((Table)value))
            {
                this.TableCollection.Add(value as Table);
            }
        }

        private void Content_Removed(int index, object value)
        {
            if (value is Table && this.TableCollection.Contains((Table)value))
            {
                this.TableCollection.Remove(value as Table);
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
            XmlNode xmlNode = this.XmlDoc.SelectSingleNode("/office:document-content/office:body/office:spreadsheet", this.NamespaceManager);
            foreach (IContent content in this.Content)
            {
                if (!(content is Table))
                {
                    continue;
                }
                xmlNode.AppendChild(((Table)content).BuildNode());
            }
            this.CreateLocalStyleContent();
            this.CreateCommonStyleContent();
        }

        private void CreateLocalStyleContent()
        {
            XmlNode xmlNode = this.XmlDoc.SelectSingleNode("/office:document-content/office:automatic-styles", this.NamespaceManager);
            foreach (IStyle style in this.Styles)
            {
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
                this.LoadBlankContent();
                this.NamespaceManager = TextDocumentHelper.NameSpace(this._xmldoc.NameTable);
                IImporter firstImporter = (new ImportHandler()).GetFirstImporter(DocumentTypes.SpreadsheetDocument, file);
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

        private void LoadBlankContent()
        {
            try
            {
                Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AODL.Resources.OD.spreadsheetcontent.xml");
                this._xmldoc = new XmlDocument();
                this._xmldoc.Load(manifestResourceStream);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public void New()
        {
            this.LoadBlankContent();
            this.NamespaceManager = TextDocumentHelper.NameSpace(this._xmldoc.NameTable);
            this.DocumentConfigurations2 = new DocumentConfiguration2();
            this.DocumentManifest = new AODL.Document.SpreadsheetDocuments.DocumentManifest();
            this.DocumentManifest.New();
            this.DocumentMetadata = new AODL.Document.TextDocuments.DocumentMetadata(this);
            this.DocumentMetadata.New();
            this.DocumentPictures = new DocumentPictureCollection();
            this.DocumentSetting = new AODL.Document.SpreadsheetDocuments.DocumentSetting();
            this.DocumentSetting.New();
            this.DocumentStyles = new AODL.Document.SpreadsheetDocuments.DocumentStyles();
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
                this.CreateContentBody();
                (new ExportHandler()).GetFirstExporter(DocumentTypes.SpreadsheetDocument, filename).Export(this, filename);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        private bool StyleNodeExists(string styleName)
        {
            bool flag;
            flag = (this.XmlDoc.SelectSingleNode(string.Concat("/office:document-content/office:automatic-styles/style:style[@style:name='", styleName, "']"), this.NamespaceManager) != null ? true : false);
            return flag;
        }

        private void TableCollection_Inserted(int index, object value)
        {
            this._tableCount++;
            if (!this.Content.Contains(value as IContent))
            {
                this.Content.Add(value as IContent);
            }
        }

        private void TableCollection_Removed(int index, object value)
        {
            this._tableCount--;
            if (this.Content.Contains(value as IContent))
            {
                this.Content.Remove(value as IContent);
            }
        }
    }
}