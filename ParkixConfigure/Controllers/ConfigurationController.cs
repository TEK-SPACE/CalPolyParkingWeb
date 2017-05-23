using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using Parkix.Processing.Entities.Parking;
using Parkix.Processing.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Parkix.Processing.Entities.Configuration;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Parkix.Processing.Entities.Sensor;

namespace Parkix.Processing.Controllers
{
    
    /// <summary>
    /// Report system status
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller"/>
    [Route("api/configuration")]
    public class ConfigurationController : Controller
    {

        /// <summary>
        /// Gets a list of the available sensors.
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("sensors")]
        public async Task<IActionResult> GetSensors([FromHeader] string authorization)
        {
            try
            {
                var validToken = await AuthenticationService.Instance.ValidateToken(authorization);

                if (!validToken)
                {
                    //return Unauthorized();
                }

                var sensors = SensorLotDatabaseService.Instance.GetAllSensorLotMappingRecords();
                return Ok(sensors);
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ReportingController", e);
            }

            return StatusCode(statusCode: 500);
        }

        /// <summary>
        /// Gets the lot summary.
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="guid"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("sensor/{guid}/config")]
        public async Task<IActionResult> PostSensorConfig([FromHeader] string authorization, string guid, [FromBody] List<ParkingSpotConfiguration> configuration)
        {
            var validToken = await AuthenticationService.Instance.ValidateToken(authorization);

            if (!validToken)
            {
                //return Unauthorized();
            }

            try
            {
                SensorConfigurationService.Instance.UpdateSensorConfiguration(guid, configuration);
                return Accepted();
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ConfigurationController", e);
            }

            return BadRequest();
        }

        /// <summary>
        /// Gets the sensor configuration.
        /// </summary>
        /// <param name="authorization">The authorization.</param>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("sensor/{guid}/config")]
        public async Task<IActionResult> GetSensorConfig([FromHeader] string authorization, string guid)
        {
            var validToken = await AuthenticationService.Instance.ValidateToken(authorization);

            if (!validToken)
            {
                //return Unauthorized();
            }
            
            try
            {
                var result = SensorConfigurationService.Instance.GetSensorConfiguration(guid, out var configuration);

                if (result)
                {
                    return Ok(configuration);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ConfigurationController", e);
            }

            return BadRequest();
        }

        /// <summary>
        /// Gets the sensor image.
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("sensor/{guid}/image")]
        public async Task<IActionResult> GetSensorImage([FromHeader] string authorization, string guid)
        {
            var validToken = await AuthenticationService.Instance.ValidateToken(authorization);

            if (!validToken)
            {
                //return Unauthorized();
            }

            try
            {
                var found = SensorConfigurationService.Instance.GetSensorPhoto(guid, out var photoBytes);

                if (found)
                {
                    return File(photoBytes, "image/jpeg");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ConfigurationController", e);
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("sensor/{guid}/image")]
        public async Task<IActionResult> PutSensorImage([FromHeader] string authorization, string guid, IFormFile file)
        {
            var validToken = await AuthenticationService.Instance.ValidateToken(authorization);

            if (!validToken)
            {
                //return Unauthorized();
            }

            try
            {
                var stream = file.OpenReadStream();
                var data = new MemoryStream();
                stream.CopyTo(data);

                SensorConfigurationService.Instance.SetSensorPhoto(guid, data.ToArray());

                return Ok(
                    "https://cp-parking-process-api.run.aws-usw02-pr.ice.predix.io/api/configuration/sensor/" + guid +
                    "/image");
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ConfigurationController", e);
            }

            return BadRequest();
        }


        /// <summary>
        /// Add a header to help the UI team out.
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            base.OnActionExecuted(context);
        }
    }
}