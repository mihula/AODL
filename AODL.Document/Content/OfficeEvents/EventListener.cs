using AODL.Document;
using AODL.Document.Content;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Content.OfficeEvents
{
	public class EventListener : IContent, ICloneable
	{
		private IDocument _document;

		private XmlNode _node;

		public IDocument Document
		{
			get
			{
				return this._document;
			}
			set
			{
				this._document = value;
			}
		}

		public string EventName
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@script:event-name", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@script:event-name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("event-name", value, "script");
				}
				this._node.SelectSingleNode("@script:event-name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string Href
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@xlink:href", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@xlink:href", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("href", value, "xlink");
				}
				this._node.SelectSingleNode("@xlink:href", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string Language
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@script:language", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@script:language", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("language", value, "script");
				}
				this._node.SelectSingleNode("@script:language", this.Document.NamespaceManager).InnerText = value;
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

		public IStyle Style
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string StyleName
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string XLinkType
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@xlink:type", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@xlink:type", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("type", value, "xlink");
				}
				this._node.SelectSingleNode("@xlink:type", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public EventListener(IDocument document, string eventname, string language, string href)
		{
			this.Document = document;
			this.NewXmlNode();
			this.EventName = eventname;
			this.Language = language;
			this.Href = href;
		}

		public EventListener(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.Node = node;
		}

		public object Clone()
		{
			EventListener eventListener = null;
			if (this.Document != null && this.Node != null)
			{
				MainContentProcessor mainContentProcessor = new MainContentProcessor(this.Document);
				eventListener = mainContentProcessor.CreateEventListener(this.Node.CloneNode(true));
			}
			return eventListener;
		}

		protected void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
			xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		private void NewXmlNode()
		{
			this.Node = this.Document.CreateNode("event-listener", "script");
		}
	}
}