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
using FluentWebControls.Tests.Extensions;

using JetBrains.Annotations;

using MvbaCore;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	[UsedImplicitly]
	public class DropDownListDataTest
	{
		[TestFixture]
		public class When_asked_to_create_a_DropDownList_for_a_property
		{
			private DropDownListData _dropDownListData;
			private string _htmlText;
			private string _id;
			private IEnumerable<KeyValuePair<string, string>> _items;
			private IPropertyMetaData _propertyMetaData;

			[SetUp]
			public void BeforeEachTest()
			{
// ReSharper disable ConvertToConstant.Local
				var value = "value";
// ReSharper restore ConvertToConstant.Local

				_id = Reflection.GetPropertyName(() => value).ToCamelCase();
				_propertyMetaData = PropertyMetaDataMocker.CreateStub(_id, true, null, null, null, null, typeof(string));

				_htmlText = "<select name='value' id='value' class='required ddlDetail'><option value='Value1'>Name1</option><option value='Value2' selected='selected'>Name2</option></select><em>*</em>";
				_items = new List<KeyValuePair<string, string>>
				         {
					         new KeyValuePair<string, string>("Name1", "Value1"),
					         new KeyValuePair<string, string>("Name2", "Value2")
				         };
			}

			[Test]
			public void Should_return_HTML_code_representing_a_DropDownList_with_its_value_embedded_in_it()
			{
				_dropDownListData = new DropDownListData(_items)
					.WithValidationFrom(_propertyMetaData)
					.WithId(_id)
					.WithSelectedValue("Value2");

				_dropDownListData.ToString().ShouldBeEqualTo(_htmlText);
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_DropDownList_for_a_property_with_Default_value
		{
			private DropDownListData _dropDownListData;
			private string _htmlText;
			private string _id;
			private IEnumerable<KeyValuePair<string, string>> _items;
			private IPropertyMetaData _propertyMetaData;

			[SetUp]
			public void BeforeEachTest()
			{
// ReSharper disable ConvertToConstant.Local
				var value = "value";
// ReSharper restore ConvertToConstant.Local

				_id = Reflection.GetPropertyName(() => value).ToCamelCase();
				_propertyMetaData = PropertyMetaDataMocker.CreateStub(_id, false, null, null, null, null, typeof(string));

				_htmlText = "<select name='value' id='value' class='ddlDetail'><option value='' selected='selected'>All</option><option value='Value1'>Name1</option><option value='Value2'>Name2</option></select>";
				_items = new List<KeyValuePair<string, string>>
				         {
					         new KeyValuePair<string, string>("Name1", "Value1"),
					         new KeyValuePair<string, string>("Name2", "Value2")
				         };
			}

			[Test]
			public void Should_return_HTML_code_representing_a_DropDownList_with_its_value_embedded_in_it()
			{
				_dropDownListData = new DropDownListData(_items)
					.WithValidationFrom(_propertyMetaData)
					.WithId(_id)
					.WithDefault("All", "")
					.WithSelectedValue("");

				_dropDownListData.ToString().ShouldBeEqualTo(_htmlText);
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_DropDownList_for_a_property_with_SubmitOnChange_set_to_true
		{
			private DropDownListData _dropDownListData;
			private string _htmlText;
			private string _id;
			private IEnumerable<KeyValuePair<string, string>> _items;
			private IPropertyMetaData _propertyMetaData;

			[SetUp]
			public void BeforeEachTest()
			{
// ReSharper disable ConvertToConstant.Local
				var value = "value";
// ReSharper restore ConvertToConstant.Local

				_id = Reflection.GetPropertyName(() => value).ToCamelCase();
				_propertyMetaData = PropertyMetaDataMocker.CreateStub(_id, false, null, null, null, null, typeof(string));

				_htmlText = "<select name='value' id='value' class='ddlDetail' onchange='this.form.submit();'><option value='Value1' selected='selected'>Name1</option><option value='Value2'>Name2</option></select>";
				_items = new List<KeyValuePair<string, string>>
				         {
					         new KeyValuePair<string, string>("Name1", "Value1"),
					         new KeyValuePair<string, string>("Name2", "Value2")
				         };
			}

			[Test]
			public void Should_return_HTML_code_representing_a_DropDownList_with_its_value_embedded_in_it()
			{
				_dropDownListData = new DropDownListData(_items)
					.WithValidationFrom(_propertyMetaData)
					.WithId(_id);

				_dropDownListData.SubmitOnChange();
				_dropDownListData.ToString().ShouldBeEqualTo(_htmlText);
			}
		}

		[TestFixture]
		public class When_asked_to_make_the_DropDownList_readonly
		{
			private string _htmlText;
			private string _id;
			private IEnumerable<KeyValuePair<string, string>> _items;

			[SetUp]
			public void BeforeEachTest()
			{
// ReSharper disable ConvertToConstant.Local
				var value = "value";
// ReSharper restore ConvertToConstant.Local

				_id = Reflection.GetPropertyName(() => value).ToCamelCase();

				_htmlText = "<select name='value' id='value_readonly' class='ddlDetail' disabled='disabled'><option value='Value1' selected='selected'>Name1</option></select><input type='hidden' id='value' name='value' value='Value1'/>";
				_items = new List<KeyValuePair<string, string>>
				         {
					         new KeyValuePair<string, string>("Name1", "Value1"),
				         };
			}

			[Test]
			public void Should_add_hidden_control_with_the_selected_value()
			{
				var dropDownListData = new DropDownListData(_items)
					.AsReadOnly()
					.WithId(_id);

				var result = dropDownListData.ToString();
				result.ShouldBeEqualTo(_htmlText);
			}
		}
	}
}