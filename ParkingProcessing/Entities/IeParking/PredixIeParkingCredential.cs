using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingProcessing.Entities.IeParking
{
    public class PredixIeParkingCredential
    {
        public string Url { get; set; }

        public string Name { get; set; }

        public PredixIeParkingCredentialZone Zone { get; set; }
    }
}
