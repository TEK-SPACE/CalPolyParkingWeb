using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Parkix.CurrentSensor.Entities.IeParking
{
    /// <summary>
    /// Predix Ie Parking Credential Zone
    /// </summary>
    public class PredixIeParkingCredentialZone
    {
        /// <summary>
        /// Gets or sets the name of the HTTP header.
        /// </summary>
        /// <value>
        /// The name of the HTTP header.
        /// </value>
        [JsonProperty("http-header-name")]
        public string HttpHeaderName { get; set; }

        /// <summary>
        /// Gets or sets the HTTP header value.
        /// </summary>
        /// <value>
        /// The HTTP header value.
        /// </value>
        [JsonProperty("http-header-value")]
        public string HttpHeaderValue { get; set; }

        /// <summary>
        /// Gets or sets the o authentication scope.
        /// </summary>
        /// <value>
        /// The o authentication scope.
        /// </value>
        [JsonProperty("oauth-scope")]
        public string OAuthScope { get; set; }
    }
}
