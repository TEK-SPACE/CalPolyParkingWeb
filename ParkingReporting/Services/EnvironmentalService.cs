using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ParkingReporting.Entities;

namespace ParkingReporting.Services
{
    public class EnvironmentalService
    {
        public static PredixVcapServices PredixServices = getPredixVcapServices();
        public static PredixVcapApplication PredixApplication = getPredixVcapApplication();

        private static PredixVcapApplication getPredixVcapApplication()
        {
            PredixVcapApplication vcaps;

            try
            {
                var e = Environment.GetEnvironmentVariables();
                var envVarText = Environment.GetEnvironmentVariable("VCAP_APPLICATION");
                vcaps = JsonConvert.DeserializeObject<PredixVcapApplication>(envVarText);
                return vcaps;
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log(e);
            }

            return null;
        }

        private static PredixVcapServices getPredixVcapServices()
        {
            PredixVcapServices vcaps;

            try
            {
                var envVarText = Environment.GetEnvironmentVariable("VCAP_SERVICES");
                vcaps = JsonConvert.DeserializeObject<PredixVcapServices>(envVarText);
                return vcaps;
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log(e);
            }

            return null;
        }
    }
}
