using ParkingProcessing.Entities;
using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ParkingProcessing.Services
{
    public class TimeseriesService
    {
        public static TimeseriesService Instance { get; } = new TimeseriesService();
        private ClientWebSocket _socket;

        private TimeseriesService() { }

        public async Task Initialize()
        {
            await OpenWebSocket();
        }

        public async Task<ClientWebSocket> OpenWebSocket()
        {
            _socket = new ClientWebSocket();
            _socket.Options.SetRequestHeader(headerName: "predix-zone-id", headerValue: EnvironmentalService.PredixServices.PredixTimeSeries.First().Credentials.Ingest.ZoneHttpHeaderValue);
            _socket.Options.SetRequestHeader(headerName: "authorization", headerValue: "Bearer " + AuthenticationService.GetAuthToken());
            _socket.Options.SetRequestHeader(headerName: "Origin", headerValue: "https://" + EnvironmentalService.PredixApplication.ApplicationUris.First());
            
            PseudoLoggingService.Log("Timeseries service: Attempting websocket connection...");
            try
            {
                var uri = new Uri(uriString: EnvironmentalService.PredixServices.PredixTimeSeries[0].Credentials.Ingest.Uri, uriKind: UriKind.Absolute);
                await _socket.ConnectAsync(uri, cancellationToken: CancellationToken.None);
                PseudoLoggingService.Log("Timeseries websocket status: " + _socket.State.ToString());
                return _socket;
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log(e);
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
                var serializedPayload = JsonConvert.SerializeObject(payload);
                var bytePayload = Encoding.ASCII.GetBytes(serializedPayload);
                var arrayPayload = new ArraySegment<byte>(bytePayload);
                await _socket.SendAsync(arrayPayload, WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log(e);
            }
        }
    }
}
