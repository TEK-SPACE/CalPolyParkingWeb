using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParkingProcessing.Entities;
using ParkingProcessing.Entities.Parking;
using ParkingProcessing.Services;
using ParkingProcessing.Entities.IeParking;
using ParkingProcessing.Helpers;

namespace ParkingProcessing.Controllers
{
    /// <summary>
    /// Processing incoming sensor data.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/processing")]
    [ProducesResponseType(typeof(ProcessingResponse), 200)]
    public class ProcessingController : Controller
    {
        /// <summary>
        /// Posts the specified parking lot data.
        /// </summary>
        /// <param name="authorization">The authorization.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromHeader]string authorization, [FromBody]ParkingLot data)
        {
            var validToken = await AuthenticationService.Instance.ValidateToken(authorization);

            if (!validToken)
            {
                //return Unauthorized();
            }
            
            try
            {
                ProcessingService.Instance.AcceptParkingLotData(data);

                PseudoLoggingService.Log("ProcessingController", "Spots " + data.ParkingSpots.First().Id + " - " + data.ParkingSpots.Last().Id + " accepted.");

                var configresult = ConfigurationService.Instance.ServicePassiveConfigurationPolling(data.SensorId);

                return Ok(configresult); //value: configRequired);
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ProcessingController", e);
            }
            
            return BadRequest();
        }
    }
}