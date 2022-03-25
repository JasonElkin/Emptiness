using NUnit.Framework;
using Our.Umbraco.Emptiness.PropertyValueConverters;
using System;

namespace Our.Umbraco.Nothingness.Tests.PropertyValueConverters
{
    [TestFixture]
    public class DatePickerValueConverterTests
    {
        [TestCase("2022-03-10 13:14:15", true)]
        [TestCase("2022-03-10T13:14:15", true)]
        [TestCase("2022-03-10 00:00:00", false)]
        [TestCase("", false)]
        public void WillConvertValidStringsToDateTimes(string date, bool expected)
        {
            var converter = new NullableDatePickerConverter();
            var dateTime = new DateTime(2022, 03, 10, 13, 14, 15);
            var result = converter.ConvertSourceToIntermediate(null, null, date, false) as DateTime?;

            if (expected == true)
            {
                Assert.AreEqual(dateTime, result);
            }
            else if (expected == false)
            {
                Assert.AreNotEqual(dateTime, result);
            }
        }

        [TestCase("", true)]
        [TestCase(null, true)]
        [TestCase("2022-03-10 13:14:15", false)]
        public void WillConvertInvalidValuesToNull(string date, bool expected)
        {
            var converter = new NullableDatePickerConverter();
            var result = converter.ConvertSourceToIntermediate(null, null, date, false) as DateTime?;

            if (expected == true)
            {
                Assert.AreEqual(null, result);
            }
            else if (expected == false)
            {
                Assert.AreNotEqual(null, result);
            }
        }
    }
}