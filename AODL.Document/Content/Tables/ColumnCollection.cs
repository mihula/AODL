using AODL.Document.Collections;
using System;
using System.Collections;
using System.Reflection;

namespace AODL.Document.Content.Tables
{
	public class ColumnCollection : CollectionWithEvents
	{
		public Column this[int index]
		{
			get
			{
				return base.List[index] as Column;
			}
		}

		public ColumnCollection()
		{
		}

		public int Add(Column value)
		{
			return base.List.Add(value);
		}

		public bool Contains(Column value)
		{
			return base.List.Contains(value);
		}

		public void Insert(int index, Column value)
		{
			base.List.Insert(index, value);
		}

		public void Remove(Column value)
		{
			base.List.Remove(value);
		}
	}
}