using Parkix.Process.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Parkix.CurrentSensor.Entities.IeParking;
using Parkix.Shared.Entities.Uaa;
using Parkix.Shared.Services;
using Parkix.Shared.Helpers;
using Parkix.CurrentSensor.Services;

namespace Parkix.Process.Services
{
    /// <summary>
    /// Used to ingest data from GE's Intelligent Environment Parking simulators.
    /// </summary>
    public class IeParkingIngestService
    {
        /// <summary>
        /// The instance of the Intelligent Environment Parking ingest service interface.
        /// </summary>
        public static IeParkingIngestService Instance { get; } = new IeParkingIngestService();
        private ConcurrentDictionary<string, Tuple<ClientWebSocket, Task>> _sockets = new ConcurrentDictionary<string, Tuple<ClientWebSocket, Task>>();
        private List<PredixIeParkingAsset> _availableAssets;

        private static string SIMULATEDLOTNAME = "GE1";

        private IeParkingIngestService()
        {
        }

        /// <summary>
        /// Initialize the ingestion service.
        /// </summary>
        /// <returns></returns>
        public async Task Initialize()
        {
            //coordinates for San Diego area
            var list = await IeParkingIngestService.Instance.FindAssets(latitudeOne: 32.955702, longitudeOne: -117.476807,
                latitudeTwo: 32.535005, longitudeTwo: -116.879700);

            PseudoLoggingService.Log("IEParking", "the following assets have been found:");
            foreach (string asset in list)
            {
                PseudoLoggingService.Log("IEParking", asset);
                await IeParkingIngestService.Instance.OpenConnection(asset);
            }
        }

        /// <summary>
        /// Queries IE for available assets within the given coordinates.
        /// </summary>
        /// <param name="latitudeOne">The latitude one.</param>
        /// <param name="longitudeOne">The longitude one.</param>
        /// <param name="latitudeTwo">The latitude two.</param>
        /// <param name="longitudeTwo">The longitude two.</param>
        /// <returns></returns>
        public async Task<List<string>> FindAssets(double latitudeOne, double longitudeOne, double latitudeTwo, double longitudeTwo)
        {
            var request = CurrentSensorEnvironmentalService.IeParkingService.Credentials.Url + "/v1/assets/search?";
            request += "size=100&event-type=PKIN,PKOUT&bbox=" + latitudeOne + ":" + longitudeOne + ", " + latitudeTwo + ":" + longitudeTwo;

            var headers = new Dictionary<string, string>()
            {
                {"predix-zone-id", CurrentSensorEnvironmentalService.IeParkingService.Credentials.Zone.HttpHeaderValue},
                {"Authorization", "Bearer " + AuthenticationService.GetAuthToken()}
            };

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
        public async Task OpenConnection(string deviceid)
        {
            try
            {
                var websocketAddress = await GetLiveEventWebsocketAddress(deviceid, "PKIN,PKOUT");
                
                var socket = new ClientWebSocket();
                socket.Options.KeepAliveInterval = TimeSpan.FromHours(1);
                socket.Options.SetRequestHeader(headerName: "predix-zone-id", headerValue: CurrentSensorEnvironmentalService.IeParkingService.Credentials.Zone.HttpHeaderValue);
                socket.Options.SetRequestHeader(headerName: "authorization", headerValue: "Bearer " + AuthenticationService.GetAuthToken());
                socket.Options.SetRequestHeader(headerName: "Origin", headerValue: "https://" + CurrentSensorEnvironmentalService.ApplicationUri);

                PseudoLoggingService.Log("IeParkingIngestService", "Attempting websocket connection to device " + deviceid + "...");
                var uri = new Uri(uriString: websocketAddress, uriKind: UriKind.Absolute);
                await socket.ConnectAsync(uri, cancellationToken: CancellationToken.None);
                PseudoLoggingService.Log("IeParkingIngestService", deviceid + " Websocket status: " + socket.State.ToString());

                if (socket.State == WebSocketState.Open)
                {
                    throw new NotImplementedException();
                    //_sockets[deviceid] = new Tuple<ClientWebSocket, Task>(socket, IngestLoop(deviceid: deviceid, socket: socket));
                }
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("IeParkingIngestService", e);
            }
        }
        

        /// <summary>
        /// Gets the live event websocket address for the given device's event stream.
        /// </summary>
        /// <param name="deviceid">The deviceid.</param>
        /// <param name="events">The event types to subscribe to.</param>
        /// <returns></returns>
        public async Task<string> GetLiveEventWebsocketAddress(string deviceid, string events)
        {
            //get the record for the specified device
            var asset = _availableAssets.First((ass => ass.DeviceId == deviceid));

            //build the CURL command
            var serviceAddress = asset.Links.Self.Href.Replace(oldValue: "http://", newValue: "https://") + "/live-events?event-types=" + events;

            //add required headers
            var headers = new Dictionary<string, string>()
            {
                {"predix-zone-id", CurrentSensorEnvironmentalService.IeParkingService.Credentials.Zone.HttpHeaderValue},
                {"Authorization", "Bearer " + AuthenticationService.GetAuthToken()}
            };

            //send request
            var socketAddress = await ServiceHelpers.SendAync<PredixIeParkingAssetWebsocketResponse>(HttpMethod.Get,
                service: serviceAddress, headers: headers);

            return socketAddress.Url;
        }
    }
}
