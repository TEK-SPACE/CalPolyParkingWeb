using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using ParkingReporting.Entities;

namespace ParkingReporting.Controllers
{
    [Route("api/processing")]
    public class ProcessingController : Controller
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return "";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]ParkingLotData data)
        {
            try
            {
                PseudoLoggingService.Log("Spots " + data.ParkingSpots.First().id + " - " + data.ParkingSpots.Last().id + " accepted.");
                return Ok();
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log(e);
            }

            return BadRequest();
        }
    }
}