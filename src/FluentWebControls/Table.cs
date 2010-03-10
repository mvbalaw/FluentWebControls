using System.Collections.Generic;

namespace FluentWebControls
{
	public class Table
	{
		public static TableData<TReturn> For<TReturn>(IEnumerable<TReturn> list)
		{
			return new TableData<TReturn>(list);
		}
	}
}