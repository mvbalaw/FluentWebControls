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
		public static DropDownListData For<TListItemType, TContainerType, TPropertyType>(IEnumerable<TListItemType> listItemSource, Func<TListItemType, string> getListItemDisplayText, Func<TListItemType, string> getListItemValue, Expression<Func<TContainerType, TPropertyType>> forId)
		{
			var options = listItemSource
				.Select(item => new KeyValuePair<string, string>(getListItemDisplayText(item), getListItemValue(item)))
				.ToList();
			var dropDownListData = new DropDownListData(options)
				.WithId(forId);
			return dropDownListData;
		}

		public static DropDownListData For<TListItemType, TPropertyType>(IEnumerable<TListItemType> listItemSource, Func<TListItemType, string> getListItemDisplayText, Func<TListItemType, string> getListItemValue, Expression<Func<TPropertyType>> forId)
		{
			var options = listItemSource
				.Select(item => new KeyValuePair<string, string>(getListItemDisplayText(item), getListItemValue(item)))
				.ToList();
			var dropDownListData = new DropDownListData(options)
				.WithId(forId);
			return dropDownListData;
		}

		[Obsolete("use .For(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData For<T>(string name, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			return For(name, items, getKey, getValue, null);
		}

		[Obsolete("use .For(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData For<T>(string name, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue)
		{
			return For(name, items, getKey, getValue, null);
		}

		[Obsolete("use .For(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData For<T>(string name, IEnumerable<T> items, Func<T, int> getKey, Func<T, string> getValue)
		{
			return For(name, items, getKey, getValue, null);
		}

		[Obsolete("use .For(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData For<TParent, TListItemType, K>(Expression<Func<TParent, K>> forIdAndValidationMetadata, IEnumerable<TListItemType> items, Func<TListItemType, string> getKey, Func<TListItemType, string> getValue)
		{
			return For(items, getKey, getValue, forIdAndValidationMetadata)
				.WithValidationFrom(forIdAndValidationMetadata);
		}

		[Obsolete("use .For(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData For<T, TParent>(Expression<Func<TParent, T>> propertyChildForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue)
		{
			var childMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(propertyChildForMetaData);
			return For(NameUtility.GetPropertyName(propertyChildForMetaData).ToCamelCase(), items, getKey, getValue, childMetaData);
		}

		[Obsolete("use .For(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData For<T, TParent>(Expression<Func<TParent, T>> forIdAndValidationMetadata, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			return For(items, getKey, getValue, forIdAndValidationMetadata)
				.WithValidationFrom(forIdAndValidationMetadata);
		}

		[Obsolete("use .For(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithIdPrefix(x=>x.Prefix).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData For<T, TParent>(Expression<Func<TParent>> propertyParentForMetaData, Expression<Func<TParent, int>> propertyChildForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue)
		{
			var childMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(propertyChildForMetaData);
			var parentMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(propertyParentForMetaData);
			childMetaData.Combine(parentMetaData);
			return For(NameUtility.GetCamelCaseMultiLevelPropertyName(parentMetaData.Name, childMetaData.Name), items, getKey, getValue, childMetaData);
		}

		[Obsolete("use .For(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithIdPrefix(x=>x.Prefix).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData For<T, TParent>(Expression<Func<TParent>> propertyParentForMetaData, Expression<Func<TParent, string>> propertyChildForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			var childMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(propertyChildForMetaData);
			var parentMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(propertyParentForMetaData);
			childMetaData.Combine(parentMetaData);
			return For(NameUtility.GetCamelCaseMultiLevelPropertyName(parentMetaData.Name, childMetaData.Name), items, getKey, getValue, childMetaData);
		}

		[Obsolete("use .For(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithIdPrefix(x=>x.Prefix).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData For<T, TParent>(Expression<Func<TParent>> propertyParentForMetaData, Expression<Func<TParent, int>> propertyChildForMetaData, IEnumerable<T> items, Func<T, int> getKey, Func<T, int> getValue)
		{
			var childMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(propertyChildForMetaData);
			var parentMetaData = IoCUtility.GetInstance<IBusinessObjectPropertyMetaDataFactory>().GetFor(propertyParentForMetaData);
			childMetaData.Combine(parentMetaData);
			return For(NameUtility.GetCamelCaseMultiLevelPropertyName(parentMetaData.Name, childMetaData.Name), items, getKey, getValue, childMetaData);
		}

		[Obsolete("use .For(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData For<T, K>(Expression<Func<T, K>> forIdAndValidationMetadata, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			return For(items, getKey, getValue, forIdAndValidationMetadata)
				.WithValidationFrom(forIdAndValidationMetadata);
		}

		[Obsolete("use .For(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData For<T>(Expression<Func<string>> forIdAndValidationMetadata, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			return For(items, getKey, getValue, forIdAndValidationMetadata)
				.WithValidationFrom(forIdAndValidationMetadata);
		}

		private static DropDownListData For<T>(string name, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue, IPropertyMetaData metaData)
		{
			var options = items.Select(item => new KeyValuePair<string, string>(getKey(item), getValue(item))).ToList();
			var dropDownListData = new DropDownListData(options)
				.WithId(name)
				.WithValidationFrom(metaData);
			return dropDownListData;
		}

		private static DropDownListData For<T>(string name, IEnumerable<T> items, Func<T, int> getKey, Func<T, int> getValue, IPropertyMetaData metaData)
		{
			var options = items.Select(item => new KeyValuePair<string, string>(getKey(item).ToString(), getValue(item).ToString())).ToList();
			var dropDownListData = new DropDownListData(options)
				.WithId(name)
				.WithValidationFrom(metaData);
			return dropDownListData;
		}

		private static DropDownListData For<T>(string name, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue, IPropertyMetaData metaData)
		{
			var options = items.Select(item => new KeyValuePair<string, string>(getKey(item), getValue(item).ToString())).ToList();
			var dropDownListData = new DropDownListData(options)
				.WithId(name)
				.WithValidationFrom(metaData);
			return dropDownListData;
		}

		private static DropDownListData For<T>(string name, IEnumerable<T> items, Func<T, int> getKey, Func<T, string> getValue, IPropertyMetaData metaData)
		{
			var options = items.Select(item => new KeyValuePair<string, string>(getKey(item).ToString(), getValue(item))).ToList();
			var dropDownListData = new DropDownListData(options)
				.WithId(name)
				.WithValidationFrom(metaData);
			return dropDownListData;
		}
	}
}