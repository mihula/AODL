using AODL.Document;
using System;

namespace AODL.Document.Export
{
	public class DocumentSupportInfo
	{
		private string _extension;

		private DocumentTypes _documentType;

		public DocumentTypes DocumentType
		{
			get
			{
				return this._documentType;
			}
			set
			{
				this._documentType = value;
			}
		}

		public string Extension
		{
			get
			{
				return this._extension;
			}
			set
			{
				this._extension = value;
			}
		}

		public DocumentSupportInfo()
		{
		}

		public DocumentSupportInfo(string extension, DocumentTypes documentTyp)
		{
			this.Extension = extension;
			this.DocumentType = documentTyp;
		}
	}
}