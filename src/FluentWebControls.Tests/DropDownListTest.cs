//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System.Collections.Generic;

using FluentAssert;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class DropDownListTest
	{
		[TestFixture]
		public class When_asked_to_create_a_DropDownList
		{
			[Test]
			public void Should_return_HTML_code_representing_a_DropDownList_with_its_value_embedded_in_it()
			{
				var items = new List<KeyValuePair<string, string>>
				            {
					            new KeyValuePair<string, string>("Name1", "Value1"),
					            new KeyValuePair<string, string>("Name2", "Value2")
				            };

				var dropDownListData = DropDownList.For(items, nvp => nvp.Key, nvp => nvp.Value, (Foo f) => f.Value);
				dropDownListData.ToString().ShouldBeEqualTo("<select name='value' id='value' class='ddlDetail'><option value='Value1' selected='selected'>Name1</option><option value='Value2'>Name2</option></select>");
			}

			public class Foo
			{
				public string Value { get; set; }
			}
		}
	}
}