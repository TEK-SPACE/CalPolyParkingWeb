using Newtonsoft.Json;
using Parkix.Shared.Entities.Uaa;
using System.Collections.Generic;

namespace Parkix.Shared.Entities.Environment
{
    /// <summary>
    /// Predix Vcap Service.
    /// </summary>
    public class CommonPredixVcapServices
    {
        /// <summary>
        /// Gets or sets the application uris.
        /// </summary>
        /// <value>
        /// The application uris.
        /// </value>
        [JsonProperty("application_uris")]
        public List<string> ApplicationUris { get; set; }

        /// <summary>
        /// Gets or sets the predix uaa.
        /// </summary>
        /// <value>
        /// The predix uaa.
        /// </value>
        [JsonProperty("predix-uaa")]
        public List<PredixUaaService> PredixUaa { get; set; }
    }
}
