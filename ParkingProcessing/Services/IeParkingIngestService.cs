using ParkingProcessing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ParkingProcessing.Entities.IeParking;
using ParkingProcessing.Entities.Uaa;
using ParkingProcessing.Helpers;

namespace ParkingProcessing.Services
{
    /// <summary>
    /// Used to ingest data from GE's Intelligent Environment Parking simulators.
    /// </summary>
    public class IeParkingIngestService
    {
        public static IeParkingIngestService Instance { get; } = new IeParkingIngestService();
        private ClientWebSocket _socket;
        private List<PredixIeParkingAsset> _availableAssets;
        private Task _streamTask;

        private IeParkingIngestService()
        {
        }

        /// <summary>
        /// Queries IE for available assets within the given coordinates.
        /// </summary>
        /// <param name="coordinateOne">The northwestern coordinate (lat,long)</param>
        /// <param name="coordinateTwo">The southeastern coordinate (lat,long)</param>
        /// <returns> the ids of the available sensors.</returns>
        public async Task<List<string>> FindAssets(double latitudeOne, double longitudeOne, double latitudeTwo, double longitudeTwo)
        {
            var request = EnvironmentalService.IeParkingService.Credentials.Url + "/v1/assets/search?";
            request += "bbox=" + latitudeOne + ":" + longitudeOne + ", " + latitudeTwo + ":" + longitudeTwo;

            List<Tuple<string, string>> headers = new List<Tuple<string, string>>();
            headers.Add(new Tuple<string, string>("predix-zone-id", EnvironmentalService.IeParkingService.Credentials.Zone.HttpHeaderValue));
            headers.Add(new Tuple<string, string>("Authorization", "Bearer " + await AuthenticationService.GetAuthToken()));

            var result = await ServiceHelpers.SendAync<PredixIeParkingAssetSearchResult>(HttpMethod.Get, service: request,
                request: "", methodName: "", headers: headers);

            _availableAssets = result.Embedded.Assets;

            List<string> ids = _availableAssets.Select((asset => asset.DeviceId)).ToList();
            return ids;
        }

        /// <summary>
        /// Opens an ingestion websocket to the specified deviceid's live event stream.
        /// </summary>
        /// <param name="deviceid">The deviceid.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<ClientWebSocket> OpenConnection(string deviceid)
        {
            throw new NotImplementedException();

            _socket = new ClientWebSocket();
            _socket.Options.KeepAliveInterval = TimeSpan.FromHours(1);
            _socket.Options.SetRequestHeader(headerName: "predix-zone-id", headerValue: EnvironmentalService.IeParkingService.Credentials.Zone.HttpHeaderValue);
            _socket.Options.SetRequestHeader(headerName: "authorization", headerValue: "Bearer " + await AuthenticationService.GetAuthToken());
            _socket.Options.SetRequestHeader(headerName: "Origin", headerValue: "https://" + EnvironmentalService.ApplicationUri);
            
            PseudoLoggingService.Log("IeParkingIngestService", "Attempting websocket connection...");
            try
            {
                var uri = new Uri(uriString: EnvironmentalService.TimeseriesService.Credentials.Ingest.Uri, uriKind: UriKind.Absolute);
                await _socket.ConnectAsync(uri, cancellationToken: CancellationToken.None);
                PseudoLoggingService.Log("IeParkingIngestService", "Websocket status: " + _socket.State.ToString());

                return _socket;
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("IeParkingIngestService", e);
            }

            return null; 
        }

        /// <summary>
        /// Accepts sensor data from the live stream until the websocket closes.
        /// </summary>
        /// <returns></returns>
        public async Task AcceptLiveStream()
        {
            while (_socket.State == WebSocketState.Open)
            {
                var result = "sdads"; //await _socket.ReceiveAsync()
            }
            
        }
    }
}
