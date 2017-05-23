using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parkix.Shared.Entities.Parking;

namespace ParkingTests
{
    [TestClass]
    public class DayTimeSeriesTests
    {
        [TestMethod]
        public void TestCreation()
        {
            var datetime = new DateTime(year: 2017, month: 1, day: 1);

            var subject = new DayTimeSeries(datetime);

            for (int i = 0; i < 288; i++)
            {
                subject.AddDataPoint(i, datetime.AddMinutes(5 * i), false);
            }

            var result = subject.Summarize(new TimeSpan(0, 5, 0));

            for (int i = 0; i < 288; i++)
            {
                Assert.IsTrue(result[i] == i);
            }
        }

        [TestMethod]
        public void TestTenMinuteSummary()
        {
            var datetime = new DateTime(year: 2017, month: 1, day: 1);

            var subject = new DayTimeSeries(datetime);

            for (int i = 0; i < 288; i++)
            {
                subject.AddDataPoint(i, datetime.AddMinutes(5 * i), false);
            }

            var result = subject.Summarize(new TimeSpan(0, 10, 0));

            for (int i = 0; i < 144; i += 2)
            {
                Assert.IsTrue(result[i] == (2 * i));
            }
        }

        [TestMethod]
        public void TestInterpolation()
        {
            var datetime = new DateTime(year: 2017, month: 1, day: 1);

            var subject = new DayTimeSeries(datetime);

            for (int i = 0; i < 300 / 5; i++)
            {
                subject.AddDataPoint(i, datetime.AddMinutes(25 * i + 25), true);
            }

            var result = subject.Summarize(new TimeSpan(0, 15, 0));

            for (int i = 0; i < 144; i += 2)
            {
                //Assert.IsTrue(result[i] == i / 5);
            }
        }
    }
}
