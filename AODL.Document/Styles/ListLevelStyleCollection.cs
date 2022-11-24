using AODL.Document.Collections;
using System;
using System.Collections;
using System.Reflection;

namespace AODL.Document.Styles
{
	public class ListLevelStyleCollection : CollectionWithEvents
	{
		public ListLevelStyle this[int index]
		{
			get
			{
				return base.List[index] as ListLevelStyle;
			}
		}

		public ListLevelStyleCollection()
		{
		}

		public int Add(ListLevelStyle value)
		{
			return base.List.Add(value);
		}

		public bool Contains(ListLevelStyle value)
		{
			return base.List.Contains(value);
		}

		public void Insert(int index, ListLevelStyle value)
		{
			base.List.Insert(index, value);
		}

		public void Remove(ListLevelStyle value)
		{
			base.List.Remove(value);
		}
	}
}