using AODL.Document;
using AODL.Document.Styles;
using AODL.Document.TextDocuments;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Content.Text
{
	public class ParagraphBuilder
	{
		public readonly static string ParagraphSeperator;

		public readonly static string ParagraphSeperator2;

		static ParagraphBuilder()
		{
			ParagraphBuilder.ParagraphSeperator = "\n\n";
			ParagraphBuilder.ParagraphSeperator2 = "\r\n\r\n";
		}

		public ParagraphBuilder()
		{
		}

		public static ParagraphCollection CreateParagraphCollection(IDocument document, string text, bool useStandardStyle, string paragraphSeperator)
		{
			string str = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
			ParagraphCollection paragraphCollection = new ParagraphCollection();
			text = text.Replace(paragraphSeperator, "<p/>");
			str = string.Concat(str, "<pg>", text, "</pg>");
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(str);
			XmlNode documentElement = xmlDocument.DocumentElement;
			if (documentElement != null)
			{
				if (!documentElement.HasChildNodes)
				{
					Paragraph paragraph = null;
					paragraph = (!useStandardStyle ? ParagraphBuilder.CreateParagraphWithCustomStyle(document, string.Concat("P", Convert.ToString(document.DocumentMetadata.ParagraphCount + 1))) : ParagraphBuilder.CreateStandardTextParagraph(document));
					paragraph.TextContent = TextBuilder.BuildTextCollection(document, documentElement.InnerText);
					paragraphCollection.Add(paragraph);
				}
				else
				{
					foreach (XmlNode childNode in documentElement.ChildNodes)
					{
						if (childNode.NodeType != XmlNodeType.Text)
						{
							Paragraph paragraph1 = null;
							paragraph1 = (!useStandardStyle ? ParagraphBuilder.CreateParagraphWithCustomStyle(document, string.Concat("P", Convert.ToString(document.DocumentMetadata.ParagraphCount + documentElement.ChildNodes.Count + 1))) : ParagraphBuilder.CreateStandardTextParagraph(document));
							paragraphCollection.Add(paragraph1);
						}
						else
						{
							Paragraph paragraph2 = null;
							paragraph2 = (!useStandardStyle ? ParagraphBuilder.CreateParagraphWithCustomStyle(document, string.Concat("P", Convert.ToString(document.DocumentMetadata.ParagraphCount + documentElement.ChildNodes.Count + 1))) : ParagraphBuilder.CreateStandardTextParagraph(document));
							paragraph2.TextContent = TextBuilder.BuildTextCollection(document, childNode.InnerText);
							paragraphCollection.Add(paragraph2);
						}
					}
				}
			}
			return paragraphCollection;
		}

		public static Paragraph CreateParagraphWithCustomStyle(IDocument document, string styleName)
		{
			return new Paragraph(document, styleName);
		}

		public static Paragraph CreateParagraphWithExistingNode(IDocument document, XmlNode paragraphNode)
		{
			return new Paragraph(paragraphNode, document);
		}

		public static Paragraph CreateSpreadsheetParagraph(IDocument document)
		{
			return new Paragraph(document);
		}

		public static Paragraph CreateStandardTextParagraph(IDocument document)
		{
			return new Paragraph(document, ParentStyles.Standard, null);
		}

		public static Paragraph CreateStandardTextTableParagraph(IDocument document)
		{
			return new Paragraph(document, ParentStyles.Table, null);
		}
	}
}