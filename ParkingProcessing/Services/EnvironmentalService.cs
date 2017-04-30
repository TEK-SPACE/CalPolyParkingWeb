using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ParkingProcessing.Entities;

namespace ParkingProcessing.Services
{
    public class EnvironmentalService
    {
        public static readonly PredixVcapServices PredixServices = parsePredixVcapServices();
        public static readonly PredixVcapApplication PredixApplication = parsePredixVcapApplication();

        /// <summary>
        /// Gets the predix vcap application variable from env vars
        /// </summary>
        /// <returns></returns>
        private static PredixVcapApplication parsePredixVcapApplication()
        {
            try
            {
                var vcapApplicationText = Environment.GetEnvironmentVariable("VCAP_APPLICATION");
                var vcaps = JsonConvert.DeserializeObject<PredixVcapApplication>(vcapApplicationText);
                return vcaps;
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("EnvironmentalService", e);
            }

            return null;
        }

        /// <summary>
        /// gets the predix services from the env vars
        /// </summary>
        /// <returns></returns>
        private static PredixVcapServices parsePredixVcapServices()
        {
            try
            {
                var vcapServicesText = Environment.GetEnvironmentVariable("VCAP_SERVICES");
                var vcaps = JsonConvert.DeserializeObject<PredixVcapServices>(vcapServicesText);
                return vcaps;
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("EnvironmentalService", e);
            }

            return null;
        }
    }
}
