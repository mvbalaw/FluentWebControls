using System;
using System.Collections.Generic;
using System.Xml;

namespace FluentWebControls.Tests.Extensions
{
	internal static class StringExtensions
	{
		public static Dictionary<string, string> ParseHtmlTag(this string htmlTag)
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