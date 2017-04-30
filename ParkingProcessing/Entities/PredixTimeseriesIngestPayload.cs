using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingProcessing.Entities
{
    public class PredixTimeseriesIngestPayload
    {
        public string MessageId { get; set; }
        public List<Tuple<DateTime, Object, int>> DataPoints { get; set; }
        public IDictionary<string, string> Attributes { get; set; } 
    }
}
