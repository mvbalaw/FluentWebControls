using System;
using System.Web.UI.WebControls;

namespace FluentWebControls
{
	public class AlignAttribute
	{
		public static AlignAttribute Center = new AlignAttribute("center", HorizontalAlign.Center);
		public static AlignAttribute Left = new AlignAttribute("left", HorizontalAlign.Left);
		public static AlignAttribute Right = new AlignAttribute("right", HorizontalAlign.Right);
		private readonly HorizontalAlign _horizontalAlign;

		private AlignAttribute(string text, HorizontalAlign horizontalAlign)
		{
			_horizontalAlign = horizontalAlign;
			Text = text;
		}

		public string Text { get; private set; }

		public HorizontalAlign ToHorizontalAlign()
		{
			return _horizontalAlign;
		}

		public override string ToString()
		{
			return String.Format(" align='{0}'", Text);
		}
	}
}