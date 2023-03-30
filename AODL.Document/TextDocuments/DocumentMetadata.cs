using AODL.Document;
using AODL.Document.Content;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace AODL.Document.TextDocuments
{
	public class DocumentMetadata : IHtml
	{
		public readonly static string FileName;

		private XmlDocument _meta;

		private IDocument _textDocument;

		public string CreationDate
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:creation-date", this.TextDocument.NamespaceManager);
				if (xmlNode == null)
				{
					innerText = null;
				}
				else
				{
					innerText = xmlNode.InnerText;
				}
				return innerText;
			}
			set
			{
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:creation-date", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					xmlNode.InnerText = value;
				}
			}
		}

		public string Creator
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._meta.SelectSingleNode("//dc:creator", this.TextDocument.NamespaceManager);
				if (xmlNode == null)
				{
					innerText = null;
				}
				else
				{
					innerText = xmlNode.InnerText;
				}
				return innerText;
			}
			set
			{
				XmlNode xmlNode = this._meta.SelectSingleNode("//dc:creator", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					xmlNode.InnerText = value;
				}
			}
		}

		internal string Generator
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:generator", this.TextDocument.NamespaceManager);
				if (xmlNode == null)
				{
					innerText = null;
				}
				else
				{
					innerText = xmlNode.InnerText;
				}
				return innerText;
			}
			set
			{
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:generator", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					xmlNode.InnerText = value;
				}
			}
		}

		public int CharacterCount
		{
			get
			{
				int num;
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:document-statistic/@meta:character-count", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					try
					{
						num = Convert.ToInt32(xmlNode.InnerText);
						return num;
					}
					catch (Exception exception)
					{
					}
				}
				num = 0;
				return num;
			}
			set
			{
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:document-statistic/@meta:character-count", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					xmlNode.InnerText=(value.ToString());
				}
			}
		}

		public int ImageCount
		{
			get
			{
				int num;
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:document-statistic/@meta:image-count", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					try
					{
						num = Convert.ToInt32(xmlNode.InnerText);
						return num;
					}
					catch (Exception exception)
					{
					}
				}
				num = 0;
				return num;
			}
			set
			{
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:document-statistic/@meta:image-count", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					xmlNode.InnerText=(value.ToString());
				}
			}
		}

		public string InitialCreator
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:initial-creator", this.TextDocument.NamespaceManager);
				if (xmlNode == null)
				{
					innerText = null;
				}
				else
				{
					innerText = xmlNode.InnerText;
				}
				return innerText;
			}
			set
			{
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:initial-creator", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					xmlNode.InnerText = value;
				}
			}
		}

		public string Keywords
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:keyword", this.TextDocument.NamespaceManager);
				if (xmlNode == null)
				{
					innerText = null;
				}
				else
				{
					innerText = xmlNode.InnerText;
				}
				return innerText;
			}
			set
			{
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:keyword", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					xmlNode.InnerText = value;
				}
			}
		}

		public string Language
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._meta.SelectSingleNode("//dc:language", this.TextDocument.NamespaceManager);
				if (xmlNode == null)
				{
					innerText = null;
				}
				else
				{
					innerText = xmlNode.InnerText;
				}
				return innerText;
			}
			set
			{
				XmlNode xmlNode = this._meta.SelectSingleNode("//dc:language", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					xmlNode.InnerText = value;
				}
			}
		}

		public string LastModified
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._meta.SelectSingleNode("//dc:date", this.TextDocument.NamespaceManager);
				if (xmlNode == null)
				{
					innerText = null;
				}
				else
				{
					innerText = xmlNode.InnerText;
				}
				return innerText;
			}
			set
			{
				XmlNode xmlNode = this._meta.SelectSingleNode("//dc:date", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					xmlNode.InnerText = value;
				}
			}
		}

		public XmlDocument Meta
		{
			get
			{
				return this._meta;
			}
			set
			{
				this._meta = value;
			}
		}

		public int ObjectCount
		{
			get
			{
				int num;
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:document-statistic/@meta:object-count", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					try
					{
						num = Convert.ToInt32(xmlNode.InnerText);
						return num;
					}
					catch (Exception exception)
					{
					}
				}
				num = 0;
				return num;
			}
			set
			{
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:document-statistic/@meta:object-count", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					xmlNode.InnerText=(value.ToString());
				}
			}
		}

		public int PageCount
		{
			get
			{
				int num;
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:document-statistic/@meta:page-count", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					try
					{
						num = Convert.ToInt32(xmlNode.InnerText);
						return num;
					}
					catch (Exception exception)
					{
					}
				}
				num = 0;
				return num;
			}
			set
			{
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:document-statistic/@meta:page-count", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					xmlNode.InnerText=(value.ToString());
				}
			}
		}

		public int ParagraphCount
		{
			get
			{
				int num;
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:document-statistic/@meta:paragraph-count", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					try
					{
						num = Convert.ToInt32(xmlNode.InnerText);
						return num;
					}
					catch (Exception exception)
					{
					}
				}
				num = 0;
				return num;
			}
			set
			{
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:document-statistic/@meta:paragraph-count", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					xmlNode.InnerText=(value.ToString());
				}
			}
		}

		public string Subject
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._meta.SelectSingleNode("//dc:subject", this.TextDocument.NamespaceManager);
				if (xmlNode == null)
				{
					innerText = null;
				}
				else
				{
					innerText = xmlNode.InnerText;
				}
				return innerText;
			}
			set
			{
				XmlNode xmlNode = this._meta.SelectSingleNode("//dc:subject", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					xmlNode.InnerText = value;
				}
			}
		}

		public int TableCount
		{
			get
			{
				int num;
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:document-statistic/@meta:table-count", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					try
					{
						num = Convert.ToInt32(xmlNode.InnerText);
						return num;
					}
					catch (Exception exception)
					{
					}
				}
				num = 0;
				return num;
			}
			set
			{
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:document-statistic/@meta:table-count", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					xmlNode.InnerText=(value.ToString());
				}
			}
		}

		public IDocument TextDocument
		{
			get
			{
				return this._textDocument;
			}
			set
			{
				this._textDocument = value;
			}
		}

		public string Title
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._meta.SelectSingleNode("//dc:title", this.TextDocument.NamespaceManager);
				if (xmlNode == null)
				{
					innerText = null;
				}
				else
				{
					innerText = xmlNode.InnerText;
				}
				return innerText;
			}
			set
			{
				XmlNode xmlNode = this._meta.SelectSingleNode("//dc:title", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					xmlNode.InnerText = value;
				}
			}
		}

		public int WordCount
		{
			get
			{
				int num;
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:document-statistic/@meta:word-count", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					try
					{
						num = Convert.ToInt32(xmlNode.InnerText);
						return num;
					}
					catch (Exception exception)
					{
					}
				}
				num = 0;
				return num;
			}
			set
			{
				XmlNode xmlNode = this._meta.SelectSingleNode("//meta:document-statistic/@meta:word-count", this.TextDocument.NamespaceManager);
				if (xmlNode != null)
				{
					xmlNode.InnerText=(value.ToString());
				}
			}
		}

		static DocumentMetadata()
		{
			DocumentMetadata.FileName = "meta.xml";
		}

		public DocumentMetadata(IDocument textDocument)
		{
			this.TextDocument = textDocument;
		}

		public DocumentMetadata()
		{
		}

		public string GetHtml()
		{
			string str = "";
			string initialCreator = "";
			string title = "";
			try
			{
				if (this.InitialCreator != null && this.InitialCreator.Length > 0)
				{
					initialCreator = this.InitialCreator;
				}
				if (initialCreator.Length == 0 && this.Creator != null && this.Creator.Length > 0)
				{
					initialCreator = this.Creator;
				}
				if (initialCreator.Length == 0)
				{
					initialCreator = "unknown";
				}
				if (this.Title != null && this.Title.Length > 0)
				{
					title = this.Title;
				}
				if (title.Length == 0)
				{
					title = "Untitled";
				}
				str = string.Concat(str, "<title>", title, "</title>\n");
				str = string.Concat(str, "<meta content=\"", initialCreator, "\" name=\"Author\">\n");
				str = string.Concat(str, "<meta content=\"", initialCreator, "\" name=\"Publisher\">\n");
				str = string.Concat(str, "<meta content=\"", initialCreator, "\" name=\"Copyright\">\n");
				if (this.Keywords != string.Empty && this.Keywords.Length > 0)
				{
					str = string.Concat(str, "<meta content=\"", this.Keywords, "\" name=\"Keywords\">\n");
				}
				if (this.Subject != string.Empty && this.Subject.Length > 0)
				{
					str = string.Concat(str, "<meta content=\"", this.Subject, "\" name=\"Description\">\n");
				}
				if (this.LastModified != string.Empty && this.LastModified.Length > 0)
				{
					str = string.Concat(str, "<meta content=\"", this.LastModified, "\" name=\"Date\">\n");
				}
			}
			catch (Exception exception)
			{
			}
			return str;
		}

		public string GetUserDefinedInfo(UserDefinedInfo info)
		{
			string innerText;
			try
			{
				XmlNode userDefinedNode = this.GetUserDefinedNode(info);
				if (userDefinedNode != null)
				{
					innerText = userDefinedNode.InnerText;
					return innerText;
				}
			}
			catch (Exception exception)
			{
				throw;
			}
			innerText = null;
			return innerText;
		}

		private XmlNode GetUserDefinedNode(UserDefinedInfo info)
		{
			XmlNode xmlNode;
			try
			{
				XmlNodeList xmlNodeList = this._meta.SelectNodes("//meta:user-defined", this.TextDocument.NamespaceManager);
				if (xmlNodeList.Count == 4)
				{
					XmlNode xmlNode1 = xmlNodeList[(int)info].SelectSingleNode("@meta:name", this.TextDocument.NamespaceManager);
					if (xmlNode1 != null)
					{
						xmlNode = xmlNode1;
						return xmlNode;
					}
				}
			}
			catch (Exception exception)
			{
				throw;
			}
			xmlNode = null;
			return xmlNode;
		}

		private void Init()
		{
			try
			{
				this.Generator = "AODL - An OpenDocument Library";
				this.PageCount = 0;
				this.ParagraphCount = 0;
				this.ObjectCount = 0;
				this.TableCount = 0;
				this.ImageCount = 0;
				this.WordCount = 0;
				this.CharacterCount = 0;
				this.CreationDate = DateTime.Now.ToString("s");
				this.LastModified = DateTime.Now.ToString("s");
				this.Language = "en-US";
				this.Title = "Untitled";
				this.Subject = "No Subject";
				this.Keywords = "";
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		public void LoadFromFile(string file)
		{
			try
			{
				this.Meta = new XmlDocument();
				this.Meta.Load(file);
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		public void New()
		{
			try
			{
				Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AODL.AODL.Resources.OD.meta.xml");
				this.Meta = new XmlDocument();
				this.Meta.Load(manifestResourceStream);
				this.Init();
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		public void SetUserDefinedInfo(UserDefinedInfo info, string text)
		{
			try
			{
				XmlNode userDefinedNode = this.GetUserDefinedNode(info);
				if (userDefinedNode != null)
				{
					userDefinedNode.InnerText=(text);
				}
			}
			catch (Exception exception)
			{
				throw;
			}
		}
	}
}