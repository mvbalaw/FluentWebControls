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
using System.Linq.Expressions;

using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;
using FluentWebControls.Tests.Extensions;

using NUnit.Framework;

using Rhino.Mocks;

namespace FluentWebControls.Tests
{
	public class TextBoxTest
	{
		internal class Test
		{
			public Test(int value)
			{
				Value = value;
			}

			public int Value { get; set; }
		}

		internal class TestString
		{
			public TestString(string value)
			{
				Value = value;
			}

			public string Value { get; set; }
		}

		[TestFixture]
		public class When_asked_to_create_a_TextBox_for_a_property_of_type_int
		{
			private IBusinessObjectPropertyMetaDataFactory _businessObjectPropertyMetaDataFactory;

			[SetUp]
			public void BeforeEachTest()
			{
				_businessObjectPropertyMetaDataFactory = MockRepository.GenerateStub<IBusinessObjectPropertyMetaDataFactory>();
				Configuration.ValidationMetaDataFactory = _businessObjectPropertyMetaDataFactory;
			}

			[Test]
			public void Should_return_HTML_code_representing_a_TextBox_with_its_value_embedded_in_it()
			{
				var test = new Test(10);
				Expression<Func<Test, int>> metadataFunc = x => x.Value;
				_businessObjectPropertyMetaDataFactory.Expect(x => x.GetFor(metadataFunc)).IgnoreArguments().Return(PropertyMetaDataMocker.CreateStub("Value", false, null, null, null, null, typeof(int)));

				var textBoxData = TextBox.For(test, x => x.Value.ToString(), x => x.Value).WithValidationFrom((Test x) => x.Value);
				textBoxData.ToString().ShouldBeEqualTo("<input type='text' id='value' name='value' class='textbox digits' value='10'/>");
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_TextBox_for_a_property_of_type_string
		{
			private IBusinessObjectPropertyMetaDataFactory _businessObjectPropertyMetaDataFactory;

			[SetUp]
			public void BeforeEachTest()
			{
				_businessObjectPropertyMetaDataFactory = MockRepository.GenerateStub<IBusinessObjectPropertyMetaDataFactory>();
				Configuration.ValidationMetaDataFactory = _businessObjectPropertyMetaDataFactory;
			}

			[Test]
			public void Should_return_HTML_code_representing_a_TextBox_with_its_value_embedded_in_it()
			{
				var test = new TestString("Ginger's House");
				Expression<Func<TestString, string>> metadataFunc = x => x.Value;
				_businessObjectPropertyMetaDataFactory.Expect(x => x.GetFor(metadataFunc)).IgnoreArguments().Return(PropertyMetaDataMocker.CreateStub("Value", false, null, null, null, null, typeof(string)));

				var textBoxData = TextBox.For(test, x => x.Value.ToString(), x => x.Value).WithValidationFrom((Test x) => x.Value);
				textBoxData.ToString().ShouldBeEqualTo("<input type='text' id='value' name='value' class='textbox' value='Ginger&apos;s House'/>");
			}
		}
	}
}