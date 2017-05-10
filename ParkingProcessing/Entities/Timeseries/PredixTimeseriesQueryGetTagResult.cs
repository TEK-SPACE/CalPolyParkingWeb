using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingProcessing.Entities.Timeseries
{
    /// <summary>
    /// Predix Timeseries Query Get Tag Result.
    /// </summary>
    public class PredixTimeseriesQueryGetTagResult
    {
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>
        /// The results.
        /// </value>
        public List<string> Results { get; set; }
    }
}
