using System;
using System.Drawing;
using System.IO;

namespace AODL.Document.TextDocuments
{
	public class DocumentPicture
	{
		private System.Drawing.Image _image;

		private string _imageName;

		private string _imagePath;

		public System.Drawing.Image Image
		{
			get
			{
				return this._image;
			}
			set
			{
				this._image = value;
			}
		}

		public string ImageName
		{
			get
			{
				return this._imageName;
			}
			set
			{
				this._imageName = value;
			}
		}

		public string ImagePath
		{
			get
			{
				return this._imagePath;
			}
			set
			{
				this._imagePath = value;
			}
		}

		public DocumentPicture()
		{
		}

		public DocumentPicture(string file)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(file);
				this.ImageName = fileInfo.Name;
				this.ImagePath = fileInfo.FullName;
			}
			catch (Exception exception)
			{
				throw;
			}
		}
	}
}