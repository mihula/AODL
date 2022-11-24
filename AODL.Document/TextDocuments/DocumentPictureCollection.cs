using AODL.Document.Collections;
using System;
using System.Collections;
using System.Reflection;

namespace AODL.Document.TextDocuments
{
	public class DocumentPictureCollection : CollectionWithEvents
	{
		public DocumentPicture this[int index]
		{
			get
			{
				return base.List[index] as DocumentPicture;
			}
		}

		public DocumentPictureCollection()
		{
		}

		public int Add(DocumentPicture value)
		{
			return base.List.Add(value);
		}

		public bool Contains(DocumentPicture value)
		{
			return base.List.Contains(value);
		}

		public void Insert(int index, DocumentPicture value)
		{
			base.List.Insert(index, value);
		}

		public void Remove(DocumentPicture value)
		{
			base.List.Remove(value);
		}
	}
}