namespace FluentWebControls.Extensions
{
	public static class GridDataExtensions
	{
		public static GridData<T> WithColumn<T>(this GridData<T> gridData, SortableColumn<T> gridColumn)
		{
			gridData.AddColumn(gridColumn);
			return gridData;
		}

		public static GridData<T> WithColumn<T>(this GridData<T> gridData, RegularColumn<T> gridColumn)
		{
			gridData.AddColumn(gridColumn);
			return gridData;
		}

		public static GridData<T> WithColumn<T>(this GridData<T> gridData, CommandColumn<T> gridColumn)
		{
			gridData.AddColumn(gridColumn);
			return gridData;
		}

		public static GridData<T> WithFilter<T>(this GridData<T> gridData, params DropDownListData[] gridFilter)
		{
			foreach (DropDownListData dropDownListData in gridFilter)
			{
				gridData.AddFilter(dropDownListData.SubmitOnChange(() => gridData.PagedListParameters.PageNumber, 1));
			}
			return gridData;
		}
	}
}