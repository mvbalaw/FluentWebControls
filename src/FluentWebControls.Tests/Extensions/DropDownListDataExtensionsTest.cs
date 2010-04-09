using System.Collections.Generic;

using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class DropDownListDataExtensionsTest
	{
		public abstract class DropDownListDataExtensionsTestBase
		{
			private readonly List<KeyValuePair<string, string>> _items = new List<KeyValuePair<string, string>>
				{
					new KeyValuePair<string, string>("Name1", "Value1"),
					new KeyValuePair<string, string>("Name2", "Value2"),
					new KeyValuePair<string, string>("Name3", "3")
				};

			protected DropDownListData _dropDownListData;
			protected IPropertyMetaData _propertyMetaData;

			[SetUp]
			public void BeforeEachTest()
			{
				_propertyMetaData = PropertyMetaDataMocker.CreateStub("Name", false, null, null, null, null, typeof(string));
				_dropDownListData = new DropDownListData(_items).WithValidationFrom(_propertyMetaData);
			}
		}

		[TestFixture]
		public class When_asked_to_assign_CssClass : DropDownListDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_DropDownListData_With_CssClass_initialized()
			{
				const string cssClass = "cssClass";
				var listData = _dropDownListData.CssClass(cssClass);
				Assert.AreSame(_dropDownListData, listData);
				listData.ToString().ParseHtmlTag()["class"].ShouldBeEqualTo(cssClass);
				_dropDownListData.ToString().Contains(cssClass).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Label : DropDownListDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_DropDownListData_With_Label_initialized()
			{
				var label = new LabelData("Id");

				var listData = _dropDownListData.WithLabel(label);
				Assert.AreSame(_dropDownListData, listData);
				listData.ToString().Contains(label.ToString()).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Set_a_Default_value : DropDownListDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_DropDownListData_with_Default_value_selected()
			{
				const string defaultText = "All";
				const string defaultValue = "";
				var listData = _dropDownListData.WithDefault(defaultText, defaultValue);
				Assert.AreSame(_dropDownListData, listData);
				_dropDownListData.ToString().Contains("<option value='' selected='selected'>All</option>").ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Set_a_Default_value_that_already_exists_in_the_items_list : DropDownListDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_DropDownListData_with_Default_value_appearing_only_once_in_the_list()
			{
				const string defaultText = "";
				const string defaultValue = "Value2";
				var listData = _dropDownListData.WithDefault(defaultText, defaultValue);
				Assert.AreSame(_dropDownListData, listData);
				string html = _dropDownListData.ToString();
				html.Contains("<option value='Value2' selected='selected'></option>").ShouldBeTrue(html);
				html.Contains("<option value='Value2'>Name2</option>").ShouldBeFalse(html);
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Set_a_selected_Value : DropDownListDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_DropDownListData_with_selected_int_assigned_to_option_that_was_selected()
			{
				var listData = _dropDownListData.WithSelectedValue(3);
				Assert.AreSame(_dropDownListData, listData);
				_dropDownListData.ToString().Contains("<option value='3' selected='selected'>Name3</option>").ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_DropDownListData_with_selected_string_assigned_to_option_that_was_selected()
			{
				var listData = _dropDownListData.WithSelectedValue("Value2");
				Assert.AreSame(_dropDownListData, listData);
				_dropDownListData.ToString().Contains("<option value='Value2' selected='selected'>Name2</option>").ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_DropDownListData_with_selected_value_assigned_to_option_that_was_selected()
			{
// ReSharper disable ConvertToConstant
// ReSharper disable ConvertToConstant.Local
				string value = "Value2";
// ReSharper restore ConvertToConstant.Local
// ReSharper restore ConvertToConstant
				var listData = _dropDownListData.WithSelectedValue(() => value);
				Assert.AreSame(_dropDownListData, listData);
				_dropDownListData.ToString().Contains("<option value='Value2' selected='selected'>Name2</option>").ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_SubmitOnChange : DropDownListDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_DropDownListData_with_onchange_script_embedded_in_the_HTMLTag()
			{
				var listData = _dropDownListData.SubmitOnChange();
				Assert.AreSame(_dropDownListData, listData);
				listData.ToString().ParseHtmlTag()["onchange"].Contains("this.form.submit();").ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_SubmitOnChange_with_form_field_value_set : DropDownListDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_DropDownListData_with_onchange_script_embedded_in_the_HTMLTag()
			{
// ReSharper disable ConvertToConstant
// ReSharper disable ConvertToConstant.Local
				int pageNumber = 4;
// ReSharper restore ConvertToConstant.Local
// ReSharper restore ConvertToConstant
				var listData = _dropDownListData.SubmitOnChange(() => pageNumber, 4);
				Assert.AreSame(_dropDownListData, listData);
				listData.ToString().ParseHtmlTag()["onchange"].Contains("setFormFieldAndSubmit(\"pageNumber\",\"4\", this)").ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_Exclude_a_Value : DropDownListDataExtensionsTestBase
		{
// ReSharper disable InconsistentNaming
			public string Value = "Value2";
// ReSharper restore InconsistentNaming

			[Test]
			public void Should_return_a_DropDownListData_with_passed_value_excluded()
			{
				var listData = _dropDownListData.Exclude(() => Value);
				Assert.AreSame(_dropDownListData, listData);
				_dropDownListData.ToString().Contains("<option value='Value2'>Name2</option>").ShouldBeFalse();
			}
		}

		[TestFixture]
		public class When_asked_to_Exclude_a_Value_of_a_nullable_parent : DropDownListDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_DropDownListData_with_passed_value_excluded()
			{
				var test = new Test();
				var listData = _dropDownListData.Exclude(() => test, t => t.Value);
				Assert.AreSame(_dropDownListData, listData);
				_dropDownListData.ToString().Contains("<option value='Value2'>Name2</option>").ShouldBeFalse();
			}

			public class Test
			{
// ReSharper disable InconsistentNaming
				public string Value = "Value2";
// ReSharper restore InconsistentNaming
			}
		}
	}
}