using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ParkingProcessing.Entities.Timeseries;
using ParkingProcessing.Entities.Parking;

namespace ParkingProcessing.Helpers
{
    public static class DataHelpers
    {

        public static List<PredixTimeseriesIngestPayload> ParkingLotDataToPredixTimeseriesIngestPayload(List<ParkingLot> datapoints)
        {
            var payloads = new Dictionary<Tuple<string, string>, PredixTimeseriesIngestPayload> ();

            foreach (ParkingLot datapoint in datapoints)
            {
                foreach (ParkingSpot spot in datapoint.ParkingSpots)
                {
                    var lookup = new Tuple<string, string>(datapoint.ParkingLotId, spot.id);

                    //create new payload if it doesn't exist yet
                    if (!payloads.ContainsKey(lookup))
                    {
                        payloads.Add(lookup, new PredixTimeseriesIngestPayload()
                        {
                            Attributes = new Dictionary<string, string>()
                            {
                                {"ParkingLotId", datapoint.ParkingLotId},
                                {"ParkingSpotId", spot.id }
                            },
                            DataPoints = new List<Tuple<DateTime, object, int>>(),
                            MessageId = Guid.NewGuid().ToString()
                        });
                    }

                    payloads[lookup].DataPoints.Add(new Tuple<DateTime, object, int>(datapoint.Timestamp, spot.status, 0));
                }
            }

            return payloads.Values.ToList();
        }

        public static List<PredixTimeseriesIngestPayload> ParkingSpotDataToPredixTimeseriesIngestPayloads(List<ParkingSpot> spots, DateTime timestamp)
        {
            var payloads = new List<PredixTimeseriesIngestPayload>();

            foreach (ParkingSpot data in spots)
            {
                var payload = new PredixTimeseriesIngestPayload();
                payload.DataPoints.Add(new Tuple<DateTime, object, int>(timestamp, data.status, 0));
                payloads.Add(payload);
            }

            return payloads;
        }
    }
}
