namespace FluentWebControls.Interfaces
{
	public interface IPagedListParameters
	{
		int PageNumber { get; }
		int PageSize { get; }
		string SortDirection { get; }
		string SortField { get; }
	}

	internal class PagedListParameters : IPagedListParameters
	{
		public int PageNumber { get; private set; }
		public int PageSize { get; private set; }
		public string SortDirection { get; private set; }
		public string SortField { get; private set; }
	}
}