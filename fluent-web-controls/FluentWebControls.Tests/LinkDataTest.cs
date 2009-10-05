using FluentAssert;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class LinkDataTest
	{
		public abstract class LinkDataTestBase
		{
			protected LinkData _linkData = new LinkData();
			protected abstract string CssClass { get; }
			protected abstract bool Disabled { get; }
			protected abstract string Href { get; }
			protected abstract string HtmlText { get; }
			protected abstract string LinkText { get; }
			protected abstract string MouseOverText { get; }

			protected virtual void AddQueryStringData()
			{
			}

			protected void SetLinkData()
			{
				_linkData = new LinkData
					{
						Href = Href,
						CssClass = CssClass,
						Disabled = Disabled,
						MouseOverText = MouseOverText,
						LinkText = LinkText
					};
			}

			[Test]
			public void Should_return_HTML_code_representing_a_link_with_all_the_values_set()
			{
				SetLinkData();
				AddQueryStringData();
				_linkData.ToString().ShouldBeEqualTo(HtmlText);
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_empty_link : LinkDataTestBase
		{
			protected override string CssClass
			{
				get { return null; }
			}
			protected override bool Disabled
			{
				get { return false; }
			}
			protected override string Href
			{
				get { return null; }
			}
			protected override string HtmlText
			{
				get { return "<a href=''></a>"; }
			}
			protected override string LinkText
			{
				get { return null; }
			}
			protected override string MouseOverText
			{
				get { return null; }
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_link_that_is_disabled_with_no_query_parameters : LinkDataTestBase
		{
			protected override string CssClass
			{
				get { return "Css"; }
			}
			protected override bool Disabled
			{
				get { return true; }
			}
			protected override string Href
			{
				get { return "AdminTest"; }
			}
			protected override string HtmlText
			{
				get { return "<a disabled class='Css' title='MouseOver'>Link</a>"; }
			}
			protected override string LinkText
			{
				get { return "Link"; }
			}
			protected override string MouseOverText
			{
				get { return "MouseOver"; }
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_link_that_is_enabled_with_no_query_parameters : LinkDataTestBase
		{
			protected override string CssClass
			{
				get { return "Css"; }
			}
			protected override bool Disabled
			{
				get { return false; }
			}
			protected override string Href
			{
				get { return "AdminTest"; }
			}
			protected override string HtmlText
			{
				get { return "<a href='AdminTest' class='Css' title='MouseOver'>Link</a>"; }
			}
			protected override string LinkText
			{
				get { return "Link"; }
			}
			protected override string MouseOverText
			{
				get { return "MouseOver"; }
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_link_that_is_enabled_with_query_parameters : LinkDataTestBase
		{
			protected override void AddQueryStringData()
			{
				_linkData.AddQueryStringData("key1", "value");
				_linkData.AddQueryStringData("key2", null);
			}

			protected override string CssClass
			{
				get { return "Css"; }
			}
			protected override bool Disabled
			{
				get { return false; }
			}
			protected override string Href
			{
				get { return "AdminTest"; }
			}
			protected override string HtmlText
			{
				get { return "<a href='AdminTest?key1=value&key2=&' class='Css' title='MouseOver'>Link</a>"; }
			}
			protected override string LinkText
			{
				get { return "Link"; }
			}
			protected override string MouseOverText
			{
				get { return "MouseOver"; }
			}
		}
	}
}