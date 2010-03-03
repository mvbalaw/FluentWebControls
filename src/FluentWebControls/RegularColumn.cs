using System;

namespace FluentWebControls
{
	public class RegularColumn<T> : GridColumnBuilder
	{
		private readonly Func<T, string> _getItemValueFunction;

		public RegularColumn(Func<T, string> getItemValueFunction, string fieldName, string columnHeader)
			: base(columnHeader, fieldName)
		{
			_getItemValueFunction = getItemValueFunction;
		}

		public override GridColumnType Type
		{
			get { return GridColumnType.Regular; }
		}

		public string GetItemValue(T item)
		{
			return _getItemValueFunction(item);
		}
	}
}