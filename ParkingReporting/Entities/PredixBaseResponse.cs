using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ParkingReporting.Entities
{
    public class PredixBaseResponse
    {
        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error { get; set; } = "";

        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        /// <value>
        /// The error description.
        /// </value>
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; } = "";

        /// <summary>
        /// Gets a value indicating whether this <see cref="PredixBaseResponse"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success
        {
            get
            {
                return string.IsNullOrWhiteSpace(Error) && string.IsNullOrWhiteSpace(ErrorDescription);
            }
        }
    }
}
