using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingProcessing.Entities;
using ParkingProcessing.Helpers;

namespace ParkingProcessing.Services
{
    /// <summary>
    /// Manages configuration requests for custom sensors.
    /// </summary>
    public class ConfigurationService
    {
        /// <summary>
        /// The instance of the Configuration Service instance.
        /// </summary>
        public static ConfigurationService Instance = new ConfigurationService();
    }
}
