using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;

namespace FluentWebControls
{
	public static class Fluent
	{
		public static ButtonData ButtonFor(IButtonType buttonType, object aspxPage)
		{
			return Button.For(buttonType, new ControllerInfo(aspxPage).Name);
		}

		public static ButtonData ButtonFor(IButtonType buttonType, string controllerName, string actionName)
		{
			return Button.For(buttonType, controllerName).WithAction(actionName);
		}

		public static CheckBoxData CheckBoxFor(Expression<Func<bool>> id)
		{
			return CheckBox.For(id);
		}

		public static ComboSelectData ComboSelectFor<T>(string name, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue)
			where T : class
		{
			return ComboSelect.For(name, items, getKey, getValue);
		}

		public static ComboSelectData ComboSelectFor<T, TParent>(Expression<Func<TParent>> propertyParentForMetaData, Expression<Func<TParent, int>> propertyChildForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue)
			where T : class
		{
			return ComboSelect.For(propertyParentForMetaData, propertyChildForMetaData, items, getKey, getValue);
		}

		public static DropDownListData DropDownListFor<T, TParent>(Expression<Func<TParent, T>> propertyChildForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			return DropDownList.For(propertyChildForMetaData, items, getKey, getValue);
		}

		public static DropDownListData DropDownListFor<T, TParent>(Expression<Func<TParent, T>> propertyChildForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue)
		{
			return DropDownList.For(propertyChildForMetaData, items, getKey, getValue);
		}

		public static DropDownListData DropDownListFor<T, TParent>(Expression<Func<TParent>> propertyParentForMetaData, Expression<Func<TParent, string>> propertyChildForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			return DropDownList.For(propertyParentForMetaData, propertyChildForMetaData, items, getKey, getValue);
		}

		public static DropDownListData DropDownListFor<T, TParent>(Expression<Func<TParent>> propertyParentForMetaData, Expression<Func<TParent, int>> propertyChildForMetaData, IEnumerable<T> items, Func<T, int> getKey, Func<T, int> getValue)
		{
			return DropDownList.For(propertyParentForMetaData, propertyChildForMetaData, items, getKey, getValue);
		}

		public static DropDownListData DropDownListFor<T, TParent>(Expression<Func<TParent>> propertyParentForMetaData, Expression<Func<TParent, int>> propertyChildForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue)
		{
			return DropDownList.For(propertyParentForMetaData, propertyChildForMetaData, items, getKey, getValue);
		}

		public static DropDownListData DropDownListFor<T>(Expression<Func<T, string>> propertyForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			return DropDownList.For(propertyForMetaData, items, getKey, getValue);
		}

		public static DropDownListData DropDownListFor<T>(Expression<Func<string>> propertyForMetaData, IEnumerable<T> items, Func<T, string> getKey, Func<T, string> getValue)
		{
			return DropDownList.For(propertyForMetaData, items, getKey, getValue);
		}

		public static DropDownListData DropDownListFor<T>(string name, IEnumerable<T> items, Func<T, string> getKey, Func<T, int> getValue)
			where T : class
		{
			return DropDownList.For(name, items, getKey, getValue);
		}

		public static DropDownListData DropDownListFor<T>(string name, IEnumerable<T> items, Func<T, int> getKey, Func<T, string> getValue)
			where T : class
		{
			return DropDownList.For(name, items, getKey, getValue);
		}

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

		public static HiddenData HiddenFor<T,K>(Expression<Func<T,K>> id)
		{
			return Hidden.For(id);
		}

		[Obsolete("use Fluent.HiddenFor(T source, x=>x.Value.ToString(), x=>x.Value)")]
		public static HiddenData HiddenFor<T>(Expression<Func<T>> id) where T : struct
		{
			return Hidden.For(id);
		}

		public static HiddenData HiddenFor<T>(T source, Expression<Func<T, string>> getValueAndValidationMetadata) where T : class
		{
			return Hidden.For(source, getValueAndValidationMetadata);
		}

		public static HiddenData HiddenFor<T,K>(T source, Func<T, string> getValue, Expression<Func<T, K>> getNameAndValidationMetadata)
		{
			return Hidden.For(source, getValue, getNameAndValidationMetadata);
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

		public static LinkData LinkTo(string controllerName, string actionName)
		{
			return Link.To(controllerName, actionName);
		}

		public static LinkData LinkTo()
		{
			return Link.To();
		}

		public static GridData<TReturn> PagedGridFor<TReturn>(IPagedList<TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return PagedGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action);
		}

		public static GridData<TReturn> PagedGridFor<TReturn>(IPagedList<string, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, string filter)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return PagedGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter.EmptyToNull(false));
		}

		public static GridData<TReturn> PagedGridFor<TReturn>(IPagedList<int?, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, int? filter)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return PagedGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter);
		}

		public static GridData<TReturn> PagedGridFor<TReturn>(IPagedList<int?, int?, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, int? filter1, int? filter2)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return PagedGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter1, filter2);
		}

		public static GridData<TReturn> PagedGridFor<TReturn>(IPagedList<int?, int?, int?, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, int? filter1, int? filter2, int? filter3)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return PagedGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter1, filter2, filter3);
		}

		public static GridData<TReturn> ScrollableGridFor<TReturn>(IPagedList<TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return ScrollableGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action);
		}

		public static GridData<TReturn> ScrollableGridFor<TReturn>(IPagedList<string, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, string filter)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return ScrollableGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter.EmptyToNull(false));
		}

		public static GridData<TReturn> ScrollableGridFor<TReturn>(IPagedList<int?, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, int? filter)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return ScrollableGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter);
		}

		public static GridData<TReturn> ScrollableGridFor<TReturn>(IPagedList<int?, int?, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, int? filter1, int? filter2)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return ScrollableGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter1, filter2);
		}

		public static GridData<TReturn> ScrollableGridFor<TReturn>(IPagedList<int?, int?, int?, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, int? filter1, int? filter2, int? filter3)
		{
			ControllerInfo controllerInfo = new ControllerInfo(aspxPage);
			return ScrollableGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter1, filter2, filter3);
		}

		[Obsolete("use Fluent.TextAreaFor(T source, x=>x.Value)")]
		public static TextAreaData TextAreaFor(Expression<Func<string>> getValueAndValidationMetadata)
		{
			return TextArea.For(getValueAndValidationMetadata);
		}

		public static TextAreaData TextAreaFor<T>(T source, Expression<Func<T, string>> getValueAndValidationMetadata)
		{
			return TextArea.For(source, getValueAndValidationMetadata);
		}

		public static TextBoxData TextBoxFor<T>(Expression<Func<T>> nullableObject, Expression<Func<T, string>> getValue) where T : class
		{
			var nullable = nullableObject.Compile();
			T parent = nullable();

			return TextBox.For(parent, getValue);
		}

		public static TextBoxData TextBoxFor<T>(T source, Expression<Func<T, string>> getValueAndValidationMetadata) where T : class
		{
			return TextBox.For(source, getValueAndValidationMetadata);
		}

		[Obsolete("use Fluent.TextBoxFor(T source, x=>x.Value)")]
		public static TextBoxData TextBoxFor(Expression<Func<string>> getValueAndValidationMetadata)
		{
			return TextBox.For(getValueAndValidationMetadata);
		}

		[Obsolete("use Fluent.TextBoxFor(T source, x=>x.Value.ToString(), x=>x.Value)")]
		public static TextBoxData TextBoxFor<T>(Expression<Func<T>> getValueAndValidationMetadata) where T : struct
		{
			return TextBox.For(getValueAndValidationMetadata);
		}

		[Obsolete("use Fluent.TextBoxFor(T source, x=>x.Value==null?\"\":x.Value.ToString(), x=>x.Value)")]
		public static TextBoxData TextBoxFor<T>(Expression<Func<T?>> getValueAndValidationMetadata) where T : struct
		{
			return TextBox.For(getValueAndValidationMetadata);
		}
	}
}