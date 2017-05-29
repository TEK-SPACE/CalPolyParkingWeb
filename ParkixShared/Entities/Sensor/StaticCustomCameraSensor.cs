using System;
using System.Collections.Generic;
using System.Text;

namespace Parkix.Shared.Entities.Sensor
{
    /// <summary>
    /// A camera-implemented sensor that tracks entities statically.
    /// </summary>
    public class StaticCustomCameraSensor : Sensor
    {
        public List<StaticEntityCoordinateSet> coordinates { get; set; }

        public byte[] Image { get; set; }
    }
}
