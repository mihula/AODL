using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Xml;

namespace AODL.Document.Exceptions
{
	public class AODLException : Exception
	{
		private XmlNode _node;

		private string _inMethod;

		private Hashtable _calledParams;

		private Exception _originalException;

		private string _message;

		public Hashtable CalledParams
		{
			get
			{
				return this._calledParams;
			}
			set
			{
				this._calledParams = value;
			}
		}

		public string InMethod
		{
			get
			{
				return this._inMethod;
			}
			set
			{
				this._inMethod = value;
			}
		}

		public override string Message
		{
			get
			{
				return this._message;
			}
		}

		public XmlNode Node
		{
			get
			{
				return this._node;
			}
			set
			{
				this._node = value;
			}
		}

		public Exception OriginalException
		{
			get
			{
				return this._originalException;
			}
			set
			{
				this._originalException = value;
			}
		}

		public AODLException()
		{
			this.CalledParams = new Hashtable();
		}

		public AODLException(string message)
		{
			this._message = message;
			this.CalledParams = new Hashtable();
		}

		public static string GetExceptionSourceInfo(StackFrame callStack)
		{
			string name = callStack.GetMethod().Name;
			string fileName = callStack.GetFileName();
			int fileLineNumber = callStack.GetFileLineNumber();
			string[] str = new string[] { fileName, ", in ", name, ", Line: ", fileLineNumber.ToString() };
			return string.Concat(str);
		}
	}
}