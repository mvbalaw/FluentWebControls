﻿using System;
using System.Linq.Expressions;

namespace FluentWebControls.Extensions
{
	public static class TextAreaDataExtensions
	{
		public static TextAreaData Cols(this TextAreaData textAreaData, int cols)
		{
			textAreaData.Cols = cols;
			return textAreaData;
		}

		public static TextAreaData CssClass(this TextAreaData textAreaData, string cssClass)
		{
			textAreaData.CssClass = cssClass;
			return textAreaData;
		}

		[Obsolete("Use .WithId(xx, x=>x.Y)")]
		public static TextAreaData Id(this TextAreaData textAreaData, Expression<Func<string>> id)
		{
			return textAreaData.WithId(id);
		}

		public static TextAreaData Rows(this TextAreaData textAreaData, int rows)
		{
			textAreaData.Rows = rows;
			return textAreaData;
		}

		public static TextAreaData Width(this TextAreaData textAreaData, string width)
		{
			textAreaData.Width = width;
			return textAreaData;
		}

		public static TextAreaData WithLabel(this TextAreaData textAreaData, LabelData label)
		{
			textAreaData.Label = label;
			return textAreaData;
		}
	}
}