using System.Collections.Generic;

using FluentAssert;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class GridColumnTest
	{
		[TestFixture]
		public class When_asked_to_create_GridColumn
		{
			[Test]
			public void Should_map_the_constructor_parameters_to_the_right_properties()
			{
				var rows = new List<string>
					{
						"Row1",
						"Row2"
					};

				const GridColumnType gridColumnType = GridColumnType.Sortable;
				const string columnheader = "ColumnHeader";
				const string fieldname = "FieldName";
				var left = AlignAttribute.Left;
				const string actionname = "ActionName";
				IGridColumn gridColumn = new GridColumn(gridColumnType, columnheader, fieldname, left, true, false, actionname, rows);

				gridColumn.Type.ShouldBeEqualTo(gridColumnType);
				gridColumn.ColumnHeader.ShouldBeEqualTo(columnheader);
				gridColumn.FieldName.ShouldBeEqualTo(fieldname);
				gridColumn.Align.ShouldBeEqualTo(left);
				gridColumn.IsDefaultSortColumn.ShouldBeTrue();
				gridColumn.IsClientSideSortable.ShouldBeFalse();
				gridColumn.ActionName.ShouldBeEqualTo(actionname);
				gridColumn.Count.ShouldBeEqualTo(rows.Count);
				gridColumn[0].ShouldBeEqualTo(rows[0]);
				gridColumn[1].ShouldBeEqualTo(rows[1]);
			}
		}
	}
}