using AODL.Document;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.Document.TextDocuments;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Import.OpenDocument.NodeProcessors
{
	public class LocalStyleProcessor
	{
		private IDocument _document;

		private bool _common;

		private string _xmlPathCommonStyle = "/office:document-content/office:styles";

		private XmlDocument _currentXmlDocument;

		public XmlDocument CurrentXmlDocument
		{
			get
			{
				return this._currentXmlDocument;
			}
			set
			{
				this._currentXmlDocument = value;
			}
		}

		public LocalStyleProcessor(IDocument document, bool readCommonStyles)
		{
			this._document = document;
			this._currentXmlDocument = this._document.XmlDoc;
			this._common = readCommonStyles;
		}

		private CellProperties CreateCellProperties(IStyle style, XmlNode propertyNode)
		{
			return new CellProperties(style as CellStyle)
			{
				Node = propertyNode
			};
		}

		private void CreateCellStyle(XmlNode styleNode)
		{
			CellStyle cellStyle = new CellStyle(this._document)
			{
				Node = styleNode.CloneNode(true)
			};
			IPropertyCollection propertyCollection = new IPropertyCollection();
			if (styleNode.HasChildNodes)
			{
				foreach (XmlNode childNode in styleNode.ChildNodes)
				{
					IProperty property = this.GetProperty(cellStyle, childNode.CloneNode(true));
					if (property == null)
					{
						continue;
					}
					propertyCollection.Add(property);
				}
			}
			cellStyle.Node.InnerXml=("");
			foreach (IProperty property1 in propertyCollection)
			{
				cellStyle.PropertyCollection.Add(property1);
			}
			if (this._common)
			{
				this._document.CommonStyles.Add(cellStyle);
			}
			else
			{
				this._document.Styles.Add(cellStyle);
			}
		}

		private ColumnProperties CreateColumnProperties(IStyle style, XmlNode propertyNode)
		{
			return new ColumnProperties(style)
			{
				Node = propertyNode
			};
		}

		private void CreateColumnStyle(XmlNode styleNode)
		{
			ColumnStyle columnStyle = new ColumnStyle(this._document)
			{
				Node = styleNode.CloneNode(true)
			};
			IPropertyCollection propertyCollection = new IPropertyCollection();
			if (styleNode.HasChildNodes)
			{
				foreach (XmlNode childNode in styleNode.ChildNodes)
				{
					IProperty property = this.GetProperty(columnStyle, childNode.CloneNode(true));
					if (property == null)
					{
						continue;
					}
					propertyCollection.Add(property);
				}
			}
			columnStyle.Node.InnerXml=("");
			foreach (IProperty property1 in propertyCollection)
			{
				columnStyle.PropertyCollection.Add(property1);
			}
			if (this._common)
			{
				this._document.CommonStyles.Add(columnStyle);
			}
			else
			{
				this._document.Styles.Add(columnStyle);
			}
		}

		private void CreateFrameStyle(XmlNode styleNode)
		{
			FrameStyle frameStyle = new FrameStyle(this._document, styleNode.CloneNode(true));
			IPropertyCollection propertyCollection = new IPropertyCollection();
			if (styleNode.HasChildNodes)
			{
				foreach (XmlNode childNode in styleNode.ChildNodes)
				{
					IProperty property = this.GetProperty(frameStyle, childNode.CloneNode(true));
					if (property == null)
					{
						continue;
					}
					propertyCollection.Add(property);
				}
			}
			frameStyle.Node.InnerXml=("");
			foreach (IProperty property1 in propertyCollection)
			{
				frameStyle.PropertyCollection.Add(property1);
			}
			if (this._common)
			{
				this._document.CommonStyles.Add(frameStyle);
			}
			else
			{
				this._document.Styles.Add(frameStyle);
			}
		}

		private GraphicProperties CreateGraphicProperties(IStyle style, XmlNode propertyNode)
		{
			return new GraphicProperties(style)
			{
				Node = propertyNode
			};
		}

		private void CreateListStyle(XmlNode styleNode)
		{
			ListStyle listStyle = new ListStyle(this._document, styleNode);
			if (this._common)
			{
				this._document.CommonStyles.Add(listStyle);
			}
			else
			{
				this._document.Styles.Add(listStyle);
			}
		}

		private ParagraphProperties CreateParagraphProperties(IStyle style, XmlNode propertyNode)
		{
			ParagraphProperties paragraphProperty = new ParagraphProperties(style)
			{
				Node = propertyNode
			};
			TabStopStyleCollection tabStopStyleCollection = new TabStopStyleCollection(this._document);
			if (propertyNode.HasChildNodes)
			{
				foreach (XmlNode childNode in propertyNode.ChildNodes)
				{
					if (childNode.Name != "style:tab-stops")
					{
						continue;
					}
					foreach (XmlNode xmlNode in childNode.ChildNodes)
					{
						if (xmlNode.Name != "style:tab-stop")
						{
							continue;
						}
						tabStopStyleCollection.Add(this.CreateTabStopStyle(xmlNode));
					}
				}
			}
			if (tabStopStyleCollection.Count > 0)
			{
				paragraphProperty.TabStopStyleCollection = tabStopStyleCollection;
			}
			return paragraphProperty;
		}

		private void CreateParagraphStyle(XmlNode styleNode)
		{
			ParagraphStyle paragraphStyle = new ParagraphStyle(this._document, styleNode);
			IPropertyCollection propertyCollection = new IPropertyCollection();
			if (styleNode.HasChildNodes)
			{
				foreach (XmlNode childNode in styleNode.ChildNodes)
				{
					IProperty property = this.GetProperty(paragraphStyle, childNode.CloneNode(true));
					if (property == null)
					{
						continue;
					}
					propertyCollection.Add(property);
				}
			}
			paragraphStyle.Node.InnerXml=("");
			foreach (IProperty property1 in propertyCollection)
			{
				paragraphStyle.PropertyCollection.Add(property1);
			}
			if (this._common)
			{
				this._document.CommonStyles.Add(paragraphStyle);
			}
			else
			{
				this._document.Styles.Add(paragraphStyle);
			}
		}

		private RowProperties CreateRowProperties(IStyle style, XmlNode propertyNode)
		{
			return new RowProperties(style)
			{
				Node = propertyNode
			};
		}

		private void CreateRowStyle(XmlNode styleNode)
		{
			RowStyle rowStyle = new RowStyle(this._document)
			{
				Node = styleNode.CloneNode(true)
			};
			IPropertyCollection propertyCollection = new IPropertyCollection();
			if (styleNode.HasChildNodes)
			{
				foreach (XmlNode childNode in styleNode.ChildNodes)
				{
					IProperty property = this.GetProperty(rowStyle, childNode.CloneNode(true));
					if (property == null)
					{
						continue;
					}
					propertyCollection.Add(property);
				}
			}
			rowStyle.Node.InnerXml=("");
			foreach (IProperty property1 in propertyCollection)
			{
				rowStyle.PropertyCollection.Add(property1);
			}
			if (this._common)
			{
				this._document.CommonStyles.Add(rowStyle);
			}
			else
			{
				this._document.Styles.Add(rowStyle);
			}
		}

		private SectionProperties CreateSectionProperties(IStyle style, XmlNode propertyNode)
		{
			return new SectionProperties(style)
			{
				Node = propertyNode
			};
		}

		private void CreateSectionStyle(XmlNode styleNode)
		{
			SectionStyle sectionStyle = new SectionStyle(this._document, styleNode.CloneNode(true));
			IPropertyCollection propertyCollection = new IPropertyCollection();
			if (styleNode.HasChildNodes)
			{
				foreach (XmlNode childNode in styleNode.ChildNodes)
				{
					IProperty property = this.GetProperty(sectionStyle, childNode.CloneNode(true));
					if (property == null)
					{
						continue;
					}
					propertyCollection.Add(property);
				}
			}
			sectionStyle.Node.InnerXml=("");
			foreach (IProperty property1 in propertyCollection)
			{
				sectionStyle.PropertyCollection.Add(property1);
			}
			if (this._common)
			{
				this._document.CommonStyles.Add(sectionStyle);
			}
			else
			{
				this._document.Styles.Add(sectionStyle);
			}
		}

		private TableProperties CreateTableProperties(IStyle style, XmlNode propertyNode)
		{
			return new TableProperties(style)
			{
				Node = propertyNode
			};
		}

		private void CreateTableStyle(XmlNode styleNode)
		{
			TableStyle tableStyle = new TableStyle(this._document)
			{
				Node = styleNode.CloneNode(true)
			};
			IPropertyCollection propertyCollection = new IPropertyCollection();
			if (styleNode.HasChildNodes)
			{
				foreach (XmlNode childNode in styleNode.ChildNodes)
				{
					IProperty property = this.GetProperty(tableStyle, childNode.CloneNode(true));
					if (property == null)
					{
						continue;
					}
					propertyCollection.Add(property);
				}
			}
			tableStyle.Node.InnerXml=("");
			foreach (IProperty property1 in propertyCollection)
			{
				tableStyle.PropertyCollection.Add(property1);
			}
			if (this._common)
			{
				this._document.CommonStyles.Add(tableStyle);
			}
			else
			{
				this._document.Styles.Add(tableStyle);
			}
		}

		private TabStopStyle CreateTabStopStyle(XmlNode styleNode)
		{
			TabStopStyle tabStopStyle = new TabStopStyle(this._document, styleNode.CloneNode(true));
			IPropertyCollection propertyCollection = new IPropertyCollection();
			return tabStopStyle;
		}

		private TextProperties CreateTextProperties(IStyle style, XmlNode propertyNode)
		{
			return new TextProperties(style)
			{
				Node = propertyNode
			};
		}

		private void CreateTextStyle(XmlNode styleNode)
		{
			TextStyle textStyle = new TextStyle(this._document, styleNode.CloneNode(true));
			IPropertyCollection propertyCollection = new IPropertyCollection();
			if (styleNode.HasChildNodes)
			{
				foreach (XmlNode childNode in styleNode.ChildNodes)
				{
					IProperty property = this.GetProperty(textStyle, childNode.CloneNode(true));
					if (property == null)
					{
						continue;
					}
					propertyCollection.Add(property);
				}
			}
			textStyle.Node.InnerXml=("");
			foreach (IProperty property1 in propertyCollection)
			{
				textStyle.PropertyCollection.Add(property1);
			}
			if (this._common)
			{
				this._document.CommonStyles.Add(textStyle);
			}
			else
			{
				this._document.Styles.Add(textStyle);
			}
		}

		private UnknownProperty CreateUnknownProperties(IStyle style, XmlNode propertyNode)
		{
			return new UnknownProperty(style, propertyNode);
		}

		private void CreateUnknownStyle(XmlNode styleNode)
		{
			if (this._common)
			{
				this._document.CommonStyles.Add(new UnknownStyle(this._document, styleNode.CloneNode(true)));
			}
			else
			{
				this._document.Styles.Add(new UnknownStyle(this._document, styleNode.CloneNode(true)));
			}
		}

		private IProperty GetProperty(IStyle style, XmlNode propertyNode)
		{
			IProperty property;
			if (propertyNode == null || style == null)
			{
				property = null;
			}
			else if (propertyNode.Name == "style:table-cell-properties")
			{
				property = this.CreateCellProperties(style, propertyNode);
			}
			else if (propertyNode.Name == "style:table-column-properties")
			{
				property = this.CreateColumnProperties(style, propertyNode);
			}
			else if (propertyNode.Name == "style:graphic-properties")
			{
				property = this.CreateGraphicProperties(style, propertyNode);
			}
			else if (propertyNode.Name == "style:paragraph-properties")
			{
				property = this.CreateParagraphProperties(style, propertyNode);
			}
			else if (propertyNode.Name == "style:table-row-properties")
			{
				property = this.CreateRowProperties(style, propertyNode);
			}
			else if (propertyNode.Name == "style:section-properties")
			{
				property = this.CreateSectionProperties(style, propertyNode);
			}
			else if (propertyNode.Name == "style:table-properties")
			{
				property = this.CreateTableProperties(style, propertyNode);
			}
			else if (propertyNode.Name != "style:text-properties")
			{
				property = this.CreateUnknownProperties(style, propertyNode);
			}
			else
			{
				property = this.CreateTextProperties(style, propertyNode);
			}
			return property;
		}

		internal void ReadStyles()
		{
			XmlNode xmlNode = null;
			xmlNode = (this._common ? this._document.XmlDoc.SelectSingleNode(this._xmlPathCommonStyle, this._document.NamespaceManager) : this._document.XmlDoc.SelectSingleNode(TextDocumentHelper.AutomaticStylePath, this._document.NamespaceManager));
			if (xmlNode != null)
			{
				foreach (XmlNode xmlNode1 in xmlNode)
				{
					XmlNode xmlNode2 = xmlNode1.SelectSingleNode("@style:family", this._document.NamespaceManager);
					if (xmlNode2 == null)
					{
						if (xmlNode1.Name != "text:list-style")
						{
							this.CreateUnknownStyle(xmlNode1);
						}
						else
						{
							this.CreateListStyle(xmlNode1);
						}
					}
					else if (xmlNode2.InnerText == "table")
					{
						this.CreateTableStyle(xmlNode1);
					}
					else if (xmlNode2.InnerText == "table-column")
					{
						this.CreateColumnStyle(xmlNode1);
					}
					else if (xmlNode2.InnerText == "table-row")
					{
						this.CreateRowStyle(xmlNode1);
					}
					else if (xmlNode2.InnerText == "table-cell")
					{
						this.CreateCellStyle(xmlNode1);
					}
					else if (xmlNode2.InnerText == "paragraph")
					{
						this.CreateParagraphStyle(xmlNode1);
					}
					else if (xmlNode2.InnerText == "graphic")
					{
						this.CreateFrameStyle(xmlNode1);
					}
					else if (xmlNode2.InnerText == "section")
					{
						this.CreateSectionStyle(xmlNode1);
					}
					else if (xmlNode2.InnerText == "text")
					{
						this.CreateTextStyle(xmlNode1);
					}
				}
			}
			xmlNode.RemoveAll();
		}
	}
}