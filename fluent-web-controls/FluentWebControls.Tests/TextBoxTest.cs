using System;
using System.Linq.Expressions;

using FluentAssert;

using FluentWebControls.Extensions;
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
		public class When_asked_to_create_a_TextBox_for_a_property_of_type_int
		{
			private IBusinessObjectPropertyMetaDataFactory _businessObjectPropertyMetaDataFactory;

			[SetUp]
			public void BeforeEachTest()
			{
				_businessObjectPropertyMetaDataFactory = MockRepository.GenerateStub<IBusinessObjectPropertyMetaDataFactory>();
				IoCUtility.Inject(_businessObjectPropertyMetaDataFactory);
			}

			[Test]
			public void Should_return_HTML_code_representing_a_TextBox_with_its_value_embedded_in_it()
			{
				var test = new Test(10);
				Expression<Func<Test, int>> metadataFunc = x => x.Value;
				_businessObjectPropertyMetaDataFactory.Expect(x => x.GetFor(metadataFunc)).IgnoreArguments().Return(PropertyMetaDataMocker.CreateStub("Value", false, null, null, null, null, typeof(int)));

				var textBoxData = TextBox.For(test, x => x.Value.ToString(), x => x.Value).WithValidationFrom((Test x) => x.Value);
				textBoxData.ToString().ShouldBeEqualTo("<input type='text' id='value' name='value' class='textbox digits' value='10'/>");
			}
		}
	}
}