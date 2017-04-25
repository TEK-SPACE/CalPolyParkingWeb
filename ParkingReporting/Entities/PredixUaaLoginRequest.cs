using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ParkingReporting.Entities
{
    public class PredixUaaLoginRequest
    {
        /// <summary>
        /// Gets or sets the type of the response.
        /// </summary>
        /// <value>
        /// The type of the response.
        /// </value>
        [JsonProperty("response_type")]
        public string ResponseType { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the type of the grant.
        /// </summary>
        /// <value>
        /// The type of the grant.
        /// </value>
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        /// <value>
        /// The client secret.
        /// </value>
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

    }
}
