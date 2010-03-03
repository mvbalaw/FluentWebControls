using System;

namespace FluentWebControls
{
	public class AlignAttribute
	{
		public static AlignAttribute Center = new AlignAttribute("center");
		public static AlignAttribute Left = new AlignAttribute("left");
		public static AlignAttribute Right = new AlignAttribute("right");

		private AlignAttribute(string text)
		{
			Text = text;
		}

		public string Text { get; private set; }

		public override string ToString()
		{
			return String.Format(" align='{0}'", Text);
		}
	}
}