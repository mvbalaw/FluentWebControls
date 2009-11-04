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
			private const string Quote = "%22";
			private const string Space = "+";
			private const string TestString = "TestString";

			[Test]
			public void Should_remove_characters_leading_to_Cross_Site_Scripting()
			{
				const string value = TestString + "\"   ";
				var result = value.EscapeForUrl();
				result.ShouldBeEqualTo(TestString + Quote + Space + Space + Space);
			}
		}
	}
}