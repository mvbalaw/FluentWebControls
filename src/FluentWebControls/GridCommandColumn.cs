using System;

namespace FluentWebControls
{
	public class GridCommandColumn<T> : GridColumnBuilder
	{
		private readonly Func<T, string> _getItemIdFunction;

		public GridCommandColumn(Func<T, string> getItemIdFunction, string fieldName, string actionName)
			: base("", fieldName)
		{
			_getItemIdFunction = getItemIdFunction;
			ActionName = actionName;
			IsClientSideSortable = false;
		}

		public string ActionName { get; private set; }

		public override GridColumnType Type
		{
			get { return GridColumnType.Command; }
		}

		public string GetItemId(T item)
		{
			return _getItemIdFunction(item);
		}
	}
}