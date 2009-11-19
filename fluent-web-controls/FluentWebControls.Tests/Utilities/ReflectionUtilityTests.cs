using System;
using System.Linq.Expressions;

using FluentAssert;

using FluentWebControls.Tools;

using NUnit.Framework;

namespace FluentWebControls.Tests.Utilities
{
	public class ReflectionUtilityTests
	{
		[TestFixture]
		public class When_asked_to_get_the_value_of_an_expression
		{
			[Test]
			public void Should_be_able_to_get_the_value_if_it_is_a_constant()
			{
				Expression<Func<int>> expr = () => TestClass.Id;
				ReflectionUtility.GetValueAsString(expr.Body).ShouldBeEqualTo(TestClass.Id.ToString());
			}

			[Test]
			public void Should_be_able_to_get_the_value_if_it_is_a_static_method()
			{
				Expression<Func<int>> expr = () => TestClass.GetId();
				ReflectionUtility.GetValueAsString(expr.Body).ShouldBeEqualTo(TestClass.Id.ToString());
			}

			[Test]
			public void Should_be_able_to_get_the_value_if_it_is_a_property()
			{
				TestClass testClass = new TestClass();
				Expression<Func<int>> expr = () => testClass.MyId;
				ReflectionUtility.GetValueAsString(expr.Body).ShouldBeEqualTo(TestClass.Id.ToString());
			}

			public class TestClass
			{
				public const int Id = 1234;

				public static int GetId()
				{
					return Id;
				}

				public int MyId { get{ return Id; } }
			}
		}
	}
}