using Newtonsoft.Json;
using Parkix.Shared.Entities.Redis;
using Parkix.Shared.Entities.Uaa;
using System.Collections.Generic;

namespace Parkix.Process.Entities.Environment
{
    /// <summary>
    /// Predix Vcap Service.
    /// </summary>
    public class PredixVcapServices
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

        /// <summary>
        /// Gets or sets the redis11.
        /// </summary>
        /// <value>
        /// The redis11.
        /// </value>
        [JsonProperty("redis-11")]
        public List<RedisService> Redis11 { get; set; }
    }
}
