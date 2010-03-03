using System;

namespace FluentWebControls.Extensions
{
	public static class LabelDataExtensions
	{
		public static LabelData Style(this LabelData labelData, string style)
		{
			labelData.Style = style;
			return labelData;
		}

		public static LabelData Width(this LabelData labelData, string width)
		{
			labelData.Width = width;
			return labelData;
		}

		public static LabelData WithData<T>(this LabelData labelData, T item, Func<T, string> getValue)
		{
			labelData.Value = getValue(item);
			return labelData;
		}

		public static LabelData WithText(this LabelData labelData, string text)
		{
			labelData.Text = text;
			return labelData;
		}
	}
}