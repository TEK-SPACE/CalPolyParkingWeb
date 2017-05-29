using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkix.Shared.Entities.Configure
{
    /// <summary>
    /// The Parkix System as a whole.
    /// </summary>
    public class ParkixSystem
    {
        /// <summary>
        /// The name of the system.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A collection of all registered parking lots.
        /// </summary>
        public IList<string> ParkingLots { get; set; }

        /// <summary>
        /// A collection of all registered sensors.
        /// </summary>
        public IList<string> Sensors { get; set; }
    }
}
