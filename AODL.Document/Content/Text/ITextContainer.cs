using System;

namespace AODL.Document.Content.Text
{
	public interface ITextContainer
	{
		ITextCollection TextContent
		{
			get;
			set;
		}
	}
}