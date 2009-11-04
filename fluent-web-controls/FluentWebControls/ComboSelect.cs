using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class ComboSelect
	{
		public static ComboSelectData For<T>(string name, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			return For(name, items, getKey, getValue, null);
		}

		public static ComboSelectData For<T>(string name, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue)
		{
			return For(name, items, getKey, getValue, null);
		}

		private static ComboSelectData For<T>(string name, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue, IPropertyMetaData metaData)
		{
			List<KeyValuePair<string, string>> options = new List<KeyValuePair<string, string>>();
			items.Select(item => new KeyValuePair<string, string>(getKey(item), getValue(item))).ForEach(options.Add);
			ComboSelectData comboSelectData = new ComboSelectData(options, metaData)
				.WithId(name);
			return comboSelectData;
		}

		private static ComboSelectData For<T>(string name, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue, IPropertyMetaData metaData)
		{
			List<KeyValuePair<string, string>> options = new List<KeyValuePair<string, string>>();
			items.Select(item => new KeyValuePair<string, string>(getKey(item), getValue(item).ToString())).ForEach(options.Add);
			ComboSelectData comboSelectData = new ComboSelectData(options, metaData)
				.WithId(name);
			return comboSelectData;
		}

		public static ComboSelectData For<T, TParent>(Expression<Func<TParent>> propertyParentForMetaData, Expression<Func<TParent, int>> propertyChildForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue)
		{
			IPropertyMetaData childMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(propertyChildForMetaData);
			IPropertyMetaData parentMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(propertyParentForMetaData);
			childMetaData.Combine(parentMetaData);
			return For(NameUtility.GetCamelCaseMultiLevelPropertyName(parentMetaData.Name, childMetaData.Name), items, getKey, getValue, childMetaData);
		}
	}
}