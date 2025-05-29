//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using MvbaCore.Interfaces;

namespace FluentWebControls.Mapping
{
	public class BooleanMap : IBooleanMap, IFreeTextMap
	{
		public BooleanMap(string id, bool value)
		{
			Id = id;
			IsChecked = value;
		}

		public string Id { get; set; }
		public bool IsChecked { get; set; }

		object IModelMap.GetValueForModel()
		{
			return IsChecked;
		}

		string IFreeTextMap.Value => IsChecked.ToString();

		public IPropertyMetaData Validation { get; set; }
	}
}