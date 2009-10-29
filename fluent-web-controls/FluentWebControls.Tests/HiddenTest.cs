using FluentAssert;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class HiddenTest
	{
		[TestFixture]
		public class When_asked_to_create_a_hidden_field_for_a_property_of_type_int
		{
			public class Foo
			{
				public int Value { get; set; }
			}
			[Test]
			public void Should_return_html_code_representing_a_hidden_field_with_its_value_embedded_in_it()
			{
				const int value = 10;
				Foo foo = new Foo{Value = value};
				HiddenData hiddenData = Hidden.For(foo, x => x.Value.ToString(), x=>x.Value);
				hiddenData.ToString().ShouldBeEqualTo("<input type='hidden' id='value' name='value' value='" + value + "'/>");
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_hidden_field_for_a_property_of_type_nullable_int
		{
			public class Foo
			{
				public int? Value { get; set; }
			}
			[Test]
			public void Should_return_html_code_representing_a_hidden_field_with_its_value_embedded_in_it()
			{
				int? value = 10;
				Foo foo = new Foo { Value = value };
				HiddenData hiddenData = Hidden.For(foo, x => x.Value==null?"":x.Value.ToString(), x => x.Value);
				hiddenData.ToString().ShouldBeEqualTo("<input type='hidden' id='value' name='value' value='" + value + "'/>");
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_hidden_field_for_a_property_of_type_string
		{
			public class Foo
			{
				public string Value { get; set; }
			}

			[Test]
			public void Should_return_html_code_representing_a_hidden_field_with_its_value_embedded_in_it()
			{
				const string value = "Bar";
				Foo foo = new Foo { Value = value };
				HiddenData hiddenData = Hidden.For(foo, x => x.Value);
				hiddenData.ToString().ShouldBeEqualTo("<input type='hidden' id='value' name='value' value='" + value + "'/>");
			}
		}
	}
}