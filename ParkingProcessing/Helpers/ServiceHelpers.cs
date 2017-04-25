using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;

namespace ParkingProcessing.Helpers
{
    /// <summary>
    /// Shared service helpers.
    /// </summary>
    public static class ServiceHelpers
    {
        private static HttpClient _client = new HttpClient();

        /// <summary>
        /// Inquires the specified service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service">The service.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="request">The request object.</param>
        /// <returns></returns>
        public static async Task<T> SendAync<T>(HttpMethod method, string service, string methodName, object request)
        {
            string json = JsonConvert.SerializeObject(request, GetSerializationSettings());
            string result = await SendAsync(method, service, methodName, json);
            return JsonConvert.DeserializeObject<T>(result, GetSerializationSettings());
        }

        /// <summary>
        /// WCFs the inquiry asynchronous.
        /// </summary>
        /// <param name="methodRequestType">Type of the method request.</param>
        /// <param name="service">The service.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="content">The content.</param>
        /// <returns>The result of the inquiry</returns>
        private static async Task<string> SendAsync(HttpMethod methodRequestType, string service, string methodName,
            string content = "")
        {
            try
            {
                string serviceUri = service + methodName;
                HttpRequestMessage request = new HttpRequestMessage(methodRequestType, serviceUri)
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded")
                };

                HttpResponseMessage response = await _client.SendAsync(request);
                string returnString = await response.Content.ReadAsStringAsync();
                return returnString;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the serialization settings.
        /// </summary>
        /// <returns>The Serialization Settings</returns>
        private static JsonSerializerSettings GetSerializationSettings()
        {
            return new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };
        }
    }
}
