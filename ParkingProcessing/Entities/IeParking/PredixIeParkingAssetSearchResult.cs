using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ParkingProcessing.Entities.IeParking
{
    public class PredixIeParkingAssetSearchResult
    {
        [JsonProperty("_embedded")]
        public PredixIeParkingSearchResultEmbedded Embedded { get; set; }
    }
}
