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

using FluentWebControls.Extensions;

using Microsoft.Build.Framework.XamlTypes;

namespace FluentWebControls
{
	public abstract class WebControlBase : IWebControl
	{
		protected string Data
		{
			get
			{
				var data = ((IWebControl)this).Data;
				return data != null && !data.Name.IsNullOrEmpty()
					? data.Value.CreateQuotedAttribute($"data-{data.Name}")
					: "";
			}
		}

		protected string IdWithPrefix
		{
			get
			{
				var prefix = ((IWebControl)this).IdPrefix ?? "";
				if (!string.IsNullOrEmpty(prefix))
				{
					prefix += Constants.WebCompatibleSeparator;
				}
				var id = ((IWebControl)this).Id;
				if (prefix.IsNullOrEmpty())
				{
					id = id.ToCamelCase();
				}
				id = prefix + id;
				return id;
			}
		}

		protected string NameWithPrefix
		{
			get
			{
				var name = ((IWebControl)this).Name;
				var id = ((IWebControl)this).Id;
				var prefix = ((IWebControl)this).NamePrefix ?? "";

				if (name.IsNullOrEmpty() && prefix.IsNullOrEmpty())
				{
					prefix = ((IWebControl)this).IdPrefix ?? "";
				}
				if (!string.IsNullOrEmpty(prefix))
				{
					prefix += ".";
				}
				name = name ?? id;
				if (prefix.IsNullOrEmpty())
				{
					name = name.ToCamelCase();
				}
				name = prefix + name;
				return name;
			}
		}

		string IWebControl.Id { get; set; }
		string IWebControl.Name { get; set; }
		string IWebControl.IdPrefix { get; set; }
		string IWebControl.NamePrefix { get; set; }
		NameValuePair IWebControl.Data { get; set; }
	}
}