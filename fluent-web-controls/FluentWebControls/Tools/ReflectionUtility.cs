using System;
using System.Linq;
using System.Linq.Expressions;

namespace FluentWebControls.Tools
{
	public static class ReflectionUtility
	{
		public static MethodCallData GetMethodCallData<TClass>(Expression<Func<TClass, object>> methodCall) where TClass : class
		{
			string className = typeof(TClass).Name;
			string methodName = NameUtility.GetMethodName(methodCall);

			MethodCallExpression expression = GetMethodCallExpression(methodCall);
			var parameters = expression.Method.GetParameters();
			var parameterDictionary = parameters.Select((x, i) => new
				{
					x.Name,
					Value = GetValueAsString(expression.Arguments[i])
				}
				).ToDictionary(x => x.Name, x => x.Value);

			return new MethodCallData
				{
					MethodName = methodName,
					ClassName = className,
					ParameterValues = parameterDictionary
				};
		}

		public static MethodCallExpression GetMethodCallExpression<T, TReturn>(Expression<Func<T, TReturn>> expression)
		{
			var methodCallExpression = expression.Body as MethodCallExpression;
			if (methodCallExpression == null)
			{
				var unaryExpression = expression.Body as UnaryExpression;
				if (unaryExpression == null)
				{
					throw new ArgumentException(
						"expression must be in the form: (Foo instance) => instance.Method()");
				}
				methodCallExpression = unaryExpression.Operand as MethodCallExpression;
				if (methodCallExpression == null)
				{
					throw new ArgumentException(
						"expression must be in the form: (Foo instance) => instance.Method()");
				}
			}
			return methodCallExpression;
		}

		/// <summary>
		///		http://stackoverflow.com/questions/340525/accessing-calling-object-from-methodcallexpression
		/// </summary>
		private static object GetValue(Expression expression)
		{
			var lambda = Expression.Lambda<Func<object>>(Expression.Convert(expression, typeof(object)));
			var func = lambda.Compile();
			return func.Invoke();
		}

		public static string GetValueAsString(Expression expression)
		{
			switch (expression.NodeType)
			{
				case ExpressionType.Call:
					return GetValueAsString((MethodCallExpression)expression);
				case ExpressionType.Constant:
					return GetValueAsString((ConstantExpression)expression);
				case ExpressionType.MemberAccess:
					return GetValueAsString((MemberExpression)expression);
				default:
					throw new NotImplementedException(expression.GetType() + " with nodeType " + expression.NodeType);
			}
		}

		private static string GetValueAsString(ConstantExpression expression)
		{
			return expression.Value.ToString();
		}

		private static string GetValueAsString(MemberExpression expression)
		{
			object result = GetValue(expression);
			return result.ToString();
		}

		private static string GetValueAsString(MethodCallExpression expression)
		{
			object result = GetValue(expression);
			return result.ToString();
		}
	}
}