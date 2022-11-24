using System;
using System.IO;

namespace AODL.Document.TextDocuments
{
	public class DocumentConfiguration2
	{
		public readonly static string FolderName;

		private string _filename;

		private string _configurations2Content;

		public string Configurations2Content
		{
			get
			{
				return this._configurations2Content;
			}
			set
			{
				this._configurations2Content = value;
			}
		}

		public string FileName
		{
			get
			{
				return this._filename;
			}
			set
			{
				this._filename = value;
			}
		}

		static DocumentConfiguration2()
		{
			DocumentConfiguration2.FolderName = "Configurations2";
		}

		public DocumentConfiguration2()
		{
		}

		public void Load(string filename)
		{
			try
			{
				StreamReader streamReader = new StreamReader(filename);
				string str = null;
				while (true)
				{
					string str1 = streamReader.ReadLine();
					str = str1;
					if (str1 == null)
					{
						break;
					}
					DocumentConfiguration2 documentConfiguration2 = this;
					documentConfiguration2.Configurations2Content = string.Concat(documentConfiguration2.Configurations2Content, str);
				}
				streamReader.Close();
			}
			catch (Exception exception)
			{
				throw;
			}
		}
	}
}