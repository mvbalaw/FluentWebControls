using System;
using System.Collections.Generic;

using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;
using FluentWebControls.Tests.Extensions;

using MvbaCore;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
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
				string value = "value";

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
				string value = "value";

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
				string value = "value";

				_id = Reflection.GetPropertyName(() => value).ToCamelCase();
				_propertyMetaData = PropertyMetaDataMocker.CreateStub(_id, false, null, null, null, null, typeof(string));

				_htmlText = "<select name='value' id='value' class='ddlDetail' onchange='this.form.submit();'><option value='Value1'>Name1</option><option value='Value2'>Name2</option></select>";
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
	}
}