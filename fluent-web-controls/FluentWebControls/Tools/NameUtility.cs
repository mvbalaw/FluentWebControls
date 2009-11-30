using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
		public static string GetFinalPropertyName<T>(Expression<Func<T>> expression)
		{
			MemberExpression memberExpression = expression.Body as MemberExpression;
			if (memberExpression == null)
			{
				throw new ArgumentException("expression must be in the form: () => instance.Property");
			}
			var names = GetNames(memberExpression);
			return names.Last();
		}

		[DebuggerStepThrough]
		public static string GetMethodName<T, TReturn>(Expression<Func<T, TReturn>> expression)
		{
			var methodCallExpression = expression.Body as MethodCallExpression;
			if (methodCallExpression == null)
			{
				throw new ArgumentException("expression must be in the form: (Foo instance) => instance.Method");
			}
			return methodCallExpression.Method.Name;
		}

		public static string GetMultiLevelPropertyName(params string[] propertyNames)
		{
			return propertyNames.Join(".");
		}

		private static List<string> GetNames(MemberExpression memberExpression)
		{
			var names = new List<string>
				{
					memberExpression.Member.Name
				};
			while (memberExpression.Expression as MemberExpression != null)
			{
				memberExpression = (MemberExpression)memberExpression.Expression;
				names.Insert(0, memberExpression.Member.Name);
			}
			return names;
		}

		public static List<string> GetArguments<T, TReturn>(Expression<Func<T, TReturn>> expression)
		{
			var methodCallExpression = expression.Body as MethodCallExpression;
			if (methodCallExpression == null)
			{
				throw new ArgumentException("expression must be in the form: (Foo instance) => instance.Method");
			}
			var arguments = methodCallExpression.Arguments;
            return arguments.Select(p => Expression.Lambda(p).Compile().DynamicInvoke().ToString()).ToList();
		}

		//[DebuggerStepThrough]
		public static string GetPropertyName<T, TReturn>(Expression<Func<T, TReturn>> expression)
		{
			MemberExpression memberExpression = expression.Body as MemberExpression;
			if (memberExpression == null)
			{
				var unaryExpression = expression.Body as UnaryExpression;
				if (unaryExpression == null)
				{
					throw new ArgumentException(
						"expression must be in the form: (Thing instance) => instance.Property[.Optional.Other.Properties.In.Chain]");
				}
				memberExpression = unaryExpression.Operand as MemberExpression;
				if (memberExpression == null)
				{
					throw new ArgumentException(
						"expression must be in the form: (Thing instance) => instance.Property[.Optional.Other.Properties.In.Chain]");
				}
			}
			var names = GetNames(memberExpression);
			string name = names.Join(".");
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
			var names = GetNames(memberExpression);
			string name = names.Count > 1 ? names.Skip(1).Join(".") : names.Join(".");
			return name;
		}

		public static string GetControllerName<TControllerType>()
		{
			string name = typeof(TControllerType).Name;
			const string controller = "Controller";
			if (name.EndsWith(controller))
			{
				name = name.Substring(0, name.Length - controller.Length);
			}
			return name;
		}
	}
}