//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

namespace FluentWebControls.Interfaces
{
	public interface IPagedListParameters
	{
		int PageNumber { get; }
		int PageSize { get; }
		string SortDirection { get; }
		string SortField { get; }
	}

	internal class PagedListParameters : IPagedListParameters
	{
		public int PageNumber { get; private set; }
		public int PageSize { get; private set; }
		public string SortDirection { get; private set; }
		public string SortField { get; private set; }
	}
}