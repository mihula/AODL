using AODL.Document;
using AODL.Document.Content;
using AODL.Document.Export;
using AODL.Document.Export.OpenDocument;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;

namespace AODL.Document.Export.Html
{
	public class OpenDocumentHtmlExporter : IExporter, IPublisherInfo
	{
		private readonly string _imgFolder = "tempHtmlImg";

		private IDocument _document;

		private ArrayList _supportedExtensions;

		private ArrayList _exporterror;

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
				return this._exporterror;
			}
		}

		public string InfoUrl
		{
			get
			{
				return this._infoUrl;
			}
		}

		public OpenDocumentHtmlExporter()
		{
			this._exporterror = new ArrayList();
			this._supportedExtensions = new ArrayList();
			this._supportedExtensions.Add(new DocumentSupportInfo(".html", DocumentTypes.TextDocument));
			this._supportedExtensions.Add(new DocumentSupportInfo(".html", DocumentTypes.SpreadsheetDocument));
			this._supportedExtensions.Add(new DocumentSupportInfo(".htm", DocumentTypes.TextDocument));
			this._supportedExtensions.Add(new DocumentSupportInfo(".htm", DocumentTypes.SpreadsheetDocument));
			this._author = "Lars Behrmann, lb@OpenDocument4all.com";
			this._infoUrl = "http://AODL.OpenDocument4all.com";
			this._description = "This the standard HTML exporter of the OpenDocument library AODL.";
		}

		private string AppendHtml(IContentCollection contentlist, string template)
		{
			string str;
			try
			{
				HTMLContentBuilder hTMLContentBuilder = new HTMLContentBuilder()
				{
					GraphicTargetFolder = this._imgFolder
				};
				string contentCollectionAsHtml = hTMLContentBuilder.GetIContentCollectionAsHtml(this._document.Content);
				template = string.Concat(template, contentCollectionAsHtml);
				template = string.Concat(template, "</body>\n</html>");
				template = this.SetMetaContent(template);
				str = template;
			}
			catch (Exception exception)
			{
				throw;
			}
			return str;
		}

		public void Export(IDocument document, string filename)
		{
			try
			{
				this._document = document;
				string currentDirectory = Environment.CurrentDirectory;
				int num = filename.LastIndexOf("\\");
				if (num != -1)
				{
					currentDirectory = filename.Substring(0, num);
				}
				string str = this.AppendHtml(this._document.Content, this.GetTemplate());
				this.WriteHtmlFile(filename, str);
				string str1 = "\\Pictures";
				string str2 = string.Concat(currentDirectory, "\\", this._imgFolder);
				if (!Directory.Exists(string.Concat(str2, str1)))
				{
					Directory.CreateDirectory(string.Concat(str2, str1));
				}
				OpenDocumentTextExporter.SaveGraphic(this._document, str2);
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		private string GetTemplate()
		{
			string str;
			try
			{
				Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AODL.Resources.OD.htmltemplate.html");
				string str1 = null;
				StreamReader streamReader = new StreamReader(manifestResourceStream);
				try
				{
					string str2 = null;
					while (true)
					{
						string str3 = streamReader.ReadLine();
						str2 = str3;
						if (str3 == null)
						{
							break;
						}
						str1 = string.Concat(str1, str2, "\n");
					}
					streamReader.Close();
				}
				finally
				{
					if (streamReader != null)
					{
						streamReader.Dispose();
					}
				}
				manifestResourceStream.Close();
				str = str1;
			}
			catch (Exception exception)
			{
				throw;
			}
			return str;
		}

		private string SetMetaContent(string text)
		{
			try
			{
				string html = ((IHtml)this._document.DocumentMetadata).GetHtml();
				if (html != string.Empty)
				{
					text = text.Replace("<!--meta-->", html);
				}
			}
			catch (Exception exception)
			{
			}
			return text;
		}

		private void WriteHtmlFile(string filename, string html)
		{
			try
			{
				FileStream fileStream = File.Create(filename);
				StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
				streamWriter.WriteLine(html);
				streamWriter.Close();
				fileStream.Close();
			}
			catch (Exception exception)
			{
				throw;
			}
		}
	}
}