//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

namespace FluentWebControls
{
	public class ControllerInfo
	{
		public ControllerInfo(object aspxPage)
		{
			if (aspxPage != null)
			{
				var type = aspxPage.GetType().BaseType;
// ReSharper disable once PossibleNullReferenceException
				var parts = type.FullName.Split('.');
				Action = parts[parts.Length - 1];
				Name = parts[parts.Length - 2];
			}
		}

		public string Action { get; private set; }
		public string Name { get; private set; }
	}
}