using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingProcessing.Entities;
using ParkingProcessing.Helpers;

namespace ParkingProcessing.Services
{
    public class DifferentialProcessingService
    {
        public static DifferentialProcessingService Instance; //= new DifferentialProcessingService();
        private ConcurrentDictionary<string, ParkingLot> snapshot;

        private DifferentialProcessingService()
        {
        }

        /// <summary>
        /// accepts parking lot data.
        /// If there are any changes in parking spot status, the affected spots are updated in timseries.
        /// </summary>
        /// <param name="datapoint"></param>
        public void AcceptParkingLotData(ParkingLot parkingLotData)
        {
            throw new NotImplementedException();

            List<PredixTimeseriesIngestPayload> payloads;

            if (!snapshot.ContainsKey(parkingLotData.ParkingLotId))
            {
                snapshot[parkingLotData.ParkingLotId] = parkingLotData;
                payloads =
                    DataHelpers.ParkingSpotDataToPredixTimeseriesIngestPayloads(parkingLotData.ParkingSpots,
                        parkingLotData.Timestamp);
            }
            else
            {
                foreach (ParkingSpot spot in snapshot[parkingLotData.ParkingLotId].ParkingSpots)
                {
                    
                }
                
            }
        }
    }
}
