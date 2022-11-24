using AODL.Document;
using AODL.Document.Content;
using AODL.Document.Content.Text;
using AODL.Document.Content.Text.Indexes;
using System;

namespace AODL.Document.Content.Draw
{
	public class FrameBuilder
	{
		public FrameBuilder()
		{
		}

		public static Frame BuildIllustrationFrame(IDocument document, string frameStyleName, string graphicName, string pathToGraphic, string illustrationText, int illustrationNumber)
		{
			DrawTextBox drawTextBox = new DrawTextBox(document);
			Frame frame = new Frame(document, frameStyleName)
			{
				DrawName = string.Concat(frameStyleName, "_", graphicName),
				ZIndex = "0"
			};
			Paragraph paragraph = ParagraphBuilder.CreateStandardTextParagraph(document);
			paragraph.StyleName = "Illustration";
			Frame frame1 = new Frame(document, string.Concat("InnerFrame_", frameStyleName), graphicName, pathToGraphic)
			{
				ZIndex = "1"
			};
			paragraph.Content.Add(frame1);
			paragraph.TextContent.Add(new SimpleText(document, "Illustration"));
			TextSequence textSequence = new TextSequence(document)
			{
				Name = "Illustration",
				NumFormat = "1",
				RefName = string.Concat("refIllustration", illustrationNumber.ToString()),
				Formula = "ooow:Illustration+1"
			};
			textSequence.TextContent.Add(new SimpleText(document, illustrationNumber.ToString()));
			paragraph.TextContent.Add(textSequence);
			paragraph.TextContent.Add(new SimpleText(document, illustrationText));
			drawTextBox.Content.Add(paragraph);
			frame.SvgWidth = frame1.SvgWidth;
			drawTextBox.MinWidth = frame1.SvgWidth;
			drawTextBox.MinHeight = frame1.SvgHeight;
			frame.Content.Add(drawTextBox);
			return frame;
		}

		public static Frame BuildStandardGraphicFrame(IDocument document, string frameStyleName, string graphicName, string pathToGraphic)
		{
			return new Frame(document, frameStyleName, graphicName, pathToGraphic);
		}
	}
}