using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Parkix.Shared.Helpers
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
        /// <param name="method">The method.</param>
        /// <param name="service">The service.</param>
        /// <param name="request">The request.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="headers">The headers.</param>
        /// <returns></returns>
        public static async Task<T> SendAync<T>(HttpMethod method, string service, object request = null, string methodName = "", Dictionary<string, string> headers = null)
        {
            string requestString = "";

            if (request != null)
            {
                requestString = JsonConvert.SerializeObject(request, GetSerializationSettings());
            }

            string result = await SendAsync(method, service, methodName, requestString, headers);
            return JsonConvert.DeserializeObject<T>(result, GetSerializationSettings());
        }

        /// <summary>
        /// Sends a request with the specified parameters.
        /// </summary>
        /// <param name="methodRequestType">Type of the method request.</param>
        /// <param name="service">The service.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="content">The content.</param>
        /// <param name="headers">The headers.</param>
        /// <returns></returns>
        private static async Task<string> SendAsync(HttpMethod methodRequestType, string service, string methodName,
            string content = "", Dictionary<string,string> headers = null)
        {
            try
            {
                string serviceUri = service + methodName;
                HttpRequestMessage request = new HttpRequestMessage(methodRequestType, serviceUri)
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded")
                };

                if (headers != null)
                {
                    foreach (var key in headers.Keys)
                    {
                        request.Headers.Add(key, headers[key]);
                    }
                }

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
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };
        }
    }
}
