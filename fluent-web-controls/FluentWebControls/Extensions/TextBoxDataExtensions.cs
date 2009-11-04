using System;
using System.Linq.Expressions;

namespace FluentWebControls.Extensions
{
	public static class TextBoxDataExtensions
	{
		public static TextBoxData CssClass(this TextBoxData textBoxData, string cssClass)
		{
			textBoxData.CssClass = cssClass;
			return textBoxData;
		}

		[Obsolete("use .WithId(xx)")]
		public static TextBoxData Id(this TextBoxData textBoxData, string id)
		{
			return textBoxData.WithId(id);
		}

		[Obsolete("use .WithId(x=>x.Y)")]
		public static TextBoxData Id(this TextBoxData textBoxData, Expression<Func<string>> id)
		{
			return textBoxData.WithId(id);
		}

		public static TextBoxData MaxValue(this TextBoxData textBoxData, int maxValue)
		{
			textBoxData.MaxValue = maxValue;
			return textBoxData;
		}

		public static TextBoxData MinValue(this TextBoxData textBoxData, int minValue)
		{
			textBoxData.MinValue = minValue;
			return textBoxData;
		}

		public static TextBoxData Width(this TextBoxData textBoxData, string width)
		{
			textBoxData.Width = width;
			return textBoxData;
		}

		public static TextBoxData WithLabel(this TextBoxData textBoxData, LabelData label)
		{
			textBoxData.Label = label;
			label.ForId = ((IWebControl)textBoxData).Id;
			return textBoxData;
		}
	}
}