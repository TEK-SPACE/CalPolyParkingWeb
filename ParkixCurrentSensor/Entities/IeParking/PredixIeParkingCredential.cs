using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkix.CurrentSensor.Entities.IeParking
{
    /// <summary>
    /// Predix Ie Parking Credential
    /// </summary>
    public class PredixIeParkingCredential
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the zone.
        /// </summary>
        /// <value>
        /// The zone.
        /// </value>
        public PredixIeParkingCredentialZone Zone { get; set; }
    }
}
