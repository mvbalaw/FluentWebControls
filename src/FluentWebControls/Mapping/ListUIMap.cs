using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using MvbaCore;
using MvbaCore.Extensions;

namespace FluentWebControls.Mapping
{
	public class ListUIMap<TDomain, TModel> : IListUIMap
	{
		private readonly Dictionary<string, object> _columns = new Dictionary<string, object>();

		public ListUIMap(IEnumerable<TDomain> items)
		{
			ListItems = items;
		}

		public string IdPrefix { get; set; }
		public IEnumerable<TDomain> ListItems { get; private set; }

		public TableData<TDomain> AsTable()
		{
			return Fluent.TableFor(ListItems);
		}

		public CommandColumn<TDomain> CommandColumnFor(Func<TDomain, string> getControllerActionHrefForSpecificItem)
		{
			return Fluent.CommandColumnFor(getControllerActionHrefForSpecificItem);
		}

		protected ListUIMap<TDomain, TModel> ConfigureColumn(Expression<Func<TDomain, object>> forId, Func<TDomain, string> getText)
		{
			string id = Reflection.GetPropertyName(forId);
			var column = new UIColumn<TDomain>(getText);
			_columns.Add(id, column);
			return this;
		}

		public DataColumn<TDomain> DataColumnFor(Expression<Func<TDomain, object>> forPropertyName)
		{
			var uiMap = TryGetRequestedMap(forPropertyName);
			var column = uiMap.TryCastTo<UIColumn<TDomain>>();
			return Fluent.DataColumnFor(column.TextMethod);
		}

		private object TryGetRequestedMap(Expression<Func<TDomain, object>> source)
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