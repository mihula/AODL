using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace AODL.Document.TextDocuments
{
	public class DocumentManifest
	{
		public readonly static string FolderName;

		public readonly static string FileName;

		private XmlDocument _manifest;

		public XmlDocument Manifest
		{
			get
			{
				return this._manifest;
			}
			set
			{
				this._manifest = value;
			}
		}

		static DocumentManifest()
		{
			DocumentManifest.FolderName = "META-INF";
			DocumentManifest.FileName = "manifest.xml";
		}

		public DocumentManifest()
		{
		}

		private void DTDReplacer(string file)
		{
			try
			{
				string str = null;
				StreamReader streamReader = new StreamReader(file);
				try
				{
					while (true)
					{
						string str1 = streamReader.ReadLine();
						string str2 = str1;
						if (str1 == null)
						{
							break;
						}
						str = string.Concat(str, str2);
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
				str = str.Replace("<!DOCTYPE manifest:manifest PUBLIC \"-//OpenOffice.org//DTD Manifest 1.0//EN\" \"Manifest.dtd\">", "");
				FileStream fileStream = File.Create(file);
				StreamWriter streamWriter = new StreamWriter(fileStream);
				streamWriter.WriteLine(str);
				streamWriter.Close();
				fileStream.Close();
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
				this.Manifest = new XmlDocument();
				this.Manifest.Load(file);
			}
			catch (Exception exception)
			{
				this.DTDReplacer(file);
				this.LoadFromFile(file);
			}
		}

		public virtual void New()
		{
			try
			{
				Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AODL.Resources.OD.manifest.xml");
				this.Manifest = new XmlDocument();
				this.Manifest.Load(manifestResourceStream);
			}
			catch (Exception exception)
			{
				throw;
			}
		}
	}
}