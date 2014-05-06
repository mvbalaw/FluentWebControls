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
using System.Web.UI.WebControls;

namespace FluentWebControls.Extensions
{
	public static class TableDataExtensions
	{
		public static TableData<T> WithBorderWidth<T>(this TableData<T> table, Unit border)
		{
			table.BorderWidth = border;
			return table;
		}

		public static TableData<T> WithCellSpacing<T>(this TableData<T> table, int cellSpacing)
		{
			table.CellSpacing = cellSpacing;
			return table;
		}

		public static TableData<T> WithClass<T>(this TableData<T> table, string @class)
		{
			table.AddCssClass(@class);
			return table;
		}

		public static TableData<T> WithColumn<T>(this TableData<T> table, DataColumn<T> dataColumn)
		{
			table.AddColumn(dataColumn);
			return table;
		}

		public static TableData<T> WithColumn<T>(this TableData<T> table, CommandColumn<T> commandColumn)
		{
			table.AddColumn(commandColumn);
			return table;
		}

		public static TableData<T> WithGridLines<T>(this TableData<T> table, GridLines gridLines)
		{
			table.GridLines = gridLines;
			return table;
		}

        public static TableData<T> WithRowLink<T>(this TableData<T> table, Func<T, string> getLink)
        {
            table.GetLink = getLink;
            return table;
        }

        public static TableData<T> WithRowId<T>(this TableData<T> table, Func<T, int> getId, string prefix = "tr_")
        {
            table.GetRowId = getId;
            table.RowIdPrefix = prefix;
            return table;
        }

        public static TableData<T> WithRowCssClass<T>(this TableData<T> table, string cssClass)
        {
            table.RowCssClass = cssClass;
            return table;
        }

		public static TableData<T> WithId<T>(this TableData<T> table, string id)
		{
			table.Id = id;
			return table;
		}

		public static TableData<T> WithStyle<T>(this TableData<T> table, string styleName, string styleValue)
		{
			table.Style.Add(styleName, styleValue);
			return table;
		}
	}
}