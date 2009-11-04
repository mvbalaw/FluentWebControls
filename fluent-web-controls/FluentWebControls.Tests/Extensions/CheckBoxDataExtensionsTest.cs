using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Tools;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class CheckBoxDataExtensionsTest
	{
		public abstract class CheckBoxDataExtensionsTestBase
		{
			protected CheckBoxData _checkBoxData;
			protected bool _isChecked;

			[SetUp]
			public void BeforeEachTest()
			{
				_isChecked = true;
				_checkBoxData = new CheckBoxData(true);
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Id : CheckBoxDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_CheckBoxData_With_Id_initialized()
			{
				CheckBoxData checkBoxData = _checkBoxData.WithId("_isChecked");
				Assert.AreSame(_checkBoxData, checkBoxData);
				string propertyName = NameUtility.GetPropertyName(() => _isChecked);
				TestWebControlsUtility.HtmlParser(_checkBoxData.ToString())["id"].ShouldBeEqualTo(propertyName.ToCamelCase());
				checkBoxData.ToString().Contains(propertyName).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_IsChecked : CheckBoxDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_CheckBoxData_With_IsChecked_initialized()
			{
				CheckBoxData checkBoxData = _checkBoxData.IsChecked(_isChecked);
				Assert.AreSame(_checkBoxData, checkBoxData);
				const string checkedAttribute = "checked";
				TestWebControlsUtility.HtmlParser(_checkBoxData.ToString())[checkedAttribute].ShouldBeEqualTo("checked");
				checkBoxData.ToString().Contains(checkedAttribute).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Label : CheckBoxDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_CheckBoxData_With_Label_initialized()
			{
				LabelData label = new LabelData("Id");
				LabelData blankLabel = new LabelData
					{
						Text = "&nbsp;"
					};

				CheckBoxData checkBoxData = _checkBoxData.WithLabel(label);
				Assert.AreSame(_checkBoxData, checkBoxData);
				checkBoxData.ToString().Contains(label.ToString()).ShouldBeTrue();
				checkBoxData.ToString().Contains(blankLabel.ToString()).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Label_on_left : CheckBoxDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_CheckBoxData_With_Label_initialized()
			{
				LabelData label = new LabelData("Id");
				LabelData blankLabel = new LabelData
					{
						Text = "&nbsp;"
					};

				CheckBoxData checkBoxData = _checkBoxData.WithLabelAlignedLeft(label);
				Assert.AreSame(_checkBoxData, checkBoxData);
				checkBoxData.ToString().Contains(label.ToString()).ShouldBeTrue();
				checkBoxData.ToString().Contains(blankLabel.ToString()).ShouldBeFalse();
			}
		}
	}
}