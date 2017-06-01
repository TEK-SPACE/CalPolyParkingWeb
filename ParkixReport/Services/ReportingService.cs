using Parkix.Shared.Entities.Parking;
using Parkix.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkix.Report.Services
{
    public class ReportingService : RedisDatabaseService
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static ReportingService Instance { get; } = new ReportingService();

        /// <summary>
        /// Prevents a default instance of the <see cref="ReportingService"/> class from being created.
        /// </summary>
        private ReportingService()
        {

        }

        public bool GetLatestParkingLotPercentFull(string lotid, out double percentFull)
        {
            var lotExists = SystemService.Instance.GetParkingLot<ParkingLot>(id: lotid, value: out var lot);
            if (!lotExists)
            {
                percentFull = 0;
                return false;
            }

            var frameExists = GetValue<ParkingLotFrame>(key: lot.NewestKey, value: out var frame);
            if (!frameExists)
            {
                percentFull = 0;
                return false;
            }

            percentFull = frame.SpotsTaken.LastDataPoint() / lot.TotalSpots;
            return true;
        }
    }
}
