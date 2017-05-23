using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

using Parkix.Process.Entities.Timeseries;
using Parkix.Process.Entities.Uaa;
using Parkix.Process.Entities.Redis;

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
        /// Gets or sets the predix time series.
        /// </summary>
        /// <value>
        /// The predix time series.
        /// </value>
        [JsonProperty("predix-timeseries")]
        public List<PredixTimeseriesService> PredixTimeSeries { get; set; }

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
