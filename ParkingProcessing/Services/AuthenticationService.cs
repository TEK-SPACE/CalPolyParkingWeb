using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

using ParkingProcessing.Entities.Uaa;
using ParkingProcessing.Entities.UAA;
using ParkingProcessing.Helpers;

namespace ParkingProcessing.Services
{
    /// <summary>
    /// Authenticates with Predix services and manages credentials.
    /// </summary>
    public class AuthenticationService
    {
        public static AuthenticationService Instance { get; } = new AuthenticationService(EnvironmentalService.UaaService);
        private PredixUaaService _service;
        private string _token = "";

        private AuthenticationService(PredixUaaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Initialize()
        {
            var result = false;
            try
            {
                result = await Instance.Login();
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("AuthenticationService", e);
            }
            return result;
        }

        /// <summary>
        /// Gets the authentication token. Assumes this cannot fail.
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetAuthToken()
        {
            var valid = await Instance.ValidateToken(Instance._token);

            if (!valid)
            {
                await Instance.Login();
            }

            return Instance._token;
        }

        private async Task<bool> Login()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>()
            {
                {"client_id", Config.PredixUaaClientID },
                {"client_secret", Config.PredixUaaClientSecret },
                {"grant_type", "client_credentials" },
                {"response_type", "token" }
            };
            var request = _service.Credentials.IssuerId.AddCURLParams(dict);
            var response = await ServiceHelpers.SendAync<PredixUaaLoginResonse>(HttpMethod.Post, service: request, methodName: "", request: "");
            
            if (response.Success)
            {
                _token = response.AccessToken;
                PseudoLoggingService.Log("AuthenticationService", "UAA Access Token Granted.");
            }
            else
            {
                PseudoLoggingService.Log("AuthenticationService", "ERROR: AuthenticationService failed to authenticate.");
            }
            return response.Success;
        }

        /// <summary>
        /// Validates the token.
        /// </summary>
        /// <returns>Whether the token is valid or not</returns>
        private async Task<bool> ValidateToken(string token)
        {
            Dictionary<string, string> args = new Dictionary<string, string>()
            {
                {"authorization", "Bearer" + token}
            };

            var request = _service.Credentials.Uri + "/check_token";
            request = request.AddCURLParams(args);
            var response = await ServiceHelpers.SendAync<PredixUaaValidateTokenResponse>(HttpMethod.Post, service: request, methodName: "", request: "");

            return response.Error == null;
        }
    }
}
