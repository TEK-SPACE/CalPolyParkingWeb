using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingProcessing.Entities;
using ParkingProcessing.Helpers;

using ParkingProcessing.Entities.Parking;

namespace ParkingProcessing.Services
{
    /// <summary>
    /// Processing incoming sensor data from sensors and GE Current APIs
    /// </summary>
    public class ProcessingService
    {
        /// <summary>
        /// The instance of the processing service.
        /// </summary>
        public static ProcessingService Instance = new ProcessingService();
        private List<ParkingLot> dataQueue = new List<ParkingLot>();

        /// <summary>
        /// Prevents a default instance of the <see cref="ProcessingService"/> class from being created.
        /// </summary>
        private ProcessingService()
        {
        }

        /// <summary>
        /// Accepts the parking lot data.
        /// </summary>
        /// <param name="datapoint">The datapoint.</param>
        public void AcceptParkingLotData(ParkingLot datapoint)
        {
            dataQueue.Add(datapoint);

            if (dataQueue.Count() > 10)
            {
                var payloads = DataHelpers.ParkingLotDataToPredixTimeseriesIngestPayload(dataQueue);
                PseudoLoggingService.Log("ProcessingService", "Timeseries payloads generated: " + payloads.Count.ToString());
                TimeseriesService.Instance.IngestData(payloads);
                dataQueue.Clear();
            }
        }
    }
}
