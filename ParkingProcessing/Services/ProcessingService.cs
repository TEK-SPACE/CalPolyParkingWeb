using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingProcessing.Entities;

namespace ParkingProcessing.Services
{
    public class ProcessingService
    {
        public static ProcessingService Instance = new ProcessingService();
        private List<ParkingLotData> dataQueue;

        private ProcessingService()
        {
        }

        public void acceptParkingLotData(ParkingLotData datapoint)
        {
            dataQueue.Add(datapoint);

            if (dataQueue.Count() > 10)
            {
                var payload = 
            }
        }


    }
}
