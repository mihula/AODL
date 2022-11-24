using AODL.Document.Collections;
using System;
using System.Collections;
using System.Reflection;

namespace AODL.Document.Styles.Properties
{
	public class IPropertyCollection : CollectionWithEvents
	{
		public IProperty this[int index]
		{
			get
			{
				return base.List[index] as IProperty;
			}
		}

		public IPropertyCollection()
		{
		}

		public int Add(IProperty value)
		{
			return base.List.Add(value);
		}

		public bool Contains(IProperty value)
		{
			return base.List.Contains(value);
		}

		public void Insert(int index, IProperty value)
		{
			base.List.Insert(index, value);
		}

		public void Remove(IProperty value)
		{
			base.List.Remove(value);
		}
	}
}