using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ParkingProcessing.Entities.Uaa
{
    public class PredixUaaZone
    {
        [JsonProperty("http-header-value")]
        public string  HttpHeaderValue { get; set; }

        [JsonProperty("http-header-name")]
        public string HttpHeaderName { get; set; }
    }
}
