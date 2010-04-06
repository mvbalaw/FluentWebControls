using System;
using System.Linq.Expressions;

using MvbaCore;

namespace FluentWebControls
{
	public static class SortableGridColumn
	{
		public static SortableColumn<T> For<T, TReturn>(Expression<Func<T, TReturn>> columnName) where TReturn : struct
		{
			string name = Reflection.GetPropertyName(columnName);
			return For(columnName, name);
		}

		public static SortableColumn<T> For<T, TReturn>(Expression<Func<T, TReturn?>> columnName) where TReturn : struct
		{
			string name = Reflection.GetPropertyName(columnName);
			return For(columnName, name);
		}

		public static SortableColumn<T> For<T>(Expression<Func<T, string>> columnName)
		{
			string name = Reflection.GetPropertyName(columnName);
			return For(columnName, name);
		}

		public static SortableColumn<T> For<T>(Expression<Func<T, int?>> columnName, string columnHeader)
		{
			return new SortableColumn<T>(t =>
			                             	{
			                             		var i = columnName.Compile()(t);
			                             		return i == null ? "" : i.ToString();
			                             	}, Reflection.GetPropertyName(columnName), columnHeader);
		}

		public static SortableColumn<T> For<T, TReturn>(Expression<Func<T, TReturn>> columnName, string columnHeader) where TReturn : struct
		{
			return new SortableColumn<T>(t => columnName.Compile()(t).ToString(), Reflection.GetPropertyName(columnName), columnHeader);
		}

		public static SortableColumn<T> For<T, TReturn>(Expression<Func<T, TReturn?>> columnName, string columnHeader) where TReturn : struct
		{
			return new SortableColumn<T>(t => columnName.Compile()(t).ToString(), Reflection.GetPropertyName(columnName), columnHeader);
		}

		public static SortableColumn<T> For<T>(Expression<Func<T, string>> columnName, string columnHeader)
		{
			return new SortableColumn<T>(columnName.Compile(), Reflection.GetPropertyName(columnName), columnHeader);
		}

		public static SortableColumn<T> For<T, TReturn>(Expression<Func<T, TReturn>> columnNameForSorting, string columnHeader, Func<T, string> columnValue)
		{
			return new SortableColumn<T>(columnValue, Reflection.GetPropertyName(columnNameForSorting), columnHeader);
		}
	}
}