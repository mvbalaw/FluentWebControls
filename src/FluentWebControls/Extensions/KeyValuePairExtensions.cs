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
using System.Linq;

using JetBrains.Annotations;

namespace FluentWebControls.Extensions
{
	public static class KeyValuePairExtensions
	{
		public static string ToQueryString([CanBeNull] this IEnumerable<KeyValuePair<string, string>> parameters)
		{
			if (parameters == null)
			{
				return "";
			}
			var memoized = parameters.Memoize();
			if (memoized.Any(x => x.Key.IsNullOrEmpty(true)))
			{
				throw new ArgumentException("Keys cannot be null");
			}
			return memoized.Select(x => x.Key.EscapeForUrl() + "=" + x.Value.EscapeForUrl()).Join("&");
		}
	}
}