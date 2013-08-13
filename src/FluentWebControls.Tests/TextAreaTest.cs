//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using FluentAssert;

using FluentWebControls.Interfaces;

using NUnit.Framework;

using Rhino.Mocks;

namespace FluentWebControls.Tests
{
	public class TextAreaTest
	{
		internal class Test
		{
			public Test(string value)
			{
				Value = value;
			}

			public string Value { get; set; }
		}

		[TestFixture]
		public class When_asked_to_create_a_textarea_for_a_property_of_type_string
		{
			[SetUp]
			public void BeforeEachTest()
			{
				Configuration.ValidationMetaDataFactory = MockRepository.GenerateStub<IBusinessObjectPropertyMetaDataFactory>();
			}

			[Test]
			public void Should_return_html_code_representing_a_textarea_with_its_value_embedded_in_it()
			{
				var test = new Test("text");
				var textAreaData = TextArea.For(test, x => x.Value, x => x.Value);
				textAreaData.ToString().ShouldBeEqualTo("<textarea id='value' name='value' class='textbox'>text</textarea>");
			}
		}
	}
}