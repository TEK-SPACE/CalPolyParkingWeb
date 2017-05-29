using System;
using System.Collections.Generic;
using System.Text;

namespace Parkix.Shared.Entities.Sensor
{
    /// <summary>
    /// Identifies a statically-tracked entity with four sides.
    /// </summary>
    public class StaticEntityCoordinateSet
    {
        /// <summary>
        /// Identifies the entity.
        /// (using spot id to avoid contract change, fix later)
        /// </summary>
        public int spotid { get; set; }

        /// <summary>
        /// The coordinates for the entity.
        /// </summary>
        public CoordinateSet coordinates { get; set; }
    }
}
