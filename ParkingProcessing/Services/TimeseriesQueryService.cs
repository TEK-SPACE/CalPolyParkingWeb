using ParkingProcessing.Entities.Timeseries;
using ParkingProcessing.Helpers;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace ParkingProcessing.Services
{
    /// <summary>
    /// Handles connections and ingestion of timeseries data to a Predix Timeseries instance.
    /// </summary>
    public class TimeseriesQueryService
    {
        /// <summary>
        /// Gets the Timeseries service instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static TimeseriesQueryService Instance { get; } = new TimeseriesQueryService();

        private TimeseriesQueryService() { }

        /// <summary>
        /// Get tags of all assets in the timeseries.
        /// </summary>
        /// <returns>List of tags</returns>
        public async Task<List<string>> GetTags()
        {
            var headers = new Dictionary<string, string>()
            {
                {"predix-zone-id", EnvironmentalService.TimeseriesService.Credentials.Query.ZoneHttpHeaderValue},
                {"Authorization", "Bearer " + AuthenticationService.GetAuthToken()}
            };

            var service = "https://time-series-store-predix.run.aws-usw02-pr.ice.predix.io/v1/tags";

            var result = await ServiceHelpers.SendAync<PredixTimeseriesQueryGetTagResult>(HttpMethod.Get, service: service, headers: headers);

            return result.Results;
        }
    }
}
