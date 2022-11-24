using AODL.Document;
using AODL.Document.Content;
using AODL.Document.Content.Draw;
using AODL.Document.Content.Tables;
using AODL.Document.Content.Text;
using AODL.Document.Content.Text.Indexes;
using AODL.Document.Content.Text.TextControl;
using AODL.Document.Exceptions;
using AODL.Document.Helper;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.Document.TextDocuments;
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml;

namespace AODL.Document.Export.Html
{
	public class HTMLContentBuilder
	{
		private AODL.Document.Export.Html.HTMLStyleBuilder _hTMLStyleBuilder;

		private string _graphicTargetFolder;

		private string _nextImageMapName;

		public string GraphicTargetFolder
		{
			get
			{
				return this._graphicTargetFolder;
			}
			set
			{
				this._graphicTargetFolder = value;
			}
		}

		public AODL.Document.Export.Html.HTMLStyleBuilder HTMLStyleBuilder
		{
			get
			{
				return this._hTMLStyleBuilder;
			}
			set
			{
				this._hTMLStyleBuilder = value;
			}
		}

		public HTMLContentBuilder()
		{
			this.HTMLStyleBuilder = new AODL.Document.Export.Html.HTMLStyleBuilder();
		}

		public HTMLContentBuilder(string graphicTargetFolder)
		{
			this.GraphicTargetFolder = graphicTargetFolder;
			this.HTMLStyleBuilder = new AODL.Document.Export.Html.HTMLStyleBuilder();
		}

		private string GetAnchorLink(string outlineLinkTarget, XLink xLink)
		{
			string str;
			try
			{
				outlineLinkTarget = outlineLinkTarget.Replace("|outline", "");
				outlineLinkTarget = outlineLinkTarget.Substring(6);
				if (xLink.Document != null && xLink.Document.Content != null)
				{
					foreach (IContent content in xLink.Document.Content)
					{
						if (!(content is Header) || ((Header)content).OutLineLevel == null)
						{
							continue;
						}
						string str1 = "";
						foreach (IText textContent in ((Header)content).TextContent)
						{
							if (textContent.Text == null)
							{
								continue;
							}
							str1 = string.Concat(str1, textContent.Text);
						}
						if (!str1.EndsWith(outlineLinkTarget))
						{
							continue;
						}
						str = str1;
						return str;
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				if (HTMLContentBuilder.OnWarning != null)
				{
					AODLWarning aODLWarning = new AODLWarning("Exception while trying to get an anchor string from a XLink object.")
					{
						InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
						OriginalException = exception,
						Node = xLink.Node
					};
					HTMLContentBuilder.OnWarning(aODLWarning);
				}
			}
			str = null;
			return str;
		}

		public string GetCellAsHtml(Cell cell)
		{
			string str = "<td ";
			try
			{
				if (cell != null)
				{
					if (cell.ColumnRepeating != null)
					{
						str = string.Concat(str, "columnspan=\"", cell.ColumnRepeating, "\" ");
					}
					string cellStyleAsHtml = this.HTMLStyleBuilder.GetCellStyleAsHtml(cell.CellStyle);
					if (cellStyleAsHtml.Length > 0)
					{
						str = string.Concat(str, cellStyleAsHtml);
					}
					int cellIndex = -1;
					if (cell.Row != null)
					{
						cellIndex = cell.Row.GetCellIndex(cell);
					}
					ColumnStyle columnStyle = null;
					if (cellIndex != -1 && cell.Table != null && cell.Table.ColumnCollection != null && cell.Table.ColumnCollection.Count - 1 <= cellIndex && cell.Table.ColumnCollection[cellIndex].ColumnStyle != null)
					{
						columnStyle = cell.Table.ColumnCollection[cellIndex].ColumnStyle;
					}
					string columnStyleAsHtml = this.HTMLStyleBuilder.GetColumnStyleAsHtml(columnStyle);
					if (columnStyleAsHtml.Length > 0)
					{
						str = string.Concat(str, columnStyleAsHtml);
					}
					str = string.Concat(str, ">\n");
					string contentCollectionAsHtml = this.GetIContentCollectionAsHtml(cell.Content);
					if (contentCollectionAsHtml.Length > 0)
					{
						str = string.Concat(str, contentCollectionAsHtml);
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from a Cell object.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			str = (str.Equals("<td ") ? "" : string.Concat(str, "</td>\n"));
			return str;
		}

		public string GetDrawAreaAsHtml(DrawArea drawArea)
		{
			int pixelFromAnOfficeSizeValue;
			int num;
			string str = "<area shape=\"#type#\" coords=\"#coords#\" href=\"#link#\" target=\"_top\">\n";
			int pixelFromAnOfficeSizeValue1 = 0;
			try
			{
				if (drawArea != null)
				{
					if (drawArea is DrawAreaRectangle)
					{
						str = str.Replace("#link#", ((DrawAreaRectangle)drawArea).Href);
						pixelFromAnOfficeSizeValue = SizeConverter.GetPixelFromAnOfficeSizeValue(((DrawAreaRectangle)drawArea).X);
						num = SizeConverter.GetPixelFromAnOfficeSizeValue(((DrawAreaRectangle)drawArea).Y);
						int num1 = SizeConverter.GetPixelFromAnOfficeSizeValue(((DrawAreaRectangle)drawArea).Width);
						int pixelFromAnOfficeSizeValue2 = SizeConverter.GetPixelFromAnOfficeSizeValue(((DrawAreaRectangle)drawArea).Height);
						int num2 = pixelFromAnOfficeSizeValue + num1;
						int num3 = num + pixelFromAnOfficeSizeValue2;
						string[] strArray = new string[] { pixelFromAnOfficeSizeValue.ToString(), ",", num.ToString(), ",", num2.ToString(), ",", num3.ToString() };
						str = str.Replace("#coords#", string.Concat(strArray));
						str = str.Replace("#type#", "rect");
					}
					else if (drawArea is DrawAreaCircle)
					{
						str = str.Replace("#link#", ((DrawAreaCircle)drawArea).Href);
						pixelFromAnOfficeSizeValue = SizeConverter.GetPixelFromAnOfficeSizeValue(((DrawAreaCircle)drawArea).CX);
						num = SizeConverter.GetPixelFromAnOfficeSizeValue(((DrawAreaCircle)drawArea).CY);
						pixelFromAnOfficeSizeValue1 = SizeConverter.GetPixelFromAnOfficeSizeValue(((DrawAreaCircle)drawArea).Radius);
						string[] str1 = new string[] { pixelFromAnOfficeSizeValue.ToString(), ",", num.ToString(), ",", pixelFromAnOfficeSizeValue1.ToString() };
						str = str.Replace("#coords#", string.Concat(str1));
						str = str.Replace("#type#", "circle");
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from a ImageMap object.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			return str;
		}

		public string GetDrawFrameAsHtml(Frame frame)
		{
			string str = "<p>\n";
			try
			{
				if (frame != null && frame.Content != null)
				{
					bool flag = false;
					foreach (IContent content in frame.Content)
					{
						if (!(content is ImageMap))
						{
							continue;
						}
						this._nextImageMapName = Guid.NewGuid().ToString();
						flag = true;
						break;
					}
					if (!flag)
					{
						this._nextImageMapName = null;
					}
					str = string.Concat(str, this.GetIContentCollectionAsHtml(frame.Content));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from a Frame object.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			str = (str.Equals("<p>\n") ? "" : string.Concat(str, "</p>\n"));
			return str;
		}

		public string GetDrawTextBoxAsHtml(DrawTextBox drawTextBox)
		{
			string str = "";
			try
			{
				if (drawTextBox != null && drawTextBox.Content != null)
				{
					str = string.Concat(str, this.GetIContentCollectionAsHtml(drawTextBox.Content));
				}
				str = string.Concat(str, "\n");
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from a ImageMap object.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			return str;
		}

		public string GetFormatedTextAsHtml(FormatedText formatedText)
		{
			string str = "<span ";
			try
			{
				if (formatedText.TextContent != null)
				{
					string textStyleAsHtml = "";
					if (formatedText.TextStyle != null)
					{
						textStyleAsHtml = this.HTMLStyleBuilder.GetTextStyleAsHtml(formatedText.TextStyle.TextProperties);
					}
					if (textStyleAsHtml.Length <= 0)
					{
						string textStyleAsHtml1 = "";
						IStyle styleByName = formatedText.Document.CommonStyles.GetStyleByName(formatedText.StyleName);
						if (styleByName != null && styleByName is TextStyle)
						{
							textStyleAsHtml1 = this.HTMLStyleBuilder.GetTextStyleAsHtml(((TextStyle)styleByName).TextProperties);
							if (textStyleAsHtml1.Length > 0)
							{
								str = string.Concat(str, textStyleAsHtml1);
							}
						}
					}
					else
					{
						str = string.Concat(str, textStyleAsHtml);
					}
					str = string.Concat(str, ">\n");
					string textCollectionAsHtml = this.GetITextCollectionAsHtml(formatedText.TextContent, null);
					if (textCollectionAsHtml.Length > 0)
					{
						str = string.Concat(str, textCollectionAsHtml);
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from a FormatedText object.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			str = (str.Equals("<span ") ? "" : string.Concat(str, "</span>\n"));
			return str;
		}

		public string GetGraphicAsHtml(Graphic graphic)
		{
			string str = "<img hspace=\"12\" vspace=\"12\" ";
			try
			{
				if (graphic != null)
				{
					if (graphic.HRef != null)
					{
						string str1 = str;
						string[] graphicTargetFolder = new string[] { str1, "src=\"", this.GraphicTargetFolder, "/", graphic.HRef, "\" " };
						str = string.Concat(graphicTargetFolder);
					}
					string frameStyleAsHtml = "";
					if (graphic.Frame != null)
					{
						frameStyleAsHtml = this.HTMLStyleBuilder.GetFrameStyleAsHtml(graphic.Frame);
					}
					if (frameStyleAsHtml.Length > 0)
					{
						str = string.Concat(str, frameStyleAsHtml, " ");
					}
					if (this._nextImageMapName != null)
					{
						str = string.Concat(str, "usemap=\"#", this._nextImageMapName, "\"");
					}
					str = string.Concat(str, ">\n");
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from a Graphic object.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			str = (str.Equals("<img ") ? "" : string.Concat(str, "</img>\n"));
			return str;
		}

		public string GetHeadingAsHtml(Header heading)
		{
			string str = "<p ";
			try
			{
				if (heading != null)
				{
					string headingStyleAsHtml = this.HTMLStyleBuilder.GetHeadingStyleAsHtml(heading);
					if (headingStyleAsHtml.Length <= 0)
					{
						str = string.Concat(str.Replace(" ", ""), ">\n");
					}
					else
					{
						str = string.Concat(str, headingStyleAsHtml);
						str = string.Concat(str, ">\n");
					}
					string str1 = "";
					string outlineString = this.GetOutlineString(heading);
					string textCollectionAsHtml = this.GetITextCollectionAsHtml(heading.TextContent, null);
					if (textCollectionAsHtml.Length > 0)
					{
						str1 = string.Concat(str1, textCollectionAsHtml);
					}
					string str2 = str;
					string[] strArray = new string[] { str2, "<a name=\"", str1, "\">\n", outlineString, " ", str1, "\n</a>\n" };
					str = string.Concat(strArray);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from a Heading object.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			str = (str.Equals("<p ") ? "" : string.Concat(str, "</p>\n"));
			return str;
		}

		public string GetIContentCollectionAsHtml(IContentCollection iContentCollection)
		{
			string str = "";
			try
			{
				if (iContentCollection != null)
				{
					foreach (IContent content in iContentCollection)
					{
						if (content is Table)
						{
							str = string.Concat(str, this.GetTableAsHtml(content as Table));
						}
						else if (content is Paragraph)
						{
							str = string.Concat(str, this.GetParagraphAsHtml(content as Paragraph));
						}
						else if (content is List)
						{
							str = string.Concat(str, this.GetListAsHtml(content as List));
						}
						else if (content is Frame)
						{
							str = string.Concat(str, this.GetDrawFrameAsHtml(content as Frame));
						}
						else if (content is DrawTextBox)
						{
							str = string.Concat(str, this.GetDrawTextBoxAsHtml(content as DrawTextBox));
						}
						else if (content is Graphic)
						{
							str = string.Concat(str, this.GetGraphicAsHtml(content as Graphic));
						}
						else if (content is ListItem)
						{
							str = string.Concat(str, this.GetListItemAsHtml(content as ListItem));
						}
						else if (content is Header)
						{
							str = string.Concat(str, this.GetHeadingAsHtml(content as Header));
						}
						else if (content is TableOfContents)
						{
							str = string.Concat(str, this.GetTableOfContentsAsHtml(content as TableOfContents));
						}
						else if (content is UnknownContent)
						{
							str = string.Concat(str, this.GetUnknowContentAsHtml(content as UnknownContent));
						}
						else if (content is ImageMap)
						{
							str = string.Concat(str, this.GetImageMapAsHtml(content as ImageMap));
						}
						else if (!(content is DrawArea))
						{
							if (HTMLContentBuilder.OnWarning == null)
							{
								continue;
							}
							AODLWarning aODLWarning = new AODLWarning("Finding total unknown content. This should (could) never happen.")
							{
								InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true))
							};
							HTMLContentBuilder.OnWarning(aODLWarning);
						}
						else
						{
							str = string.Concat(str, this.GetDrawAreaAsHtml(content as DrawArea));
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from an IContentCollection.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			return str;
		}

		public string GetImageMapAsHtml(ImageMap imageMap)
		{
			string str = string.Concat("<div>\n<map name=\"", this._nextImageMapName, "\">\n");
			try
			{
				if (imageMap != null && imageMap.Content != null)
				{
					str = string.Concat(str, this.GetIContentCollectionAsHtml(imageMap.Content));
				}
				str = string.Concat(str, "</map>\n</div>\n");
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from a ImageMap object.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			return str;
		}

		public string GetITextCollectionAsHtml(ITextCollection iTextCollection, ParagraphStyle paragraphStyle)
		{
			string str = "";
			int num = 0;
			try
			{
				if (iTextCollection != null)
				{
					foreach (IText text in iTextCollection)
					{
						if (text is SimpleText)
						{
							string innerText = text.Node.InnerText;
							str = string.Concat(str, this.ReplaceControlNodes(innerText));
						}
						else if (text is FormatedText)
						{
							str = string.Concat(str, this.GetFormatedTextAsHtml(text as FormatedText));
						}
						else if (text is WhiteSpace)
						{
							str = string.Concat(str, this.GetWhiteSpacesAsHtml(text as WhiteSpace));
						}
						else if (text is TabStop)
						{
							str = string.Concat(str, this.GetTabStopAsHtml(text as TabStop, num, str, paragraphStyle));
							num++;
						}
						else if (text is XLink)
						{
							str = string.Concat(str, this.GetXLinkAsHtml(text as XLink));
						}
						else if (text is LineBreak)
						{
							str = string.Concat(str, this.GetLineBreakAsHtml());
						}
						else if (!(text is UnknownTextContent))
						{
							if (HTMLContentBuilder.OnWarning == null)
							{
								continue;
							}
							AODLWarning aODLWarning = new AODLWarning("Finding total unknown text content. This should (could) never happen.")
							{
								InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true))
							};
							HTMLContentBuilder.OnWarning(aODLWarning);
						}
						else
						{
							str = string.Concat(str, this.GetUnknowTextContentAsHtml(text as UnknownTextContent));
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from an ITextCollection.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			return str;
		}

		public string GetLineBreakAsHtml()
		{
			return "<br>\n";
		}

		public string GetListAsHtml(List list)
		{
			string str = "<";
			try
			{
				if (list != null)
				{
					str = (list.ListType != ListStyles.Number ? string.Concat(str, "ul>\n") : string.Concat(str, "ol>\n"));
					if (list.Content != null)
					{
						foreach (IContent content in list.Content)
						{
							str = string.Concat(str, this.GetIContentCollectionAsHtml(list.Content));
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from a List object.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			if (!str.StartsWith("<ol"))
			{
				str = (!str.StartsWith("<ul") ? "" : string.Concat(str, "</ul>\n"));
			}
			else
			{
				str = string.Concat(str, "</ol>\n");
			}
			return str;
		}

		public string GetListItemAsHtml(ListItem listItem)
		{
			string str = "<li>\n";
			try
			{
				if (listItem != null && listItem.Content != null)
				{
					str = string.Concat(str, this.GetIContentCollectionAsHtml(listItem.Content));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from a ListItem object.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			str = (str.Equals("<li>\n") ? "" : string.Concat(str, "</li>\n"));
			return str;
		}

		public string GetMixedContentAsHTML(ArrayList mixedContent, ParagraphStyle paragraphStyle)
		{
			string str = "";
			int num = 0;
			try
			{
				if (mixedContent != null)
				{
					foreach (object obj in mixedContent)
					{
						if (obj is SimpleText)
						{
							str = string.Concat(str, this.ReplaceControlNodes(((IText)obj).Node.InnerText));
						}
						else if (obj is FormatedText)
						{
							str = string.Concat(str, this.GetFormatedTextAsHtml(obj as FormatedText));
						}
						else if (obj is WhiteSpace)
						{
							str = string.Concat(str, this.GetWhiteSpacesAsHtml(obj as WhiteSpace));
						}
						else if (obj is TabStop)
						{
							str = string.Concat(str, this.GetTabStopAsHtml(obj as TabStop, num, str, paragraphStyle));
							num++;
						}
						else if (obj is XLink)
						{
							str = string.Concat(str, this.GetXLinkAsHtml(obj as XLink));
						}
						else if (obj is LineBreak)
						{
							str = string.Concat(str, this.GetLineBreakAsHtml());
						}
						else if (obj is UnknownTextContent)
						{
							str = string.Concat(str, this.GetUnknowTextContentAsHtml(obj as UnknownTextContent));
						}
						else if (obj is Table)
						{
							str = string.Concat(str, this.GetTableAsHtml(obj as Table));
						}
						else if (obj is Paragraph)
						{
							str = string.Concat(str, this.GetParagraphAsHtml(obj as Paragraph));
						}
						else if (obj is List)
						{
							str = string.Concat(str, this.GetListAsHtml(obj as List));
						}
						else if (obj is Frame)
						{
							str = string.Concat(str, this.GetDrawFrameAsHtml(obj as Frame));
						}
						else if (obj is Graphic)
						{
							str = string.Concat(str, this.GetGraphicAsHtml(obj as Graphic));
						}
						else if (obj is ListItem)
						{
							str = string.Concat(str, this.GetListItemAsHtml(obj as ListItem));
						}
						else if (obj is Header)
						{
							str = string.Concat(str, this.GetHeadingAsHtml(obj as Header));
						}
						else if (!(obj is UnknownContent))
						{
							if (HTMLContentBuilder.OnWarning == null)
							{
								continue;
							}
							AODLWarning aODLWarning = new AODLWarning("Finding total unknown content in mixed content. This should (could) never happen.")
							{
								InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true))
							};
							HTMLContentBuilder.OnWarning(aODLWarning);
						}
						else
						{
							str = string.Concat(str, this.GetUnknowContentAsHtml(obj as UnknownContent));
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from an ITextCollection.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			return str;
		}

		private string GetOutlineString(Header header)
		{
			string str;
			try
			{
				int num = 0;
				int num1 = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				if (header.Document != null && header.Document is TextDocument && header.Document.Content != null)
				{
					foreach (IContent content in header.Document.Content)
					{
						if (!(content is Header) || ((Header)content).OutLineLevel == null)
						{
							continue;
						}
						int num6 = Convert.ToInt32(((Header)content).OutLineLevel);
						if (num6 == 1)
						{
							num++;
							num1 = 0;
							num2 = 0;
							num3 = 0;
							num4 = 0;
							num5 = 0;
						}
						else if (num6 == 2)
						{
							num1++;
						}
						else if (num6 == 3)
						{
							num2++;
						}
						else if (num6 == 4)
						{
							num3++;
						}
						else if (num6 == 5)
						{
							num4++;
						}
						else if (num6 == 6)
						{
							num5++;
						}
						if (content != header)
						{
							continue;
						}
						string str1 = string.Concat(num.ToString(), ".");
						string str2 = "";
						if (num5 != 0)
						{
							str2 = string.Concat(".", num5.ToString(), ".");
						}
						if (num4 != 0)
						{
							str2 = string.Concat(str2, ".", num4.ToString(), ".");
						}
						if (num3 != 0)
						{
							str2 = string.Concat(str2, ".", num3.ToString(), ".");
						}
						if (num2 != 0)
						{
							str2 = string.Concat(str2, ".", num2.ToString(), ".");
						}
						if (num1 != 0)
						{
							str2 = string.Concat(str2, ".", num1.ToString(), ".");
						}
						str1 = string.Concat(str1, str2);
						str = str1.Replace("..", ".");
						return str;
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				if (HTMLContentBuilder.OnWarning != null)
				{
					AODLWarning aODLWarning = new AODLWarning("Exception while trying to get a outline string for a heading.")
					{
						InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
						OriginalException = exception,
						Node = header.Node
					};
					HTMLContentBuilder.OnWarning(aODLWarning);
				}
			}
			str = null;
			return str;
		}

		public string GetParagraphAsHtml(Paragraph paragraph)
		{
			string str = "<p ";
			try
			{
				if (paragraph != null)
				{
					if (paragraph.StyleName != null)
					{
						if (!(paragraph.StyleName != "Text_20_body") || !(paragraph.StyleName != "standard") || !(paragraph.StyleName != "Table_20_body"))
						{
							str = str.Replace(" ", "");
						}
						else
						{
							string paragraphStyleAsHtml = this.HTMLStyleBuilder.GetParagraphStyleAsHtml(paragraph.ParagraphStyle);
							if (paragraphStyleAsHtml.Length <= 0)
							{
								IStyle styleByName = paragraph.Document.CommonStyles.GetStyleByName(paragraph.StyleName);
								string paragraphStyleAsHtml1 = "";
								if (styleByName == null || !(styleByName is ParagraphStyle))
								{
									str = str.Replace(" ", "");
								}
								else
								{
									paragraphStyleAsHtml1 = this.HTMLStyleBuilder.GetParagraphStyleAsHtml(styleByName as ParagraphStyle);
									str = (paragraphStyleAsHtml1.Length <= 0 ? str.Replace(" ", "") : string.Concat(str, paragraphStyleAsHtml1));
								}
							}
							else
							{
								str = string.Concat(str, paragraphStyleAsHtml);
							}
						}
					}
					str = string.Concat(str, ">\n");
					string str1 = "<span ";
					bool flag = false;
					if (paragraph.ParagraphStyle == null)
					{
						string textStyleAsHtml = "";
						IStyle style = paragraph.Document.CommonStyles.GetStyleByName(paragraph.StyleName);
						if (style != null && style is ParagraphStyle)
						{
							textStyleAsHtml = this.HTMLStyleBuilder.GetTextStyleAsHtml(((ParagraphStyle)style).TextProperties);
							if (textStyleAsHtml.Length > 0)
							{
								str1 = string.Concat(str1, textStyleAsHtml, ">\n");
								str = string.Concat(str, str1);
								flag = true;
							}
						}
					}
					else
					{
						string textStyleAsHtml1 = this.HTMLStyleBuilder.GetTextStyleAsHtml(paragraph.ParagraphStyle.TextProperties);
						if (str1.Length > 0)
						{
							str1 = string.Concat(str1, textStyleAsHtml1, ">\n");
							str = string.Concat(str, str1);
							flag = true;
						}
					}
					string mixedContentAsHTML = this.GetMixedContentAsHTML(paragraph.MixedContent, paragraph.ParagraphStyle);
					str = (mixedContentAsHTML.Length <= 0 ? string.Concat(str, "&nbsp;") : string.Concat(str, mixedContentAsHTML, "&nbsp;"));
					if (str.Equals("<p "))
					{
						str = "";
					}
					else
					{
						str = (!flag ? string.Concat(str, "</p>\n") : string.Concat(str, "</span>\n</p>\n"));
					}
					if (str.Equals("<p >"))
					{
						str = "";
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from a Heading object.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			return str;
		}

		public string GetRowAsHtml(Row row)
		{
			string str = "<tr>\n";
			try
			{
				if (row != null && row.CellCollection != null)
				{
					foreach (Cell cellCollection in row.CellCollection)
					{
						str = string.Concat(str, this.GetCellAsHtml(cellCollection));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from a Row object.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			str = (str.Equals("<tr>\n") ? "" : string.Concat(str, "</tr>\n"));
			return str;
		}

		public string GetTableAsHtml(Table table)
		{
			string str = "<table border=\"1\" ";
			try
			{
				if (table != null)
				{
					string tableStyleAsHtml = this.HTMLStyleBuilder.GetTableStyleAsHtml(table.TableStyle);
					if (tableStyleAsHtml.Length > 0)
					{
						str = string.Concat(str, tableStyleAsHtml);
						str = string.Concat(str, ">\n");
					}
					if (table.RowCollection != null)
					{
						foreach (Row rowCollection in table.RowCollection)
						{
							str = string.Concat(str, this.GetRowAsHtml(rowCollection));
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from a Table object.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			str = (str.Equals("<table border=\"1\" ") ? "" : string.Concat(str, "</table>\n"));
			return str;
		}

		public string GetTableOfContentsAsHtml(TableOfContents tableOfContents)
		{
			string str = "";
			try
			{
				if (tableOfContents != null)
				{
					XmlNode xmlNode = tableOfContents.Node.SelectSingleNode("text:index-body/text:index-title/text:p", tableOfContents.Document.NamespaceManager);
					if (xmlNode != null)
					{
						str = string.Concat(str, "<p ", this.HTMLStyleBuilder.HeaderHtmlStyles[0], ">\n");
						str = string.Concat(str, xmlNode.InnerText);
						str = string.Concat(str, "\n</p>\n");
					}
					if (tableOfContents.Content != null)
					{
						str = string.Concat(str, this.GetIContentCollectionAsHtml(tableOfContents.Content));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from a TableOfContents.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			return str;
		}

		public string GetTabStopAsHtml(TabStop tabStop, int tabStopIndex, string htmlStringBefore, ParagraphStyle paragraphStyle)
		{
			string str = "&nbsp;&nbsp;&nbsp;&nbsp;";
			string str1 = "";
			try
			{
				if (paragraphStyle != null && paragraphStyle.ParagraphProperties != null && paragraphStyle.ParagraphProperties.TabStopStyleCollection != null && paragraphStyle.ParagraphProperties.TabStopStyleCollection.Count - 1 <= tabStopIndex)
				{
					TabStopStyle item = paragraphStyle.ParagraphProperties.TabStopStyleCollection[tabStopIndex];
					string leaderText = "&nbsp;";
					if (item.LeaderText != null)
					{
						leaderText = item.LeaderText;
					}
					string[] strArray = item.Position.Split(new char[] { '.' });
					if ((int)strArray.Length == 2)
					{
						double num = Convert.ToDouble(strArray[0]);
						if (htmlStringBefore != null)
						{
							for (int i = 0; i < htmlStringBefore.Length; i++)
							{
								num -= 0.5;
							}
						}
						if (num > 0.5)
						{
							for (double j = 0; j < num; j += 0.25)
							{
								str1 = string.Concat(str1, leaderText);
							}
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				if (HTMLContentBuilder.OnWarning != null)
				{
					AODLWarning aODLWarning = new AODLWarning("Exception while trying to build a simulated html tabstop.")
					{
						InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
						OriginalException = exception
					};
					HTMLContentBuilder.OnWarning(aODLWarning);
				}
			}
			if (str1.Length == 0)
			{
				str1 = str;
			}
			return str1;
		}

		public string GetUnknowContentAsHtml(UnknownContent unknownContent)
		{
			string str = "<span>\n";
			try
			{
				if (unknownContent != null && unknownContent.Node != null)
				{
					foreach (XmlNode childNode in unknownContent.Node.ChildNodes)
					{
						if (childNode.InnerText == null)
						{
							continue;
						}
						str = string.Concat(str, this.ReplaceControlNodes(string.Concat(childNode.InnerText, " ")));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from a UnknownContent object.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			str = (str.Equals("<span>\n") ? "" : string.Concat(str, "</span>\n"));
			return str;
		}

		public string GetUnknowTextContentAsHtml(UnknownTextContent unknownTextContent)
		{
			string str = "";
			try
			{
				if (unknownTextContent != null && unknownTextContent.Node != null)
				{
					str = string.Concat(str, this.ReplaceControlNodes(unknownTextContent.Node.InnerText));
				}
			}
			catch (Exception exception)
			{
			}
			return str;
		}

		public string GetWhiteSpacesAsHtml(WhiteSpace whiteSpace)
		{
			string str = "";
			int num = 0;
			try
			{
				if (whiteSpace.Count != null)
				{
					num = Convert.ToInt32(whiteSpace.Count);
				}
				for (int i = 0; i < num; i++)
				{
					str = string.Concat(str, "&nbsp;");
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				if (HTMLContentBuilder.OnWarning != null)
				{
					AODLWarning aODLWarning = new AODLWarning("Exception while trying to build HTML whitespaces.")
					{
						InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
						OriginalException = exception
					};
					HTMLContentBuilder.OnWarning(aODLWarning);
				}
			}
			return str;
		}

		public string GetXLinkAsHtml(XLink xLink)
		{
			string str = "<a ";
			try
			{
				if (xLink != null)
				{
					if (xLink.Href != null)
					{
						if (xLink.Href.ToLower().IndexOf("|outline") != -1)
						{
							string anchorLink = this.GetAnchorLink(xLink.Href, xLink);
							str = (anchorLink == null ? string.Concat(str, "href=\"", xLink.Href, "\" ") : string.Concat(str, "href=\"#", anchorLink, "\" "));
						}
						else
						{
							str = string.Concat(str, "href=\"", xLink.Href, "\" ");
						}
					}
					if (xLink.TargetFrameName != null)
					{
						str = string.Concat(str, "target=\"", xLink.TargetFrameName, "\">\n");
					}
					if (!str.EndsWith(">\n"))
					{
						str = string.Concat(str, ">\n");
					}
					string textCollectionAsHtml = this.GetITextCollectionAsHtml(xLink.TextContent, null);
					if (textCollectionAsHtml.Length > 0)
					{
						str = string.Concat(str, textCollectionAsHtml);
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML string from a XLink object.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			str = (str.Equals("<a ") ? "" : string.Concat(str, "</a>\n"));
			return str;
		}

		private string ReplaceControlNodes(string text)
		{
			try
			{
				text = text.Replace("<", "&lt;");
				text = text.Replace(">", "&gt;");
			}
			catch (Exception exception)
			{
			}
			return text;
		}

		public static event HTMLContentBuilder.Warning OnWarning;

		public delegate void Warning(AODLWarning warning);
	}
}