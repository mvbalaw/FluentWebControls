namespace FluentWebControls.Interfaces
{
	public interface IPagedListParameters
	{
		int PageNumber { get; }
		int PageSize { get; }
		string SortDirection { get; }
		string SortField { get; }
	}
}