using AODL.Document;
using AODL.Document.Content.Text.TextControl;
using System;
using System.Collections;
using System.Xml;

namespace AODL.Document.Content.Text
{
	public class TextBuilder
	{
		public TextBuilder()
		{
		}

		public static ITextCollection BuildTextCollection(IDocument document, string text)
		{
			string str = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
			ITextCollection textCollection = new ITextCollection();
			text = WhiteSpaceHelper.GetWhiteSpaceXml(text);
			text = text.Replace("\t", "<t/>");
			text = text.Replace("\n", "<n/>");
			str = string.Concat(str, "<txt>", text, "</txt>");
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(str);
			XmlNode documentElement = xmlDocument.DocumentElement;
			if (documentElement != null)
			{
				if (!documentElement.HasChildNodes)
				{
					textCollection.Add(new SimpleText(document, text));
				}
				else
				{
					foreach (XmlNode childNode in documentElement.ChildNodes)
					{
						if (childNode.NodeType == XmlNodeType.Text)
						{
							textCollection.Add(new SimpleText(document, childNode.InnerText));
						}
						else if (childNode.Name == "ws")
						{
							if (childNode.Attributes.Count == 1)
							{
								XmlNode namedItem = childNode.Attributes.GetNamedItem("id");
								if (namedItem != null)
								{
									textCollection.Add(new WhiteSpace(document, Convert.ToInt32(namedItem.InnerText)));
								}
							}
						}
						else if (childNode.Name != "t")
						{
							if (childNode.Name != "n")
							{
								continue;
							}
							textCollection.Add(new LineBreak(document));
						}
						else
						{
							textCollection.Add(new TabStop(document));
						}
					}
				}
			}
			return textCollection;
		}
	}
}