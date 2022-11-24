using AODL.Document;
using AODL.Document.Styles.Properties;
using System;
using System.Xml;

namespace AODL.Document.Styles
{
	public class TabStopStyle : IStyle
	{
		private XmlNode _node;

		private IDocument _document;

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

		public string LeaderStyle
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:leader-style", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:leader-style", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("leader-style", value, "style");
				}
				this._node.SelectSingleNode("@style:leader-style", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string LeaderText
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:leader-text", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:leader-text", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("leader-text", value, "style");
				}
				this._node.SelectSingleNode("@style:leader-text", this.Document.NamespaceManager).InnerText = value;
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

		public string Position
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:position", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:position", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("postion", value, "style");
				}
				this._node.SelectSingleNode("@style:position", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public IPropertyCollection PropertyCollection
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
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:name", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:name", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("name", value, "style");
				}
				this._node.SelectSingleNode("@style:name", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public string Type
		{
			get
			{
				string innerText;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:type", this.Document.NamespaceManager);
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
				if (this._node.SelectSingleNode("@style:type", this.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("type", value, "style");
				}
				this._node.SelectSingleNode("@style:type", this.Document.NamespaceManager).InnerText = value;
			}
		}

		public TabStopStyle(IDocument document, double position)
		{
			this.Document = document;
			this.NewXmlNode(position);
		}

		public TabStopStyle(IDocument document, XmlNode node)
		{
			this.Document = document;
			this.Node = node;
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, prefix);
			xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		private void NewXmlNode(double position)
		{
			this.Node = this.Document.CreateNode("tab-stop", "style");
			XmlAttribute xmlAttribute = this.Document.CreateAttribute("position", "style");
			xmlAttribute.Value = string.Concat(position.ToString().Replace(",", "."), "cm");
			this.Node.Attributes.Append(xmlAttribute);
		}
	}
}