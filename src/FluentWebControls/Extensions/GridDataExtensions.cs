namespace FluentWebControls.Extensions
{
	public static class GridDataExtensions
	{
		public static TGridData WithColumn<TGridData, T>(this TGridData gridData, SortableColumn<T> gridColumn) where TGridData : GridData<T>
		{
			gridData.AddColumn(gridColumn);
			return gridData;
		}

		public static TGridData WithColumn<TGridData, T>(this TGridData gridData, RegularColumn<T> gridColumn) where TGridData : GridData<T>
		{
			gridData.AddColumn(gridColumn);
			return gridData;
		}

		public static TGridData WithColumn<TGridData, T>(this TGridData gridData, GridCommandColumn<T> gridColumn) where TGridData : GridData<T>
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