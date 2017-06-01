using System;
using System.Collections.Generic;
using System.Text;

namespace Parkix.Shared.Entities.Parking
{
    /// <summary>
    /// Represents a parking lot.
    /// </summary>
    public class ParkingLot
    {
        public string LotId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string OldestKey { get; set; }
        public string NewestKey { get; set; }
        public byte[] Image { get; set; }
        public int TotalSpots { get; set; }
    }
}
