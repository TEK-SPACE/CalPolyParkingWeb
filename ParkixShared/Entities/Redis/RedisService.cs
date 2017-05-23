using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkix.Process.Entities.Redis
{
    /// <summary>
    /// Redis Service
    /// </summary>
    public class RedisService
    {
        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        public RedisServiceCredential Credentials { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}
