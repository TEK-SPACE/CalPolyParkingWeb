using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ParkingReporting.Entities
{
    public class PredixVcapApplication
    {
        [JsonProperty("application_uris")]
        public List<string> ApplicationUris { get; set; }

    }
}
