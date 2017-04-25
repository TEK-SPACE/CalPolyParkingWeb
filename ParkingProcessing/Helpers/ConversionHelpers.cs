using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ParkingProcessing.Entities;

namespace ParkingProcessing.Helpers
{
    public static class ConversionHelpers
    {

        public static PredixTimeseriesIngestPayload ParkingSpotDataListToPredixTimeseriesIngestPayload(
            List<ParkingSpot> spots)
        {
            var payload = new PredixTimeseriesIngestPayload();
            return payload;
        }
    }
}
