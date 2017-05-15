using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using ParkingProcessing.Entities.Parking;
using ParkingProcessing.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using ParkingProcessing.Entities.Configuration;
using System.Net.Http;
using ParkingProcessing.Entities.Sensor;

namespace ParkingProcessing.Controllers
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
                var config = SensorConfigurationService.Instance.GetSensorConfiguration(guid);
                return Ok(config);
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ConfigurationController", e);
            }

            return BadRequest();
        }

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
                return Ok("http://i1.kym-cdn.com/entries/icons/original/000/021/971/saltybae.PNG");
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