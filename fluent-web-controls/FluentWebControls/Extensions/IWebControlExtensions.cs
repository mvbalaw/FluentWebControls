using System;
using System.Linq.Expressions;

using FluentWebControls.Tools;

namespace FluentWebControls.Extensions
{
	public static class IWebControlExtensions
	{
		[Obsolete("Use .WithId(xx)")]
		public static T Id<T>(this T webControl, string id) where T : IWebControl
		{
			webControl.Id = id.ToCamelCase();
			return webControl;
		}

		public static T WithId<T>(this T webControl, string id) where T : IWebControl
		{
			webControl.Id = id.ToCamelCase();
			return webControl;
		}

		public static T WithId<T, TFuncResult>(this T webControl, Expression<Func<TFuncResult>> id) where T : IWebControl
		{
			return webControl.WithId(NameUtility.GetPropertyName(id));
		}

		public static T WithId<T, TFuncInput, TFuncResult>(this T webControl, Expression<Func<TFuncInput, TFuncResult>> id) where T : IWebControl
		{
			return webControl.WithId(NameUtility.GetPropertyName(id));
		}

		public static T WithIdPrefix<T>(this T webControl, string idPrefix) where T : IWebControl
		{
			webControl.IdPrefix = idPrefix.ToCamelCase();
			return webControl;
		}
	}
}