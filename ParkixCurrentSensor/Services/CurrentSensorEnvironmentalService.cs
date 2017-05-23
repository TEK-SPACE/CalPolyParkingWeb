using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parkix.Shared.Services;
using Parkix.CurrentSensor.Entities.IeParking;
using Parkix.CurrentSensor.Entities.Environment;

namespace Parkix.CurrentSensor.Services
{
    /// <summary>
    /// Interface to the service environment to fetch configuration information.
    /// </summary>
    public class CurrentSensorEnvironmentalService : CommonEnvironmentalService<CurrentSensorPredixVcapServices>
    {

        /// <summary>
        /// Gets the ie parking service.
        /// </summary>
        /// <value>
        /// The ie parking service.
        /// </value>
        public static PredixIeParkingService IeParkingService
        {
            get
            {
                if (PredixServices.IeParking.Count != 1)
                {
                    PseudoLoggingService.Log("EnvironmentalService", "There is not exactly one Intelligent Environments Parking service specified.");
                }

                return PredixServices.IeParking[0];
            }
        }
    }
}
