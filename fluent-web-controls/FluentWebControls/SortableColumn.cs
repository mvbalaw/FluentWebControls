using System;

namespace FluentWebControls
{
	public class SortableColumn<T> : RegularColumn<T>
	{
		public SortableColumn(Func<T, string> getValue, string fieldName, string columnHeader)
			: base(getValue, fieldName, columnHeader)
		{
		}

		public override GridColumnType Type
		{
			get { return GridColumnType.Sortable; }
		}
	}
}