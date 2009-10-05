using System;

namespace FluentWebControls
{
	public class SortableColumn<T> : RegularColumn<T>
	{
		public SortableColumn(Func<T, string> getItemValueFunction, string fieldName, string columnHeader)
			: base(getItemValueFunction, fieldName, columnHeader)
		{
		}

		public override GridColumnType Type
		{
			get { return GridColumnType.Sortable; }
		}
	}
}