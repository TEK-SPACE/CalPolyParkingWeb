using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Parkix.Process.Entities.Timeseries
{
    /// <summary>
    /// Predix Timeseries Service
    /// </summary>
    public class PredixTimeseriesService
    {
        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        public PredixTimeseriesCredentials Credentials { get; set; }
    }
}
