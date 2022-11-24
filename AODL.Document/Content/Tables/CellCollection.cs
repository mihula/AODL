using AODL.Document.Collections;
using System;
using System.Collections;
using System.Reflection;

namespace AODL.Document.Content.Tables
{
	public class CellCollection : CollectionWithEvents
	{
		public Cell this[int index]
		{
			get
			{
				return base.List[index] as Cell;
			}
		}

		public CellCollection()
		{
		}

		public int Add(Cell value)
		{
			return base.List.Add(value);
		}

		public bool Contains(Cell value)
		{
			return base.List.Contains(value);
		}

		public void Insert(int index, Cell value)
		{
			base.List.Insert(index, value);
		}

		public void Remove(Cell value)
		{
			base.List.Remove(value);
		}
	}
}