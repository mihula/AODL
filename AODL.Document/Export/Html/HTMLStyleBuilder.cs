using AODL.Document;
using AODL.Document.Content.Draw;
using AODL.Document.Content.Text;
using AODL.Document.Exceptions;
using AODL.Document.Helper;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace AODL.Document.Export.Html
{
	public class HTMLStyleBuilder
	{
		private string[] _headings = new string[] { "style=\"font-family: Arial; font-weight: bold; font-size: 14pt; margin-top: 0.25cm; margin-bottom: 0.25cm; \"", "style=\"font-family: Arial; font-weight: bold; font-size: 16pt; margin-top: 0.25cm; margin-bottom: 0.25cm; \"", "style=\"font-family: Arial; font-weight: bold; font-style:italic; font-size: 14pt; margin-top: 0.25cm; margin-bottom: 0.25cm; \"" };

		private static string CSSStyle;

		public string[] HeaderHtmlStyles
		{
			get
			{
				return this._headings;
			}
		}

		static HTMLStyleBuilder()
		{
			HTMLStyleBuilder.CSSStyle = "<style type=\"text/css\">\n<!--\n.p {font-family: Times New Roman, Arial; font-weight: bold; font-size: 12pt; }.td {font-family: Times New Roman, Arial; font-weight: bold; font-size: 12pt; }.li {font-family: Times New Roman, Arial; font-weight: bold; font-size: 12pt; }\n-->\n</style>\n";
		}

		public HTMLStyleBuilder()
		{
		}

		public string GetAGlobalStylAsHtml(IDocument document, string styleName)
		{
			string str = "style=\"";
			try
			{
				var cs = document.CommonStyles;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException(string.Concat("Exception while trying to build a HTML style from a global style:", styleName))
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				throw aODLException;
			}
			str = (str.EndsWith("; ") ? string.Concat(str, "\"") : "");
			return str;
		}

		public string GetCellStyleAsHtml(CellStyle cellStyle)
		{
			string str = "";
			try
			{
				if (cellStyle != null && cellStyle.CellProperties != null && cellStyle.CellProperties.BackgroundColor != null)
				{
					str = string.Concat(str, "bgcolor=\"", cellStyle.CellProperties.BackgroundColor, "\" ");
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML style string from a CellStyle.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				if (cellStyle != null && cellStyle.Node != null)
				{
					aODLException.Node = cellStyle.Node;
				}
				throw aODLException;
			}
			return str;
		}

		public string GetColumnStyleAsHtml(ColumnStyle columnStyle)
		{
			string str = "";
			try
			{
				if (columnStyle != null && columnStyle.ColumnProperties != null && columnStyle.ColumnProperties.Width != null)
				{
					string width = columnStyle.ColumnProperties.Width;
					if (width.EndsWith("cm"))
					{
						width = width.Replace("cm", "");
					}
					else if (width.EndsWith("in"))
					{
						width = width.Replace("in", "");
					}
					try
					{
						double num = Convert.ToDouble(width, NumberFormatInfo.InvariantInfo);
						string pixelAsString = "";
						if (columnStyle.ColumnProperties.Width.EndsWith("cm"))
						{
							pixelAsString = SizeConverter.CmToPixelAsString(num);
						}
						else if (columnStyle.ColumnProperties.Width.EndsWith("in"))
						{
							pixelAsString = SizeConverter.InchToPixelAsString(num);
						}
						if (pixelAsString.Length > 0)
						{
							str = string.Concat("width=\"", pixelAsString, "\" ");
						}
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						if (this.OnWarning != null)
						{
							AODLWarning aODLWarning = new AODLWarning(string.Concat("Exception while trying to build a column width.: ", columnStyle.ColumnProperties.Width))
							{
								InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
								OriginalException = exception
							};
							this.OnWarning(aODLWarning);
						}
					}
				}
			}
			catch (Exception exception3)
			{
				Exception exception2 = exception3;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML style string from a CellStyle.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception2
				};
				if (columnStyle != null && columnStyle.Node != null)
				{
					aODLException.Node = columnStyle.Node;
				}
				throw aODLException;
			}
			return str;
		}

		public string GetFrameStyleAsHtml(Frame frame)
		{
			string str = "";
			try
			{
				if (frame != null)
				{
					string svgWidth = frame.SvgWidth;
					if (svgWidth != null)
					{
						if (svgWidth.EndsWith("cm"))
						{
							svgWidth = svgWidth.Replace("cm", "");
						}
						else if (svgWidth.EndsWith("in"))
						{
							svgWidth = svgWidth.Replace("in", "");
						}
					}
					string svgHeight = frame.SvgHeight;
					if (svgHeight != null)
					{
						if (svgHeight.EndsWith("cm"))
						{
							svgHeight = svgHeight.Replace("cm", "");
						}
						else if (svgHeight.EndsWith("in"))
						{
							svgHeight = svgHeight.Replace("in", "");
						}
					}
					try
					{
						if (svgWidth != null)
						{
							double num = Convert.ToDouble(svgWidth, NumberFormatInfo.InvariantInfo);
							string pixelAsString = "";
							if (frame.SvgWidth.EndsWith("cm"))
							{
								pixelAsString = SizeConverter.CmToPixelAsString(num);
							}
							else if (frame.SvgWidth.EndsWith("in"))
							{
								pixelAsString = SizeConverter.InchToPixelAsString(num);
							}
							if (pixelAsString.Length > 0)
							{
								str = string.Concat("width=\"", pixelAsString, "\" ");
							}
						}
						if (svgHeight != null)
						{
							double num1 = Convert.ToDouble(svgHeight, NumberFormatInfo.InvariantInfo);
							string pixelAsString1 = "";
							if (frame.SvgHeight.EndsWith("cm"))
							{
								pixelAsString1 = SizeConverter.CmToPixelAsString(num1);
							}
							else if (frame.SvgHeight.EndsWith("in"))
							{
								pixelAsString1 = SizeConverter.InchToPixelAsString(num1);
							}
							if (pixelAsString1.Length > 0)
							{
								str = string.Concat("height=\"", pixelAsString1, "\" ");
							}
						}
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						if (this.OnWarning != null)
						{
							AODLWarning aODLWarning = new AODLWarning(string.Concat("Exception while trying to build a graphic width & height.: ", frame.SvgWidth, "/", frame.SvgHeight))
							{
								InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
								OriginalException = exception
							};
							this.OnWarning(aODLWarning);
						}
					}
				}
			}
			catch (Exception exception3)
			{
				Exception exception2 = exception3;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML style string from a FrameStyle.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception2
				};
				if (frame != null && frame.Node != null)
				{
					aODLException.Node = frame.Node;
				}
				throw aODLException;
			}
			return str;
		}

		public string GetHeadingStyleAsHtml(Header header)
		{
			string headerHtmlStyles;
			try
			{
				if (header == null || header.StyleName == null)
				{
					headerHtmlStyles = this.HeaderHtmlStyles[0];
					return headerHtmlStyles;
				}
				else if (!header.StyleName.Equals(Headings.Heading_20_1.ToString()))
				{
					headerHtmlStyles = (!header.StyleName.Equals(Headings.Heading_20_2.ToString()) ? this.HeaderHtmlStyles[0] : this.HeaderHtmlStyles[2]);
				}
				else
				{
					headerHtmlStyles = this.HeaderHtmlStyles[1];
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML style string from a TextStyle.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				if (header != null && header.Node != null)
				{
					aODLException.Node = header.Node;
				}
				throw aODLException;
			}
			return headerHtmlStyles;
		}

		public string GetParagraphStyleAsHtml(ParagraphStyle paragraphStyle)
		{
			string str = "style=\"";
			try
			{
				if (paragraphStyle != null && paragraphStyle.ParagraphProperties != null)
				{
					if (paragraphStyle.ParagraphProperties.Alignment != null && paragraphStyle.ParagraphProperties.Alignment != "start")
					{
						str = string.Concat(str, "text-align: ", paragraphStyle.ParagraphProperties.Alignment, "; ");
					}
					if (paragraphStyle.ParagraphProperties.MarginLeft != null)
					{
						str = string.Concat(str, "text-indent: ", paragraphStyle.ParagraphProperties.MarginLeft, "; ");
					}
					if (paragraphStyle.ParagraphProperties.LineSpacing != null)
					{
						str = string.Concat(str, "line-height: ", paragraphStyle.ParagraphProperties.LineSpacing, "; ");
					}
					if (paragraphStyle.ParagraphProperties.Border != null && paragraphStyle.ParagraphProperties.Padding == null)
					{
						str = string.Concat(str, "border-width:1px; border-style:solid; padding: 0.5cm; ");
					}
					if (paragraphStyle.ParagraphProperties.Border != null && paragraphStyle.ParagraphProperties.Padding != null)
					{
						str = string.Concat(str, "border-width:1px; border-style:solid; padding:", paragraphStyle.ParagraphProperties.Padding, "; ");
					}
					if (paragraphStyle.ParagraphProperties.BackgroundColor != null)
					{
						str = string.Concat(str, "background-color: ", paragraphStyle.ParagraphProperties.BackgroundColor, "; ");
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML style string from a ParagraphStyle.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				if (paragraphStyle != null && paragraphStyle.Node != null)
				{
					aODLException.Node = paragraphStyle.Node;
				}
				throw aODLException;
			}
			str = (str.EndsWith("; ") ? string.Concat(str, "\"") : "");
			return str;
		}

		public string GetTableStyleAsHtml(TableStyle tableStyle)
		{
			string str = "";
			try
			{
				if (tableStyle != null && tableStyle.TableProperties != null)
				{
					if (tableStyle.TableProperties.Width != null)
					{
						string width = tableStyle.TableProperties.Width;
						if (width.EndsWith("cm"))
						{
							width = width.Replace("cm", "");
						}
						else if (width.EndsWith("in"))
						{
							width = width.Replace("in", "");
						}
						try
						{
							double num = Convert.ToDouble(width, NumberFormatInfo.InvariantInfo);
							string pixelAsString = "";
							if (tableStyle.TableProperties.Width.EndsWith("cm"))
							{
								pixelAsString = SizeConverter.CmToPixelAsString(num);
							}
							else if (tableStyle.TableProperties.Width.EndsWith("in"))
							{
								pixelAsString = SizeConverter.InchToPixelAsString(num);
							}
							if (pixelAsString.Length > 0)
							{
								str = string.Concat("width=\"", pixelAsString.Replace("px", ""), "\" ");
							}
						}
						catch (Exception exception1)
						{
							Exception exception = exception1;
							if (this.OnWarning != null)
							{
								AODLWarning aODLWarning = new AODLWarning(string.Concat("Exception while trying to build a table width width.: ", tableStyle.TableProperties.Width))
								{
									InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
									OriginalException = exception
								};
								this.OnWarning(aODLWarning);
							}
						}
					}
					if (tableStyle.TableProperties.Align != null && tableStyle.TableProperties.Align != "margin")
					{
						if (tableStyle.TableProperties.Align == "center")
						{
							str = string.Concat(str, "align=\"center\" ");
						}
						else if (tableStyle.TableProperties.Align == "right")
						{
							str = string.Concat(str, "align=\"center\" ");
						}
					}
				}
			}
			catch (Exception exception3)
			{
				Exception exception2 = exception3;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML style string from a TableStyle.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception2
				};
				if (tableStyle != null && tableStyle.Node != null)
				{
					aODLException.Node = tableStyle.Node;
				}
				throw aODLException;
			}
			return str;
		}

		public string GetTextStyleAsHtml(TextProperties textStyle)
		{
			string str = "style=\"";
			try
			{
				if (textStyle != null)
				{
					if (textStyle.Italic != null && textStyle.Italic != "normal")
					{
						str = string.Concat(str, "font-style: italic; ");
					}
					if (textStyle.Bold != null)
					{
						str = string.Concat(str, "font-weight: ", textStyle.Bold, "; ");
					}
					if (textStyle.Underline != null)
					{
						str = string.Concat(str, "text-decoration: underline; ");
					}
					if (textStyle.TextLineThrough != null)
					{
						str = string.Concat(str, "text-decoration: line-through; ");
					}
					if (textStyle.FontName != null)
					{
						str = string.Concat(str, "font-family: ", FontFamilies.HtmlFont(textStyle.FontName), "; ");
					}
					if (textStyle.FontSize != null)
					{
						str = string.Concat(str, "font-size: ", FontFamilies.PtToPx(textStyle.FontSize), "; ");
					}
					if (textStyle.FontColor != null)
					{
						str = string.Concat(str, "color: ", textStyle.FontColor, "; ");
					}
					if (textStyle.BackgroundColor != null)
					{
						str = string.Concat(str, "background-color: ", textStyle.BackgroundColor, "; ");
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to build a HTML style string from a TextStyle.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					OriginalException = exception
				};
				if (textStyle != null && textStyle.Node != null)
				{
					aODLException.Node = textStyle.Node;
				}
				throw aODLException;
			}
			str = (str.EndsWith("; ") ? string.Concat(str, "\"") : "");
			return str;
		}

		public event HTMLStyleBuilder.Warning OnWarning;

		public delegate void Warning(AODLWarning warning);
	}
}