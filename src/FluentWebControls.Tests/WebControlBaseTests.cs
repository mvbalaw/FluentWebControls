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

using FluentWebControls.Extensions;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class WebControlBaseTests
	{
		public class FooControl : WebControlBase
		{
			public string GetIdWithPrefix()
			{
				return IdWithPrefix;
			}

			public string GetNameWithPrefix()
			{
				return NameWithPrefix;
			}
		}

		[TestFixture]
		public class When_asked_for_the_id_with_prefix
		{
			[Test]
			public void Given_the_control_has_a_name_prefix_but_no_id_prefix__should_return_only_the_id()
			{
				var control = new FooControl().WithId("McKennitt").WithNamePrefix("Jack");
				var idWithPrefix = control.GetIdWithPrefix();
				idWithPrefix.ShouldBeEqualTo("mcKennitt");
			}

			[Test]
			public void Given_the_control_has_an_id_prefix__should_join_the_prefix_and_the_id()
			{
				var control = new FooControl().WithId("McKennitt").WithIdPrefix("Loreena");
				var idWithPrefix = control.GetIdWithPrefix();
				idWithPrefix.ShouldBeEqualTo("loreena_McKennitt");
			}

			[Test]
			public void Given_the_control_has_no_id_prefix__should_return_only_the_id()
			{
				var control = new FooControl().WithId("McKennitt");
				var idWithPrefix = control.GetIdWithPrefix();
				idWithPrefix.ShouldBeEqualTo("mcKennitt");
			}
		}

		[TestFixture]
		public class When_asked_for_the_name_with_prefix
		{
			[Test]
			public void Given_the_control_has_a_name_with_a_name_prefix_and_an_id_prefix__should_join_the_name_prefix_and_the_name()
			{
				var control = new FooControl().WithName("McKennitt").WithNamePrefix("Loreena").WithIdPrefix("Irene");
				var nameWithPrefix = control.GetNameWithPrefix();
				nameWithPrefix.ShouldBeEqualTo("loreena.McKennitt");
			}

			[Test]
			public void Given_the_control_has_a_name_with_a_name_prefix_but_no_id_prefix__should_join_the_name_prefix_and_the_name()
			{
				var control = new FooControl().WithName("McKennitt").WithNamePrefix("Loreena");
				var nameWithPrefix = control.GetNameWithPrefix();
				nameWithPrefix.ShouldBeEqualTo("loreena.McKennitt");
			}

			[Test]
			public void Given_the_control_has_a_name_with_no_name_prefix_and_no_id_prefix__should_return_only_the_name()
			{
				var control = new FooControl().WithName("McKennitt");
				var nameWithPrefix = control.GetNameWithPrefix();
				nameWithPrefix.ShouldBeEqualTo("mcKennitt");
			}

			[Test]
			public void Given_the_control_has_a_name_with_no_name_prefix_but_has_an_id_prefix__should_return_only_the_name()
			{
				var control = new FooControl().WithName("McKennitt").WithIdPrefix("Irene");
				var nameWithPrefix = control.GetNameWithPrefix();
				nameWithPrefix.ShouldBeEqualTo("mcKennitt");
			}

			[Test]
			public void Given_the_control_has_no_name_with_no_name_prefix_but_has_an_id_and_id_prefix__should_return_the_id_with_id_prefix()
			{
				var control = new FooControl().WithId("McKennitt").WithIdPrefix("Loreena");
				var nameWithPrefix = control.GetNameWithPrefix();
				nameWithPrefix.ShouldBeEqualTo("loreena.McKennitt");
			}
		}
	}
}