using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;
using FluentWebControls.Tools;

namespace FluentWebControls
{
	public static class Fluent
	{
		public static ButtonData ButtonFor(IButtonType buttonType, object aspxPage)
		{
			return Button.For(buttonType, new ControllerInfo(aspxPage).Name);
		}

		public static ButtonData ButtonFor<TController>(IButtonType buttonType, Expression<Func<TController, object>> forControllerAndActionNames)
		{
			return Button.For(buttonType, NameUtility.GetControllerName<TController>()).WithAction(NameUtility.GetMethodName(forControllerAndActionNames));
		}

		public static ButtonData ButtonFor(IButtonType buttonType, string controllerName, string actionName)
		{
			return Button.For(buttonType, controllerName).WithAction(actionName);
		}

		[Obsolete("use Fluent.CheckBoxFor<T,TModel>(T source, x=>x.IsChecked, x=>x.Value.ToString(), y=>y.Name)")]
		public static CheckBoxData CheckBoxFor(Expression<Func<bool>> id)
		{
			return CheckBox.For(id);
		}

		public static CheckBoxData CheckBoxFor<T>(T source, bool @checked, Expression<Func<T, object>> forId)
		{
			return CheckBox.For(source, @checked, forId);
		}

		public static CheckBoxData CheckBoxFor<T>(T source, Expression<Func<T, bool>> forIdAndChecked)
		{
			return CheckBox.For(source, forIdAndChecked);
		}

		public static CheckBoxData CheckBoxFor<TSource, TModel>(TSource source, bool @checked, Func<TSource, string> forValue, Expression<Func<TModel, object>> forId)
		{
			return CheckBox.For(source, @checked, forValue, forId);
		}

		public static ComboSelectData ComboSelectFor<TListItemType, TContainerType, TPropertyType>(IEnumerable<TListItemType> itemSource, Func<TListItemType, string> getListItemDisplayText, Func<TListItemType, string> getListItemValue, Expression<Func<TContainerType, TPropertyType>> forId)
		{
			return ComboSelect.For(itemSource, getListItemDisplayText, getListItemValue, forId);
		}

		public static ComboSelectData ComboSelectFor<TListItemType, TPropertyType>(IEnumerable<TListItemType> itemSource, Func<TListItemType, string> getListItemDisplayText, Func<TListItemType, string> getListItemValue, Expression<Func<TPropertyType>> forId)
		{
			return ComboSelect.For(itemSource, getListItemDisplayText, getListItemValue, forId);
		}

		[Obsolete("use Fluent.ComboSelectFor(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId)")]
		public static ComboSelectData ComboSelectFor<T>(string name, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue)
			where T : class
		{
			return ComboSelect.For(name, items, getKey, getValue);
		}

		[Obsolete("use Fluent.ComboSelectFor(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithIdPrefix(x=>x.Prefix).WithValidationFrom(x=>x.StorageId)")]
		public static ComboSelectData ComboSelectFor<T, TParent>(Expression<Func<TParent>> propertyParentForMetaData, Expression<Func<TParent, int>> propertyChildForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue)
			where T : class
		{
			return ComboSelect.For(propertyParentForMetaData, propertyChildForMetaData, items, getKey, getValue);
		}

		public static DropDownListData DropDownListFor<TListItemType, TContainerType, TPropertyType>(IEnumerable<TListItemType> listItemSource, Func<TListItemType, string> getListItemDisplayText, Func<TListItemType, string> getListItemValue, Expression<Func<TContainerType, TPropertyType>> forId)
		{
			return DropDownList.For(listItemSource, getListItemDisplayText, getListItemValue, forId);
		}

		public static DropDownListData DropDownListFor<TListItemType, TPropertyType>(IEnumerable<TListItemType> listItemSource, Func<TListItemType, string> getListItemDisplayText, Func<TListItemType, string> getListItemValue, Expression<Func<TPropertyType>> forId)
		{
			return DropDownList.For(listItemSource, getListItemDisplayText, getListItemValue, forId);
		}

		[Obsolete("use Fluent.DropDownListFor(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData DropDownListFor<T, TParent>(Expression<Func<TParent, T>> propertyChildForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			return DropDownList.For(items, getKey, getValue, propertyChildForMetaData).WithValidationFrom(propertyChildForMetaData);
		}

		[Obsolete("use Fluent.DropDownListFor(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData DropDownListFor<T, TParent>(Expression<Func<TParent, T>> propertyChildForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue)
		{
			return DropDownList.For(propertyChildForMetaData, items, getKey, getValue);
		}

		[Obsolete("use Fluent.DropDownListFor(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData DropDownListFor<T, TParent>(Expression<Func<TParent>> propertyParentForMetaData, Expression<Func<TParent, string>> propertyChildForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			return DropDownList.For(propertyParentForMetaData, propertyChildForMetaData, items, getKey, getValue);
		}

		[Obsolete("use Fluent.DropDownListFor(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData DropDownListFor<T, TParent>(Expression<Func<TParent>> propertyParentForMetaData, Expression<Func<TParent, int>> propertyChildForMetaData, IEnumerable<T> items, Func<T, int> getKey, Func<T, int> getValue)
		{
			return DropDownList.For(propertyParentForMetaData, propertyChildForMetaData, items, getKey, getValue);
		}

		[Obsolete("use Fluent.DropDownListFor(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData DropDownListFor<T, TParent>(Expression<Func<TParent>> propertyParentForMetaData, Expression<Func<TParent, int>> propertyChildForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue)
		{
			return DropDownList.For(propertyParentForMetaData, propertyChildForMetaData, items, getKey, getValue);
		}

		[Obsolete("use Fluent.DropDownListFor(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData DropDownListFor<T, K>(Expression<Func<T, K>> propertyForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			return DropDownList.For(items, getKey, getValue, propertyForMetaData).WithValidationFrom(propertyForMetaData);
		}

		[Obsolete("use Fluent.DropDownListFor(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData DropDownListFor<TParent, T, K>(Expression<Func<TParent, K>> propertyForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			return DropDownList.For(items, getKey, getValue, propertyForMetaData).WithValidationFrom(propertyForMetaData);
		}

		[Obsolete("use Fluent.DropDownListFor(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData DropDownListFor<T>(Expression<Func<string>> propertyForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			return DropDownList.For(propertyForMetaData, items, getKey, getValue);
		}

		[Obsolete("use Fluent.DropDownListFor(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData DropDownListFor<T>(string name, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue)
			where T : class
		{
			return DropDownList.For(name, items, getKey, getValue);
		}

		[Obsolete("use Fluent.DropDownListFor(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData DropDownListFor<T>(string name, IEnumerable<T> items, Func<T, int> getKey, Func<T, string> getValue)
			where T : class
		{
			return DropDownList.For(name, items, getKey, getValue);
		}

		[Obsolete("use Fluent.DropDownListFor(source, x=>x.DisplayText, x=>x.Value.ToString(), x=>x.StorageId).WithValidationFrom(x=>x.Storage)")]
		public static DropDownListData DropDownListFor<T>(string name, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
			where T : class
		{
			return DropDownList.For(name, items, getKey, getValue);
		}

		[Obsolete("use Fluent.HiddenFor(T source, x=>x.Value)")]
		public static HiddenData HiddenFor(Expression<Func<string>> id)
		{
			return Hidden.For(id);
		}

		public static HiddenData HiddenFor<T, K>(Expression<Func<T, K>> id)
		{
			return Hidden.For(id);
		}

		[Obsolete("use Fluent.HiddenFor(T source, x=>x.Value.ToString(), x=>x.Value)")]
		public static HiddenData HiddenFor<T>(Expression<Func<T>> id) where T : struct
		{
			return Hidden.For(id);
		}

		public static HiddenData HiddenFor<T>(T source, Expression<Func<T, string>> forValueAndId) where T : class
		{
			return Hidden.For(source, forValueAndId);
		}

		public static HiddenData HiddenFor<T, K>(T source, Func<T, string> getValue, Expression<Func<T, K>> forValueAndId)
		{
			return Hidden.For(source, getValue, forValueAndId);
		}

		[Obsolete("use Fluent.HiddenFor(T source, x=>x.Value==null?\"\":x.Value.ToString(), x=>x.Value)")]
		public static HiddenData HiddenFor<T>(Expression<Func<T?>> id) where T : struct
		{
			return Hidden.For(id);
		}

		public static LabelData LabelFor(Expression<Func<string>> id)
		{
			return Label.For(id);
		}

		public static LabelData LabelFor<T>(Expression<Func<T>> id) where T : struct
		{
			return Label.For(id);
		}

		public static LabelData LabelFor<T>(Expression<Func<T?>> id) where T : struct
		{
			return Label.For(id);
		}

		public static LabelData LabelFor(string id)
		{
			return Label.For(id);
		}

		public static LabelData LabelForIt()
		{
			return Label.ForIt();
		}

		public static LinkData LinkTo()
		{
			return Link.To();
		}

		public static LinkData LinkTo(string controllerName, string controllerExtension, string actionName)
		{
			return Link.To(controllerName, controllerExtension, actionName);
		}

		public static LinkData LinkTo(string controllerName, string actionName)
		{
			return LinkTo(controllerName, "", actionName);
		}

		public static LinkData LinkTo<TControllerType>(Expression<Func<TControllerType, object>> targetControllerAction)
		{
			return Link.To(targetControllerAction);
		}

		public static PagedGridData<TReturn> PagedGridFor<TReturn>(IPagedList<TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return PagedGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action);
		}

		public static PagedGridData<TReturn> PagedGridFor<TReturn>(IPagedList<string, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, string filter)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return PagedGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter.EmptyToNull(false));
		}

		public static PagedGridData<TReturn> PagedGridFor<TReturn>(IPagedList<int?, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, int? filter)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return PagedGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter);
		}

		public static PagedGridData<TReturn> PagedGridFor<TReturn>(IPagedList<int?, int?, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, int? filter1, int? filter2)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return PagedGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter1, filter2);
		}

		public static PagedGridData<TReturn> PagedGridFor<TReturn>(IPagedList<int?, int?, int?, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, int? filter1, int? filter2, int? filter3)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return PagedGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter1, filter2, filter3);
		}

		[Obsolete("Use Fluent.ScrollableGridFor(x=>listData,(Controller c)=>c.ListAction()")]
		public static ScrollableGridData<TReturn> ScrollableGridFor<TReturn>(IEnumerable<TReturn> list, object aspxPage)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return ScrollableGrid.For(list, new PagedListParameters(), controllerInfo.Name, controllerInfo.Action);
		}

		public static ScrollableGridData<TItemType> ScrollableGridFor<TItemType, TControllerType>(IEnumerable<TItemType> list, Expression<Func<TControllerType, object>> listAction)
		{
			string name = NameUtility.GetControllerName<TControllerType>();
			return ScrollableGrid.For(list, new PagedListParameters(), name, NameUtility.GetMethodName(listAction));
		}

		public static ScrollableGridData<TReturn> ScrollableGridFor<TReturn>(IEnumerable<TReturn> list, IPagedListParameters pagedListParameters, object aspxPage)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return ScrollableGrid.For(list, pagedListParameters, controllerInfo.Name, controllerInfo.Action);
		}

		public static ScrollableGridData<TReturn> ScrollableGridFor<TReturn>(IPagedList<TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return ScrollableGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action);
		}

		public static ScrollableGridData<TReturn> ScrollableGridFor<TReturn>(IPagedList<string, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, string filter)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return ScrollableGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter.EmptyToNull(false));
		}

		public static ScrollableGridData<TReturn> ScrollableGridFor<TReturn>(IPagedList<int?, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, int? filter)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return ScrollableGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter);
		}

		public static ScrollableGridData<TReturn> ScrollableGridFor<TReturn>(IPagedList<int?, int?, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, int? filter1, int? filter2)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return ScrollableGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter1, filter2);
		}

		public static ScrollableGridData<TReturn> ScrollableGridFor<TReturn>(IPagedList<int?, int?, int?, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, int? filter1, int? filter2, int? filter3)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return ScrollableGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter1, filter2, filter3);
		}

		[Obsolete("use Fluent.TextAreaFor(T source, x=>x.Value).WithValidationFrom(x=>x.Value)")]
		public static TextAreaData TextAreaFor(Expression<Func<string>> getValueAndValidationMetadata)
		{
			return TextArea.For(getValueAndValidationMetadata);
		}

		[Obsolete("use Fluent.TextAreaFor(T source, x=>x.Value, x=>x.Value).WithValidationFrom(x=>x.Value)")]
		public static TextAreaData TextAreaFor<T>(T source, Expression<Func<T, string>> getValueAndValidationMetadata)
		{
			return TextArea.For(source, getValueAndValidationMetadata);
		}

		public static TextAreaData TextAreaFor<T, K>(T source, Func<T, string> getValue, Expression<Func<T, K>> forId)
		{
			return TextArea.For(source, getValue, forId);
		}

		public static TextBoxData TextBoxFor<T>(Expression<Func<T>> nullableObject, Expression<Func<T, string>> getValue) where T : class
		{
			var nullable = nullableObject.Compile();
			T parent = nullable();

			return TextBox.For(parent, getValue);
		}

		public static TextBoxData TextBoxFor<T, K>(T source, Func<T, string> getValue, Expression<Func<T, K>> forId) where T : class
		{
			return TextBox.For(source, getValue, forId);
		}

		public static TextBoxData TextBoxFor<T>(T source, Expression<Func<T, string>> forValueAndId) where T : class
		{
			return TextBox.For(source, forValueAndId);
		}

		[Obsolete("use Fluent.TextBoxFor(T source, x=>x.Value).WithValidationFrom(x=>x.Value)")]
		public static TextBoxData TextBoxFor(Expression<Func<string>> getValueAndValidationMetadata)
		{
			return TextBox.For(getValueAndValidationMetadata);
		}

		[Obsolete("use Fluent.TextBoxFor(T source, x=>x.Value.ToString(), x=>x.Value).WithValidationFrom(x=>x.Value)")]
		public static TextBoxData TextBoxFor<T>(Expression<Func<T>> getValueAndValidationMetadata) where T : struct
		{
			return TextBox.For(getValueAndValidationMetadata);
		}

		[Obsolete("use Fluent.TextBoxFor(T source, x=>x.Value==null?\"\":x.Value.ToString(), x=>x.Value).WithValidationFrom(x=>x.Value)")]
		public static TextBoxData TextBoxFor<T>(Expression<Func<T?>> getValueAndValidationMetadata) where T : struct
		{
			return TextBox.For(getValueAndValidationMetadata);
		}
	}
}