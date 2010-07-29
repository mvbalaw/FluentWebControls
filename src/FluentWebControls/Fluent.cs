using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.UI.WebControls;

using FluentWebControls.Extensions;
using FluentWebControls.Interfaces;

using MvbaCore;

namespace FluentWebControls
{
	public static class Fluent
	{
		public static ButtonData ButtonFor(IButtonType buttonType, object aspxPage)
		{
			return Button.For(buttonType, new ControllerInfo(aspxPage).Name);
		}

		public static ButtonData ButtonFor<TController>(IButtonType buttonType, Expression<Func<TController, object>> forControllerAndActionName) where TController : class
		{
			return Button.For(buttonType, forControllerAndActionName);
		}

		public static ButtonData ButtonFor(IButtonType buttonType, string controllerName, string actionName)
		{
			return Button.For(buttonType, controllerName).WithAction(actionName);
		}

		public static CommandColumn<TDomain> CheckBoxCommandColumnFor<TModel, TDomain, TValueHolder>(Expression<Func<TValueHolder, object>> forCheckBoxId, Func<TDomain, string> getCheckBoxValue)
		{
			string checkBoxId = Reflection.GetCamelCasePropertyName(forCheckBoxId);
			return CommandColumn.For((TDomain item, string text) =>
			                         	{
			                         		var checkBox = new System.Web.UI.WebControls.CheckBox
			                         			{
			                         				ID = checkBoxId
			                         			};
			                         		checkBox.InputAttributes["Value"] = getCheckBoxValue(item);
			                         		return checkBox;
			                         	});
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

		[Obsolete("Use LinkCommandColumnFor")]
		public static CommandColumn<T> CommandColumnFor<T>(Func<T, string> getHref)
		{
			return LinkCommandColumnFor(getHref);
		}

		public static DataColumn<T> DataColumnFor<T>(Func<T, string> getItemText, string columnName)
		{
			return DataColumn.For(getItemText, columnName);
		}

		public static DataItem<T> DataItemFor<T>(Func<T, string> getItemText, string columnName)
		{
			return DataItem.For(getItemText, columnName);
		}

		public static DropDownListData DropDownListFor<TListItemType, TContainerType, TPropertyType>(IEnumerable<TListItemType> listItemSource, Func<TListItemType, string> getListItemDisplayText, Func<TListItemType, string> getListItemValue, Expression<Func<TContainerType, TPropertyType>> forId)
		{
			return DropDownList.For(listItemSource, getListItemDisplayText, getListItemValue, forId);
		}

		public static DropDownListData DropDownListFor<TListItemType, TPropertyType>(IEnumerable<TListItemType> listItemSource, Func<TListItemType, string> getListItemDisplayText, Func<TListItemType, string> getListItemValue, Expression<Func<TPropertyType>> forId)
		{
			return DropDownList.For(listItemSource, getListItemDisplayText, getListItemValue, forId);
		}

		public static HiddenData HiddenFor<T, K>(Expression<Func<T, K>> id)
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

		public static CommandColumn<T> LinkCommandColumnFor<T>(Func<T, string> getHref)
		{
			return CommandColumn.For((T item, string text) =>
			                         	{
			                         		string navigateUrl = getHref(item);
			                         		string linkId = navigateUrl.Replace('/', '_').TrimStart(new[] { '_' });
			                         		return new HyperLink
			                         			{
			                         				NavigateUrl = navigateUrl,
			                         				Text = text,
			                         				ID = linkId
			                         			};
			                         	});
		}
		
		public static CommandItem<T> LinkCommandItemFor<T>(Func<T, string> getHref)
		{
			return CommandItem.For(getHref);
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

		public static LinkData LinkTo<TControllerType>(Expression<Func<TControllerType, object>> targetControllerAction) where TControllerType : class
		{
			return Link.To(targetControllerAction);
		}

		public static PagedGridData<TReturn> PagedGridFor<TReturn>(IPagedList<TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage)
		{
			var controllerInfo = new ControllerInfo(aspxPage);
			return PagedGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action);
		}

		public static PagedGridData<TReturn> PagedGridFor<TReturn>(IPagedList<string, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, string filter)
		{
			var controllerInfo = new ControllerInfo(aspxPage);
			return PagedGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter.EmptyToNull(false));
		}

		public static PagedGridData<TReturn> PagedGridFor<TReturn>(IPagedList<int?, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, int? filter)
		{
			var controllerInfo = new ControllerInfo(aspxPage);
			return PagedGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter);
		}

		public static PagedGridData<TReturn> PagedGridFor<TReturn>(IPagedList<int?, int?, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, int? filter1, int? filter2)
		{
			var controllerInfo = new ControllerInfo(aspxPage);
			return PagedGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter1, filter2);
		}

		public static PagedGridData<TReturn> PagedGridFor<TReturn>(IPagedList<int?, int?, int?, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, int? filter1, int? filter2, int? filter3)
		{
			var controllerInfo = new ControllerInfo(aspxPage);
			return PagedGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter1, filter2, filter3);
		}

		public static ScrollableGridData<TItemType> ScrollableGridFor<TItemType, TControllerType>(IEnumerable<TItemType> list, Expression<Func<TControllerType, object>> listAction)
		{
			string name = Reflection.GetControllerName<TControllerType>();
			return ScrollableGrid.For(list, new PagedListParameters(), name, Reflection.GetMethodName(listAction));
		}

		public static ScrollableGridData<TReturn> ScrollableGridFor<TReturn>(IEnumerable<TReturn> list, IPagedListParameters pagedListParameters, object aspxPage)
		{
			var controllerInfo = new ControllerInfo(aspxPage);
			return ScrollableGrid.For(list, pagedListParameters, controllerInfo.Name, controllerInfo.Action);
		}

		public static ScrollableGridData<TReturn> ScrollableGridFor<TReturn>(IPagedList<TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage)
		{
			var controllerInfo = new ControllerInfo(aspxPage);
			return ScrollableGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action);
		}

		public static ScrollableGridData<TReturn> ScrollableGridFor<TReturn>(IPagedList<string, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, string filter)
		{
			var controllerInfo = new ControllerInfo(aspxPage);
			return ScrollableGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter.EmptyToNull(false));
		}

		public static ScrollableGridData<TReturn> ScrollableGridFor<TReturn>(IPagedList<int?, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, int? filter)
		{
			var controllerInfo = new ControllerInfo(aspxPage);
			return ScrollableGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter);
		}

		public static ScrollableGridData<TReturn> ScrollableGridFor<TReturn>(IPagedList<int?, int?, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, int? filter1, int? filter2)
		{
			var controllerInfo = new ControllerInfo(aspxPage);
			return ScrollableGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter1, filter2);
		}

		public static ScrollableGridData<TReturn> ScrollableGridFor<TReturn>(IPagedList<int?, int?, int?, TReturn> pagedList, IPagedListParameters pagedListParameters, object aspxPage, int? filter1, int? filter2, int? filter3)
		{
			var controllerInfo = new ControllerInfo(aspxPage);
			return ScrollableGrid.For(pagedList, pagedListParameters, controllerInfo.Name, controllerInfo.Action, filter1, filter2, filter3);
		}

		public static TableData<TItemType> TableFor<TItemType>(IEnumerable<TItemType> list)
		{
			return Table.For(list);
		}

		public static ListData<TItemType> ListFor<TItemType>(IEnumerable<TItemType> list)
		{
			return HtmlList.For(list);
		}

		public static TextAreaData TextAreaFor<T, K>(T source, Func<T, string> getValue, Expression<Func<T, K>> forId)
		{
			return TextArea.For(source, getValue, forId);
		}

		public static TextBoxData TextBoxFor<T>(Expression<Func<T>> nullableObject, Expression<Func<T, string>> getValue) where T : class
		{
			var nullable = nullableObject.Compile();
			var parent = nullable();

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
	}
}