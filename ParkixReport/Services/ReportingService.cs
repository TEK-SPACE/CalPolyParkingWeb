using Parkix.Shared.Entities.Parking;
using Parkix.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Parkix.Report.Entities;
using Parkix.Shared.Helpers;

namespace Parkix.Report.Services
{
    /// <summary>
    /// Determines parking situations based on historical and external data sources.
    /// </summary>
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

        /// <summary>
        /// Gets the latest parking lot percent full.
        /// </summary>
        /// <param name="lotid">The lotid.</param>
        /// <param name="percentFull">The percent full.</param>
        /// <returns></returns>
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

            PseudoLoggingService.Log("ReportingService", "Calculating percentage...");
            percentFull = (double)frame.SpotsTaken.LastDataPoint() / lot.TotalSpots;
            PseudoLoggingService.Log("Reporting Service", "...returning percentage. Virtually done.");
            return true;
        }

        /// <summary>
        /// Gets the parking lots' historical data if available.
        /// </summary>
        /// <param name="lotid"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="data"></param>
        /// <returns>True if dataset is valid, false otherwise.</returns>
        public bool GetHistoricalParkingLotPercentFull(string lotid, DateTime start, DateTime end, out List<TimestampDoubleValuePair> data)
        {
            data = new List<TimestampDoubleValuePair>();

            var lotExists = SystemService.Instance.GetParkingLot<ParkingLot>(id: lotid, value: out var lot);
            if (!lotExists)
            {
                return false;
            }

            for (DateTime iterator = start.Date; iterator <= end.Date; iterator = iterator.AddDays(1))
            {
                var frameKey = lotid + "_" + DateHelpers.DatetimeToEpochMs(iterator);
                var frameExists = GetValue<ParkingLotFrame>(key: lotid + "_" + DateHelpers.DatetimeToEpochMs(iterator) , value: out var frame);
                if (frameExists)
                {
                    var spotsTakenList = frame.SpotsTaken.SeriesData.ToList();
                    foreach (var value in spotsTakenList)
                    {
                        data.Add(new TimestampDoubleValuePair
                        {
                            Timestamp = iterator.AddMinutes(5 * value.Key),
                            Value = (double)value.Value / lot.TotalSpots
                        });
                    }
                }
                else
                {
                    PseudoLoggingService.Log("ReportingService", "Frame nonexistent: " + frameKey);
                }
            }

            return data.Count > 0;
        }
    }
}
