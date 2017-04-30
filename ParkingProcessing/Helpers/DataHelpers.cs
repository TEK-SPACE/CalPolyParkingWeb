using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ParkingProcessing.Entities;

namespace ParkingProcessing.Helpers
{
    public static class DataHelpers
    {

        public static List<PredixTimeseriesIngestPayload> ParkingLotDataToPredixTimeseriesIngestPayload(List<ParkingLotData> datapoints)
        {
            var payloads = new Dictionary<Tuple<string, string>, PredixTimeseriesIngestPayload> ();

            foreach (ParkingLotData datapoint in datapoints)
            {
                foreach (ParkingSpot spot in datapoint.ParkingSpots)
                {
                    var lookup = new Tuple<string, string>(datapoint.ParkingLotId, spot.id);

                    //create new payload if it doesn't exist yet
                    if (!payloads.ContainsKey(lookup))
                    {
                        payloads.Add(lookup, new ParkingSpotDataPayload()
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
    }
}
