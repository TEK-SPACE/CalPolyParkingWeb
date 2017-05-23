using Newtonsoft.Json;
using Parkix.CurrentSensor.Entities.IeParking;
using Parkix.Shared.Entities.Environment;
using Parkix.Shared.Entities.Uaa;
using System.Collections.Generic;

namespace Parkix.CurrentSensor.Entities.Environment
{
    /// <summary>
    /// Predix Vcap Service.
    /// </summary>
    public class CurrentSensorPredixVcapServices : CommonPredixVcapServices
    {
        /// <summary>
        /// Gets or sets the ie parking.
        /// </summary>
        /// <value>
        /// The ie parking.
        /// </value>
        [JsonProperty("ie-parking")]
        public List<PredixIeParkingService> IeParking { get; set; }
    }
}
