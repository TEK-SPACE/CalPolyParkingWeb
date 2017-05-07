using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using ParkingProcessing.Entities.Parking;
using ParkingProcessing.Services;

namespace ParkingProcessing.Controllers
{
    [Route("api/configure")]
    public class ConfigurationController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "";
        }

        [HttpPost]
        public IActionResult Post([FromBody]ParkingLot data)
        {
            try
            {
                ProcessingService.Instance.AcceptParkingLotData(data);
                PseudoLoggingService.Log("ProcessingController", "Spots " + data.ParkingSpots.First().id + " - " + data.ParkingSpots.Last().id + " accepted.");
                return Json("testing 123!");
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ProcessingController", e);
            }
            
            return BadRequest();
        }
    }
}