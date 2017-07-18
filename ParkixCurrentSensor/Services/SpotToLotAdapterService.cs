using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parkix.Shared.Entities.Parking;
using Parkix.Shared.Helpers;
using Parkix.Shared.Entities;
using System.Net.Http;
using Parkix.Shared.Services;

namespace Parkix.CurrentSensor.Services
{
    /// <summary>
    /// Adapts individual spots to a parking lot model.
    /// </summary>
    public class SpotToLotAdapterService
    {
        private Dictionary<string, bool> _parkingLot = new Dictionary<string, bool>();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static SpotToLotAdapterService Instance { get; } = new SpotToLotAdapterService();

        /// <summary>
        /// Prevents a default instance of the <see cref="SpotToLotAdapterService"/> class from being created.
        /// </summary>
        private SpotToLotAdapterService()
        {

        }

        /// <summary>
        /// Logs the event.
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <param name="theevent">The theevent.</param>
        public void LogEvent(string sensor, string theevent)
        {
            _parkingLot[sensor] = (theevent == "PKIN");
            SubmitSnapshot();
        }

        /// <summary>
        /// Calulates the spots taken.
        /// </summary>
        /// <returns></returns>
        public short CalulateSpotsTaken()
        {
            var spotsTaken = 0;

            foreach (var value in _parkingLot.Values)
            {
                spotsTaken += (value == true) ? 1 : 0;
            }

            return (short)spotsTaken;
        }

        /// <summary>
        /// Submits the snapshot.
        /// </summary>
        public async void SubmitSnapshot()
        {
            var snapshot = new ParkingLotSnapshot()
            {
                SensorId = CurrentSensorSettings.SensorId,
                SpotsTaken = CalulateSpotsTaken(),
                Timestamp = DateTime.Now
            };

            var headers = new Dictionary<string, string>()
            {
                {"authorization", AuthenticationService.GetAuthToken() }
            };

            PseudoLoggingService.Log("SpotToLotAdapterService", "Logging snapshot of " + snapshot.SpotsTaken + " spots taken.");
            var response = await ServiceHelpers.SendAync<ProcessingResponse>(method: HttpMethod.Post, service: CurrentSensorSettings.ParkixProcessingEndpoint, request: snapshot, headers: headers, isJson: true);

            if (response == null)
            {
                PseudoLoggingService.Log("SpotToLotAdapter", "Processing endpoint did not accept data!");
            }
        }

        /// <summary>
        /// Sets the state.
        /// </summary>
        /// <param name="state">The state.</param>
        public void SetState(Dictionary<string, bool> state)
        {
            foreach (var key in state.Keys)
            {
                _parkingLot[key] = state[key];
            }
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, bool> GetState()
        {
            return _parkingLot;
        }
    }
}
