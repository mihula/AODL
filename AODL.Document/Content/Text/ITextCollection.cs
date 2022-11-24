using AODL.Document.Collections;
using System;
using System.Collections;
using System.Reflection;

namespace AODL.Document.Content.Text
{
	public class ITextCollection : CollectionWithEvents
	{
		public IText this[int index]
		{
			get
			{
				return base.List[index] as IText;
			}
		}

		public ITextCollection()
		{
		}

		public int Add(IText value)
		{
			return base.List.Add(value);
		}

		public bool Contains(IText value)
		{
			return base.List.Contains(value);
		}

		public void Insert(int index, IText value)
		{
			base.List.Insert(index, value);
		}

		public void Remove(IText value)
		{
			base.List.Remove(value);
		}
	}
}