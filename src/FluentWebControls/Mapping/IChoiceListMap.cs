//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System.Collections.Generic;

using MvbaCore.Interfaces;

namespace FluentWebControls.Mapping
{
	public interface IChoiceListMap : IModelMap
	{
		string Id { get; }
		IEnumerable<KeyValuePair<string, string>> ListItems { get; }
		string SelectedValue { get; }
		IEnumerable<string> SelectedValues { get; }
		IPropertyMetaData Validation { get; }
	}
}