using System;
using System.Linq.Expressions;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public class RegularGridColumn
	{
		public static RegularColumn<T> For<T>(Expression<Func<T, int>> columnName)
		{
			string name = NameUtility.GetPropertyName(columnName);
			return For(columnName, name);
		}

		public static RegularColumn<T> For<T>(Expression<Func<T, int?>> columnName)
		{
			string name = NameUtility.GetPropertyName(columnName);
			return For(columnName, name);
		}

		public static RegularColumn<T> For<T>(Expression<Func<T, bool>> columnName)
		{
			string name = NameUtility.GetPropertyName(columnName);
			return For(columnName, name);
		}

		public static RegularColumn<T> For<T>(Expression<Func<T, string>> columnName)
		{
			string name = NameUtility.GetPropertyName(columnName);
			return For(columnName, name);
		}

		public static RegularColumn<T> For<T>(Expression<Func<T, int?>> columnName, string columnHeader)
		{
			return new RegularColumn<T>(t =>
			                            	{
			                            		int? i = columnName.Compile()(t);
			                            		return i == null ? "" : i.ToString();
			                            	}, NameUtility.GetPropertyName(columnName), columnHeader);
		}

		public static RegularColumn<T> For<T>(Expression<Func<T, int>> columnName, string columnHeader)
		{
			return new RegularColumn<T>(t => columnName.Compile()(t).ToString(), NameUtility.GetPropertyName(columnName), columnHeader);
		}

		public static RegularColumn<T> For<T>(Expression<Func<T, bool>> columnName, string columnHeader)
		{
			return new RegularColumn<T>(t => columnName.Compile()(t).ToString(), NameUtility.GetPropertyName(columnName), columnHeader);
		}

		public static RegularColumn<T> For<T>(Expression<Func<T, string>> columnName, string columnHeader)
		{
			return new RegularColumn<T>(columnName.Compile(), NameUtility.GetPropertyName(columnName), columnHeader);
		}

		public static RegularColumn<T> For<T, TReturn>(Expression<Func<T, TReturn>> columnName, string columnHeader, Func<T, string> columnValue)
		{
			return new RegularColumn<T>(columnValue, NameUtility.GetPropertyName(columnName), columnHeader);
		}
	}
}