using NUnit.Framework;
using Our.Umbraco.Emptiness;

namespace Our.Umbraco.Nothingness.Tests.PropertyValueConverters
{
    [TestFixture]
    public class NullableYesNoValueConverterTests
    {
        [TestCase(1, true)]
        [TestCase(0, false)]
        [TestCase("", null)]
        [TestCase("1", true)]
        [TestCase("0", false)]
        [TestCase("true", true)]
        [TestCase("false", false)]
        [TestCase("TrUe", true)]
        [TestCase("FaLse", false)]
        [TestCase(null, null)]
        [TestCase(null, null)]
        public void WillConvertOrReturnNullValue(object value, bool? expected)
        {
            var pvc = new NullableYesNoConverter();

            var converted = pvc.ConvertSourceToIntermediate(null, null, value, false);

            Assert.AreEqual(expected, converted);
        }
    }
}
