using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ParkingProcessing.Entities.Environment;
using ParkingProcessing.Entities.Uaa;
using ParkingProcessing.Entities.Timeseries;
using ParkingProcessing.Entities.IeParking;

namespace ParkingProcessing.Services
{
    public static class EnvironmentalService
    {
        private static readonly PredixVcapServices PredixServices = ParsePredixVcapServices();

        /// <summary>
        /// The predix application.
        /// </summary>
        private static readonly PredixVcapApplication PredixApplication = ParsePredixVcapApplication();

        /// <summary>
        /// Gets the uaa service.
        /// </summary>
        /// <value>
        /// The uaa service.
        /// </value>
        public static PredixUaaService UaaService
        {
            get
            {
                if (PredixServices.PredixUaa.Count != 1)
                {
                    PseudoLoggingService.Log("EnvironmentalService", "There is not exactly one UAA service specified.");
                }

                return PredixServices.PredixUaa[0];
            }
        }

        /// <summary>
        /// Gets the timeseries service.
        /// </summary>
        /// <value>
        /// The timeseries service.
        /// </value>
        public static PredixTimeseriesService TimeseriesService
        {
            get
            {
                if (PredixServices.PredixTimeSeries.Count != 1)
                {
                    PseudoLoggingService.Log("EnvironmentalService", "There is not exactly one Timeseries service specified.");
                }

                return PredixServices.PredixTimeSeries[0];
            }
        }

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

        /// <summary>
        /// Gets the primary application URI. Multiple Uris supported but not implemented.
        /// </summary>
        /// <value>
        /// The application URI.
        /// </value>
        public static string ApplicationUri
        {
            get
            {
                if (PredixApplication.ApplicationUris.Count != 1)
                {
                    PseudoLoggingService.Log("EnvironmentalService", "There is not exactly one application uri specified. This may cause request issues.");
                }

                return PredixApplication.ApplicationUris[0];
            }
        }

        /// <summary>
        /// Gets the predix vcap application variable from env vars
        /// </summary>
        /// <returns></returns>
        private static PredixVcapApplication ParsePredixVcapApplication()
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
        private static PredixVcapServices ParsePredixVcapServices()
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
