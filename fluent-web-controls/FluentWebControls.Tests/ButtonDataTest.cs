using System;
using System.Collections.Generic;

using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class ButtonDataTest
	{
		public abstract class ButtonDataTestBase
		{
			protected ButtonData _buttonData;
			protected abstract ButtonData.ButtonType ButtonType { get; }

			protected abstract string HtmlText { get; }

			protected virtual string ControllerName()
			{
				return null;
			}

			protected virtual void SetAdditionalParameters()
			{
			}

			[Test]
			public void Should_return_HTML_code_representing_a_button()
			{
				string controllerName = ControllerName();

				IPathUtility pathUtility = new TestPathUtility();
				_buttonData = controllerName == null ? new ButtonData(ButtonType, pathUtility) : new ButtonData(ButtonType, pathUtility, controllerName)
					{
						ControllerExtension = ".mvc"
					};
				SetAdditionalParameters();
				_buttonData.ToString().ShouldBeEqualTo(HtmlText);
			}

			private class TestPathUtility : IPathUtility
			{
				public string GetUrl(string virtualDirectory)
				{
					return virtualDirectory;
				}
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_Basic_Button : ButtonDataTestBase
		{
			protected override ButtonData.ButtonType ButtonType
			{
				get { return ButtonData.ButtonType.Basic; }
			}

			protected override string HtmlText
			{
				get { return String.Format("<input Id='btnBasic' name='btnBasic' value='Populate Button' class='cancel' type='button' onClick='validate()'/>"); }
			}

			protected override void SetAdditionalParameters()
			{
				_buttonData.Text = "Populate Button";
				_buttonData.OnClickMethod = "validate()";
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_Basic_Button_with_given_width : ButtonDataTestBase
		{
			protected override ButtonData.ButtonType ButtonType
			{
				get { return ButtonData.ButtonType.Basic; }
			}

			protected override string HtmlText
			{
				get { return String.Format("<input Id='btnBasic' name='btnBasic' value='Basic' class='cancel' style='width:400px' type='button'/>"); }
			}

			protected override void SetAdditionalParameters()
			{
				_buttonData.Width = "400px";
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_Button_for_Delete : ButtonDataTestBase
		{
			protected override ButtonData.ButtonType ButtonType
			{
				get { return ButtonData.ButtonType.Delete; }
			}

			protected override string ControllerName()
			{
				return "Admin";
			}

			protected override string HtmlText
			{
				get { return String.Format("<input Id='btnDelete' name='btnDelete' value='Delete' class='cancel' type='submit' action='/Admin.mvc/Delete' onClick='javascript:return confirmThenChangeFormAction({0}{1}{0}, this)'/>", "\"".EscapeForTagAttribute(), ButtonData.ButtonType.Delete.ConfirmationMessage); }
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_Button_for_Delete_with_additional_parameters : ButtonDataTestBase
		{
			protected override ButtonData.ButtonType ButtonType
			{
				get { return ButtonData.ButtonType.Delete; }
			}

			protected override string ControllerName()
			{
				return "Admin";
			}

			protected override string HtmlText
			{
				get { return String.Format("<input Id='btnDelete' name='btnDelete' value='Text' class='cancel' type='submit' action='/Admin.mvc/Test' onClick='javascript:return confirmThenChangeFormAction({0}{1}{0}, this)'/>", "\"".EscapeForTagAttribute(), _buttonData.ConfirmMessage); }
			}

			protected override void SetAdditionalParameters()
			{
				_buttonData.Text = "Text";
				_buttonData.ActionName = "Test";
				_buttonData.ConfirmMessage = "Test Message";
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_Button_for_Save : ButtonDataTestBase
		{
			protected override ButtonData.ButtonType ButtonType
			{
				get { return ButtonData.ButtonType.Save; }
			}

			protected override string ControllerName()
			{
				return "Admin";
			}

			protected override string HtmlText
			{
				get { return "<input Id='btnSave' name='btnSave' value='Save' class='button' type='submit' action='/Admin.mvc/Save' onClick='javascript:return changeFormAction(this)'/>"; }
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_Button_with_Default_set : ButtonDataTestBase
		{
			protected override ButtonData.ButtonType ButtonType
			{
				get { return ButtonData.ButtonType.Save; }
			}

			protected override string ControllerName()
			{
				return "Admin";
			}

			protected override string HtmlText
			{
				get { return "<input Id='btnSave' name='btnSave' value='Save' class='button default' type='submit' action='/Admin.mvc/Save' onClick='javascript:return changeFormAction(this)'/>"; }
			}

			protected override void SetAdditionalParameters()
			{
				_buttonData.AsDefault();
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_Button_with_Visibility_set_to_false : ButtonDataTestBase
		{
			protected override ButtonData.ButtonType ButtonType
			{
				get { return ButtonData.ButtonType.Save; }
			}

			protected override string ControllerName()
			{
				return "Admin";
			}

			protected override string HtmlText
			{
				get { return ""; }
			}

			protected override void SetAdditionalParameters()
			{
				_buttonData.Visible = false;
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_Link_Button : ButtonDataTestBase
		{
			protected override ButtonData.ButtonType ButtonType
			{
				get { return ButtonData.ButtonType.Link; }
			}

			protected override string HtmlText
			{
				get { return String.Format("<input Id='btnLink' name='btnLink' value='Cancel' class='cancel' type='button' onClick='javascript:location.href=&quot;/Test/Action/4/name&quot;'/>"); }
			}

			protected override void SetAdditionalParameters()
			{
				_buttonData.Text = "Cancel";
				_buttonData.ControllerName = "Test";
				_buttonData.ActionName = "Action";
				_buttonData.AddUrlParameters(new List<string>
					{
						"4",
						"name"
					});
			}
		}
	}
}