using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parkix.Process.Entities;
using Parkix.Shared.Services;

using Parkix.Shared.Entities.Parking;

namespace Parkix.Process.Services
{
    /// <summary>
    /// Processing incoming sensor data from sensors and GE Current APIs
    /// </summary>
    public class ProcessingService
    {
        /// <summary>
        /// The instance of the processing service.
        /// </summary>
        public static ProcessingService Instance = new ProcessingService();
        private List<ParkingLotSnapshot> dataQueue = new List<ParkingLotSnapshot>();

        /// <summary>
        /// Prevents a default instance of the <see cref="ProcessingService"/> class from being created.
        /// </summary>
        private ProcessingService()
        {
        }

        /// <summary>
        /// Accepts the parking lot data.
        /// </summary>
        /// <param name="datapoint">The datapoint.</param>
        public void AcceptParkingLotData(ParkingLotSnapshot datapoint)
        {
            throw new NotImplementedException();
        }
    }
}
