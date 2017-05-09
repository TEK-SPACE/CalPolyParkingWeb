using ParkingProcessing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ParkingProcessing.Entities.Timeseries;

namespace ParkingProcessing.Services
{
    /// <summary>
    /// Handles connections and ingestion of timeseries data to a Predix Timeseries instance.
    /// </summary>
    public class TimeseriesService
    {
        /// <summary>
        /// Gets the Timeseries service instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static TimeseriesService Instance { get; } = new TimeseriesService();
        private ClientWebSocket _socket;

        private TimeseriesService() { }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns></returns>
        public async Task Initialize()
        {
            await OpenWebSocket();
        }

        /// <summary>
        /// Opens a websocket to the Timeseries database.
        /// </summary>
        /// <returns></returns>
        public async Task<ClientWebSocket> OpenWebSocket()
        {
            _socket = new ClientWebSocket();
            _socket.Options.KeepAliveInterval = TimeSpan.FromHours(1);
            _socket.Options.SetRequestHeader(headerName: "predix-zone-id", headerValue: EnvironmentalService.TimeseriesService.Credentials.Ingest.ZoneHttpHeaderValue);
            _socket.Options.SetRequestHeader(headerName: "authorization", headerValue: "Bearer " + AuthenticationService.GetAuthToken());
            _socket.Options.SetRequestHeader(headerName: "Origin",
                headerValue: "https://" + EnvironmentalService.ApplicationUri);
            
            PseudoLoggingService.Log("TimeseriesService", "Attempting websocket connection...");
            try
            {
                var uri = new Uri(uriString: EnvironmentalService.TimeseriesService.Credentials.Ingest.Uri, uriKind: UriKind.Absolute);
                await _socket.ConnectAsync(uri, cancellationToken: CancellationToken.None);
                PseudoLoggingService.Log("TimeseriesService", "Websocket status: " + _socket.State.ToString());

                return _socket;
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("TimeseriesService", e);
            }

            return null; 
        }

        /// <summary>
        /// Submits a timeseries ingestion payload.
        /// </summary>
        /// <param name="payload"></param>
        private async void IngestData(PredixTimeseriesIngestPayload payload)
        {
            try
            {
                var payloadJSON = JsonConvert.SerializeObject(payload);
                var payloadBytes = Encoding.ASCII.GetBytes(payloadJSON);
                var payloadArraySegment = new ArraySegment<byte>(payloadBytes);
                await _socket.SendAsync(payloadArraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
                PseudoLoggingService.Log("Timeseries Service", "Payload sent!");
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("TimeseriesService", e);
            }
        }

        /// <summary>
        /// Ingests the data.
        /// </summary>
        /// <param name="payload">The payload.</param>
        public void IngestData(List<PredixTimeseriesIngestPayload> payload)
        {
            foreach (PredixTimeseriesIngestPayload load in payload)
            {
                IngestData(load);
            }
        }
    }
}
