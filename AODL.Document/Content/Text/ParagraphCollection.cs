using AODL.Document.Collections;
using System;
using System.Collections;
using System.Reflection;

namespace AODL.Document.Content.Text
{
	public class ParagraphCollection : CollectionWithEvents
	{
		public Paragraph this[int index]
		{
			get
			{
				return base.List[index] as Paragraph;
			}
		}

		public ParagraphCollection()
		{
		}

		public int Add(Paragraph value)
		{
			return base.List.Add(value);
		}

		public bool Contains(Paragraph value)
		{
			return base.List.Contains(value);
		}

		public void Insert(int index, Paragraph value)
		{
			base.List.Insert(index, value);
		}

		public void Remove(Paragraph value)
		{
			base.List.Remove(value);
		}
	}
}