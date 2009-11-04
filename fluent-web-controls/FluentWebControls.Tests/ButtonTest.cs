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
		[TestFixture]
		public class When_asked_to_create_a_button
		{
			[SetUp]
			public void BeforeEachTest()
			{
				IoCUtility.Inject<IPathUtility>(new TestPathUtility());
			}

			[Test]
			public void Should_return_html_code_representing_a_Delete_button()
			{
				ButtonData buttonData = Button.For(ButtonData.ButtonType.Delete, "AdminDelete");
				buttonData.ToString().ShouldBeEqualTo(String.Format("<input Id='btnDelete' name='btnDelete' value='Delete' class='cancel' type='submit' action='/AdminDelete.mvc/Delete' onClick='javascript:return confirmThenChangeFormAction({0}{1}{0}, this)'/>", "\"".EscapeForTagAttribute(), ButtonData.ButtonType.Delete.ConfirmationMessage));
			}

			[Test]
			public void Should_return_html_code_representing_a_Save_button()
			{
				ButtonData buttonData = Button.For(ButtonData.ButtonType.Save, "AdminSave");
				buttonData.ToString().ShouldBeEqualTo("<input Id='btnSave' name='btnSave' value='Save' class='button' type='submit' action='/AdminSave.mvc/Save' onClick='javascript:return changeFormAction(this)'/>");
			}

			private class TestPathUtility : IPathUtility
			{
				public string GetUrl(string virtualDirectory)
				{
					return virtualDirectory;
				}
			}
		}
	}
}