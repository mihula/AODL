using AODL.Document;
using AODL.Document.Content;
using AODL.Document.Content.OfficeEvents;
using System;
using System.Xml;

namespace AODL.Document.Content.Draw
{
	public class DrawAreaRectangle : DrawArea
	{
		public string Height
		{
			get
			{
				string innerText;
				XmlNode xmlNode = base.Node.SelectSingleNode("@svg:height", base.Document.NamespaceManager);
				if (xmlNode == null)
				{
					innerText = null;
				}
				else
				{
					innerText = xmlNode.InnerText;
				}
				return innerText;
			}
			set
			{
				if (base.Node.SelectSingleNode("@svg:height", base.Document.NamespaceManager) == null)
				{
					base.CreateAttribute("height", value, "svg");
				}
				base.Node.SelectSingleNode("@svg:height", base.Document.NamespaceManager).InnerText = value;
			}
		}

		public string Width
		{
			get
			{
				string innerText;
				XmlNode xmlNode = base.Node.SelectSingleNode("@svg:width", base.Document.NamespaceManager);
				if (xmlNode == null)
				{
					innerText = null;
				}
				else
				{
					innerText = xmlNode.InnerText;
				}
				return innerText;
			}
			set
			{
				if (base.Node.SelectSingleNode("@svg:width", base.Document.NamespaceManager) == null)
				{
					base.CreateAttribute("width", value, "svg");
				}
				base.Node.SelectSingleNode("@svg:width", base.Document.NamespaceManager).InnerText = value;
			}
		}

		public string X
		{
			get
			{
				string innerText;
				XmlNode xmlNode = base.Node.SelectSingleNode("@svg:x", base.Document.NamespaceManager);
				if (xmlNode == null)
				{
					innerText = null;
				}
				else
				{
					innerText = xmlNode.InnerText;
				}
				return innerText;
			}
			set
			{
				if (base.Node.SelectSingleNode("@svg:x", base.Document.NamespaceManager) == null)
				{
					base.CreateAttribute("x", value, "svg");
				}
				base.Node.SelectSingleNode("@svg:x", base.Document.NamespaceManager).InnerText = value;
			}
		}

		public string Y
		{
			get
			{
				string innerText;
				XmlNode xmlNode = base.Node.SelectSingleNode("@svg:y", base.Document.NamespaceManager);
				if (xmlNode == null)
				{
					innerText = null;
				}
				else
				{
					innerText = xmlNode.InnerText;
				}
				return innerText;
			}
			set
			{
				if (base.Node.SelectSingleNode("@svg:y", base.Document.NamespaceManager) == null)
				{
					base.CreateAttribute("y", value, "svg");
				}
				base.Node.SelectSingleNode("@svg:y", base.Document.NamespaceManager).InnerText = value;
			}
		}

		public DrawAreaRectangle(IDocument document, XmlNode node) : base(document)
		{
			base.Node = node;
		}

		public DrawAreaRectangle(IDocument document, string x, string y, string width, string height) : base(document)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
		}

		public DrawAreaRectangle(IDocument document, string x, string y, string width, string height, EventListeners listeners) : base(document)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
			if (listeners != null)
			{
				base.Content.Add(listeners);
			}
		}

		protected override void NewXmlNode()
		{
			base.Node = base.Document.CreateNode("area-rectangle", "draw");
		}
	}
}