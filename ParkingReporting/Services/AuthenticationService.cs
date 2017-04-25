using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

using ParkingReporting.Entities;
using ParkingReporting.Helpers;

namespace ParkingReporting.Services
{
    /// <summary>
    /// Authenticates with Predix services and manages credentials.
    /// </summary>
    public class AuthenticationService
    {
        public static AuthenticationService Instance { get; } = new AuthenticationService();
        private string _token = "";

        private AuthenticationService() { }

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <returns></returns>
        public async Task Initialize()
        {
            bool response = false;
            try
            {
                var uaaService = EnvironmentalService.PredixServices.PredixUaa[0];
                response = await Instance.Login(uaaService);
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log(e);
            }

            PseudoLoggingService.Log("Service has UAA token: " + response.ToString());
            PseudoLoggingService.Log("Service has UAA token: " + AuthenticationService.Instance.GetAuthToken());

        }

        public string GetAuthToken()
        {
            return _token;
        }

        public async Task<bool> Login(PredixUaaService service)
        {
            Dictionary<string, string> dick = new Dictionary<string, string>()
            {
                {"client_id", Config.PredixUaaClientID },
                {"client_secret", Config.PredixUaaClientSecret },
                {"grant_type", "client_credentials" },
                {"response_type", "token" }
            };
            var request = service.Credentials.IssuerId.addParams(dick);

            var response = await ServiceHelpers.SendAync<PredixUaaLoginResonse>(HttpMethod.Post, service: request, methodName: "", request: "");
            
            if (response.Success)
            {
                _token = response.AccessToken;
            }

            return response.Success;

        }
    }
}
