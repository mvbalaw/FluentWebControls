using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;

namespace FluentWebControls
{
	public static class Fluent
	{
		public static ButtonData ButtonFor(ButtonData.ButtonType buttonType, object aspxPage)
		{
			return Button.For(buttonType, new ControllerInfo(aspxPage).Name);
		}

		public static ButtonData ButtonFor(ButtonData.ButtonType buttonType, string controllerName, string actionName)
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

		public static HiddenData HiddenFor(Expression<Func<string>> id)
		{
			return Hidden.For(id);
		}

		public static HiddenData HiddenFor<T>(Expression<Func<T>> id) where T : struct
		{
			return Hidden.For(id);
		}

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

		public static TextAreaData TextAreaFor(Expression<Func<string>> getValue)
		{
			return TextArea.For(getValue);
		}

		public static TextBoxData TextBoxFor<T>(Expression<Func<T>> nullableObject, Expression<Func<T, string>> getValue) where T : class
		{
			return TextBox.For(nullableObject, getValue);
		}

		public static TextBoxData TextBoxFor(Expression<Func<string>> getValue)
		{
			return TextBox.For(getValue);
		}

		public static TextBoxData TextBoxFor<T>(Expression<Func<T>> getValue) where T : struct
		{
			return TextBox.For(getValue);
		}

		public static TextBoxData TextBoxFor<T>(Expression<Func<T?>> getValue) where T : struct
		{
			return TextBox.For(getValue);
		}
	}
}