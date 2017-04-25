using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using ParkingReporting.Services;

namespace ParkingReporting.Services
{
    public class TimeseriesQueryService
    {
        public static TimeseriesQueryService Instance { get; } = new TimeseriesQueryService();

        private TimeseriesQueryService() { }
    }
}
