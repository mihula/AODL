using AODL.Document;
using AODL.Document.Styles;
using System;
using System.Xml;

namespace AODL.Document.Styles.Properties
{
	public class SectionProperties : IProperty
	{
		private XmlNode _node;

		private IStyle _style;

		public bool Editable
		{
			get
			{
				bool flag;
				XmlNode xmlNode = this._node.SelectSingleNode("@style:editable", this.Style.Document.NamespaceManager);
				flag = (xmlNode == null ? false : Convert.ToBoolean(xmlNode.InnerText));
				return flag;
			}
			set
			{
				if (this._node.SelectSingleNode("@style:editable", this.Style.Document.NamespaceManager) == null)
				{
					this.CreateAttribute("editable", value.ToString(), "style");
				}
				this._node.SelectSingleNode("@style:editable", this.Style.Document.NamespaceManager).InnerText=(value.ToString());
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
				this._style = value;
			}
		}

		public SectionProperties(IStyle style)
		{
			this.Style = style;
			this.NewXmlNode();
		}

		public void AddStandardColumnStyle()
		{
			XmlNode xmlNode = this.Style.Document.CreateNode("columns", "style");
			XmlAttribute xmlAttribute = this.Style.Document.CreateAttribute("column-count", "fo");
			xmlAttribute.Value = ("0");
			xmlNode.Attributes.Append(xmlAttribute);
			xmlAttribute = this.Style.Document.CreateAttribute("column-gap", "fo");
			xmlAttribute.Value = ("0cm");
			xmlNode.Attributes.Append(xmlAttribute);
			this.Node.AppendChild(xmlNode);
		}

		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xmlAttribute = this.Style.Document.CreateAttribute(name, prefix);
			xmlAttribute.Value = text;
			this.Node.Attributes.Append(xmlAttribute);
		}

		private void NewXmlNode()
		{
			this.Node = this.Style.Document.CreateNode("section-properties", "style");
			XmlAttribute xmlAttribute = this.Style.Document.CreateAttribute("editable", "style");
			xmlAttribute.Value = "false";
			this.Node.Attributes.Append(xmlAttribute);
		}
	}
}