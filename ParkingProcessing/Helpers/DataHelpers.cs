using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingProcessing.Entities.IeParking;
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
            var payloads = new Dictionary<Tuple<string, string>, PredixTimeseriesIngestPayload>();

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

                            Body = new List<PredixTimeseriesIngestPayloadBody>()
                            {
                                new PredixTimeseriesIngestPayloadBody()
                                {
                                    Name = "IN_USE",
                                    Datapoints = new List<List<object>>(),
                                    Attributes = new PredixTimeseriesIngestPayloadAttributes()
                                    {
                                        ParkingLotId = datapoint.ParkingLotId,
                                        ParkingSpotId = spot.Id
                                    }
                                }
                            },
                            MessageId = DatetimeToEpochMs(datapoint.Timestamp).ToString()
                        });
                    }

                    payloads[lookup].Body.First().Datapoints.Add(new List<object>() { DatetimeToEpochMs(datapoint.Timestamp), spot.Status, 1 });
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
            throw new NotImplementedException();

#pragma warning disable CS0162 // Unreachable code detected
            var payloads = new List<PredixTimeseriesIngestPayload>();
#pragma warning restore CS0162 // Unreachable code detected

            foreach (ParkingSpot data in spots)
            {
                var payload = new PredixTimeseriesIngestPayload();
                //payload.DataPoints.Add(new Tuple<DateTime, object, int>(timestamp, data.Status, 0));
                payloads.Add(payload);
            }

            return payloads;
        }

        /// <summary>
        /// Converts a Ie Parking Event to a Timeseries Ingest Payload.
        /// </summary>
        /// <param name="eventObj"></param>
        /// <returns></returns>
        public static PredixTimeseriesIngestPayload IeParkingEventToTimeseriesIngestPayload(
            PredixIeParkingEvent eventObj)
        {
            var result = new PredixTimeseriesIngestPayload()
            {
                Body = new List<PredixTimeseriesIngestPayloadBody>()
                {
                    new PredixTimeseriesIngestPayloadBody()
                    {
                        Name = eventObj.DeviceId + ":" + eventObj.LocationId,
                        Datapoints = new List<List<object>>()
                        {
                            new List<object>()
                            {
                                eventObj.Timestamp,
                                eventObj.EventType == "PKIN",
                                3
                            }
                        },
                        Attributes = new PredixTimeseriesIngestPayloadAttributes()
                        {
                            ParkingLotId = eventObj.DeviceId,
                            ParkingSpotId = eventObj.LocationId
                        }
                    }
                }
            };

            return result;
        }


        private static long DatetimeToEpochMs(DateTime dateTime)
        {
            return (long)(dateTime - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        /// <summary>
        /// The unix epoch date time.
        /// </summary>
        private static DateTime unixEpochDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

        /// <summary>
        /// Converts a Unix Epoch timestamp (1970) to datetime.
        /// </summary>
        /// <param name="unixTimeStamp">The unix time stamp.</param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            var result = unixEpochDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return result;
        }



    }
}
