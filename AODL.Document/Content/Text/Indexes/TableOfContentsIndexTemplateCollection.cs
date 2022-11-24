using AODL.Document.Collections;
using System;
using System.Collections;
using System.Reflection;

namespace AODL.Document.Content.Text.Indexes
{
	public class TableOfContentsIndexTemplateCollection : CollectionWithEvents
	{
		public TableOfContentsIndexTemplate this[int index]
		{
			get
			{
				return base.List[index] as TableOfContentsIndexTemplate;
			}
		}

		public TableOfContentsIndexTemplateCollection()
		{
		}

		public int Add(TableOfContentsIndexTemplate value)
		{
			return base.List.Add(value);
		}

		public bool Contains(TableOfContentsIndexTemplate value)
		{
			return base.List.Contains(value);
		}

		public void Insert(int index, TableOfContentsIndexTemplate value)
		{
			base.List.Insert(index, value);
		}

		public void Remove(TableOfContentsIndexTemplate value)
		{
			base.List.Remove(value);
		}
	}
}