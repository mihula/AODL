using AODL.Document.Collections;
using System;
using System.Collections;
using System.Reflection;

namespace AODL.Document.Content.Tables
{
	public class TableCollection : CollectionWithEvents
	{
		public Table this[int index]
		{
			get
			{
				return base.List[index] as Table;
			}
		}

		public TableCollection()
		{
		}

		public int Add(Table value)
		{
			return base.List.Add(value);
		}

		public bool Contains(Table value)
		{
			return base.List.Contains(value);
		}

		public void Insert(int index, Table value)
		{
			base.List.Insert(index, value);
		}

		public void Remove(Table value)
		{
			base.List.Remove(value);
		}
	}
}