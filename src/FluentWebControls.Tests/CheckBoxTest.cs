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

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class CheckBoxTest
	{
		[TestFixture]
		public class When_asked_to_create_a_checkbox_for_a_property
		{
			[Test]
			public void Should_return_html_code_representing_a_checkbox_with_its_value_embedded_in_it()
			{
				const bool value = true;
				var foo = new Foo
				          {
					          Value = value
				          };
				var checkBoxData = CheckBox.For(foo, x => x.Value);
				checkBoxData.ToString().ShouldBeEqualTo("<input type='checkbox' id='value' name='value' checked='checked' value='true'/>");
			}

			public class Foo
			{
				public bool Value { get; set; }
			}
		}
	}
}