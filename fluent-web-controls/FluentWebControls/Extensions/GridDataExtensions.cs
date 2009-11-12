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

		public static GridData<T> WithControllerExtension<T>(this GridData<T> gridData, string controllerExtension)
		{
			gridData.ControllerExtension = controllerExtension;
			return gridData;
		}

		public static GridData<T> WithFilter<T>(this GridData<T> gridData, params DropDownListData[] gridFilter)
		{
			foreach (var dropDownListData in gridFilter)
			{
				gridData.AddFilter(dropDownListData.SubmitOnChange(() => gridData.PagedListParameters.PageNumber, 1));
			}
			return gridData;
		}
	}
}