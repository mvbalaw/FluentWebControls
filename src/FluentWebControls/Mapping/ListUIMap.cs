using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using MvbaCore;

namespace FluentWebControls.Mapping
{
	public class ListUIMap<TDomain, TModel> : IListUIMap
	{
		private readonly IDictionary<string, object> _columns;
		private readonly dynamic _container = new ExpandoObject();

		public ListUIMap(IEnumerable<TDomain> items)
		{
			ListItems = items;
			_columns = (IDictionary<string, object>)_container;
			foreach (var matchingProperty in Reflection
				.GetMatchingProperties(typeof (TDomain), typeof (TModel)))
			{
				_columns.Add(matchingProperty.Name, GetMap(matchingProperty));
			}
		}

		public string IdPrefix { get; set; }
		public IEnumerable<TDomain> ListItems { get; private set; }

		public TableData<TDomain> AsTable()
		{
			return Fluent.TableFor(ListItems);
		}

		public ListData<TDomain> AsList()
		{
			return Fluent.ListFor(ListItems);
		}

		public CommandColumn<TDomain> CheckBoxCommandColumnFor<TValueHolder>(
			Expression<Func<TValueHolder, object>> forCheckBoxId, Func<TDomain, string> getCheckBoxValue)
		{
			return Fluent.CheckBoxCommandColumnFor<TModel, TDomain, TValueHolder>(forCheckBoxId, getCheckBoxValue);
		}

		[Obsolete("Use LinkCommandColumnFor")]
		public CommandColumn<TDomain> CommandColumnFor(Func<TDomain, string> getControllerActionHrefForSpecificItem)
		{
			return LinkCommandColumnFor(getControllerActionHrefForSpecificItem);
		}

		protected ListUIMap<TDomain, TModel> ConfigureColumn(Expression<Func<TModel, object>> forId,
		                                                     Func<TDomain, string> getText)
		{
			string id = Reflection.GetPropertyName(forId);
			var column = new UIColumn<TDomain>(getText);
			_columns[id] = column;
			return this;
		}

		public DataColumn<TDomain> DataColumnFor(Expression<Func<TModel, object>> forPropertyName)
		{
			var uiMap = TryGetRequestedMap(forPropertyName);
			var column = uiMap.TryCastTo<UIColumn<TDomain>>();
			return Fluent.DataColumnFor(column.TextMethod, Reflection.GetPropertyName(forPropertyName));
		}

		public DataItem<TDomain> DataItemFor(Expression<Func<TModel, object>> forPropertyName)
		{
			var uiMap = TryGetRequestedMap(forPropertyName);
			var column = uiMap.TryCastTo<UIColumn<TDomain>>();
			return Fluent.DataItemFor(column.TextMethod, Reflection.GetPropertyName(forPropertyName));
		}

		public Func<TDomain, string> ForId(Expression<Func<TModel, object>> forId)
		{
			var uiMap = TryGetRequestedMap(forId);
			var column = uiMap.TryCastTo<UIColumn<TDomain>>();
			return column.TextMethod;
		}

		private static UIColumn<TDomain> GetMap(PropertyMappingInfo propertyMappingInfo)
		{
			return new UIColumn<TDomain>(y =>
			                             	{
			                             		var source = propertyMappingInfo.GetValueFromSource(y);
			                             		return source == null ? "" : source.ToString();
			                             	});
		}

		public CommandColumn<TDomain> LinkCommandColumnFor(Func<TDomain, string> getControllerActionHrefForSpecificItem)
		{
			return Fluent.LinkCommandColumnFor(getControllerActionHrefForSpecificItem);
		}

		public CommandItem<TDomain> LinkCommandItemFor(Func<TDomain, string> getControllerActionHrefForSpecificItem)
		{
			return Fluent.LinkCommandItemFor(getControllerActionHrefForSpecificItem);
		}

		private object TryGetRequestedMap(Expression<Func<TModel, object>> source)
		{
			string key = Reflection.GetPropertyName(source);
			object uiMap;
			if (!_columns.TryGetValue(key, out uiMap))
			{
				throw new ArgumentException("No mapping defined for '" + key + "'");
			}
			return uiMap;
		}
	}
}