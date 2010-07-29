using System.Text;

namespace FluentWebControls
{
	public interface IListItem<T>
	{
		StringBuilder Render(T item);
	}
}