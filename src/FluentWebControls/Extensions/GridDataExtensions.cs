//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

namespace FluentWebControls.Extensions
{
	public static class GridDataExtensions
	{
		public static TGridData WithColumn<TGridData, T>(this TGridData gridData, SortableColumn<T> gridColumn) where TGridData : GridData<T>
		{
			gridData.AddColumn(gridColumn);
			return gridData;
		}

		public static TGridData WithColumn<TGridData, T>(this TGridData gridData, RegularColumn<T> gridColumn) where TGridData : GridData<T>
		{
			gridData.AddColumn(gridColumn);
			return gridData;
		}

		public static TGridData WithColumn<TGridData, T>(this TGridData gridData, GridCommandColumn<T> gridColumn) where TGridData : GridData<T>
		{
			gridData.AddColumn(gridColumn);
			return gridData;
		}

		public static GridData<T> WithControllerExtension<T>(this GridData<T> gridData, string controllerExtension)
		{
			gridData.ControllerExtension = controllerExtension;
			return gridData;
		}

		public static GridData<T> WithFilter<T>(this GridData<T> gridData, params DropDownListData[] gridFilter)
		{
			foreach (var dropDownListData in gridFilter)
			{
				gridData.AddFilter(dropDownListData.SubmitOnChange(() => gridData.PagedListParameters.PageNumber, 1));
			}
			return gridData;
		}
	}
}