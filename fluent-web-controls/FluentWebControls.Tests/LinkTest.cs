using System;

using FluentAssert;

using FluentWebControls.Interfaces;
using FluentWebControls.Tools;

using NUnit.Framework;

using Rhino.Mocks;

namespace FluentWebControls.Tests
{
	public class LinkTest
	{
		[TestFixture]
		public class When_asked_to_create_a_link
		{
			private IPathUtility _pathUtility;

			[SetUp]
			public void BeforeEachTest()
			{
				_pathUtility = MockRepository.GenerateMock<IPathUtility>();
				IoCUtility.Inject(_pathUtility);
			}

			[TearDown]
			public void AfterEachTest()
			{
				_pathUtility.VerifyAllExpectations();
			}

			[Test]
			public void Should_return_html_code_containg_virtualdirectory_representing_a_link_with_the_url()
			{
				const string virtualdirectory = "/AdminTest.mvc/Test";
				_pathUtility.Expect(x => x.GetUrl(null)).IgnoreArguments().Return(virtualdirectory);
				LinkData linkData = Link.To("AdminTest", "Test");
				string expected = String.Format("<a href='{0}'></a>", virtualdirectory);
				linkData.ToString().ShouldBeEqualTo(expected);
			}

			[Test]
			public void Should_return_html_code_representing_a_link()
			{
				const string virtualdirectory = "/AdminTest.mvc";
				_pathUtility.Expect(x => x.GetUrl(null)).IgnoreArguments().Return(virtualdirectory);
				LinkData linkData = Link.To("AdminTest");
				linkData.ToString().ShouldBeEqualTo("<a href='" + virtualdirectory + "'></a>");
			}

			[Test]
			public void Should_return_html_code_representing_a_link_with_the_url()
			{
				const string virtualdirectory = "/AdminTest.mvc/Test";
				_pathUtility.Expect(x => x.GetUrl(null)).IgnoreArguments().Return(virtualdirectory);
				LinkData linkData = Link.To("AdminTest", "Test");
				linkData.ToString().ShouldBeEqualTo("<a href='" + virtualdirectory + "'></a>");
			}
		}
	}
}