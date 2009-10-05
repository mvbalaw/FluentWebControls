using System;
using System.Diagnostics;
using System.Linq.Expressions;
using FluentWebControls.Extensions;

namespace FluentWebControls.Tools
{
	public static class NameUtility
	{
		public static string GetCamelCaseMultiLevelPropertyName(params string[] propertyNames)
		{
			return GetMultiLevelPropertyName(propertyNames).ToCamelCase();
		}

		public static string GetCamelCasePropertyName<T, TReturn>(Expression<Func<T, TReturn>> expression)
		{
			return GetPropertyName(expression).ToCamelCase();
		}

		[DebuggerStepThrough]
		public static string GetMethodName<T, TReturn>(Expression<Func<T, TReturn>> expression)
		{
			var memberExpression = expression.Body as MethodCallExpression;
			if (memberExpression == null)
			{
				throw new ArgumentException("expression must be in the form: (Foo instance) => instance.Method");
			}
			return memberExpression.Method.Name;
		}

		public static string GetMultiLevelPropertyName(params string[] propertyNames)
		{
			return propertyNames.Join(".");
		}

		[DebuggerStepThrough]
		public static string GetPropertyName<T, TReturn>(Expression<Func<T, TReturn>> expression)
		{
			MemberExpression memberExpression = expression.Body as MemberExpression;
			if (memberExpression == null)
			{
				throw new ArgumentException(
					"expression must be in the form: (Thing instance) => instance.Property[.Optional.Other.Properties.In.Chain]");
			}
			string name = memberExpression.Member.Name;
			while (memberExpression.Expression as MemberExpression != null)
			{
				memberExpression = (MemberExpression) memberExpression.Expression;
				name = memberExpression.Member.Name + "." + name;
			}
			return name;
		}

		[DebuggerStepThrough]
		public static string GetPropertyName<T>(Expression<Func<T>> expression)
		{
			MemberExpression memberExpression = expression.Body as MemberExpression;
			if (memberExpression == null)
			{
				throw new ArgumentException("expression must be in the form: () => instance.Property");
			}
			return memberExpression.Member.Name;
		}
	}
}