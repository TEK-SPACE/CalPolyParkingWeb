using Newtonsoft.Json;
using Parkix.Shared.Entities.Environment;
using Parkix.Shared.Entities.Uaa;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Parkix.Shared.Services
{
    /// <summary>
    /// Interface to the service environment to fetch configuration information.
    /// </summary>
    public class CommonEnvironmentalService<T> where T : CommonPredixVcapServices
    {
        protected static readonly T PredixServices = ParsePredixVcapServices();

        /// <summary>
        /// The predix application.
        /// </summary>
        protected static readonly CommonPredixVcapApplication PredixApplication = ParsePredixVcapApplication();

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
        private static CommonPredixVcapApplication ParsePredixVcapApplication()
        {
            try
            {
                var vcapApplicationText = Environment.GetEnvironmentVariable("VCAP_APPLICATION");
                var vcaps = JsonConvert.DeserializeObject<CommonPredixVcapApplication>(vcapApplicationText);
                return vcaps;
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("EnvironmentalService", e);
                throw new InvalidProgramException();
            }
        }

        /// <summary>
        /// gets the predix services from the env vars
        /// </summary>
        /// <returns></returns>
        private static T ParsePredixVcapServices()
        {
            try
            {
                var vcapServicesText = Environment.GetEnvironmentVariable("VCAP_SERVICES");
                var vcaps = JsonConvert.DeserializeObject<T>(vcapServicesText);
                return vcaps;
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("EnvironmentalService", e);
                throw new InvalidProgramException();
            }
        }
    }
}
