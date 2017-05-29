using Microsoft.AspNetCore.Mvc;
using Parkix.Process.Services;
using Parkix.Shared.Entities;
using Parkix.Shared.Entities.Parking;
using Parkix.Shared.Entities.Sensor;
using Parkix.Shared.Services;
using System;
using System.Threading.Tasks;

namespace Parkix.Process.Controllers
{
    /// <summary>
    /// Processing incoming sensor data.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api")]
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
        [Route("ingest")]
        public async Task<IActionResult> Post([FromHeader]string authorization, [FromBody]ParkingLotSnapshot data)
        {
            try
            {
                var validToken = await AuthenticationService.Instance.ValidateToken(authorization);

                if (!validToken)
                {
                    return Unauthorized();
                }

                var exists = SystemService.Instance.GetSensor<StaticCustomCameraSensor>(guid: data.SensorId, value: out var record);
                if (!exists)
                {
                    return NotFound();
                }

                ProcessingService.Instance.AcceptSnapshot(snapshot: data);

                var needsConfig = record.ConfigStatus == ConfigurationStatus.Update;

                var response = new ProcessingResponse
                {
                    RequireConfig = needsConfig
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ProcessingController", e);
            }

            return new StatusCodeResult(500);
        }
    }
}