using System;
using System.Linq.Expressions;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class SortableGridColumn
	{
		public static SortableColumn<T> For<T, TReturn>(Expression<Func<T, TReturn>> columnName) where TReturn : struct
		{
			string name = NameUtility.GetPropertyName(columnName);
			return For(columnName, name);
		}

		public static SortableColumn<T> For<T, TReturn>(Expression<Func<T, TReturn?>> columnName) where TReturn : struct
		{
			string name = NameUtility.GetPropertyName(columnName);
			return For(columnName, name);
		}

		public static SortableColumn<T> For<T>(Expression<Func<T, string>> columnName)
		{
			string name = NameUtility.GetPropertyName(columnName);
			return For(columnName, name);
		}

		public static SortableColumn<T> For<T>(Expression<Func<T, int?>> columnName, string columnHeader)
		{
			return new SortableColumn<T>(t =>
			                             	{
			                             		int? i = columnName.Compile()(t);
			                             		return i == null ? "" : i.ToString();
			                             	}, NameUtility.GetPropertyName(columnName), columnHeader);
		}

		public static SortableColumn<T> For<T, TReturn>(Expression<Func<T, TReturn>> columnName, string columnHeader) where TReturn : struct
		{
			return new SortableColumn<T>(t => columnName.Compile()(t).ToString(), NameUtility.GetPropertyName(columnName), columnHeader);
		}

		public static SortableColumn<T> For<T, TReturn>(Expression<Func<T, TReturn?>> columnName, string columnHeader) where TReturn : struct
		{
			return new SortableColumn<T>(t => columnName.Compile()(t).ToString(), NameUtility.GetPropertyName(columnName), columnHeader);
		}

		public static SortableColumn<T> For<T>(Expression<Func<T, string>> columnName, string columnHeader)
		{
			return new SortableColumn<T>(columnName.Compile(), NameUtility.GetPropertyName(columnName), columnHeader);
		}

		public static SortableColumn<T> For<T, TReturn>(Expression<Func<T, TReturn>> columnNameForSorting, string columnHeader, Func<T, string> columnValue)
		{
			return new SortableColumn<T>(columnValue, NameUtility.GetPropertyName(columnNameForSorting), columnHeader);
		}
	}
}