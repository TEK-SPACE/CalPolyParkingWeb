using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingProcessing.Entities.Timeseries
{
    /// <summary>
    /// Predix Timeseries Credential.
    /// </summary>
    public class PredixTimeseriesCredentials
    {
        /// <summary>
        /// Gets or sets the ingest credential.
        /// </summary>
        /// <value>
        /// The ingest.
        /// </value>
        public PredixTimeseriesCredential Ingest { get; set; }

        /// <summary>
        /// Gets or sets the query credential.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public PredixTimeseriesCredential Query { get; set; }
        
    }
}
