using System;

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
					return null;

				return Convert.ChangeType(obj, underlyingType);
			}
			else
			{
				return Convert.ChangeType(obj, targetType);
			}
		}
	}
}