using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Parkix.Process.Entities.Uaa
{
    /// <summary>
    /// Predix Uaa Login Reponse.
    /// </summary>
    /// <seealso cref="Parkix.Process.Entities.PredixBaseResponse" />
    public class PredixUaaLoginResponse : PredixBaseResponse
    {

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the expires in.
        /// </summary>
        /// <value>
        /// The expires in.
        /// </value>
        public int ExpiresIn { get; set; }
    }
}
