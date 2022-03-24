using NUnit.Framework;
using Our.Umbraco.Emptiness;
using Umbraco.Cms.Core.PropertyEditors;

namespace Our.Umbraco.Nothingness.Tests.PropertyValueConverters
{
    [TestFixture]
    public class NullableIntegerValueConverterTests
    {
        [TestCase("1", 1)]
        [TestCase("0", 0)]
        [TestCase("", null)]
        [TestCase(null, null)]
        [TestCase("-1", -1)]
        [TestCase("0002", 2)]
        [TestCase("-0002", -2)]
        public void WillConvertIntegersOrNull(object value, double? expected)
        {
            var converter = new NullableIntegerConverter();
            var inter = converter.ConvertSourceToIntermediate(null, null, value, false);
            var result = converter.ConvertIntermediateToObject(null, null, PropertyCacheLevel.Unknown, inter, false);

            Assert.AreEqual(expected, result);
        }
    }
}
