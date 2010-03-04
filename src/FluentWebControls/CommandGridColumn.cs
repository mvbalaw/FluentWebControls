using System;
using System.Linq.Expressions;

using MvbaCore;

namespace FluentWebControls
{
	public static class CommandGridColumn
	{
		public static CommandColumn<T> For<T, TController>(Expression<Func<T, string>> getValueAndPropertyName, Expression<Func<TController, object>> targetControllerAction) where T : class
		{
			return new CommandColumn<T>(getValueAndPropertyName.Compile(), Reflection.GetPropertyName(getValueAndPropertyName).ToCamelCase(), Reflection.GetMethodName(targetControllerAction));
		}

		public static CommandColumn<T> For<T, TController>(Func<T, string> getValue, Expression<Func<T, object>> forQueryStringId, Expression<Func<TController, object>> targetControllerAction) where T : class
		{
			return new CommandColumn<T>(getValue, Reflection.GetPropertyName(forQueryStringId).ToCamelCase(), Reflection.GetMethodName(targetControllerAction));
		}
	}
}