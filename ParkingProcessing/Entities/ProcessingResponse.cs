using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingProcessing.Entities.Configuration;

namespace ParkingProcessing.Entities
{
    /// <summary>
    /// Processing Response.
    /// </summary>
    public class ProcessingResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ProcessingResponse"/> is requireconfig.
        /// </summary>
        /// <value>
        ///   <c>true</c> if requireconfig; otherwise, <c>false</c>.
        /// </value>
        public bool RequireConfig { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public List<ParkingSpotConfiguration> Config { get; set; }
    }
}
