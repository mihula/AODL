using System;

namespace AODL.Document.Export
{
	public interface IPublisherInfo
	{
		string Author
		{
			get;
		}

		string Description
		{
			get;
		}

		string InfoUrl
		{
			get;
		}
	}
}