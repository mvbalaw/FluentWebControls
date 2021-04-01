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

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class KeyValuePairExtensionsTests
	{
		[TestFixture]
		public class When_asked_to_construct_a_query_string
		{
			private List<KeyValuePair<string, string>> _parameters;

			[SetUp]
			public void BeforeEachTest()
			{
				_parameters = new List<KeyValuePair<string, string>>();
			}

			[Test]
			public void Should_return_a_query_string_if_there_is_one_key_value_pair()
			{
				const string key1 = "key1";
				const string value1 = "value1";
				_parameters.Add(new KeyValuePair<string, string>(key1, value1));
				_parameters.ToQueryString().ShouldBeEqualTo($"{key1}={value1}");
			}

			[Test]
			public void Should_return_a_query_string_with_keys_Url_encoded()
			{
				const string key1 = "ke y1";
				const string value1 = "value1";
				_parameters.Add(new KeyValuePair<string, string>(key1, value1));

				_parameters.ToQueryString().ShouldBeEqualTo($"{key1.EscapeForUrl()}={value1}");
			}

			[Test]
			public void Should_return_a_query_string_with_values_Url_encoded()
			{
				const string key1 = "key1";
				const string value1 = "val ue1";
				_parameters.Add(new KeyValuePair<string, string>(key1, value1));

				_parameters.ToQueryString().ShouldBeEqualTo($"{key1}={value1.EscapeForUrl()}");
			}

			[Test]
			public void Should_return_a_valid_query_string_if_any_value_is_empty()
			{
				const string key1 = "key1";
				const string value1 = "";
				const string value2 = "value2";
				const string key2 = "key2";

				_parameters.Add(new KeyValuePair<string, string>(key1, value1));
				_parameters.Add(new KeyValuePair<string, string>(key2, value2));
				_parameters.ToQueryString().ShouldBeEqualTo($"{key1}=&{key2}={value2}");
			}

			[Test]
			public void Should_return_a_valid_query_string_if_any_value_is_null()
			{
				const string key1 = "key1";
				const string value1 = null;
				const string value2 = "value2";
				const string key2 = "key2";

				_parameters.Add(new KeyValuePair<string, string>(key1, value1));
				_parameters.Add(new KeyValuePair<string, string>(key2, value2));
				_parameters.ToQueryString().ShouldBeEqualTo($"{key1}=&{key2}={value2}");
			}

			[Test]
			public void Should_return_a_valid_query_string_if_there_are_multiple_key_value_pairs()
			{
				const string key1 = "key1";
				const string value1 = "value1";
				const string value2 = "value2";
				const string key2 = "key2";

				_parameters.Add(new KeyValuePair<string, string>(key1, value1));
				_parameters.Add(new KeyValuePair<string, string>(key2, value2));
				_parameters.ToQueryString().ShouldBeEqualTo($"{key1}={value1}&{key2}={value2}");
			}

			[Test]
			public void Should_return_empty_string_if_the_input_is_empty_string()
			{
				_parameters.ToQueryString().ShouldBeEqualTo("");
			}

			[Test]
			public void Should_return_empty_string_if_the_input_is_null()
			{
				_parameters = null;
				_parameters.ToQueryString().ShouldBeEqualTo("");
			}

			[Test]
			public void Should_throw_exception_if_any_key_is_empty_when_trimmed()
			{
				const string key1 = " ";
				const string value1 = "value1";
				const string value2 = "value2";
				const string key2 = null;

				_parameters.Add(new KeyValuePair<string, string>(key1, value1));
				_parameters.Add(new KeyValuePair<string, string>(key2, value2));
				Assert.Throws(typeof(ArgumentException), () =>
					_parameters.ToQueryString()
					);
			}

			[Test]
			public void Should_throw_exception_if_any_key_is_null()
			{
				const string key1 = "key1";
				const string value1 = "value1";
				const string value2 = "value2";
				const string key2 = null;

				_parameters.Add(new KeyValuePair<string, string>(key1, value1));
				_parameters.Add(new KeyValuePair<string, string>(key2, value2));
				Assert.Throws(typeof(ArgumentException), () =>
					_parameters.ToQueryString()
					);
			}
		}
	}
}