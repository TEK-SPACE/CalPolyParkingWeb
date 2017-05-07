using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace ParkingProcessing.Entities.UAA
{
    public class PredixUaaValidateTokenResponse
    {

        public string Error { get; set; }

        [JsonProperty("client_id")]
        public string ClientID { get; set; }
    }
}
