using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace AODL.Document.Content.Text.TextControl
{
	public class WhiteSpaceHelper
	{
		internal string Value;

		internal string Replacement;

		public WhiteSpaceHelper()
		{
		}

		private static string GetHtmlWhiteSpace(int length)
		{
			string str = "";
			for (int i = 0; i < length; i++)
			{
				str = string.Concat(str, "&nbsp;");
			}
			return str;
		}

		public static string GetWhiteSpaceHtml(string text)
		{
			try
			{
				ArrayList arrayList = new ArrayList();
				string[] str = new string[] { "<text:s text:c=", '\"'.ToString(), "\\d+", '\"'.ToString(), " xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />" };
				string str1 = string.Concat(str);
				for (Match i = (new Regex(str1, RegexOptions.IgnoreCase)).Match(text); i.Success; i = i.NextMatch())
				{
					Regex regex = new Regex("\\d", RegexOptions.IgnoreCase);
					Match match = regex.Match(i.Value);
					string str2 = "";
					if (match.Success)
					{
						int num = Convert.ToInt32(match.Value);
						for (int j = 0; j < num; j++)
						{
							str2 = string.Concat(str2, "&nbsp;");
						}
					}
					if (str2.Length > 0)
					{
						WhiteSpaceHelper whiteSpaceHelper = new WhiteSpaceHelper()
						{
							Value = str2,
							Replacement = i.Value
						};
						arrayList.Add(whiteSpaceHelper);
					}
				}
				foreach (WhiteSpaceHelper whiteSpaceHelper1 in arrayList)
				{
					text = text.Replace(whiteSpaceHelper1.Replacement, whiteSpaceHelper1.Value);
				}
			}
			catch (Exception)
			{
			}
			return text;
		}

		public static string GetWhiteSpaceXml(string stringToConvert)
		{
			try
			{
				ArrayList arrayList = new ArrayList();
				for (Match i = (new Regex("\\s{2,}", RegexOptions.IgnoreCase)).Match(stringToConvert); i.Success; i = i.NextMatch())
				{
					WhiteSpaceHelper whiteSpaceHelper = new WhiteSpaceHelper();
					for (int j = 0; j < i.Length; j++)
					{
						WhiteSpaceHelper whiteSpaceHelper1 = whiteSpaceHelper;
						whiteSpaceHelper1.Value = string.Concat(whiteSpaceHelper1.Value, " ");
					}
					int length = i.Length;
					whiteSpaceHelper.Replacement = string.Concat("<ws id=\"", length.ToString(), "\"/>");
					arrayList.Add(whiteSpaceHelper);
				}
				foreach (WhiteSpaceHelper whiteSpaceHelper2 in arrayList)
				{
					stringToConvert = stringToConvert.Replace(whiteSpaceHelper2.Value, whiteSpaceHelper2.Replacement);
				}
			}
			catch (Exception)
			{
			}
			return stringToConvert;
		}

		private static string GetXmlWhiteSpace(int length)
		{
			return string.Concat("<text:s text:c=\"", length, "\" xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" />");
		}
	}
}