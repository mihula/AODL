using AODL.Document.Collections;
using System;
using System.Collections;
using System.Reflection;

namespace AODL.Document.Content
{
	public class IContentCollection : CollectionWithEvents
	{
		public IContent this[int index]
		{
			get
			{
				return base.List[index] as IContent;
			}
		}

		public IContentCollection()
		{
		}

		public int Add(IContent value)
		{
			return base.List.Add(value);
		}

		public bool Contains(IContent value)
		{
			return base.List.Contains(value);
		}

		public void Insert(int index, IContent value)
		{
			base.List.Insert(index, value);
		}

		public void Remove(IContent value)
		{
			base.List.Remove(value);
		}
	}
}