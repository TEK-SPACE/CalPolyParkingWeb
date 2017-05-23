using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Parkix.Process.Entities;
using Parkix.Process.Entities.Parking;
using Parkix.Process.Services;
using Parkix.Shared.Helpers;
using Parkix.Shared.Services;
using Parkix.Shared.Entities.Parking;

namespace Parkix.Process.Controllers
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
        public async Task<IActionResult> Post([FromHeader]string authorization, [FromBody]ParkingLotSnapshot data)
        {
            var validToken = await AuthenticationService.Instance.ValidateToken(authorization);

            if (!validToken)
            {
                //return Unauthorized();
            }
            
            try
            {
                ProcessingService.Instance.AcceptParkingLotData(data);

                //PseudoLoggingService.Log("ProcessingController", "Spots " + data.ParkingSpots.First().Id + " - " + data.ParkingSpots.Last().Id + " accepted.");

                //var result = SensorConfigurationService.Instance.ServicePassiveConfigurationPolling(data.SensorId, out var response);

                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ProcessingController", e);
            }
            
            return BadRequest();
        }
    }
}