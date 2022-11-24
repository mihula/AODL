using System;

namespace AODL.Document.Exceptions
{
	public class AODLWarning : AODLException
	{
		public AODLWarning()
		{
		}

		public AODLWarning(string message) : base(message)
		{
		}
	}
}