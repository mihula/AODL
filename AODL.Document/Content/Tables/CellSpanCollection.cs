using AODL.Document.Collections;
using System;
using System.Collections;
using System.Reflection;

namespace AODL.Document.Content.Tables
{
	public class CellSpanCollection : CollectionWithEvents
	{
		public CellSpan this[int index]
		{
			get
			{
				return base.List[index] as CellSpan;
			}
		}

		public CellSpanCollection()
		{
		}

		public int Add(CellSpan value)
		{
			return base.List.Add(value);
		}

		public bool Contains(CellSpan value)
		{
			return base.List.Contains(value);
		}

		public void Insert(int index, CellSpan value)
		{
			base.List.Insert(index, value);
		}

		public void Remove(CellSpan value)
		{
			base.List.Remove(value);
		}
	}
}