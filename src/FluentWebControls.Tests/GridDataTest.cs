using System.Collections.Generic;
using System.Linq;

using FluentAssert;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class GridDataTest
	{
		[TestFixture]
		public class When_asked_to_create_a_GridData
		{
			private const string ActionName = "Action";
			private const string ControllerName = "Controller";
			private readonly List<string> _rows = new List<string>
				{
					"Row1",
					"Row2"
				};

			[Test]
			public void Should_map_the_constructor_parameters_to_the_right_properties()
			{
				var gridColumns = new List<IGridColumn>
					{
						new GridColumn(GridColumnType.Sortable, "Header", "Field", AlignAttribute.Left, false, true, "", ActionName, _rows)
					};
				var filters = new List<DropDownListData>();
				var pagedGridData = new GridData(null, ControllerName, ".mvc", ActionName, gridColumns, gridColumns.Count, filters, _rows.Count);
				pagedGridData.PagedListParameters.ShouldBeEqualTo(null);
				pagedGridData.ControllerName.ShouldBeEqualTo(ControllerName);
				pagedGridData.ActionName.ShouldBeEqualTo(ActionName);
				Assert.AreSame(gridColumns, pagedGridData.GridColumns);
				pagedGridData.Total.ShouldBeEqualTo(gridColumns.Count);
				pagedGridData.Filters.ShouldBeEqualTo(filters);
				pagedGridData.RowCount.ShouldBeEqualTo(_rows.Count);
			}
		}
	}

	public class GridDataTestT
	{
		public abstract class GridDataTestTBase
		{
			protected const string ActionName = "Action";
			protected const string ControllerName = "Controller";

			protected readonly List<TestData.Item> _items = new List<TestData.Item>
				{
					new TestData.Item(1, "Item1"),
					new TestData.Item(2, "Item2")
				};

			protected GridData<TestData.Item> _gridData;

			[SetUp]
			public void BeforeEachTest()
			{
				_gridData = new GridData<TestData.Item>(null, ControllerName, ActionName, _items, _items.Count);
			}
		}

		[TestFixture]
		public class When_asked_to_add_a_CommandColumn_to_GridData_of_Type_T : GridDataTestTBase
		{
			private readonly GridCommandColumn<TestData.Item> _gridCommandColumn = new GridCommandColumn<TestData.Item>(item => item.ItemId.ToString(), "Edit", ActionName);

			[Test]
			public void Should_add_the_GridColumn_to_the_Paged_Grid_using_items_supplied_in_the_constructor()
			{
				_gridData.AddColumn(_gridCommandColumn);

				var gridColumns = _gridData.GridColumns.ToList();
				gridColumns.Count.ShouldBeEqualTo(1);
				gridColumns[0][0].ShouldBeEqualTo(_items[0].ItemId.ToString());
				gridColumns[0][1].ShouldBeEqualTo(_items[1].ItemId.ToString());

				gridColumns[0].ColumnHeader.ShouldBeEqualTo(_gridCommandColumn.ColumnHeader);
				gridColumns[0].FieldName.ShouldBeEqualTo(_gridCommandColumn.FieldName);
				gridColumns[0].Align.ShouldBeEqualTo(_gridCommandColumn.Align);
				gridColumns[0].ActionName.ShouldBeEqualTo(_gridData.ActionName);
			}
		}

		[TestFixture]
		public class When_asked_to_add_a_SortableColumn_to_GridData_of_Type_T : GridDataTestTBase
		{
			private readonly SortableColumn<TestData.Item> _sortableColumn = new SortableColumn<TestData.Item>(item => item.ItemName, "ItemName", "ColumnHeader");

			[Test]
			public void Should_add_the_GridColumn_to_the_Paged_Grid_using_items_supplied_in_the_constructor()
			{
				_gridData.AddColumn(_sortableColumn);

				var gridColumns = _gridData.GridColumns.ToList();
				gridColumns.Count.ShouldBeEqualTo(1);
				gridColumns[0][0].ShouldBeEqualTo(_items[0].ItemName);
				gridColumns[0][1].ShouldBeEqualTo(_items[1].ItemName);

				gridColumns[0].ColumnHeader.ShouldBeEqualTo(_sortableColumn.ColumnHeader);
				gridColumns[0].FieldName.ShouldBeEqualTo(_sortableColumn.FieldName);
				gridColumns[0].Align.ShouldBeEqualTo(_sortableColumn.Align);
				gridColumns[0].ActionName.ShouldBeEqualTo(_gridData.ActionName);
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_GridData_of_Type_T
		{
			private const string ActionName = "Action";
			private const string ControllerName = "Controller";

			private readonly List<TestData.Item> _items = new List<TestData.Item>
				{
					new TestData.Item(1, "Item1"),
					new TestData.Item(2, "Item2")
				};

			[Test]
			public void Should_map_the_constructor_parameters_to_the_right_properties()
			{
				var gridData = new GridData<TestData.Item>(null, ControllerName, ActionName, _items, _items.Count);
				gridData.PagedListParameters.ShouldBeEqualTo(null);
				gridData.ControllerName.ShouldBeEqualTo(ControllerName);
				gridData.ActionName.ShouldBeEqualTo(ActionName);
			}
		}
	}
}