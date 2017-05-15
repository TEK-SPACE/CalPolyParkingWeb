using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingProcessing.Entities.Timeseries
{
    /// <summary>
    /// Predix Ie Parking Latest Datapoint Tag Result.
    /// </summary>
    public class PredixTimeseriesQueryLatestDatapointTagResult
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
        public PredixTimeseriesQueryLatestDatapointTagAttributes Attributes { get; set; }
    }
}
