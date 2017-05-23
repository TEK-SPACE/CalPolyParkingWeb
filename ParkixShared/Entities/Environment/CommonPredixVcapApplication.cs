using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Parkix.Shared.Entities.Environment
{
    /// <summary>
    /// Predix Vcap Application.
    /// </summary>
    public class CommonPredixVcapApplication
    {
        /// <summary>
        /// Gets or sets the application uris.
        /// </summary>
        /// <value>
        /// The application uris.
        /// </value>
        [JsonProperty("application_uris")]
        public List<string> ApplicationUris { get; set; }

    }
}
