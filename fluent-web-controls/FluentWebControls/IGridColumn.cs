namespace FluentWebControls
{
	public enum GridColumnType
	{
		Sortable,
		Command,
		Regular
	}

	public interface IGridColumn
	{
		string ActionName { get; }
		AlignAttribute Align { get; }
		string ColumnHeader { get; }
		int Count { get; }
		string FieldName { get; }
		bool IsClientSideSortable { get; }
		bool IsDefaultSortColumn { get; }
		string this[int rowId] { get; }
		GridColumnType Type { get; }
	}
}