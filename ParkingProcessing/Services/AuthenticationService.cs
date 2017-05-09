using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
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
        /// <summary>
        /// The instance of the UAA Authentication Service interface.
        /// </summary>
        public static AuthenticationService Instance { get; } = new AuthenticationService(EnvironmentalService.UaaService);
        private PredixUaaService _service;

        /// <summary>
        /// Tracks the Predix UAA Authentication token used by this application instance.
        /// </summary>
        private string _token = "";

        /// <summary>
        /// Used to prevent frequent auth token verifications.
        /// </summary>
        private Timer _verificiationTimer;

        private AuthenticationService(PredixUaaService service)
        {
            _service = service;
        }

        private void SetupVerificationTimer()
        {
            TimerCallback action = new TimerCallback(TokenKeepAlive);
            _verificiationTimer = new Timer(action, null, TimeSpan.FromHours(1).Milliseconds, TimeSpan.FromHours(1).Milliseconds);
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
                SetupVerificationTimer();
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
        public static string GetAuthToken()
        {
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
            var response = await ServiceHelpers.SendAync<PredixUaaLoginResponse>(HttpMethod.Post, service: request, methodName: "", request: "");
            
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

        private async void TokenKeepAlive(object state)
        {
            PseudoLoggingService.Log("AuthenticationService", "Token Keep Alive invoked...");

            var valid = await ValidateToken(_token);

            if (!valid)
            {
                PseudoLoggingService.Log("AuthenticationService", "...token expired. Reauthenticating...");
                await Login();
            }
            else
            {
                PseudoLoggingService.Log("AuthenticationService", "...token is still valid.");
            }
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
