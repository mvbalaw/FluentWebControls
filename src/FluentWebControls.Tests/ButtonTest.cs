using System;

using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;
using FluentWebControls.Tools;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class ButtonTest
	{
		private class TestPathUtility : IPathUtility
		{
			public string GetUrl(string virtualDirectory)
			{
				return virtualDirectory;
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_button_of_type_Delete
		{
			[SetUp]
			public void BeforeEachTest()
			{
				IoCUtility.Inject<IPathUtility>(new TestPathUtility());
			}

			[Test]
			public void Should_generate_the_correct_HTML_code()
			{
				var buttonData = Button.For(ButtonData.ButtonType.Delete, "AdminDelete").WithControllerExtension(".mvc");
				buttonData.ToString().ShouldBeEqualTo(String.Format("<input Id='btnDelete' name='btnDelete' value='Delete' class='cancel' type='submit' action='/AdminDelete.mvc/Delete' onClick='javascript:return confirmThenChangeFormAction({0}{1}{0}, this)'/>", "\"".EscapeForTagAttribute(), ButtonData.ButtonType.Delete.ConfirmationMessage));
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_button_of_type_Save
		{
			[SetUp]
			public void BeforeEachTest()
			{
				IoCUtility.Inject<IPathUtility>(new TestPathUtility());
			}

			[Test]
			public void Should_generate_the_correct_HTML_code()
			{
				var buttonData = Button.For(ButtonData.ButtonType.Save, "AdminSave")
					.WithControllerExtension(".mvc");
				buttonData.ToString().ShouldBeEqualTo("<input Id='btnSave' name='btnSave' value='Save' class='button' type='submit' action='/AdminSave.mvc/Save' onClick='javascript:return changeFormAction(this)'/>");
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_LinkButton
		{
			[SetUp]
			public void BeforeEachTest()
			{
				IoCUtility.Inject<IPathUtility>(new TestPathUtility());
			}

			[Test]
			public void Should_generate_the_correct_HTML_code()
			{
				var buttonData = Button.For(ButtonData.ButtonType.Link, (TestController controller) => controller.Action(4, "name"));
				buttonData.ToString().ShouldBeEqualTo(String.Format("<input Id='btnLink' name='btnLink' value='Link' class='cancel' type='button' onClick='javascript:location.href=&quot;/Test/Action/4/name&quot;'/>"));
			}

			public class TestController
			{
				public object Action(int id, string name)
				{
					return 0;
				}
			}
		}
	}
}