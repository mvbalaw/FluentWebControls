using System.Web.UI;

namespace FluentWebControls
{
	public interface IHtmlColumn<T>
	{
		void Render(T item, HtmlTextWriter writer);
		void RenderHeader(HtmlTextWriter writer);
	}
}