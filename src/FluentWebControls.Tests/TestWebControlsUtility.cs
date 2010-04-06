using System;
using System.Collections.Generic;
using System.Xml;

namespace FluentWebControls.Tests
{
	internal static class TestWebControlsUtility
	{
		public static Dictionary<string, string> HtmlParser(string htmlTag)
		{
			var xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(htmlTag);

			var attributes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			foreach (XmlAttribute attribute in xmlDocument.FirstChild.Attributes)
			{
				attributes.Add(attribute.Name, attribute.Value);
			}
			return attributes;
		}
	}
}