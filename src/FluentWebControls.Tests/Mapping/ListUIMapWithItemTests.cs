using System.Collections.Generic;

using FluentAssert;

using FluentWebControls.Mapping;

using NUnit.Framework;

namespace FluentWebControls.Tests.Mapping
{
	public class ListUIMapWithItemTests
	{
		[TestFixture]
		public class When_the_constructor_is_called
		{
			private Foo _item;
			private FooUIMap _uiMap;

			[Test]
			public void Given_a_null_item()
			{
				Test.Static()
					.When(Constructor_is_called)
					.With(A_null_item)
					.Should(Create_a_new_item)
					.Verify();
			}

			[Test]
			public void Given_a_non_null_item()
			{
				Test.Static()
					.When(Constructor_is_called)
					.With(A_non_null_item)
					.Should(Put_input_on_item_property)
					.Verify();
			}

			private void A_null_item()
			{
				_item = null;
			}
			private void A_non_null_item()
			{
				_item = new Foo();
			}

			private void Create_a_new_item()
			{
				_uiMap.Item.ShouldNotBeNull();
			}

			private void Put_input_on_item_property()
			{
				ReferenceEquals(_uiMap.Item, _item).ShouldBeTrue();
			}

			private void Constructor_is_called()
			{
				_uiMap = new FooUIMap(_item, null);
			}

			public class FooUIMap : ListUIMapWithItem<Foo,Foo,Foo>
			{
				public FooUIMap(Foo item, IEnumerable<Foo> items)
					: base(item, items)
				{
				}
			}

			public class Foo
			{}
		}
	}
}