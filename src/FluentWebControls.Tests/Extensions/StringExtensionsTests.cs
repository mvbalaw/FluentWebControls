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
				string result = teststring.EscapeForHtml();
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
				string result = value.EscapeForUrl();
				result.ShouldBeEqualTo(TestString + Quote + Space + LessThan + Space + Space);

				string decoded = HttpUtility.UrlDecode(result);
				decoded.ShouldBeEqualTo(value);
			}

			[Test]
			public void Should_return_correct_result_if_the_input_contains_percent_followed_by_20()
			{
				const string value = "%20";
				string result = value.EscapeForUrl();
				result.ShouldBeEqualTo("%2520");

				string decoded = HttpUtility.UrlDecode(result);
				decoded.ShouldBeEqualTo(value);
			}

			[Test]
			public void Should_return_empty_if_the_input_is_null()
			{
				const string value = null;
				string result = value.EscapeForUrl();
				result.ShouldBeEqualTo("");
			}
		}
	}
}