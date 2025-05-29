//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
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
				.GetMatchingProperties(typeof(TDomain), typeof(TModel)))
			{
				_columns.Add(matchingProperty.Name, GetMap(matchingProperty));
			}
		}

		public string IdPrefix { get; set; }
		public IEnumerable<TDomain> ListItems { get; }

		public void Populate<TMapModel>(TMapModel model)
		{
			var properties = typeof(TMapModel).GetProperties()
				.ToDictionary(x => x.Name, x => x);
			foreach (var mapping in _columns)
			{
				if (!properties.TryGetValue(mapping.Key, out var property))
				{
					continue;
				}

                var source = mapping.Value as UIColumn<TDomain>;
				if (source == null)
				{
					continue;
				}

				var itemValues = ListItems.Select(source.TextMethod).ToList();

				if (!itemValues.Any())
				{
					continue;
				}

				if (property.PropertyType.IsGenericType &&
					property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
				{
					var targetList = property.GetValue(model, null);
					var addMethod = targetList.GetType().GetMethod("Add");
					var targetType = property.PropertyType.GetGenericArguments().Single();
					foreach (var item in itemValues)
					{
						// handle Guids
						if (targetType == typeof(int) && !int.TryParse(item, out _))
						{
							continue;
						}
						var convertedValue = item.To(targetType);
                        addMethod?.Invoke(targetList, new[] {convertedValue});
                    }
				}
			}
		}

		public ListData<TDomain> AsList()
		{
			return Fluent.ListFor(ListItems);
		}

		public TableData<TDomain> AsTable()
		{
			return Fluent.TableFor(ListItems);
		}

		public CommandColumn<TDomain> CheckBoxCommandColumnFor<TValueHolder>(
			Expression<Func<TValueHolder, object>> forCheckBoxId, Func<TDomain, string> getCheckBoxValue)
		{
			return Fluent.CheckBoxCommandColumnFor<TModel, TDomain, TValueHolder>(forCheckBoxId, getCheckBoxValue);
		}

		public CommandColumn<TDomain> CheckBoxCommandColumnFor<TValueHolder>(
			Expression<Func<TValueHolder, object>> forCheckBoxId)
		{
			var column = (UIColumn<TDomain>)_columns[Reflection.GetPropertyName(forCheckBoxId)];
			return Fluent.CheckBoxCommandColumnFor<TModel, TDomain, TValueHolder>(forCheckBoxId, column.TextMethod);
		}

		public CommandItem<TDomain> CheckBoxCommandItemFor<TValueHolder>(
			Expression<Func<TValueHolder, object>> forCheckBoxId, Func<TDomain, string> getCheckBoxValue)
		{
			return Fluent.CheckBoxCommandItemFor<TModel, TDomain, TValueHolder>(forCheckBoxId, getCheckBoxValue);
		}

		public CommandItem<TDomain> CheckBoxCommandItemFor<TValueHolder>(
			Expression<Func<TValueHolder, object>> forCheckBoxId)
		{
			var column = (UIColumn<TDomain>)_columns[Reflection.GetPropertyName(forCheckBoxId)];
			return Fluent.CheckBoxCommandItemFor<TModel, TDomain, TValueHolder>(forCheckBoxId, column.TextMethod);
		}

		[Obsolete("Use LinkCommandColumnFor")]
		public CommandColumn<TDomain> CommandColumnFor(Func<TDomain, string> getControllerActionHrefForSpecificItem)
		{
			return LinkCommandColumnFor(getControllerActionHrefForSpecificItem);
		}

		protected ListUIMap<TDomain, TModel> ConfigureColumn(Expression<Func<TModel, object>> forId,
			Func<TDomain, string> getText)
		{
			var id = Reflection.GetPropertyName(forId);
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
			var key = Reflection.GetPropertyName(source);
			if (!_columns.TryGetValue(key, out var uiMap))
			{
				throw new ArgumentException("No mapping defined for '" + key + "'");
			}
			return uiMap;
		}

		public ListUIMap<TDomain, TModel> WithCommandColumnFor<TValueHolder>(
			Expression<Func<TValueHolder, object>> forId, Func<TDomain, string> getText)
		{
			var id = Reflection.GetPropertyName(forId);
			var column = new UIColumn<TDomain>(getText);
			_columns[id] = column;
			return this;
		}
	}
}