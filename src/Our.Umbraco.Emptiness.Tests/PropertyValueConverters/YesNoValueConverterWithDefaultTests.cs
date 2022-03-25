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
    public class YesNoValueConverterWithDefaultTests
    {
        [TestCase(1, false, true)]
        [TestCase(1, true, true)]
        [TestCase(0, false, false)]
        [TestCase(0, true, false)]
        [TestCase("", false, false)]
        [TestCase("", true, true)]
        [TestCase("1", false, true)]
        [TestCase("0", true, false)]
        [TestCase("true", false, true)]
        [TestCase("false", true, false)]
        [TestCase("TrUe", false, true)]
        [TestCase("FaLse", true, false)]
        [TestCase(null, false, false)]
        [TestCase(null, true, true)]
        public void WillConvertOrReturnDefaultConfigValue(object value, bool defaultValue, bool expected)
        {
            var mockPublishedContentTypeFactory = new Mock<IPublishedContentTypeFactory>();
            mockPublishedContentTypeFactory.Setup(x => x.GetDataType(123))
                .Returns(new PublishedDataType(123, "test", new Lazy<object>(() => new TrueFalseConfiguration
                {
                    Default = defaultValue
                })));

            var publishedPropType = new PublishedPropertyType(
                new PublishedContentType(Guid.NewGuid(), 1234, "test", PublishedItemType.Content, Enumerable.Empty<string>(), Enumerable.Empty<PublishedPropertyType>(), ContentVariation.Nothing),
                new PropertyType(Mock.Of<IShortStringHelper>(), "test", ValueStorageType.Nvarchar) { DataTypeId = 123 },
                new PropertyValueConverterCollection(() => Enumerable.Empty<IPropertyValueConverter>()),
                Mock.Of<IPublishedModelFactory>(),
                mockPublishedContentTypeFactory.Object);

            var pvc = new YesNoDefaultConverter();

            var converted = pvc.ConvertSourceToIntermediate(null, publishedPropType, value, false);

            Assert.AreEqual(expected, converted);
        }
    }
}
