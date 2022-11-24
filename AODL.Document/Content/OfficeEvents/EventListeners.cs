using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Content;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Content.OfficeEvents
{
	public class EventListeners : IContent, IContentContainer, ICloneable
	{
		private IContentCollection _content;

		private IDocument _document;

		private XmlNode _node;

		public IContentCollection Content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

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

		public EventListeners(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.InitStandards();
			this.Node = node;
		}

		public EventListeners(IDocument document, EventListener[] EventListeners)
		{
			this.Document = document;
			this.InitStandards();
			this.NewXmlNode();
			if (EventListeners != null)
			{
				EventListener[] eventListeners = EventListeners;
				for (int i = 0; i < (int)eventListeners.Length; i++)
				{
					EventListener eventListener = eventListeners[i];
					this._content.Add(eventListener);
				}
			}
		}

		public object Clone()
		{
			EventListeners eventListener = null;
			if (this.Document != null && this.Node != null)
			{
				MainContentProcessor mainContentProcessor = new MainContentProcessor(this.Document);
				eventListener = mainContentProcessor.CreateEventListeners(this.Node.CloneNode(true));
			}
			return eventListener;
		}

		private void Content_Inserted(int index, object value)
		{
			if (this.Node != null)
			{
				this.Node.AppendChild(((IContent)value).Node);
			}
		}

		private void Content_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IContent)value).Node);
		}

		private void InitStandards()
		{
			this.Content = new IContentCollection();
			this.Content.Inserted += new CollectionWithEvents.CollectionChange(this.Content_Inserted);
			this.Content.Removed += new CollectionWithEvents.CollectionChange(this.Content_Removed);
		}

		private void NewXmlNode()
		{
			this.Node = this.Document.CreateNode("event-listeners", "office");
		}
	}
}