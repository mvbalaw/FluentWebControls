//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

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

			if (obj is string && new List<Type>
			                     {
				                     typeof(decimal),
				                     typeof(double),
				                     typeof(int)
			                     }.Contains(targetType))
			{
				double d;
				Double.TryParse((string)obj, NumberStyles.AllowCurrencySymbol | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, null, out d);
				obj = d;
			}

			return Convert.ChangeType(obj, targetType);
		}
	}
}