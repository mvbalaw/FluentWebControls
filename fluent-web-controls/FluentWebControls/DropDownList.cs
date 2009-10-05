using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class DropDownList
	{
		public static DropDownListData For<T>(string name, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			return For(name, items, getKey, getValue, null);
		}

		public static DropDownListData For<T>(string name, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue)
		{
			return For(name, items, getKey, getValue, null);
		}

		public static DropDownListData For<T>(string name, IEnumerable<T> items, Func<T, int> getKey, Func<T, string> getValue)
		{
			return For(name, items, getKey, getValue, null);
		}

		public static DropDownListData For<T, TParent>(Expression<Func<TParent, T>> propertyChildForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue)
		{
			IPropertyMetaData childMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(propertyChildForMetaData);
			return For(childMetaData.Name.ToCamelCase(), items, getKey, getValue, childMetaData);
		}

		public static DropDownListData For<T, TParent>(Expression<Func<TParent, T>> propertyChildForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			IPropertyMetaData childMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(propertyChildForMetaData);
			return For(childMetaData.Name.ToCamelCase(), items, getKey, getValue, childMetaData);
		}

		public static DropDownListData For<T, TParent>(Expression<Func<TParent>> propertyParentForMetaData, Expression<Func<TParent, int>> propertyChildForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue)
		{
			IPropertyMetaData childMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(propertyChildForMetaData);
			IPropertyMetaData parentMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(propertyParentForMetaData);
			childMetaData.Combine(parentMetaData);
			return For(NameUtility.GetCamelCaseMultiLevelPropertyName(parentMetaData.Name, childMetaData.Name), items, getKey, getValue, childMetaData);
		}

		public static DropDownListData For<T, TParent>(Expression<Func<TParent>> propertyParentForMetaData, Expression<Func<TParent, string>> propertyChildForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			IPropertyMetaData childMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(propertyChildForMetaData);
			IPropertyMetaData parentMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(propertyParentForMetaData);
			childMetaData.Combine(parentMetaData);
			return For(NameUtility.GetCamelCaseMultiLevelPropertyName(parentMetaData.Name, childMetaData.Name), items, getKey, getValue, childMetaData);
		}

		public static DropDownListData For<T, TParent>(Expression<Func<TParent>> propertyParentForMetaData, Expression<Func<TParent, int>> propertyChildForMetaData, IEnumerable<T> items, Func<T, int> getKey, Func<T, int> getValue)
		{
			IPropertyMetaData childMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(propertyChildForMetaData);
			IPropertyMetaData parentMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(propertyParentForMetaData);
			childMetaData.Combine(parentMetaData);
			return For(NameUtility.GetCamelCaseMultiLevelPropertyName(parentMetaData.Name, childMetaData.Name), items, getKey, getValue, childMetaData);
		}

		public static DropDownListData For<T>(Expression<Func<T, string>> propertyForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			IPropertyMetaData metaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(propertyForMetaData);
			return For(metaData.Name.ToCamelCase(), items, getKey, getValue, metaData);
		}

		private static DropDownListData For<T>(string name, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue, IPropertyMetaData metaData)
		{
			List<KeyValuePair<string, string>> options = new List<KeyValuePair<string, string>>();
			items.Select(item => new KeyValuePair<string, string>(getKey(item), getValue(item))).ForEach(options.Add);
			DropDownListData dropDownListData = new DropDownListData(options, metaData)
				{
					Id = name
				};
			return dropDownListData;
		}

		private static DropDownListData For<T>(string name, IEnumerable<T> items, Func<T, int> getKey, Func<T, int> getValue, IPropertyMetaData metaData)
		{
			List<KeyValuePair<string, string>> options = new List<KeyValuePair<string, string>>();
			items.Select(item => new KeyValuePair<string, string>(getKey(item).ToString(), getValue(item).ToString())).ForEach(options.Add);
			DropDownListData dropDownListData = new DropDownListData(options, metaData)
				{
					Id = name
				};
			return dropDownListData;
		}

		private static DropDownListData For<T>(string name, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue, IPropertyMetaData metaData)
		{
			List<KeyValuePair<string, string>> options = new List<KeyValuePair<string, string>>();
			items.Select(item => new KeyValuePair<string, string>(getKey(item), getValue(item).ToString())).ForEach(options.Add);
			DropDownListData dropDownListData = new DropDownListData(options, metaData)
				{
					Id = name
				};
			return dropDownListData;
		}

		private static DropDownListData For<T>(string name, IEnumerable<T> items, Func<T, int> getKey, Func<T, string> getValue, IPropertyMetaData metaData)
		{
			List<KeyValuePair<string, string>> options = new List<KeyValuePair<string, string>>();
			items.Select(item => new KeyValuePair<string, string>(getKey(item).ToString(), getValue(item))).ForEach(options.Add);
			DropDownListData dropDownListData = new DropDownListData(options, metaData)
				{
					Id = name
				};
			return dropDownListData;
		}
	}
}