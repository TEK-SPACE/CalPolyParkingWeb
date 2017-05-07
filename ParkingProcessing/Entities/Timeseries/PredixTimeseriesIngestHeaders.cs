using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using ParkingProcessing.Services;

namespace ParkingProcessing.Entities.Timeseries
{
    public class PredixTimeseriesIngestHeaders
    {
        public string Authorization { get; set; }

        [JsonProperty("Predix-Zone-Id")]
        public string PredixZoneId { get; set; }

        public string Origin { get; set; }
    }
}
