using System;
using System.Linq.Expressions;

using FluentAssert;

using FluentWebControls.Interfaces;
using FluentWebControls.Tests.Extensions;
using FluentWebControls.Tools;

using NUnit.Framework;

using Rhino.Mocks;

namespace FluentWebControls.Tests
{
	public class TextBoxTest
	{
		internal class Test
		{
			public Test(int value)
			{
				Value = value;
			}

			public int Value { get; set; }
		}

		[TestFixture]
		public class When_asked_to_create_a_textbox_for_a_property_of_type_int
		{
			private IBusinessObjectPropertyMetaDataFactory businessObjectPropertyMetaDataFactory;

			[SetUp]
			public void BeforeEachTest()
			{
				businessObjectPropertyMetaDataFactory = MockRepository.GenerateStub<IBusinessObjectPropertyMetaDataFactory>();
				IoCUtility.Inject(businessObjectPropertyMetaDataFactory);
			}

			[Test]
			public void Should_return_html_code_representing_a_textbox_with_its_value_embedded_in_it()
			{
				Test test = new Test(10);
				Expression<Func<int>> valueFunc = () => test.Value;
				businessObjectPropertyMetaDataFactory.Expect(x => x.GetFor(valueFunc)).IgnoreArguments().Return(PropertyMetaDataMocker.CreateStub("Value", false, null, null, null, null, typeof(int)));

				TextBoxData textBoxData = TextBox.For(valueFunc);
				textBoxData.ToString().ShouldBeEqualTo("<input type='text' id='value' name='value' class='textbox digits' value='10'/>");
			}
		}
	}
}