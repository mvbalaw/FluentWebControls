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
using System.Linq;
using System.Linq.Expressions;

using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;
using FluentWebControls.Tests.Extensions;

using LinqToHtml;

using MvbaCore;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class ComboSelectDataTest
	{
		[TestFixture]
		public class When_asked_to_build_the_combo_select_HTML
		{
			private ComboSelectData _comboSelect;
			private IEnumerable<KeyValuePair<string, string>> _items;
			private HTMLDocument _result;
			private string _resultHtml;

			[Test]
			public void Given_items_that_are_not_selected()
			{
				Test.Verify(
					with_items,
					with_a_ComboSelectData_container,
					when_asked_to_build_the_combo_select_HTML,
					should_not_return_null,
					should_include_an_option_tag_for_each_item,
					should_not_have_any_selected_items
					);
			}

			private void should_include_an_option_tag_for_each_item()
			{
				var options = _result.DescendantTags.OfType("option").IgnoreCase().ToList();

				foreach (var item in _items)
				{
					options
						.WithAttributeNamed("value").IgnoreCase()
						.HavingValue(item.Value)
						.Any()
						.ShouldBeTrue();
				}
			}

			private void should_not_have_any_selected_items()
			{
				var options = _result.DescendantTags.OfType("option").IgnoreCase().ToList();
				options.WithAttributeNamed("selected").IgnoreCase().Count().ShouldBeEqualTo(0);
			}

			private void should_not_return_null()
			{
				_resultHtml.ShouldNotBeNull();
			}

			private void when_asked_to_build_the_combo_select_HTML()
			{
				_resultHtml = _comboSelect.ToString();
				_result = HTMLParser.Parse(_resultHtml);
			}

			private void with_a_ComboSelectData_container()
			{
				_comboSelect = new ComboSelectData(_items);
			}

			private void with_items()
			{
				_items = new List<KeyValuePair<string, string>>
				         {
					         new KeyValuePair<string, string>("Name1", "Value1"),
					         new KeyValuePair<string, string>("Name2", "Value2"),
					         new KeyValuePair<string, string>("Name3", "Value3"),
					         new KeyValuePair<string, string>("Name4", "Value4")
				         };
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_ComboSelect_for_a_property
		{
			private ComboSelectData _comboSelectData;

			// cannot be const or Reflection.GetPropertyName won't handle it correctly
// ReSharper disable ConvertToConstant.Local
// ReSharper disable once FieldCanBeMadeReadOnly.Local
			private string _value = "value";

			[Test]
			public void Should_return_HTML_code_representing_a_ComboSelect_with_its_value_embedded_in_it()
			{
				_comboSelectData = new ComboSelectData(Items)
					.WithValidationFrom(PropertyMetaData)
					.WithId(Id);

				SetSelected();
				AssertAreEqual(HtmlText, _comboSelectData);
			}

			private static void AssertAreEqual(string textToCompare, ComboSelectData comboSelectData)
			{
				comboSelectData.ToString().ShouldBeEqualTo(textToCompare);
			}

			private static string HtmlText
			{
				get { return "<select name='_value' id='_value' class='required comboselect' multiple='multiple' size='6'><option value='Value1' selected='selected'>Name1</option><option value='Value2'>Name2</option><option value='Value3' selected='selected'>Name3</option><option value='Value4'>Name4</option></select><em>*</em>"; }
			}

			private string Id
			{
				get
				{
					Expression<Func<string>> expr = () => _value;
					return Reflection.GetPropertyName(expr).ToCamelCase();
				}
			}

			private static IEnumerable<KeyValuePair<string, string>> Items
			{
				get
				{
					return new List<KeyValuePair<string, string>>
					       {
						       new KeyValuePair<string, string>("Name1", "Value1"),
						       new KeyValuePair<string, string>("Name2", "Value2"),
						       new KeyValuePair<string, string>("Name3", "Value3"),
						       new KeyValuePair<string, string>("Name4", "Value4")
					       };
				}
			}

			private IPropertyMetaData PropertyMetaData
			{
				get { return PropertyMetaDataMocker.CreateStub(Id, true, null, null, null, null, typeof(string)); }
			}

			private void SetSelected()
			{
				_comboSelectData.SelectedValues.Add("Value1");
				_comboSelectData.SelectedValues.Add("Value3");
			}
		}
	}
}