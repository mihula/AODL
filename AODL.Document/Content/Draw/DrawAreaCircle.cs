using AODL.Document;
using AODL.Document.Content;
using AODL.Document.Content.OfficeEvents;
using System;
using System.Xml;

namespace AODL.Document.Content.Draw
{
	public class DrawAreaCircle : DrawArea
	{
		public string CX
		{
			get
			{
				string innerText;
				XmlNode xmlNode = base.Node.SelectSingleNode("@svg:cx", base.Document.NamespaceManager);
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
				if (value != "")
				{
					if (base.Node.SelectSingleNode("@svg:cx", base.Document.NamespaceManager) == null)
					{
						base.CreateAttribute("cx", value, "svg");
					}
					base.Node.SelectSingleNode("@svg:cx", base.Document.NamespaceManager).InnerText = value;
				}
			}
		}

		public string CY
		{
			get
			{
				string innerText;
				XmlNode xmlNode = base.Node.SelectSingleNode("@svg:cy", base.Document.NamespaceManager);
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
				if (value != "")
				{
					if (base.Node.SelectSingleNode("@svg:cy", base.Document.NamespaceManager) == null)
					{
						base.CreateAttribute("cy", value, "svg");
					}
					base.Node.SelectSingleNode("@svg:cy", base.Document.NamespaceManager).InnerText = value;
				}
			}
		}

		public string Radius
		{
			get
			{
				string innerText;
				XmlNode xmlNode = base.Node.SelectSingleNode("@svg:r", base.Document.NamespaceManager);
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
				if (value != "")
				{
					if (base.Node.SelectSingleNode("@svg:r", base.Document.NamespaceManager) == null)
					{
						base.CreateAttribute("r", value, "svg");
					}
					base.Node.SelectSingleNode("@svg:r", base.Document.NamespaceManager).InnerText = value;
				}
			}
		}

		public DrawAreaCircle(IDocument document, XmlNode node) : base(document)
		{
			base.Node = node;
		}

		public DrawAreaCircle(IDocument document, string cx, string cy, string radius) : base(document)
		{
			this.CX = cx;
			this.CY = cy;
			this.Radius = radius;
		}

		public DrawAreaCircle(IDocument document, string cx, string cy, string radius, EventListeners listeners) : base(document)
		{
			this.CX = cx;
			this.CY = cy;
			this.Radius = radius;
			if (listeners != null)
			{
				base.Content.Add(listeners);
			}
		}

		protected override void NewXmlNode()
		{
			base.Node = base.Document.CreateNode("area-circle", "draw");
		}
	}
}