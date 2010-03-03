using System;

using FluentAssert;

using FluentWebControls.Interfaces;
using FluentWebControls.Tools;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class TestPathUtility : IPathUtility
	{
		public string GetUrl(string virtualDirectory)
		{
			return String.Format("/{0}", virtualDirectory);
		}
	}

	public class LinkTest
	{
		public class TestController
		{
			public object Action(int id, string name)
			{
				return 0;
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_link
		{
			private IPathUtility _pathUtility;

			[SetUp]
			public void BeforeEachTest()
			{
				_pathUtility = new TestPathUtility();
				IoCUtility.Inject(_pathUtility);
			}

			[Test]
			public void Should_return_html_code_representing_a_link()
			{
				var linkData = Link.To("AdminTest", ".mvc", "");
				string expected = String.Format("<a href='{0}'></a>", "/AdminTest.mvc/");
				linkData.ToString().ShouldBeEqualTo(expected);
			}

			[Test]
			public void Should_return_html_code_representing_a_link_with_the_url()
			{
				var linkData = Link.To("AdminTest", ".mvc", "Test");
				string expected = String.Format("<a href='{0}'></a>", "/AdminTest.mvc/Test");
				linkData.ToString().ShouldBeEqualTo(expected);
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_link_using_a_lambda
		{
			private IPathUtility _pathUtility;

			[SetUp]
			public void BeforeEachTest()
			{
				_pathUtility = new TestPathUtility();
				IoCUtility.Inject(_pathUtility);
			}

			[Test]
			public void Should_return_html_code_representing_a_link_when_argument_values_are_properties_of_an_object()
			{
				var test = new Test
					{
						Id = 4,
						Name = "Name"
					};
				var linkData = Link.To((TestController controller) => controller.Action(test.Id, test.Name));
				string expected = String.Format("<a href='{0}'></a>", "/Test/Action/4/Name");
				linkData.ToString().ShouldBeEqualTo(expected);
			}

			[Test]
			public void Should_return_html_code_representing_a_link_when_argument_values_are_variables()
			{
				const int id = 4;
				const string name = "Name";
				var linkData = Link.To((TestController controller) => controller.Action(id, name));
				string expected = String.Format("<a href='{0}'></a>", "/Test/Action/4/Name");
				linkData.ToString().ShouldBeEqualTo(expected);
			}

			[Test]
			public void Should_return_html_code_representing_a_link_when_arguments_are_constants()
			{
				const int id = 4;
				const string name = "Name";
				var linkData = Link.To((TestController controller) => controller.Action(id, name));
				string expected = String.Format("<a href='{0}'></a>", "/Test/Action/4/Name");
				linkData.ToString().ShouldBeEqualTo(expected);
			}

			public class Test
			{
				public int Id { get; set; }
				public string Name { get; set; }
			}
		}
	}
}