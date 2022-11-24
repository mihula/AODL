using AODL.Document;
using AODL.Document.Content.Text;
using AODL.Document.Content.Text.Indexes;
using AODL.Document.Content.Text.TextControl;
using AODL.Document.Exceptions;
using AODL.Document.Styles;
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml;

namespace AODL.Document.Import.OpenDocument.NodeProcessors
{
	public class TextContentProcessor
	{
		public TextContentProcessor()
		{
		}

		public Bookmark CreateBookmark(IDocument document, XmlNode node, BookmarkType type)
		{
			Bookmark bookmark;
			try
			{
				Bookmark bookmark1 = null;
				if (type != BookmarkType.Standard)
				{
					bookmark1 = (type != BookmarkType.Start ? new Bookmark(document, BookmarkType.End, "noname") : new Bookmark(document, BookmarkType.Start, "noname"));
				}
				else
				{
					bookmark1 = new Bookmark(document, BookmarkType.Standard, "noname");
				}
				bookmark1.Node = node.CloneNode(true);
				bookmark = bookmark1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a Bookmark.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = node,
					OriginalException = exception
				};
				throw aODLException;
			}
			return bookmark;
		}

		public Footnote CreateFootnote(IDocument document, XmlNode node)
		{
			Footnote footnote;
			try
			{
				Footnote footnote1 = new Footnote(document)
				{
					Node = node.CloneNode(true)
				};
				footnote = footnote1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a Footnote.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = node,
					OriginalException = exception
				};
				throw aODLException;
			}
			return footnote;
		}

		public FormatedText CreateFormatedText(IDocument document, XmlNode node)
		{
			FormatedText formatedText;
			try
			{
				FormatedText formatedText1 = new FormatedText(document, node);
				ITextCollection textCollection = new ITextCollection();
				formatedText1.Document = document;
				formatedText1.Node = node;
				IStyle styleByName = document.Styles.GetStyleByName(formatedText1.StyleName);
				if (styleByName != null)
				{
					formatedText1.Style = styleByName;
				}
				else if (document.CommonStyles.GetStyleByName(formatedText1.StyleName) == null && TextContentProcessor.OnWarning != null)
				{
					AODLWarning aODLWarning = new AODLWarning("A TextStyle for the FormatedText object wasn't found.")
					{
						InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
						Node = node
					};
					TextContentProcessor.OnWarning(aODLWarning);
				}
				foreach (XmlNode childNode in node.ChildNodes)
				{
					IText text = this.CreateTextObject(document, childNode.CloneNode(true));
					if (text == null)
					{
						textCollection.Add(new UnknownTextContent(document, childNode));
					}
					else
					{
						textCollection.Add(text);
					}
				}
				formatedText1.Node.InnerText=("");
				foreach (IText text1 in textCollection)
				{
					formatedText1.TextContent.Add(text1);
				}
				formatedText = formatedText1;
			}
			catch (Exception exception)
			{
				throw;
			}
			return formatedText;
		}

		public IText CreateTextObject(IDocument document, XmlNode aTextNode)
		{
			IText simpleText;
			if (PrivateImplementationDetails.method0x60003f7 == null)
			{
				Hashtable hashtable = new Hashtable(22, 0.5f);
				hashtable.Add("#text", 0);
				hashtable.Add("text:span", 1);
				hashtable.Add("text:bookmark", 2);
				hashtable.Add("text:bookmark-start", 3);
				hashtable.Add("text:bookmark-end", 4);
				hashtable.Add("text:a", 5);
				hashtable.Add("text:note", 6);
				hashtable.Add("text:line-break", 7);
				hashtable.Add("text:s", 8);
				hashtable.Add("text:tab", 9);
				PrivateImplementationDetails.method0x60003f7 = hashtable;
			}
			int num = 0;
			if (aTextNode.OuterXml.IndexOf("Contains state ") > -1)
			{
				num++;
			}
			string name = aTextNode.Name;
			object obj = name;
			if (name != null)
			{
				object item = PrivateImplementationDetails.method0x60003f7[obj];
				obj = item;
				if (item != null)
				{
					switch ((int)obj)
					{
						case 0:
						{
							simpleText = new SimpleText(document, aTextNode.InnerText);
							break;
						}
						case 1:
						{
							simpleText = this.CreateFormatedText(document, aTextNode);
							break;
						}
						case 2:
						{
							simpleText = this.CreateBookmark(document, aTextNode, BookmarkType.Standard);
							break;
						}
						case 3:
						{
							simpleText = this.CreateBookmark(document, aTextNode, BookmarkType.Start);
							break;
						}
						case 4:
						{
							simpleText = this.CreateBookmark(document, aTextNode, BookmarkType.End);
							break;
						}
						case 5:
						{
							simpleText = this.CreateXLink(document, aTextNode);
							break;
						}
						case 6:
						{
							simpleText = this.CreateFootnote(document, aTextNode);
							break;
						}
						case 7:
						{
							simpleText = new LineBreak(document);
							break;
						}
						case 8:
						{
							simpleText = new WhiteSpace(document, aTextNode.CloneNode(true));
							break;
						}
						case 9:
						{
							simpleText = new TabStop(document);
							break;
						}
						default:
						{
							simpleText = null;
							return simpleText;
						}
					}
				}
				else
				{
					simpleText = null;
					return simpleText;
				}
			}
			else
			{
				simpleText = null;
				return simpleText;
			}
			return simpleText;
		}

		public TextSequence CreateTextSequence(IDocument document, XmlNode node)
		{
			TextSequence textSequence;
			try
			{
				textSequence = new TextSequence(document, node);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a TextSequence.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = node,
					OriginalException = exception
				};
				throw aODLException;
			}
			return textSequence;
		}

		public XLink CreateXLink(IDocument document, XmlNode node)
		{
			XLink xLink;
			try
			{
				XLink xLink1 = new XLink(document)
				{
					Node = node.CloneNode(true)
				};
				ITextCollection textCollection = new ITextCollection();
				foreach (XmlNode childNode in xLink1.Node.ChildNodes)
				{
					IText text = this.CreateTextObject(xLink1.Document, childNode);
					if (text == null)
					{
						continue;
					}
					textCollection.Add(text);
				}
				xLink1.Node.InnerXml=("");
				foreach (IText text1 in textCollection)
				{
					xLink1.TextContent.Add(text1);
				}
				xLink = xLink1;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				AODLException aODLException = new AODLException("Exception while trying to create a XLink.")
				{
					InMethod = AODLException.GetExceptionSourceInfo(new StackFrame(1, true)),
					Node = node,
					OriginalException = exception
				};
				throw aODLException;
			}
			return xLink;
		}

		private string ReplaceSpecialCharacter(string nodeInnerText)
		{
			nodeInnerText = nodeInnerText.Replace("<", "&lt;");
			nodeInnerText = nodeInnerText.Replace(">", "&gt;");
			return nodeInnerText;
		}

		public static event TextContentProcessor.Warning OnWarning;

		public delegate void Warning(AODLWarning warning);
	}
}