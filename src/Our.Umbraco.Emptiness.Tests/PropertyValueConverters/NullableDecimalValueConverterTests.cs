using NUnit.Framework;
using Our.Umbraco.Emptiness;
using Umbraco.Cms.Core.PropertyEditors;

namespace Our.Umbraco.Nothingness.Tests.PropertyValueConverters
{
    [TestFixture]
    public class NullableDecimalValueConverterTests
    {
        [TestCase("1", 1)]
        [TestCase("0", 0)]
        [TestCase("", null)]
        [TestCase(null, null)]
        [TestCase("-1", -1)]
        [TestCase("1.65", 1.65)]
        [TestCase("-1.65", -1.65)]
        public void WillConvertDecimalsOrNull(object value, decimal? expected)
        {
            var converter = new NullableDecimalConverter();
            var inter = converter.ConvertSourceToIntermediate(null, null, value, false);
            var result = converter.ConvertIntermediateToObject(null, null, PropertyCacheLevel.Unknown, inter, false);

            Assert.AreEqual(expected, result);
        }
    }
}
