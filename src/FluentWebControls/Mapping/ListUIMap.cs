using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using MvbaCore;

namespace FluentWebControls.Mapping
{
	public class ListUIMap<TDomain, TModel> : IListUIMap
	{
		private readonly Dictionary<string, UIColumn<TDomain>> _columns;

		public ListUIMap(IEnumerable<TDomain> items)
		{
			ListItems = items;
			_columns = Reflection
				.GetMatchingProperties(typeof(TDomain), typeof(TModel))
				.ToDictionary(x => x.Name, x => GetMap(x));
		}

		public string IdPrefix { get; set; }
		public IEnumerable<TDomain> ListItems { get; private set; }

		public TableData<TDomain> AsTable()
		{
			return Fluent.TableFor(ListItems);
		}

		public CommandColumn<TDomain> CheckBoxCommandColumnFor<TValueHolder>(Expression<Func<TValueHolder, object>> forCheckBoxId, Func<TDomain, string> getCheckBoxValue)
		{
			return Fluent.CheckBoxCommandColumnFor<TModel, TDomain, TValueHolder>(forCheckBoxId, getCheckBoxValue);
		}

		[Obsolete("Use LinkCommandColumnFor")]
		public CommandColumn<TDomain> CommandColumnFor(Func<TDomain, string> getControllerActionHrefForSpecificItem)
		{
			return LinkCommandColumnFor(getControllerActionHrefForSpecificItem);
		}

		protected ListUIMap<TDomain, TModel> ConfigureColumn(Expression<Func<TModel, object>> forId, Func<TDomain, string> getText)
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
			return Fluent.DataColumnFor(column.TextMethod);
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

		private object TryGetRequestedMap(Expression<Func<TModel, object>> source)
		{
			string key = Reflection.GetPropertyName(source);
			UIColumn<TDomain> uiMap;
			if (!_columns.TryGetValue(key, out uiMap))
			{
				throw new ArgumentException("No mapping defined for '" + key + "'");
			}
			return uiMap;
		}
	}
}