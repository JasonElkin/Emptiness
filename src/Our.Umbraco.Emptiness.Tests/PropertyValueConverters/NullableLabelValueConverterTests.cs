using Moq;
using NUnit.Framework;
using Our.Umbraco.Emptiness.PropertyValueConverters;
using System;
using System.Linq;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Strings;

namespace Our.Umbraco.Nothingness.Tests.PropertyValueConverters
{
    [TestFixture]
    public class NullableLabelValueConverterTests
    {
        private static TestCaseData[] ValueTypeValues()
        {

            return new TestCaseData[]
            {
                new TestCaseData(ValueTypes.DateTime, new DateTime(2022, 3, 10), new DateTime(2022, 3, 10)),
                new TestCaseData(ValueTypes.DateTime, "2022-03-10", new DateTime(2022, 3, 10)),
                new TestCaseData(ValueTypes.DateTime, "", null),

                new TestCaseData(ValueTypes.Time, new DateTime(2022, 3, 10, 1, 2, 3), new TimeSpan(1,2,3)),
                new TestCaseData(ValueTypes.Time, "01:02:03", new TimeSpan(1,2,3)),
                new TestCaseData(ValueTypes.Time, "", null),

                new TestCaseData(ValueTypes.Decimal, "", null),
                new TestCaseData(ValueTypes.Decimal, 1m, 1m),
                new TestCaseData(ValueTypes.Decimal, "1", 1m),
                new TestCaseData(ValueTypes.Decimal, "0", 0m),
                new TestCaseData(ValueTypes.Decimal, "-1", -1m),
                new TestCaseData(ValueTypes.Decimal, "1.234", 1.234),
                new TestCaseData(ValueTypes.Decimal, "-1.234", -1.234),

                new TestCaseData(ValueTypes.Integer, "", null),
                new TestCaseData(ValueTypes.Integer, 1, 1),
                new TestCaseData(ValueTypes.Integer, "1", 1),
                new TestCaseData(ValueTypes.Integer, "0", 0),
                new TestCaseData(ValueTypes.Integer, "-1", -1),

                new TestCaseData(ValueTypes.Bigint, "", null),
                new TestCaseData(ValueTypes.Bigint, "1", 1f),
                new TestCaseData(ValueTypes.Bigint, "0", 0f),
                new TestCaseData(ValueTypes.Bigint, "-1", -1f),
                new TestCaseData(ValueTypes.Bigint, "-9223372036854775808", -9223372036854775808),

                new TestCaseData(ValueTypes.String, null, ""),
                new TestCaseData(ValueTypes.String, "", ""),
                new TestCaseData(ValueTypes.String, "Ahoy!", "Ahoy!"),

                new TestCaseData(ValueTypes.Json, "{json:true}", "{json:true}"),
                new TestCaseData(ValueTypes.Text, "That's no moon.", "That's no moon."),
                new TestCaseData(ValueTypes.Xml, "<xml><node>value</node></xml>", "<xml><node>value</node></xml>"),
            };
        }

        [Test, TestCaseSource("ValueTypeValues")]
        public void WillReturnCorrectlyConvertedValue(string type, object value, object expected)
        {
            var mockPublishedContentTypeFactory = new Mock<IPublishedContentTypeFactory>();
            mockPublishedContentTypeFactory.Setup(x => x.GetDataType(123))
                .Returns(new PublishedDataType(123, "test", new Lazy<object>(() => new LabelConfiguration
                {
                    ValueType = type,
                })));

            var publishedPropType = new PublishedPropertyType(
                new PublishedContentType(Guid.NewGuid(), 1234, "test", PublishedItemType.Content, Enumerable.Empty<string>(), Enumerable.Empty<PublishedPropertyType>(), ContentVariation.Nothing),
                new PropertyType(Mock.Of<IShortStringHelper>(), "test", ValueStorageType.Nvarchar) { DataTypeId = 123 },
                new PropertyValueConverterCollection(() => Enumerable.Empty<IPropertyValueConverter>()),
                Mock.Of<IPublishedModelFactory>(),
                mockPublishedContentTypeFactory.Object);

            var converter = new NullableLabelConverter();

            var inter = converter.ConvertSourceToIntermediate(null, publishedPropType, value, false);
            var result = converter.ConvertIntermediateToObject(null, publishedPropType, PropertyCacheLevel.Unknown, inter, false);

            Assert.AreEqual(result, expected);
        }
    }
}
