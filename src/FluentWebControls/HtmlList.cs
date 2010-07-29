using System.Collections.Generic;

namespace FluentWebControls
{
	public class HtmlList
	{
		public static ListData<TReturn> For<TReturn>(IEnumerable<TReturn> list)
		{
			return new ListData<TReturn>(list);
		}
	}
}