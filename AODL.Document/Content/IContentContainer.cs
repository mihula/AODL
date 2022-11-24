using System;

namespace AODL.Document.Content
{
	public interface IContentContainer
	{
		IContentCollection Content
		{
			get;
			set;
		}
	}
}