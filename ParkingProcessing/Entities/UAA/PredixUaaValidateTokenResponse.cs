using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace ParkingProcessing.Entities.UAA
{
    /// <summary>
    /// Predix Uaa Validate Token Response.
    /// </summary>
    public class PredixUaaValidateTokenResponse
    {
        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        [JsonProperty("client_id")]
        public string ClientID { get; set; }
    }
}
