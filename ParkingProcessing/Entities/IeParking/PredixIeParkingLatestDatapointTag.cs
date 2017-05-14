using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingProcessing.Entities.IeParking
{
    /// <summary>
    /// Predix Ie Parking Latest Datapoint Tag.
    /// </summary>
    public class PredixIeParkingLatestDatapointTag
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>
        /// The results.
        /// </value>
        public List<PredixIeParkingLatestDatapointTagResult> Results { get; set; }
    }
}
