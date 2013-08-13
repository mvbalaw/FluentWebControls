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

			protected DropDownListData DropDownListData;
			protected IPropertyMetaData PropertyMetaData;

			[SetUp]
			public void BeforeEachTest()
			{
				PropertyMetaData = PropertyMetaDataMocker.CreateStub("Name", false, null, null, null, null, typeof(string));
				DropDownListData = new DropDownListData(_items).WithValidationFrom(PropertyMetaData);
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
				var listData = DropDownListData.Exclude(() => Value);
				Assert.AreSame(DropDownListData, listData);
				DropDownListData.ToString().Contains("<option value='Value2'>Name2</option>").ShouldBeFalse();
			}
		}

		[TestFixture]
		public class When_asked_to_Exclude_a_Value_of_a_nullable_parent : DropDownListDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_DropDownListData_with_passed_value_excluded()
			{
				var test = new Test();
				var listData = DropDownListData.Exclude(() => test, t => t.Value);
				Assert.AreSame(DropDownListData, listData);
				DropDownListData.ToString().Contains("<option value='Value2'>Name2</option>").ShouldBeFalse();
			}

			public class Test
			{
// ReSharper disable InconsistentNaming
				public string Value = "Value2";
// ReSharper restore InconsistentNaming
			}
		}

		[TestFixture]
		public class When_asked_to_add_TabIndex : DropDownListDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_DropDownListData_With_TabIndex_initialized()
			{
				const string tabIndex = "1";

				var listData = DropDownListData.WithTabIndex(tabIndex);
				Assert.AreSame(DropDownListData, listData);
				Console.WriteLine(listData.ToString());
				listData.ToString().ParseHtmlTag()["tabindex"].ShouldBeEqualTo(tabIndex);
				listData.ToString().Contains(tabIndex).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_CssClass : DropDownListDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_DropDownListData_With_CssClass_initialized()
			{
				const string cssClass = "cssClass";
				var listData = DropDownListData.CssClass(cssClass);
				Assert.AreSame(DropDownListData, listData);
				listData.ToString().ParseHtmlTag()["class"].ShouldBeEqualTo(cssClass);
				DropDownListData.ToString().Contains(cssClass).ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_Label : DropDownListDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_DropDownListData_With_Label_initialized()
			{
				var label = new LabelData("Id");

				var listData = DropDownListData.WithLabel(label);
				Assert.AreSame(DropDownListData, listData);
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
				var listData = DropDownListData.WithDefault(defaultText, defaultValue);
				Assert.AreSame(DropDownListData, listData);
				DropDownListData.ToString().Contains("<option value='' selected='selected'>All</option>").ShouldBeTrue();
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
				var listData = DropDownListData.WithDefault(defaultText, defaultValue);
				Assert.AreSame(DropDownListData, listData);
				var html = DropDownListData.ToString();
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
				var listData = DropDownListData.WithSelectedValue(3);
				Assert.AreSame(DropDownListData, listData);
				DropDownListData.ToString().Contains("<option value='3' selected='selected'>Name3</option>").ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_DropDownListData_with_selected_string_assigned_to_option_that_was_selected()
			{
				var listData = DropDownListData.WithSelectedValue("Value2");
				Assert.AreSame(DropDownListData, listData);
				DropDownListData.ToString().Contains("<option value='Value2' selected='selected'>Name2</option>").ShouldBeTrue();
			}

			[Test]
			public void Should_return_a_DropDownListData_with_selected_value_assigned_to_option_that_was_selected()
			{
// ReSharper disable ConvertToConstant
// ReSharper disable ConvertToConstant.Local
				var value = "Value2";
// ReSharper restore ConvertToConstant.Local
// ReSharper restore ConvertToConstant
				var listData = DropDownListData.WithSelectedValue(() => value);
				Assert.AreSame(DropDownListData, listData);
				DropDownListData.ToString().Contains("<option value='Value2' selected='selected'>Name2</option>").ShouldBeTrue();
			}
		}

		[TestFixture]
		public class When_asked_to_assign_SubmitOnChange : DropDownListDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_DropDownListData_with_onchange_script_embedded_in_the_HTMLTag()
			{
				var listData = DropDownListData.SubmitOnChange();
				Assert.AreSame(DropDownListData, listData);
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
				var pageNumber = 4;
// ReSharper restore ConvertToConstant.Local
// ReSharper restore ConvertToConstant
				var listData = DropDownListData.SubmitOnChange(() => pageNumber, 4);
				Assert.AreSame(DropDownListData, listData);
				listData.ToString().ParseHtmlTag()["onchange"].Contains("setFormFieldAndSubmit(\"pageNumber\",\"4\", this)").ShouldBeTrue();
			}
		}
	}
}