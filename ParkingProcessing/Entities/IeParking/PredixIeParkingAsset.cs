using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace ParkingProcessing.Entities.IeParking
{
    public class PredixIeParkingAsset
    {
        [JsonProperty("_links")]
        public PredixIeParkingAssetLinks Links { get; set; }

        [JsonProperty("device-id")]
        public string DeviceId { get; set; }
    }
}
