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
    /// <summary>
    /// Processing incoming sensor data.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/processing")]
    public class ProcessingController : Controller
    {
        /// <summary>
        /// Posts the specified parking lot data.
        /// </summary>
        /// <param name="authorization">The authorization.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromHeader]string authorization, [FromBody]ParkingLot data)
        {
            Boolean configRequired = false;
            
            try
            {
                ProcessingService.Instance.AcceptParkingLotData(data);
                PseudoLoggingService.Log("ProcessingController", "Spots " + data.ParkingSpots.First().Id + " - " + data.ParkingSpots.Last().Id + " accepted.");
                return Ok(); //value: configRequired);
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ProcessingController", e);
            }
            
            return BadRequest();
        }
    }
}