using System;
using System.Xml;

namespace AODL.Document.TextDocuments
{
	public class TextDocumentHelper
	{
		public static string AutomaticStylePath;

		public static string OfficeTextPath;

		static TextDocumentHelper()
		{
			TextDocumentHelper.AutomaticStylePath = "/office:document-content/office:automatic-styles";
			TextDocumentHelper.OfficeTextPath = "/office:document-content/office:body/office:text";
		}

		public TextDocumentHelper()
		{
		}

		public static string GetBlankDocument()
		{
			return "<?xml version=\"1.0\" encoding=\"UTF-8\" ?> <office:document-content xmlns:office=\"urn:oasis:names:tc:opendocument:xmlns:office:1.0\" xmlns:style=\"urn:oasis:names:tc:opendocument:xmlns:style:1.0\" xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" xmlns:table=\"urn:oasis:names:tc:opendocument:xmlns:table:1.0\" xmlns:draw=\"urn:oasis:names:tc:opendocument:xmlns:drawing:1.0\" xmlns:fo=\"urn:oasis:names:tc:opendocument:xmlns:xsl-fo-compatible:1.0\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:meta=\"urn:oasis:names:tc:opendocument:xmlns:meta:1.0\" xmlns:number=\"urn:oasis:names:tc:opendocument:xmlns:datastyle:1.0\" xmlns:svg=\"urn:oasis:names:tc:opendocument:xmlns:svg-compatible:1.0\" xmlns:chart=\"urn:oasis:names:tc:opendocument:xmlns:chart:1.0\" xmlns:dr3d=\"urn:oasis:names:tc:opendocument:xmlns:dr3d:1.0\" xmlns:math=\"http://www.w3.org/1998/Math/MathML\" xmlns:form=\"urn:oasis:names:tc:opendocument:xmlns:form:1.0\" xmlns:script=\"urn:oasis:names:tc:opendocument:xmlns:script:1.0\" xmlns:ooo=\"http://openoffice.org/2004/office\" xmlns:ooow=\"http://openoffice.org/2004/writer\" xmlns:oooc=\"http://openoffice.org/2004/calc\" xmlns:dom=\"http://www.w3.org/2001/xml-events\" xmlns:xforms=\"http://www.w3.org/2002/xforms\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" office:version=\"1.0\"><office:scripts /><office:font-face-decls><style:font-face style:name=\"StarSymbol\" svg:font-family=\"StarSymbol\" style:font-charset=\"x-symbol\" /><style:font-face style:name=\"Tahoma1\" svg:font-family=\"Tahoma\" /> <style:font-face style:name=\"Lucida Sans Unicode\" svg:font-family=\"'Lucida Sans Unicode'\" style:font-pitch=\"variable\" /><style:font-face style:name=\"Tahoma\" svg:font-family=\"Tahoma\" style:font-pitch=\"variable\" /><style:font-face style:name=\"Times New Roman\" svg:font-family=\"'Times New Roman'\" style:font-family-generic=\"roman\" style:font-pitch=\"variable\" /></office:font-face-decls><office:automatic-styles></office:automatic-styles><office:body><office:text><office:forms form:automatic-focus=\"false\" form:apply-design-mode=\"false\" /> <text:sequence-decls><text:sequence-decl text:display-outline-level=\"0\" text:name=\"Illustration\" /><text:sequence-decl text:display-outline-level=\"0\" text:name=\"Table\" /> <text:sequence-decl text:display-outline-level=\"0\" text:name=\"Text\" /><text:sequence-decl text:display-outline-level=\"0\" text:name=\"Drawing\" /></text:sequence-decls></office:text></office:body></office:document-content>";
		}

		public static XmlNamespaceManager NameSpace(XmlNameTable nametable)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(nametable);
			xmlNamespaceManager.AddNamespace("office", "urn:oasis:names:tc:opendocument:xmlns:office:1.0");
			xmlNamespaceManager.AddNamespace("style", "urn:oasis:names:tc:opendocument:xmlns:style:1.0");
			xmlNamespaceManager.AddNamespace("text", "urn:oasis:names:tc:opendocument:xmlns:text:1.0");
			xmlNamespaceManager.AddNamespace("table", "urn:oasis:names:tc:opendocument:xmlns:table:1.0");
			xmlNamespaceManager.AddNamespace("draw", "urn:oasis:names:tc:opendocument:xmlns:drawing:1.0");
			xmlNamespaceManager.AddNamespace("fo", "urn:oasis:names:tc:opendocument:xmlns:xsl-fo-compatible:1.0");
			xmlNamespaceManager.AddNamespace("xlink", "http://www.w3.org/1999/xlink");
			xmlNamespaceManager.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
			xmlNamespaceManager.AddNamespace("meta", "urn:oasis:names:tc:opendocument:xmlns:meta:1.0");
			xmlNamespaceManager.AddNamespace("number", "urn:oasis:names:tc:opendocument:xmlns:datastyle:1.0");
			xmlNamespaceManager.AddNamespace("svg", "urn:oasis:names:tc:opendocument:xmlns:svg-compatible:1.0");
			xmlNamespaceManager.AddNamespace("dr3d", "urn:oasis:names:tc:opendocument:xmlns:chart:1.0");
			xmlNamespaceManager.AddNamespace("math", "http://www.w3.org/1998/Math/MathML");
			xmlNamespaceManager.AddNamespace("form", "urn:oasis:names:tc:opendocument:xmlns:form:1.0");
			xmlNamespaceManager.AddNamespace("script", "urn:oasis:names:tc:opendocument:xmlns:script:1.0");
			xmlNamespaceManager.AddNamespace("ooo", "http://openoffice.org/2004/office");
			xmlNamespaceManager.AddNamespace("ooow", "http://openoffice.org/2004/writer");
			xmlNamespaceManager.AddNamespace("oooc", "http://openoffice.org/2004/calc");
			xmlNamespaceManager.AddNamespace("dom", "http://www.w3.org/2001/xml-events");
			xmlNamespaceManager.AddNamespace("xforms", "http://www.w3.org/2002/xforms");
			xmlNamespaceManager.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
			xmlNamespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
			return xmlNamespaceManager;
		}
	}
}