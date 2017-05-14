using System;
using ParkingProcessing.Entities.IeParking;
using ParkingProcessing.Entities.Parking;
using ParkingProcessing.Entities.Timeseries;
using ParkingProcessing.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
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

        /// <summary>
        /// The timeseries query base
        /// </summary>
        private static string TimeseriesQueryBase =
            "https://time-series-store-predix.run.aws-usw02-pr.ice.predix.io/v1/";

        private TimeseriesQueryService() { }

        /// <summary>
        /// Gets the lot summary.
        /// </summary>
        /// <param name="lotid">The lotid.</param>
        /// <returns></returns>
        public async Task<ParkingLotSummary> GetLotSummary(string lotid)
        {
            //get all tags
            var allTags = await GetTags();

            //get the ones that are for the specified lot
            var matchingTags = allTags.Where((s => s.Contains(lotid + ":")));

            var request = new PredixIeParkingLatestDatapointRequest()
            {
                Tags = new List<PredixIeParkingLatestDatapointTagNames>()
                {
                    new PredixIeParkingLatestDatapointTagNames()
                    {
                        name = matchingTags.ToList()
                    }
                }
            };

            //get latest datapoints for each parking spot in lot
            var headers = DefaultHeaders();
            var service = TimeseriesQueryBase + "datapoints/latest";
            var latestDatapoints = await ServiceHelpers.SendAync<PredixIeParkingLatestDatapointResult>(HttpMethod.Post, service: service, headers: headers,
                request: request);

            //generate a lot summary from these datapoints
            var lotSummary = new ParkingLotSummary()
            {
                ParkingLotId = lotid
            };
            foreach (var tag in latestDatapoints.Tags)
            {
                //force casting required... ouch
                var values = tag.Results.First().Values[0];

                var timestamp = (long)values[0];
                Boolean.TryParse((string)values[1], out var status);

                var datetime = DataHelpers.UnixTimeStampToDateTime(timestamp);

                if (datetime > lotSummary.NewestTimestamp)
                {
                    lotSummary.NewestTimestamp = datetime;
                }

                if (datetime < lotSummary.OldestTimestamp)
                {
                    lotSummary.OldestTimestamp = datetime;
                }

                lotSummary.ParkingSpotsTotal++;
                lotSummary.ParkingSpotsTaken += status ? 1 : 0;
            }
            lotSummary.ParkingSpotsFree = lotSummary.ParkingSpotsTotal - lotSummary.ParkingSpotsTaken;

            return lotSummary;
        }

        /// <summary>
        /// Gets the lot summary.
        /// </summary>
        /// <param name="lotid">The lotid.</param>
        /// <returns></returns>
        public async Task<ParkingLotDetail> GetLotDetail(string lotid)
        {
            //get all tags
            var allTags = await GetTags();

            //get the ones that are for the specified lot
            var matchingTags = allTags.Where((s => s.Contains(lotid + ":")));

            var request = new PredixIeParkingLatestDatapointRequest()
            {
                Tags = new List<PredixIeParkingLatestDatapointTagNames>()
                {
                    new PredixIeParkingLatestDatapointTagNames()
                    {
                        name = matchingTags.ToList()
                    }
                }
            };

            //get latest datapoints for each parking spot in lot
            var headers = DefaultHeaders();
            var service = TimeseriesQueryBase + "datapoints/latest";
            var latestDatapoints = await ServiceHelpers.SendAync<PredixIeParkingLatestDatapointResult>(HttpMethod.Post, service: service, headers: headers,
                request: request);

            //generate a lot summary from these datapoints
            var lotDetail = new ParkingLotDetail()
            {
                ParkingLotId = lotid,
                Spots = new List<ParkingSpot>()
            };
            foreach (var tag in latestDatapoints.Tags)
            {
                //force casting required... ouch
                var values = tag.Results.First().Values[0];

                var timestamp = (long)values[0];
                Boolean.TryParse((string)values[1], out var status);

                var datetime = DataHelpers.UnixTimeStampToDateTime(timestamp);

                if (datetime > lotDetail.NewestTimestamp)
                {
                    lotDetail.NewestTimestamp = datetime;
                }

                if (datetime < lotDetail.OldestTimestamp)
                {
                    lotDetail.OldestTimestamp = datetime;
                }

                lotDetail.ParkingSpotsTotal++;
                lotDetail.ParkingSpotsTaken += status ? 1 : 0;

                lotDetail.Spots.Add(new ParkingSpot()
                {
                    Id = Regex.Replace(input: tag.name, pattern: "^[a-zA-Z0-9- ]+:", replacement: string.Empty),
                    Status = status
                });

            }
            lotDetail.ParkingSpotsFree = lotDetail.ParkingSpotsTotal - lotDetail.ParkingSpotsTaken;

            return lotDetail;
        }

        /// <summary>
        /// Get tags of all assets in the timeseries.
        /// </summary>
        /// <returns>List of tags</returns>
        public async Task<List<string>> GetTags()
        {
            var headers = DefaultHeaders();

            var service = "https://time-series-store-predix.run.aws-usw02-pr.ice.predix.io/v1/tags";

            var result = await ServiceHelpers.SendAync<PredixTimeseriesQueryGetTagResult>(HttpMethod.Get, service: service, headers: headers);

            return result.Results;
        }

        /// <summary>
        /// Gets the default timeseries request headers
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, string> DefaultHeaders()
        {
            return new Dictionary<string, string>()
            {
                {"predix-zone-id", EnvironmentalService.TimeseriesService.Credentials.Query.ZoneHttpHeaderValue},
                {"Authorization", "Bearer " + AuthenticationService.GetAuthToken()}
            };
        }
    }
}
