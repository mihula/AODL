using AODL.Document;
using AODL.Document.Content;
using AODL.Document.Content.Draw;
using AODL.Document.Content.OfficeEvents;
using AODL.Document.Content.Tables;
using AODL.Document.Content.Text;
using AODL.Document.Content.Text.Indexes;
using AODL.Document.Exceptions;
using AODL.Document.Import.OpenDocument;
using AODL.Document.SpreadsheetDocuments;
using AODL.Document.Styles;
using AODL.Document.TextDocuments;
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml;

namespace AODL.Document.Import.OpenDocument.NodeProcessors
{
	internal class MainContentProcessor
	{
		private bool _debugMode = false;

		private IDocument _document;

		public MainContentProcessor(IDocument document)
		{
			this._document = document;
		}

		public IContent CreateContent(XmlNode node)
		{
			IContent unknownContent;
			if (PrivateImplementationDetails.method0x60003d8 == null)
			{
				Hashtable hashtable = new Hashtable(40, 0.5f);
				hashtable.Add("text:p", 0);
				hashtable.Add("text:list", 1);
				hashtable.Add("text:list-item", 2);
				hashtable.Add("table:table", 3);
				hashtable.Add("table:table-column", 4);
				hashtable.Add("table:table-row", 5);
				hashtable.Add("table:table-header-rows", 6);
				hashtable.Add("table:table-cell", 7);
				hashtable.Add("table:covered-table-cell", 8);
				hashtable.Add("text:h", 9);
				hashtable.Add("text:table-of-content", 10);
				hashtable.Add("draw:frame", 11);
				hashtable.Add("draw:text-box", 12);
				hashtable.Add("draw:image", 13);
				hashtable.Add("draw:area-rectangle", 14);
				hashtable.Add("draw:area-circle", 15);
				hashtable.Add("draw:image-map", 16);
				hashtable.Add("office:event-listeners", 17);
				hashtable.Add("script:event-listener", 18);
				PrivateImplementationDetails.method0x60003d8 = hashtable;
			}
			try
			{
				string name = node.Name;
				object obj = name;
				if (name != null)
				{
					object item = PrivateImplementationDetails.method0x60003d8[obj];
					obj = item;
					if (item != null)
					{
						switch ((int)obj)
						{
							case 0:
							{
								unknownContent = this.CreateParagraph(node.CloneNode(true));
								return unknownContent;
							}
							case 1:
							{
								unknownContent = this.CreateList(node.CloneNode(true));
								return unknownContent;
							}
							case 2:
							{
								unknownContent = this.CreateListItem(node.CloneNode(true));
								return unknownContent;
							}
							case 3:
							{
								unknownContent = this.CreateTable(node.CloneNode(true));
								return unknownContent;
							}
							case 4:
							{
								unknownContent = this.CreateTableColumn(node.CloneNode(true));
								return unknownContent;
							}
							case 5:
							{
								unknownContent = this.CreateTableRow(node.CloneNode(true));
								return unknownContent;
							}
							case 6:
							{
								unknownContent = this.CreateTableHeaderRow(node.CloneNode(true));
								return unknownContent;
							}
							case 7:
							{
								unknownContent = this.CreateTableCell(node.CloneNode(true));
								return unknownContent;
							}
							case 8:
							{
								unknownContent = this.CreateTableCellSpan(node.CloneNode(true));
								return unknownContent;
							}
							case 9:
							{
								unknownContent = this.CreateHeader(node.CloneNode(true));
								return unknownContent;
							}
							case 10:
							{
								unknownContent = this.CreateTableOfContents(node.CloneNode(true));
								return unknownContent;
							}
							case 11:
							{
								unknownContent = this.CreateFrame(node.CloneNode(true));
								return unknownContent;
							}
							case 12:
							{
								unknownContent = this.CreateDrawTextBox(node.CloneNode(true));
								return unknownContent;
							}
							case 13:
							{
								unknownContent = this.CreateGraphic(node.CloneNode(true));
								return unknownContent;
							}
							case 14:
							{
								unknownContent = this.CreateDrawAreaRectangle(node.CloneNode(true));
								return unknownContent;
							}
							case 15:
							{
								unknownContent = this.CreateDrawAreaCircle(node.CloneNode(true));
								return unknownContent;
							}
							case 16:
							{
								unknownContent = this.CreateImageMap(node.CloneNode(true));
								return unknownContent;
							}
							case 17:
							{
								unknownContent = this.CreateEventListeners(node.CloneNode(true));
								return unknownContent;
							}
							case 18:
							{
								unknownContent = this.CreateEventListeners(node.CloneNode(true));
								return unknownContent;
							}
						}
					}
				}
				unknownContent = new UnknownContent(this._document, node.CloneNode(true));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while processing a content node.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = node,
					OriginalException = exception
				};
				throw aODLException;
			}
			return unknownContent;
		}

		private DrawAreaCircle CreateDrawAreaCircle(XmlNode drawAreaCircleNode)
		{
			DrawAreaCircle drawAreaCircle;
			try
			{
				DrawAreaCircle drawAreaCircle1 = new DrawAreaCircle(this._document, drawAreaCircleNode);
				IContentCollection contentCollection = new IContentCollection();
				if (drawAreaCircle1.Node != null)
				{
					foreach (XmlNode childNode in drawAreaCircle1.Node.ChildNodes)
					{
						IContent content = this.CreateContent(childNode);
						if (content == null)
						{
							continue;
						}
						contentCollection.Add(content);
					}
				}
				drawAreaCircle1.Node.InnerXml=("");
				foreach (IContent content1 in contentCollection)
				{
					drawAreaCircle1.Content.Add(content1);
				}
				drawAreaCircle = drawAreaCircle1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a DrawAreaCircle.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = drawAreaCircleNode,
					OriginalException = exception
				};
				throw aODLException;
			}
			return drawAreaCircle;
		}

		private DrawAreaRectangle CreateDrawAreaRectangle(XmlNode drawAreaRectangleNode)
		{
			DrawAreaRectangle drawAreaRectangle;
			try
			{
				DrawAreaRectangle drawAreaRectangle1 = new DrawAreaRectangle(this._document, drawAreaRectangleNode);
				IContentCollection contentCollection = new IContentCollection();
				if (drawAreaRectangle1.Node != null)
				{
					foreach (XmlNode childNode in drawAreaRectangle1.Node.ChildNodes)
					{
						IContent content = this.CreateContent(childNode);
						if (content == null)
						{
							continue;
						}
						contentCollection.Add(content);
					}
				}
				drawAreaRectangle1.Node.InnerXml=("");
				foreach (IContent content1 in contentCollection)
				{
					drawAreaRectangle1.Content.Add(content1);
				}
				drawAreaRectangle = drawAreaRectangle1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a DrawAreaRectangle.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = drawAreaRectangleNode,
					OriginalException = exception
				};
				throw aODLException;
			}
			return drawAreaRectangle;
		}

		private DrawTextBox CreateDrawTextBox(XmlNode drawTextBoxNode)
		{
			DrawTextBox drawTextBox;
			try
			{
				DrawTextBox drawTextBox1 = new DrawTextBox(this._document, drawTextBoxNode);
				IContentCollection contentCollection = new IContentCollection();
				foreach (XmlNode childNode in drawTextBox1.Node.ChildNodes)
				{
					IContent content = this.CreateContent(childNode);
					if (content == null)
					{
						if (this.OnWarning == null)
						{
							continue;
						}
						AODLWarning aODLWarning = new AODLWarning("Couldn't create a IContent object for a DrawTextBox.")
						{
							InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
							Node = childNode
						};
						this.OnWarning(aODLWarning);
					}
					else
					{
						contentCollection.Add(content);
					}
				}
				drawTextBox1.Node.InnerXml=("");
				foreach (IContent content1 in contentCollection)
				{
					drawTextBox1.Content.Add(content1);
				}
				drawTextBox = drawTextBox1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a Graphic.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = drawTextBoxNode,
					OriginalException = exception
				};
				throw aODLException;
			}
			return drawTextBox;
		}

		public EventListener CreateEventListener(XmlNode eventListenerNode)
		{
			EventListener eventListener;
			try
			{
				eventListener = new EventListener(this._document, eventListenerNode);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a EventListener.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = eventListenerNode,
					OriginalException = exception
				};
				throw aODLException;
			}
			return eventListener;
		}

		public EventListeners CreateEventListeners(XmlNode eventListenersNode)
		{
			EventListeners eventListener;
			try
			{
				EventListeners eventListener1 = new EventListeners(this._document, eventListenersNode);
				IContentCollection contentCollection = new IContentCollection();
				if (eventListener1.Node != null)
				{
					foreach (XmlNode childNode in eventListener1.Node.ChildNodes)
					{
						IContent content = this.CreateContent(childNode);
						if (content == null)
						{
							continue;
						}
						contentCollection.Add(content);
					}
				}
				eventListener1.Node.InnerXml=("");
				foreach (IContent content1 in contentCollection)
				{
					eventListener1.Content.Add(content1);
				}
				eventListener = eventListener1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a ImageMap.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = eventListenersNode,
					OriginalException = exception
				};
				throw aODLException;
			}
			return eventListener;
		}

		public Frame CreateFrame(XmlNode frameNode)
		{
			Frame frame;
			try
			{
				Frame frame1 = new Frame(this._document, null)
				{
					Node = frameNode
				};
				IContentCollection contentCollection = new IContentCollection();
				IStyle styleByName = this._document.Styles.GetStyleByName(frame1.StyleName);
				if (styleByName != null)
				{
					frame1.Style = styleByName;
				}
				else if (this.OnWarning != null)
				{
					AODLWarning aODLWarning = new AODLWarning("Couldn't recieve a FrameStyle.")
					{
						InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
						Node = frameNode
					};
					this.OnWarning(aODLWarning);
				}
				foreach (XmlNode childNode in frame1.Node.ChildNodes)
				{
					IContent content = this.CreateContent(childNode);
					if (content == null)
					{
						if (this.OnWarning == null)
						{
							continue;
						}
						AODLWarning aODLWarning1 = new AODLWarning("Couldn't create a IContent object for a frame.")
						{
							InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
							Node = childNode
						};
						this.OnWarning(aODLWarning1);
					}
					else
					{
						contentCollection.Add(content);
					}
				}
				frame1.Node.InnerXml=("");
				foreach (IContent content1 in contentCollection)
				{
					frame1.Content.Add(content1);
				}
				frame = frame1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a Frame.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = frameNode,
					OriginalException = exception
				};
				throw aODLException;
			}
			return frame;
		}

		private Graphic CreateGraphic(XmlNode graphicnode)
		{
			Graphic graphic;
			try
			{
				Graphic graphic1 = new Graphic(this._document, null, null);
				graphic1.Node = graphicnode;
				graphic1.GraphicRealPath = string.Concat(OpenDocumentImporter.dir, graphic1.HRef.Replace("/", "\\"));
				graphic = graphic1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a Graphic.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = graphicnode,
					OriginalException = exception
				};
				throw aODLException;
			}
			return graphic;
		}

		public Header CreateHeader(XmlNode headernode)
		{
			Header header;
			try
			{
				if (this._debugMode)
				{
					this.LogNode(headernode, "Log header node before");
				}
				Header header1 = new Header(headernode, this._document);
				ITextCollection textCollection = new ITextCollection();
				IStyle styleByName = this._document.Styles.GetStyleByName(header1.StyleName);
				if (styleByName != null)
				{
					header1.Style = styleByName;
				}
				foreach (XmlNode childNode in header1.Node.ChildNodes)
				{
					IText text = (new TextContentProcessor()).CreateTextObject(this._document, childNode);
					if (text == null)
					{
						if (this.OnWarning == null)
						{
							continue;
						}
						AODLWarning aODLWarning = new AODLWarning("Couldn't create IText object from header child node!.")
						{
							InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
							Node = childNode
						};
						this.OnWarning(aODLWarning);
					}
					else
					{
						textCollection.Add(text);
					}
				}
				header1.Node.InnerXml=("");
				foreach (IText text1 in textCollection)
				{
					if (this._debugMode)
					{
						this.LogNode(text1.Node, "Log IText node read from header");
					}
					header1.TextContent.Add(text1);
				}
				header = header1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a Header.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = headernode,
					OriginalException = exception
				};
				throw aODLException;
			}
			return header;
		}

		private ImageMap CreateImageMap(XmlNode imageMapNode)
		{
			ImageMap imageMap;
			try
			{
				ImageMap imageMap1 = new ImageMap(this._document, imageMapNode);
				IContentCollection contentCollection = new IContentCollection();
				if (imageMap1.Node != null)
				{
					foreach (XmlNode childNode in imageMap1.Node.ChildNodes)
					{
						IContent content = this.CreateContent(childNode);
						if (content == null)
						{
							continue;
						}
						contentCollection.Add(content);
					}
				}
				imageMap1.Node.InnerXml=("");
				foreach (IContent content1 in contentCollection)
				{
					imageMap1.Content.Add(content1);
				}
				imageMap = imageMap1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a ImageMap.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = imageMapNode,
					OriginalException = exception
				};
				throw aODLException;
			}
			return imageMap;
		}

		private List CreateList(XmlNode listNode)
		{
			List list;
			try
			{
				List list1 = new List(this._document, listNode);
				IContentCollection contentCollection = new IContentCollection();
				IStyle styleByName = this._document.Styles.GetStyleByName(list1.StyleName);
				if (styleByName != null)
				{
					list1.Style = styleByName;
				}
				foreach (XmlNode childNode in list1.Node.ChildNodes)
				{
					IContent content = this.CreateContent(childNode);
					if (content == null)
					{
						continue;
					}
					contentCollection.Add(content);
				}
				list1.Node.InnerXml=("");
				foreach (IContent content1 in contentCollection)
				{
					list1.Content.Add(content1);
				}
				list = list1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a List.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = listNode,
					OriginalException = exception
				};
				throw aODLException;
			}
			return list;
		}

		private ListItem CreateListItem(XmlNode node)
		{
			ListItem listItem;
			try
			{
				ListItem listItem1 = new ListItem(this._document);
				IContentCollection contentCollection = new IContentCollection();
				listItem1.Node = node;
				foreach (XmlNode childNode in listItem1.Node.ChildNodes)
				{
					IContent content = this.CreateContent(childNode);
					if (content == null)
					{
						if (this.OnWarning == null)
						{
							continue;
						}
						AODLWarning aODLWarning = new AODLWarning("Couldn't create a IContent object for a ListItem.")
						{
							InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
							Node = childNode
						};
						this.OnWarning(aODLWarning);
					}
					else
					{
						contentCollection.Add(content);
					}
				}
				listItem1.Node.InnerXml=("");
				foreach (IContent content1 in contentCollection)
				{
					listItem1.Content.Add(content1);
				}
				listItem = listItem1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a ListItem.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = node,
					OriginalException = exception
				};
				throw aODLException;
			}
			return listItem;
		}

		public void CreateMainContent(XmlNode node)
		{
			try
			{
				foreach (XmlNode childNode in node.ChildNodes)
				{
					IContent content = this.CreateContent(childNode.CloneNode(true));
					if (content == null)
					{
						if (this.OnWarning == null)
						{
							continue;
						}
						AODLWarning aODLWarning = new AODLWarning("A couldn't create any content from an an first level node!.")
						{
							InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
							Node = childNode
						};
						this.OnWarning(aODLWarning);
					}
					else
					{
						this._document.Content.Add(content);
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while processing a content node.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = node,
					OriginalException = exception
				};
				throw aODLException;
			}
		}

		public Paragraph CreateParagraph(XmlNode paragraphNode)
		{
			Paragraph paragraph;
			try
			{
				Paragraph paragraph1 = new Paragraph(paragraphNode, this._document);
				IStyle styleByName = this._document.Styles.GetStyleByName(paragraph1.StyleName);
				if (styleByName != null)
				{
					paragraph1.Style = styleByName;
				}
				else if (paragraph1.StyleName != "Standard" && paragraph1.StyleName != "Table_20_Contents" && paragraph1.StyleName != "Text_20_body" && this._document is TextDocument && this._document.CommonStyles.GetStyleByName(paragraph1.StyleName) == null && this.OnWarning != null)
				{
					AODLWarning aODLWarning = new AODLWarning("A ParagraphStyle wasn't found.")
					{
						InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
						Node = paragraphNode
					};
					this.OnWarning(aODLWarning);
				}
				paragraph = this.ReadParagraphTextContent(paragraph1);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a Paragraph.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = paragraphNode,
					OriginalException = exception
				};
				throw aODLException;
			}
			return paragraph;
		}

		private Table CreateTable(XmlNode tableNode)
		{
			Table table;
			try
			{
				Table table1 = new Table(this._document, tableNode);
				IContentCollection contentCollection = new IContentCollection();
				IStyle styleByName = this._document.Styles.GetStyleByName(table1.StyleName);
				if (styleByName != null)
				{
					table1.Style = styleByName;
				}
				else if (this.OnWarning != null)
				{
					AODLWarning aODLWarning = new AODLWarning("Couldn't recieve a TableStyle.")
					{
						InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
						Node = tableNode
					};
					this.OnWarning(aODLWarning);
				}
				foreach (XmlNode childNode in table1.Node.ChildNodes)
				{
					IContent content = this.CreateContent(childNode);
					if (content == null)
					{
						if (this.OnWarning == null)
						{
							continue;
						}
						AODLWarning aODLWarning1 = new AODLWarning("Couldn't create IContent from a table node. Content is unknown table content!")
						{
							InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
							Node = content.Node
						};
						this.OnWarning(aODLWarning1);
					}
					else
					{
						contentCollection.Add(content);
					}
				}
				table1.Node.InnerText=("");
				foreach (IContent content1 in contentCollection)
				{
					if (content1 is Column)
					{
						((Column)content1).Table = table1;
						table1.ColumnCollection.Add(content1 as Column);
					}
					else if (content1 is Row)
					{
						((Row)content1).Table = table1;
						table1.RowCollection.Add(content1 as Row);
					}
					else if (!(content1 is RowHeader))
					{
						if (this.OnWarning == null)
						{
							continue;
						}
						AODLWarning aODLWarning2 = new AODLWarning("Couldn't create IContent from a table node.")
						{
							InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
							Node = tableNode
						};
						this.OnWarning(aODLWarning2);
						table1.Node.AppendChild(content1.Node);
					}
					else
					{
						((RowHeader)content1).Table = table1;
						table1.RowHeader = content1 as RowHeader;
					}
				}
				table = table1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a Table.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = tableNode,
					OriginalException = exception
				};
				throw aODLException;
			}
			return table;
		}

		private Cell CreateTableCell(XmlNode node)
		{
			Cell cell;
			try
			{
				Cell cell1 = new Cell(this._document, node);
				IContentCollection contentCollection = new IContentCollection();
				IStyle styleByName = this._document.Styles.GetStyleByName(cell1.StyleName);
				if (styleByName != null)
				{
					int num = 0;
					cell1.Style = styleByName;
					if (styleByName.StyleName == "ce244")
					{
						num = 1;
					}
				}
				foreach (XmlNode childNode in cell1.Node.ChildNodes)
				{
					IContent content = this.CreateContent(childNode);
					if (content == null)
					{
						if (this.OnWarning == null)
						{
							continue;
						}
						AODLWarning aODLWarning = new AODLWarning("Couldn't create IContent from a table cell.")
						{
							InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
							Node = childNode
						};
						this.OnWarning(aODLWarning);
					}
					else
					{
						contentCollection.Add(content);
					}
				}
				cell1.Node.InnerXml=("");
				foreach (IContent content1 in contentCollection)
				{
					cell1.Content.Add(content1);
				}
				cell = cell1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a Table Row.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = node,
					OriginalException = exception
				};
				throw aODLException;
			}
			return cell;
		}

		private CellSpan CreateTableCellSpan(XmlNode node)
		{
			CellSpan cellSpan;
			try
			{
				cellSpan = new CellSpan(this._document, node);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a Table CellSpan.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = node,
					OriginalException = exception
				};
				throw aODLException;
			}
			return cellSpan;
		}

		private Column CreateTableColumn(XmlNode node)
		{
			Column column;
			try
			{
				Column column1 = new Column(this._document, node);
				IStyle styleByName = this._document.Styles.GetStyleByName(column1.StyleName);
				if (styleByName != null)
				{
					column1.Style = styleByName;
				}
				column = column1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a Table Column.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = node,
					OriginalException = exception
				};
				throw aODLException;
			}
			return column;
		}

		private RowHeader CreateTableHeaderRow(XmlNode node)
		{
			RowHeader rowHeader;
			try
			{
				RowHeader rowHeader1 = new RowHeader(this._document, node);
				IContentCollection contentCollection = new IContentCollection();
				IStyle styleByName = this._document.Styles.GetStyleByName(rowHeader1.StyleName);
				if (styleByName != null)
				{
					rowHeader1.Style = styleByName;
				}
				foreach (XmlNode childNode in rowHeader1.Node.ChildNodes)
				{
					IContent content = this.CreateContent(childNode);
					if (content == null)
					{
						if (this.OnWarning == null)
						{
							continue;
						}
						AODLWarning aODLWarning = new AODLWarning("Couldn't create IContent from a table row.")
						{
							InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
							Node = childNode
						};
						this.OnWarning(aODLWarning);
					}
					else
					{
						contentCollection.Add(content);
					}
				}
				rowHeader1.Node.InnerXml=("");
				foreach (IContent content1 in contentCollection)
				{
					if (!(content1 is Row))
					{
						if (this.OnWarning == null)
						{
							continue;
						}
						AODLWarning aODLWarning1 = new AODLWarning("Couldn't create IContent from a row header node. Content is unknown table row header content!")
						{
							InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
							Node = content1.Node
						};
						this.OnWarning(aODLWarning1);
					}
					else
					{
						rowHeader1.RowCollection.Add(content1 as Row);
					}
				}
				rowHeader = rowHeader1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a Table Row.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = node,
					OriginalException = exception
				};
				throw aODLException;
			}
			return rowHeader;
		}

		private TableOfContents CreateTableOfContents(XmlNode tocNode)
		{
			TableOfContents tableOfContent;
			try
			{
				if (!(this._document is TextDocument))
				{
					tableOfContent = null;
				}
				else
				{
					TableOfContents tableOfContent1 = new TableOfContents((TextDocument)this._document, tocNode);
					IStyle styleByName = this._document.Styles.GetStyleByName(tableOfContent1.StyleName);
					if (styleByName != null)
					{
						tableOfContent1.Style = styleByName;
					}
					else if (this.OnWarning != null)
					{
						AODLWarning aODLWarning = new AODLWarning("A SectionStyle for the TableOfContents object wasn't found.")
						{
							InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
							Node = tocNode
						};
						this.OnWarning(aODLWarning);
					}
					XmlNodeList xmlNodeList = tocNode.SelectNodes("text:index-body/text:p", this._document.NamespaceManager);
					XmlNode xmlNode = tocNode.SelectSingleNode("text:index-body", this._document.NamespaceManager);
					tableOfContent1._indexBodyNode = xmlNode;
					IContentCollection contentCollection = new IContentCollection();
					foreach (XmlNode xmlNode1 in xmlNodeList)
					{
						Paragraph paragraph = this.CreateParagraph(xmlNode1);
						if (xmlNode != null)
						{
							xmlNode.RemoveChild(xmlNode1);
						}
						contentCollection.Add(paragraph);
					}
					foreach (IContent content in contentCollection)
					{
						tableOfContent1.Content.Add(content);
					}
					tableOfContent = tableOfContent1;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a TableOfContents.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = tocNode,
					OriginalException = exception
				};
				throw aODLException;
			}
			return tableOfContent;
		}

		private Row CreateTableRow(XmlNode node)
		{
			Row row;
			try
			{
				Row row1 = new Row(this._document, node);
				IContentCollection contentCollection = new IContentCollection();
				IStyle styleByName = this._document.Styles.GetStyleByName(row1.StyleName);
				if (styleByName != null)
				{
					row1.Style = styleByName;
				}
				foreach (XmlNode childNode in row1.Node.ChildNodes)
				{
					IContent content = this.CreateContent(childNode);
					if (content == null)
					{
						if (this.OnWarning == null)
						{
							continue;
						}
						AODLWarning aODLWarning = new AODLWarning("Couldn't create IContent from a table row.")
						{
							InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
							Node = childNode
						};
						this.OnWarning(aODLWarning);
					}
					else
					{
						contentCollection.Add(content);
					}
				}
				row1.Node.InnerXml=("");
				foreach (IContent content1 in contentCollection)
				{
					if (content1 is Cell)
					{
						((Cell)content1).Row = row1;
						row1.CellCollection.Add(content1 as Cell);
					}
					else if (!(content1 is CellSpan))
					{
						if (this.OnWarning == null)
						{
							continue;
						}
						AODLWarning aODLWarning1 = new AODLWarning("Couldn't create IContent from a row node. Content is unknown table row content!")
						{
							InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
							Node = content1.Node
						};
						this.OnWarning(aODLWarning1);
					}
					else
					{
						((CellSpan)content1).Row = row1;
						row1.CellSpanCollection.Add(content1 as CellSpan);
					}
				}
				row = row1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a Table Row.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = node,
					OriginalException = exception
				};
				throw aODLException;
			}
			return row;
		}

		private string GetAValueFromAnAttribute(XmlNode node, string attributname)
		{
			string innerText;
			try
			{
				Console.WriteLine(attributname);
				XmlNode xmlNode = node.SelectSingleNode(attributname, this._document.NamespaceManager);
				if (xmlNode != null)
				{
					innerText = xmlNode.InnerText;
					return innerText;
				}
			}
			catch (Exception exception)
			{
				throw;
			}
			innerText = "";
			return innerText;
		}

		private ListStyles GetListStyle(XmlNode node)
		{
			ListStyles listStyle;
			try
			{
				if (node.ChildNodes.Count > 0)
				{
					string name = node.ChildNodes.Item(0).Name;
					//"text:list-level-style-bullet";
					string str = name;
					string str1 = str;
					if (str != null)
					{
						str1 = string.IsInterned(str1);
						if (str1.Equals("text:list-level-style-bullet"))
						{
							listStyle = ListStyles.Bullet;
							return listStyle;
						}
					}
					listStyle = ListStyles.Number;
					return listStyle;
				}
			}
			catch (Exception exception)
			{
				throw;
			}
			listStyle = ListStyles.Number;
			return listStyle;
		}

		private void LogNode(XmlNode node, string msg)
		{
			Console.WriteLine("\n#############################\n{0}", msg);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(Console.Out);
			xmlTextWriter.Formatting = Formatting.Indented;
			node.WriteTo(xmlTextWriter);
			int num = 0;
			if (node.InnerText.StartsWith("Open your IDE"))
			{
				num = 1;
			}
		}

		public void ReadContentNodes()
		{
			try
			{
				XmlNode xmlNode = null;
				if (this._document is TextDocument)
				{
					xmlNode = this._document.XmlDoc.SelectSingleNode(TextDocumentHelper.OfficeTextPath, this._document.NamespaceManager);
				}
				else if (this._document is SpreadsheetDocument)
				{
					xmlNode = this._document.XmlDoc.SelectSingleNode("/office:document-content/office:body/office:spreadsheet", this._document.NamespaceManager);
				}
				if (xmlNode == null)
				{
					AODLException aODLException = new AODLException("Unknow content type.")
					{
						InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true))
					};
					throw aODLException;
				}
				this.CreateMainContent(xmlNode);
				xmlNode.RemoveAll();
			}
			catch (Exception exception)
			{
				AODLException aODLException1 = new AODLException("Error while trying to load the content file!")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true))
				};
				throw aODLException1;
			}
		}

		private Paragraph ReadParagraphTextContent(Paragraph paragraph)
		{
			Paragraph paragraph1;
			try
			{
				if (this._debugMode)
				{
					this.LogNode(paragraph.Node, "Log Paragraph node before");
				}
				ArrayList arrayList = new ArrayList();
				foreach (XmlNode childNode in paragraph.Node.ChildNodes)
				{
					TextContentProcessor textContentProcessor = new TextContentProcessor();
					IText text = textContentProcessor.CreateTextObject(this._document, childNode.CloneNode(true));
					if (text == null)
					{
						IContent content = this.CreateContent(childNode);
						if (content == null)
						{
							continue;
						}
						arrayList.Add(content);
					}
					else
					{
						arrayList.Add(text);
					}
				}
				paragraph.Node.InnerXml=("");
				foreach (object obj in arrayList)
				{
					if (obj is IText)
					{
						if (this._debugMode)
						{
							this.LogNode(((IText)obj).Node, "Log IText node read");
						}
						paragraph.TextContent.Add(obj as IText);
					}
					else if (!(obj is IContent))
					{
						if (this.OnWarning == null)
						{
							continue;
						}
						AODLWarning aODLWarning = new AODLWarning("Couldn't determine the type of a paragraph child node!.")
						{
							InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
							Node = paragraph.Node
						};
						this.OnWarning(aODLWarning);
					}
					else
					{
						if (this._debugMode)
						{
							this.LogNode(((IContent)obj).Node, "Log IContent node read");
						}
						paragraph.Content.Add(obj as IContent);
					}
				}
				if (this._debugMode)
				{
					this.LogNode(paragraph.Node, "Log Paragraph node after");
				}
				paragraph1 = paragraph;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create the Paragraph content.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = paragraph.Node,
					OriginalException = exception
				};
				throw aODLException;
			}
			return paragraph1;
		}

		public event MainContentProcessor.Warning OnWarning;

		public delegate void Warning(AODLWarning warning);
	}
}