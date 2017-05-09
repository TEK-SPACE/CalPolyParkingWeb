using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ParkingProcessing.Entities.Timeseries;
using ParkingProcessing.Entities.Parking;

namespace ParkingProcessing.Helpers
{
    /// <summary>
    /// Data helpers for conversion and manipulation of data
    /// </summary>
    public static class DataHelpers
    {
        /// <summary>
        /// Converts parking lot data into timeseries data.
        /// </summary>
        /// <param name="datapoints">The datapoints.</param>
        /// <returns></returns>
        public static List<PredixTimeseriesIngestPayload> ParkingLotDataToPredixTimeseriesIngestPayload(List<ParkingLot> datapoints)
        {
            var payloads = new Dictionary<Tuple<string, string>, PredixTimeseriesIngestPayload> ();

            foreach (ParkingLot datapoint in datapoints)
            {
                foreach (ParkingSpot spot in datapoint.ParkingSpots)
                {
                    var lookup = new Tuple<string, string>(datapoint.ParkingLotId, spot.Id);

                    //create new payload if it doesn't exist yet
                    if (!payloads.ContainsKey(lookup))
                    {
                        payloads.Add(lookup, new PredixTimeseriesIngestPayload()
                        {
                            Attributes = new Dictionary<string, string>()
                            {
                                {"ParkingLotId", datapoint.ParkingLotId},
                                {"ParkingSpotId", spot.Id }
                            },
                            DataPoints = new List<Tuple<DateTime, object, int>>(),
                            MessageId = Guid.NewGuid().ToString()
                        });
                    }

                    payloads[lookup].DataPoints.Add(new Tuple<DateTime, object, int>(datapoint.Timestamp, spot.Status, 0));
                }
            }

            return payloads.Values.ToList();
        }

        /// <summary>
        /// Converts the spot data to predix timeseries ingest payloads.
        /// </summary>
        /// <param name="spots">The spots.</param>
        /// <param name="timestamp">The timestamp.</param>
        /// <returns></returns>
        public static List<PredixTimeseriesIngestPayload> ParkingSpotDataToPredixTimeseriesIngestPayloads(List<ParkingSpot> spots, DateTime timestamp)
        {
            var payloads = new List<PredixTimeseriesIngestPayload>();

            foreach (ParkingSpot data in spots)
            {
                var payload = new PredixTimeseriesIngestPayload();
                payload.DataPoints.Add(new Tuple<DateTime, object, int>(timestamp, data.Status, 0));
                payloads.Add(payload);
            }

            return payloads;
        }
    }
}
