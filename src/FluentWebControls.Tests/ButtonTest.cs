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

using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;

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
		public class When_asked_to_create_a_LinkButton
		{
			[SetUp]
			public void BeforeEachTest()
			{
				Configuration.PathUtility = new TestPathUtility();
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

		[TestFixture]
		public class When_asked_to_create_a_button_of_type_Delete
		{
			[SetUp]
			public void BeforeEachTest()
			{
				Configuration.PathUtility = new TestPathUtility();
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
				Configuration.PathUtility = new TestPathUtility();
			}

			[Test]
			public void Should_generate_the_correct_HTML_code()
			{
				var buttonData = Button.For(ButtonData.ButtonType.Save, "AdminSave")
					.WithControllerExtension(".mvc");
				buttonData.ToString().ShouldBeEqualTo("<input Id='btnSave' name='btnSave' value='Save' class='button' type='submit' action='/AdminSave.mvc/Save' onClick='javascript:return changeFormAction(this)'/>");
			}
		}
	}
}