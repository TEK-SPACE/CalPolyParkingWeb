using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parkix.Process.Entities.Uaa
{
    /// <summary>
    /// Predix Uaa Zone.
    /// </summary>
    public class PredixUaaZone
    {
        /// <summary>
        /// Gets or sets the HTTP header value.
        /// </summary>
        /// <value>
        /// The HTTP header value.
        /// </value>
        [JsonProperty("http-header-value")]
        public string  HttpHeaderValue { get; set; }

        /// <summary>
        /// Gets or sets the name of the HTTP header.
        /// </summary>
        /// <value>
        /// The name of the HTTP header.
        /// </value>
        [JsonProperty("http-header-name")]
        public string HttpHeaderName { get; set; }
    }
}
