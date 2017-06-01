using System;
using System.Collections.Generic;
using System.Text;
using Parkix.Shared.Entities.Configure;
using Parkix.Shared.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Parkix.Shared.Entities.Sensor;
using Parkix.Shared.Entities.Parking;

namespace Parkix.Tests
{
    [TestClass]
    public class SystemInit
    {
        [TestMethod]
        public void CreateSystem()
        {
            var system = new ParkixSystem()
            {
                Name = "Cal Poly Parkix",
                ParkingLots = new List<string>(),
                Sensors = new List<string>()
            };

            var sensor = new StaticCustomCameraSensor()
            {
                GUID = "3D2F63B1-AA1E-4C5A-A4B2-698F2ADDC2BB",
                SensorType = "CUSTOM",
                ConfigStatus = ConfigurationStatus.None
            };

            var serialized = JsonConvert.SerializeObject(sensor);
        }

        [TestMethod]
        public void PostSensorConfig()
        {
            var config = new List<StaticEntityCoordinateSet>()
            {
                
            };
            
            for (int i = 0; i < 10; i++)
            {
                config.Add(new StaticEntityCoordinateSet()
                {
                    spotid = i,
                    coordinates = new CoordinateSet()
                    {
                        X1 = i,
                        Y1 = i,
                        X2 = i,
                        Y2 = i,
                        X3 = i,
                        Y3 = i,
                        Y4 = i,
                        X4 = i
                    }
                });
            }

            var serialized = JsonConvert.SerializeObject(config);
        }

        [TestMethod]
        public void CreateParkingLot()
        {
            var lot = new ParkingLot()
            {
                LotId = "GE1",
                Latitude = 35.304343,
                Longitude = -120.663233
            };

            var serialized = JsonConvert.SerializeObject(lot);
        }
    }
}
