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
			public void Given_a_non_null_item()
			{
				Test.Static()
					.When(Constructor_is_called)
					.With(A_non_null_item)
					.Should(Put_input_on_item_property)
					.Verify();
			}

			[Test]
			public void Given_a_null_item()
			{
				Test.Static()
					.When(Constructor_is_called)
					.With(A_null_item)
					.Should(Create_a_new_item)
					.Verify();
			}

			private void A_non_null_item()
			{
				_item = new Foo();
			}

			private void A_null_item()
			{
				_item = null;
			}

			private void Constructor_is_called()
			{
				_uiMap = new FooUIMap(_item, null);
			}

			private void Create_a_new_item()
			{
				_uiMap.Item.ShouldNotBeNull();
			}

			public class Foo
			{
			}

			public class FooUIMap : ListUIMapWithItem<Foo, Foo, Foo>
			{
				public FooUIMap(Foo item, IEnumerable<Foo> items)
					: base(item, items)
				{
				}
			}

			private void Put_input_on_item_property()
			{
				ReferenceEquals(_uiMap.Item, _item).ShouldBeTrue();
			}
		}
	}
}