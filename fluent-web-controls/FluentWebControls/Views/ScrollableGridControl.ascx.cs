namespace FluentWebControls.Views
{
	public class ScrollableGridControl : GridControlBase
	{
		public static class BoundPropertyNames
		{
			public static string ScrollableGridControl
			{
				get { return "ScrollableGridControl"; }
			}
		}

		public int DefaultSortColumnIndex
		{
			get
			{
				int count = 0;
				foreach (IGridColumn column in GridColumns)
				{
					if (PagedListParameters.SortField == column.FieldName)
					{
						return count;
					}
					count++;
				}
				count = 0;
				foreach (IGridColumn column in GridColumns)
				{
					if (column.IsDefaultSortColumn)
					{
						return count;
					}
					count++;
				}
				return 0;
			}
		}

		protected override bool ClientSideSortingEnabled
		{
			get { return true; }
		}
	}
}