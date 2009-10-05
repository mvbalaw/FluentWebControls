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
				List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>
					{
						new KeyValuePair<string, string>("Name1", "Value1"),
						new KeyValuePair<string, string>("Name2", "Value2")
					};

				DropDownListData dropDownListData = DropDownList.For("value", items, nvp => nvp.Key, nvp => nvp.Value);
				dropDownListData.ToString().ShouldBeEqualTo("<select name='value' id='value' class='ddlDetail'><option value='Value1'>Name1</option><option value='Value2'>Name2</option></select>");
			}
		}
	}
}