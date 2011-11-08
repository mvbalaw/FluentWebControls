using System;
using System.Collections.Generic;
using System.Globalization;

namespace FluentWebControls
{
	public static class IConvertibleExtensions
	{
		public static object To(this IConvertible obj, Type targetType)
		{
			// based on: http://stackoverflow.com/questions/793714/how-can-i-fix-this-up-to-do-generic-conversion-to-nullablet

			var underlyingType = Nullable.GetUnderlyingType(targetType);

			if (underlyingType != null)
			{
				if (obj == null)
				{
					return null;
				}

				targetType = underlyingType;
			}

			if (obj is string && new List<Type>{typeof(decimal), typeof(double), typeof(int)}.Contains(targetType))
			{
				double d;
				Double.TryParse ((string)obj, NumberStyles.AllowCurrencySymbol | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, null, out d);
				obj = d;
			}

			return Convert.ChangeType(obj, targetType);
		}
	}
}