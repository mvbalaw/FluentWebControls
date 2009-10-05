using System;
using System.Linq.Expressions;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class CommandGridColumn
	{
		public static CommandColumn<T> For<T>(Expression<Func<T, int>> getItemId, string action) where T : class
		{
			Func<T, int> func = getItemId.Compile();
			return new CommandColumn<T>(item => func(item).ToString(), NameUtility.GetPropertyName(getItemId), action);
		}

		public static CommandColumn<T> For<T>(Expression<Func<T, string>> getItemId, string action) where T : class
		{
			Func<T, string> func = getItemId.Compile();
			return new CommandColumn<T>(func, NameUtility.GetPropertyName(getItemId), action);
		}
	}
}