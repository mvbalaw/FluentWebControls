using System;
using System.Linq.Expressions;

using FluentAssert;

using FluentWebControls.Extensions;
using FluentWebControls.Tools;

using NUnit.Framework;

namespace FluentWebControls.Tests
{
	public class CheckBoxDataTest
	{
		public abstract class CheckBoxDataTestBase
		{
			protected abstract bool Checked { get; }
			protected abstract string HtmlText { get; }
			protected abstract string Id { get; }

			private CheckBoxData GetCheckBoxData()
			{
				return new CheckBoxData(Checked)
					{
						Id = Id,
					};
			}

			protected virtual void SetLabel(CheckBoxData checkBoxData)
			{
			}

			[Test]
			public void Should_return_HTML_code_representing_a_checkbox_field_with_its_value_embedded_in_it()
			{
				CheckBoxData checkBoxData = GetCheckBoxData();
				SetLabel(checkBoxData);
				string actual = checkBoxData.ToString();
				actual.ShouldBeEqualTo(HtmlText, actual);
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_checkbox_that_is_checked : CheckBoxDataTestBase
		{
// ReSharper disable ConvertToConstant.Local
			private bool value = true;
// ReSharper restore ConvertToConstant.Local

			protected override bool Checked
			{
				get { return value; }
			}
			protected override string HtmlText
			{
				get { return "<input type='checkbox' id='value' name='value' checked='checked'/>"; }
			}
			protected override string Id
			{
				get
				{
					Expression<Func<bool>> expr = () => value;
					return NameUtility.GetPropertyName(expr).ToCamelCase();
				}
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_checkbox_that_is_not_checked : CheckBoxDataTestBase
		{
// ReSharper disable RedundantDefaultFieldInitializer
// ReSharper disable ConvertToConstant.Local
			private bool value = false;
// ReSharper restore ConvertToConstant.Local
// ReSharper restore RedundantDefaultFieldInitializer

			protected override bool Checked
			{
				get { return value; }
			}
			protected override string HtmlText
			{
				get { return "<input type='checkbox' id='value' name='value'/>"; }
			}
			protected override string Id
			{
				get
				{
					Expression<Func<bool>> expr = () => value;
					return NameUtility.GetPropertyName(expr).ToCamelCase();
				}
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_checkbox_with_label_in_the_left : CheckBoxDataTestBase
		{
// ReSharper disable ConvertToConstant.Local
// ReSharper disable RedundantDefaultFieldInitializer
			private bool value = false;
// ReSharper restore RedundantDefaultFieldInitializer
// ReSharper restore ConvertToConstant.Local

			protected override bool Checked
			{
				get { return value; }
			}
			protected override string HtmlText
			{
				get { return "<label for='value' style='float:left;text-align:right'>Label</label><input type='checkbox' id='value' name='value'/>"; }
			}
			protected override string Id
			{
				get
				{
					Expression<Func<bool>> expr = () => value;
					return NameUtility.GetPropertyName(expr).ToCamelCase();
				}
			}

			protected override void SetLabel(CheckBoxData checkBoxData)
			{
				LabelData label = new LabelData
					{
						Text = "Label"
					};
				checkBoxData.Label = label;
				checkBoxData.LabelAlignAttribute = AlignAttribute.Left;
			}
		}

		[TestFixture]
		public class When_asked_to_create_a_checkbox_with_label_in_the_right : CheckBoxDataTestBase
		{
// ReSharper disable RedundantDefaultFieldInitializer
// ReSharper disable ConvertToConstant.Local
			private bool value = false;
// ReSharper restore ConvertToConstant.Local
// ReSharper restore RedundantDefaultFieldInitializer

			protected override bool Checked
			{
				get { return value; }
			}
			protected override string HtmlText
			{
				get { return "<label style='float:left;text-align:right'>&nbsp;</label><input type='checkbox' id='value' name='value'/><label for='value'>Label</label>"; }
			}
			protected override string Id
			{
				get
				{
					Expression<Func<bool>> expr = () => value;
					return NameUtility.GetPropertyName(expr).ToCamelCase();
				}
			}

			protected override void SetLabel(CheckBoxData checkBoxData)
			{
				LabelData label = new LabelData
					{
						Text = "Label:"
					};
				checkBoxData.Label = label;
			}
		}
	}
}