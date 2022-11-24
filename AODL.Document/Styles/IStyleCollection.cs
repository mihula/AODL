using AODL.Document.Collections;
using System;
using System.Collections;
using System.Reflection;

namespace AODL.Document.Styles
{
	public class IStyleCollection : CollectionWithEvents
	{
		public IStyle this[int index]
		{
			get
			{
				return base.List[index] as IStyle;
			}
		}

		public IStyleCollection()
		{
		}

		public int Add(IStyle value)
		{
			return base.List.Add(value);
		}

		public bool Contains(IStyle value)
		{
			return base.List.Contains(value);
		}

		public IStyle GetStyleByName(string styleName)
		{
			IStyle style;
			foreach (IStyle list in base.List)
			{
				if (list.StyleName == null || styleName == null || !list.StyleName.ToLower().Equals(styleName.ToLower()))
				{
					continue;
				}
				style = list;
				return style;
			}
			style = null;
			return style;
		}

		public void Insert(int index, IStyle value)
		{
			base.List.Insert(index, value);
		}

		public void Remove(IStyle value)
		{
			base.List.Remove(value);
		}
	}
}