using System;
using System.Linq;
using System.Linq.Expressions;

using FluentAssert;

using FluentWebControls.Tools;

using NUnit.Framework;

namespace FluentWebControls.Tests.Utilities
{
	public class ReflectionUtilityTests
	{
		[TestFixture]
		public class When_asked_to_get_method_call_data
		{
			[Test]
			public void Should_get_the_correct_class_name()
			{
				var methodCallData = ReflectionUtility.GetMethodCallData((TestCalculator c) => c.Add(6));
				methodCallData.ClassName.ShouldBeEqualTo("TestCalculator");
			}

			[Test]
			public void Should_get_the_correct_method_name()
			{
				var methodCallData = ReflectionUtility.GetMethodCallData((TestCalculator c) => c.Add(6));
				methodCallData.MethodName.ShouldBeEqualTo("Add");
			}

			[Test]
			public void Should_get_the_correct_parameter_names()
			{
				var methodCallData = ReflectionUtility.GetMethodCallData((TestCalculator c) => c.Add(6));
				methodCallData.ParameterValues.Count.ShouldBeEqualTo(1);
				methodCallData.ParameterValues.Keys.First().ShouldBeEqualTo("addend");
			}

			[Test]
			public void Should_get_the_correct_parameter_values()
			{
				const int expected = 6;
				var methodCallData = ReflectionUtility.GetMethodCallData((TestCalculator c) => c.Add(expected));
				methodCallData.ParameterValues.Count.ShouldBeEqualTo(1);
				methodCallData.ParameterValues.Values.First().ShouldBeEqualTo(expected.ToString());
			}

			public class TestCalculator
			{
				private int _total;

				public int Add(int addend)
				{
					_total += addend;
					return _total;
				}
			}
		}

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
			public void Should_be_able_to_get_the_value_if_it_is_a_property()
			{
				var testClass = new TestClass();
				Expression<Func<int>> expr = () => testClass.MyId;
				ReflectionUtility.GetValueAsString(expr.Body).ShouldBeEqualTo(TestClass.Id.ToString());
			}

			[Test]
			public void Should_be_able_to_get_the_value_if_it_is_a_static_method()
			{
				Expression<Func<int>> expr = () => TestClass.GetId();
				ReflectionUtility.GetValueAsString(expr.Body).ShouldBeEqualTo(TestClass.Id.ToString());
			}

			public class TestClass
			{
				public const int Id = 1234;

				public int MyId
				{
					get { return Id; }
				}

				public static int GetId()
				{
					return Id;
				}
			}
		}
	}
}