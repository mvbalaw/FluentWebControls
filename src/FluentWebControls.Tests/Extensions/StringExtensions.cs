//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

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
// ReSharper disable once PossibleNullReferenceException
			foreach (XmlAttribute attribute in xmlDocument.FirstChild.Attributes)
			{
				attributes.Add(attribute.Name, attribute.Value);
			}
			return attributes;
		}
	}
}