using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

using ParkingProcessing.Entities.Timeseries;
using ParkingProcessing.Entities.Uaa;
using ParkingProcessing.Entities.IeParking;

namespace ParkingProcessing.Entities.Environment
{
    public class PredixVcapServices
    {
        [JsonProperty("application_uris")]
        public List<string> ApplicationUris { get; set; }

        [JsonProperty("predix-uaa")]
        public List<PredixUaaService> PredixUaa { get; set; }

        [JsonProperty("predix-timeseries")]
        public List<PredixTimeseriesService> PredixTimeSeries { get; set; }

        [JsonProperty("ie-parking")]
        public List<PredixIeParkingService> IeParking { get; set; }
    }
}
