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

using FluentWebControls.Tests.Extensions;

using MvbaCore;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class HiddenTest
	{
		[TestFixture]
		public class When_asked_to_create_a_hidden_field
		{
			private string _expectedId;
			private string _expectedName;
			private string _expectedValue;
			private Dictionary<string, string> _parsedResult;
			private string _result;

			[Test]
			public void Given_a_source_property_of_type_int()
			{
				Test.Given<HiddenData>()
					.CreatedBy(Create_HiddenData_from_integer_context, new IntegerContext
					                                                   {
						                                                   Value = 10
					                                                   })
					.When(ToString_is_called)
					.Should(Get_the_property_name_as_the_tag_id_attribute)
					.Should(Get_the_property_name_as_the_tag_name_attribute)
					.Should(Get_the_property_value_as_the_tag_value_attribute)
					.Verify();
			}

			[Test]
			public void Given_a_source_property_of_type_nullable_int()
			{
				Test.Given<HiddenData>()
					.CreatedBy(Create_HiddenData_from_nullable_integer_context, new NullableIntegerContext
					                                                            {
						                                                            Value = 10
					                                                            })
					.When(ToString_is_called)
					.Should(Get_the_property_name_as_the_tag_id_attribute)
					.Should(Get_the_property_name_as_the_tag_name_attribute)
					.Should(Get_the_property_value_as_the_tag_value_attribute)
					.Verify();
			}

			[Test]
			public void Given_a_source_property_of_type_string()
			{
				Test.Given<HiddenData>()
					.CreatedBy(Create_HiddenData_from_string_context, new StringContext
					                                                  {
						                                                  Value = "Bar"
					                                                  })
					.When(ToString_is_called)
					.Should(Get_the_property_name_as_the_tag_id_attribute)
					.Should(Get_the_property_name_as_the_tag_name_attribute)
					.Should(Get_the_property_value_as_the_tag_value_attribute)
					.Verify();
			}

			private HiddenData Create_HiddenData_from_integer_context(IntegerContext context)
			{
				_expectedId = Reflection.GetPropertyName(() => context.Value).ToCamelCase();
				_expectedName = _expectedId;
				_expectedValue = context.Value.ToString();
				return Hidden.For(context, x => x.Value.ToString(), x => x.Value);
			}

			private HiddenData Create_HiddenData_from_nullable_integer_context(NullableIntegerContext context)
			{
				_expectedId = Reflection.GetPropertyName(() => context.Value).ToCamelCase();
				_expectedName = _expectedId;
				_expectedValue = context.Value.ToString();
				return Hidden.For(context, x => x.Value.ToString(), x => x.Value);
			}

			private HiddenData Create_HiddenData_from_string_context(StringContext context)
			{
				_expectedId = Reflection.GetPropertyName(() => context.Value).ToCamelCase();
				_expectedName = _expectedId;
				_expectedValue = context.Value;
				return Hidden.For(context, x => x.Value.ToString(), x => x.Value);
			}

			private void Get_the_property_name_as_the_tag_id_attribute()
			{
				_parsedResult["id"].ShouldBeEqualTo(_expectedId);
			}

			private void Get_the_property_name_as_the_tag_name_attribute()
			{
				_parsedResult["name"].ShouldBeEqualTo(_expectedName);
			}

			private void Get_the_property_value_as_the_tag_value_attribute()
			{
				_parsedResult["value"].ShouldBeEqualTo(_expectedValue);
			}

			public class IntegerContext
			{
				public int Value { get; set; }
			}

			public class NullableIntegerContext
			{
				public int? Value { get; set; }
			}

			public class StringContext
			{
				public string Value { get; set; }
			}

			private void ToString_is_called(HiddenData hiddenData)
			{
				_result = hiddenData.ToString();
				_parsedResult = _result.ParseHtmlTag();
			}
		}
	}
}