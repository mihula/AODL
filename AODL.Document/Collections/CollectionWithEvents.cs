using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace AODL.Document.Collections
{
	public class CollectionWithEvents : CollectionBase
	{
		public CollectionWithEvents()
		{
		}

		protected override void OnClear()
		{
			if (this.Clearing != null)
			{
				this.Clearing();
			}
		}

		protected override void OnClearComplete()
		{
			if (this.Cleared != null)
			{
				this.Cleared();
			}
		}

		protected override void OnInsert(int index, object value)
		{
			if (this.Inserting != null)
			{
				this.Inserting(index, value);
			}
		}

		protected override void OnInsertComplete(int index, object value)
		{
			if (this.Inserted != null)
			{
				this.Inserted(index, value);
			}
		}

		protected override void OnRemove(int index, object value)
		{
			if (this.Removing != null)
			{
				this.Removing(index, value);
			}
		}

		protected override void OnRemoveComplete(int index, object value)
		{
			if (this.Removed != null)
			{
				this.Removed(index, value);
			}
		}

		public event CollectionWithEvents.CollectionClear Cleared;

		public event CollectionWithEvents.CollectionClear Clearing;

		public event CollectionWithEvents.CollectionChange Inserted;

		public event CollectionWithEvents.CollectionChange Inserting;

		public event CollectionWithEvents.CollectionChange Removed;

		public event CollectionWithEvents.CollectionChange Removing;

		public delegate void CollectionClear();

		public delegate void CollectionChange(int index, object value);
	}
}