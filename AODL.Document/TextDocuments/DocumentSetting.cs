using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace AODL.Document.TextDocuments
{
	public class DocumentSetting
	{
		public readonly static string FileName;

		private XmlDocument _settings;

		public XmlDocument Settings
		{
			get
			{
				return this._settings;
			}
			set
			{
				this._settings = value;
			}
		}

		static DocumentSetting()
		{
			DocumentSetting.FileName = "settings.xml";
		}

		public DocumentSetting()
		{
		}

		public void LoadFromFile(string file)
		{
			try
			{
				this.Settings = new XmlDocument();
				this.Settings.Load(file);
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		public virtual void New()
		{
			try
			{
				Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AODL.Resources.OD.settings.xml");
				this.Settings = new XmlDocument();
				this.Settings.Load(manifestResourceStream);
			}
			catch (Exception exception)
			{
				throw;
			}
		}
	}
}