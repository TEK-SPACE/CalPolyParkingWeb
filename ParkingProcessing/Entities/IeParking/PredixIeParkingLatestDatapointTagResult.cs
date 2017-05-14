using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingProcessing.Entities.IeParking
{
    /// <summary>
    /// Predix Ie Parking Latest Datapoint Tag Result.
    /// </summary>
    public class PredixIeParkingLatestDatapointTagResult
    {
        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public List<List<object>> Values { get; set; }

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public PredixIeParkingLatestDatapointTagAttributes Attributes { get; set; }
    }
}
