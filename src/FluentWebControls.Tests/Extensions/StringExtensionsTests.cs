//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System.Web;

using FluentAssert;

using FluentWebControls.Extensions;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class StringExtensionsTests
	{
		[TestFixture]
		public class When_asked_to_escape_for_html
		{
			private const string Ampersand = "&amp;";
			private const string Greater = "&gt;";
			private const string Lesser = "&lt;";
			private const string TestString = "TestString";

			[Test]
			public void Should_remove_characters_leading_to_Cross_Site_Scripting()
			{
				const string teststring = "<" + TestString + ">&";
				var result = teststring.EscapeForHtml();
				result.ShouldBeEqualTo(Lesser + TestString + Greater + Ampersand);
			}
		}

		[TestFixture]
		public class When_asked_to_escape_for_url_string
		{
			private const string LessThan = "%3c";
			private const string Quote = "%22";
			private const string Space = "%20";
			private const string TestString = "TestString";

			[Test]
			public void Should_remove_characters_leading_to_Cross_Site_Scripting()
			{
				const string value = TestString + "\" <  ";
				var result = value.EscapeForUrl();
				result.ShouldBeEqualTo(TestString + Quote + Space + LessThan + Space + Space);

				var decoded = HttpUtility.UrlDecode(result);
				decoded.ShouldBeEqualTo(value);
			}

			[Test]
			public void Should_return_correct_result_if_the_input_contains_percent_followed_by_20()
			{
				const string value = "%20";
				var result = value.EscapeForUrl();
				result.ShouldBeEqualTo("%2520");

				var decoded = HttpUtility.UrlDecode(result);
				decoded.ShouldBeEqualTo(value);
			}

			[Test]
			public void Should_return_empty_if_the_input_is_null()
			{
				const string value = null;
				var result = value.EscapeForUrl();
				result.ShouldBeEqualTo("");
			}
		}
	}
}