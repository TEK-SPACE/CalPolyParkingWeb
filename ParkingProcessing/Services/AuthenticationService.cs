using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

using ParkingProcessing.Entities;
using ParkingProcessing.Helpers;

namespace ParkingProcessing.Services
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
        public static async Task<bool> Initialize()
        {
            var result = false;
            try
            {
                var uaaService = EnvironmentalService.PredixServices.PredixUaa[0];
                result = await Instance.Login(uaaService);
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log(e);
            }
            return result;
        }

        public static string GetAuthToken()
        {
            return Instance._token;
        }

        private async Task<bool> Login(PredixUaaService service)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>()
            {
                {"client_id", Config.PredixUaaClientID },
                {"client_secret", Config.PredixUaaClientSecret },
                {"grant_type", "client_credentials" },
                {"response_type", "token" }
            };
            var request = service.Credentials.IssuerId.addParams(dict);
            var response = await ServiceHelpers.SendAync<PredixUaaLoginResonse>(HttpMethod.Post, service: request, methodName: "", request: "");
            
            if (response.Success)
            {
                _token = response.AccessToken;
                PseudoLoggingService.Log("UAA Access Token Granted.");
            }
            else
            {
                PseudoLoggingService.Log("ERROR: AuthenticationService failed to authenticate.");
            }
            return response.Success;
        }
    }
}
