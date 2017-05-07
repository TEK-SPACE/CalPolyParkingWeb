using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingProcessing.Entities;
using ParkingProcessing.Helpers;

using ParkingProcessing.Entities.Parking;

namespace ParkingProcessing.Services
{
    public class ProcessingService
    {
        public static ProcessingService Instance = new ProcessingService();
        private List<ParkingLot> dataQueue = new List<ParkingLot>();

        private ProcessingService()
        {
        }

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
