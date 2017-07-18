using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkix.Report.Entities
{
    /// <summary>
    /// A timestamp and numeric double pair
    /// </summary>
    public class TimestampDoubleValuePair
    {
        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the percent full.
        /// </summary>
        /// <value>
        /// The percent full.
        /// </value>
        public double Value { get; set; }
    }
}
