namespace FluentWebControls.Extensions
{
	public static class TableDataExtensions
	{
		public static TableData<T> WithColumn<T>(this TableData<T> table, DataColumn<T> dataColumn)
		{
			table.AddColumn(dataColumn);
			return table;
		}
		
		public static TableData<T> WithColumn<T>(this TableData<T> table, CommandColumn<T> commandColumn)
		{
			table.AddColumn(commandColumn);
			return table;
		}
	}
}