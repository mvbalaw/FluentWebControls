using System;
using System.Linq.Expressions;
using Microsoft.Build.Framework.XamlTypes;
using MvbaCore;

namespace FluentWebControls.Extensions
{
	public static class IWebControlExtensions
	{
		public static T WithId<T>(this T webControl, string id) where T : IWebControl
		{
			webControl.Id = id;
			return webControl;
		}

		public static T WithData<T>(this T webControl, string name, string value) where T : IWebControl
		{
			webControl.Data = new NameValuePair
			                  	{
			                  		Name = name,
			                  		Value = value
			                  	};
			return webControl;
		}

		public static T WithId<T, TFuncResult>(this T webControl, Expression<Func<TFuncResult>> id) where T : IWebControl
		{
			return webControl.WithId(Reflection.GetPropertyName(id));
		}

		public static T WithId<T, TFuncInput, TFuncResult>(this T webControl, Expression<Func<TFuncInput, TFuncResult>> id)
			where T : IWebControl
		{
			return webControl.WithId(Reflection.GetPropertyName(id));
		}

		public static T WithIdPrefix<T>(this T webControl, string idPrefix) where T : IWebControl
		{
			webControl.IdPrefix = idPrefix.ToCamelCase();
			return webControl;
		}

		public static T WithNamePrefix<T>(this T webControl, string namePrefix) where T : IWebControl
		{
			webControl.NamePrefix = namePrefix.ToCamelCase();
			return webControl;
		}
	}
}