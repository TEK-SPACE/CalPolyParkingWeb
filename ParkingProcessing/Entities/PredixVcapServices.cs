using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace ParkingProcessing.Entities
{
    public class PredixVcapServices
    {
        [JsonProperty("application_uris")]
        public List<string> ApplicationUris { get; set; }

        [JsonProperty("predix-uaa")]
        public List<PredixUaaService> PredixUaa { get; set; }

        [JsonProperty("predix-timeseries")]
        public List<PredixTimeseriesService> PredixTimeSeries { get; set; }
    }
}
