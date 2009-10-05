using System.Collections.Generic;
using System.Linq;

using FluentAssert;

using FluentWebControls.Extensions;

using NUnit.Framework;

namespace FluentWebControls.Tests.Extensions
{
	public class GridDataExtensionsTest
	{
		public class GridDataExtensionsTestBase
		{
			protected const string ActionName = "Action";
			protected const string ControllerName = "Controller";

			private readonly List<TestData.Item> _items = new List<TestData.Item>
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
		public class When_asked_to_add_command_column : GridDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_GridData_With_the_newly_added_CommandColumn()
			{
				CommandColumn<TestData.Item> commandColumn = new CommandColumn<TestData.Item>(item => item.ItemName, "FieldName", ActionName);
				GridData<TestData.Item> grid = _gridData.WithColumn(commandColumn);
				Assert.AreSame(_gridData, grid);
				_gridData.GridColumns.ToList().Count.ShouldBeEqualTo(1);
				_gridData.ActionName.ShouldBeEqualTo(ActionName);
			}
		}

		[TestFixture]
		public class When_asked_to_add_filter : GridDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_GridData_With_the_newly_added_filter()
			{
				List<KeyValuePair<string, string>> kvpList = new List<KeyValuePair<string, string>>
					{
						new KeyValuePair<string, string>("Name1", "Value2"),
						new KeyValuePair<string, string>("Name1", "Value2")
					};

				DropDownListData dropDownListData = new DropDownListData(kvpList, null);
				dropDownListData.SubmitOnChange = true;
				GridData<TestData.Item> grid = _gridData.WithFilter(dropDownListData);
				Assert.AreSame(_gridData, grid);
			}
		}

		[TestFixture]
		public class When_asked_to_add_sortable_column : GridDataExtensionsTestBase
		{
			[Test]
			public void Should_return_a_GridData_With_the_newly_added_SortableColumn()
			{
				SortableColumn<TestData.Item> sortableColumn = new SortableColumn<TestData.Item>(item => item.ItemName, "FieldName", "ColumnHeader");
				GridData<TestData.Item> grid = _gridData.WithColumn(sortableColumn);
				Assert.AreSame(_gridData, grid);
				_gridData.GridColumns.ToList().Count.ShouldBeEqualTo(1);
			}
		}
	}
}