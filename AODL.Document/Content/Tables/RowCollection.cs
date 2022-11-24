using AODL.Document.Collections;
using System;
using System.Collections;
using System.Reflection;

namespace AODL.Document.Content.Tables
{
	public class RowCollection : CollectionWithEvents
	{
		public Row this[int index]
		{
			get
			{
				return base.List[index] as Row;
			}
		}

		public RowCollection()
		{
		}

		public int Add(Row value)
		{
			return base.List.Add(value);
		}

		public bool Contains(Row value)
		{
			return base.List.Contains(value);
		}

		public void Insert(int index, Row value)
		{
			base.List.Insert(index, value);
		}

		public void Remove(Row value)
		{
			base.List.Remove(value);
		}
	}
}