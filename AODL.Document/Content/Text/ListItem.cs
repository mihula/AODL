using AODL.Document;
using AODL.Document.Collections;
using AODL.Document.Content;
using AODL.Document.Styles;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Content.Text
{
	public class ListItem : IContent, IContentContainer, IHtml
	{
		private AODL.Document.Content.Text.List _list;

		private IContentCollection _content;

		private IDocument _document;

		private IStyle _style;

		private XmlNode _node;

		public IContentCollection Content
		{
			get
			{
				return this._content;
			}
			set
			{
				if (this._content != null)
				{
					foreach (IContent content in this._content)
					{
						this.Node.RemoveChild(content.Node);
					}
				}
				this._content = value;
				if (this._content != null)
				{
					foreach (IContent content1 in this._content)
					{
						this.Node.AppendChild(content1.Node);
					}
				}
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

		public AODL.Document.Content.Text.List List
		{
			get
			{
				return this._list;
			}
			set
			{
				this._list = value;
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
				return this._style;
			}
			set
			{
				this.StyleName = value.StyleName;
				this._style = value;
			}
		}

		public string StyleName
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@text:style-name", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@text:style-name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("style-name", value, "text");
				}
				this._node.SelectSingleNode("@text:style-name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public ListItem(IDocument document)
		{
			this.Document = document;
			this.InitStandards();
		}

		public ListItem(AODL.Document.Content.Text.List list)
		{
			this.Document = list.Document;
			this.List = list;
			this.InitStandards();
		}

		private void Content_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IContent)value).Node);
		}

		private void Content_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IContent)value).Node);
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
            xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		public string GetHtml()
		{
			string str = "<li>\n";
			foreach (IContent content in this.Content)
			{
				if (!(content is IHtml))
				{
					continue;
				}
				str = string.Concat(str, ((IHtml)content).GetHtml());
			}
			str = string.Concat(str, "</li>\n");
			return str;
		}

		private void InitStandards()
		{
			this.NewXmlNode();
			this.Content = new IContentCollection();
			this.Content.Inserted += new CollectionWithEvents.CollectionChange(this.Content_Inserted);
			this.Content.Removed += new CollectionWithEvents.CollectionChange(this.Content_Removed);
		}

		private void NewXmlNode()
		{
			this.Node = this.Document.CreateNode("list-item", "text");
		}
	}
}