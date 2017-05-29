using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Parkix.Shared.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Parkix.Shared.Entities.Sensor;
using Newtonsoft.Json;

namespace Parkix.Processing.Controllers
{
    /// <summary>
    /// Report system status
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller"/>
    [Route("api")]
    public class ConfigurationController : Controller
    {

        #region SYSTEM

        /// <summary>
        /// Gets a list of the available sensors.
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("sensors")]
        [ProducesResponseType(type: typeof(string[]), statusCode: 200)]
        public async Task<IActionResult> GetSensors([FromHeader] string authorization)
        {
            try
            {
                var authenticated = await AuthenticationService.Instance.ValidateToken(authorization);
                if (!authenticated)
                {
                    return Unauthorized();
                }

                var system = ConfigurationService.Instance.GetSystem();
                return Ok(system.Sensors);
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ConfigurationController, GetSensors", e);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Gets a list of the available sensors.
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("lots")]
        [ProducesResponseType(type: typeof(string[]), statusCode: 200)]
        public async Task<IActionResult> GetParkingLots([FromHeader] string authorization)
        {
            try
            {
                var authenticated = await AuthenticationService.Instance.ValidateToken(authorization);
                if (!authenticated)
                {
                    return Unauthorized();
                }

                var system = ConfigurationService.Instance.GetSystem();
                return Ok(system.ParkingLots);
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ConfigurationController, GetParkingLots", e);
                return new StatusCodeResult(500);
            }
        }

        #endregion
        #region SENSOR

        /// <summary>
        /// Gets the sensor config.
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("sensor/{guid}/config")]
        [ProducesResponseType(type: typeof(List<StaticEntityCoordinateSet>), statusCode: 200)]
        public async Task<IActionResult> GetSensorConfig(string authorization, string guid)
        {
            try
            {
                var authenticated = await AuthenticationService.Instance.ValidateToken(authorization);
                if (!authenticated)
                {
                    return Unauthorized();
                }

                if (!ConfigurationService.Instance.GetSystem().Sensors.Contains(guid))
                {
                    return NotFound();
                }

                var exists = SystemService.Instance.GetSensor<StaticCustomCameraSensor>(guid: guid, value: out var record);
                if (!exists)
                {
                    return NotFound();
                }

                if (record.coordinates == null || record.ConfigStatus == ConfigurationStatus.None)
                {
                    return NotFound();
                }

                record.ConfigStatus = ConfigurationStatus.Current;
                ConfigurationService.Instance.PutSensor(record);
                return Ok(record.coordinates);
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ConfigurationController, GetSensorConfig", e);
            }

            return new StatusCodeResult(500);
        }

        /// <summary>
        /// Puts the sensor config.
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="guid"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("sensor/{guid}/config")]
        public async Task<IActionResult> PostSensorConfig([FromHeader] string authorization, string guid, [FromBody] List<StaticEntityCoordinateSet> configuration)
        {
            try
            {
                var authenticated = await AuthenticationService.Instance.ValidateToken(authorization);
                if (!authenticated)
                {
                    return Unauthorized();
                }

                if (!ConfigurationService.Instance.GetSystem().Sensors.Contains(guid))
                {
                    return NotFound();
                }

                var sensorRecord = new StaticCustomCameraSensor()
                {
                    GUID = guid,
                    coordinates = configuration,
                    SensorType = "STATIC_CUSTOM_CAMERA",
                    ConfigStatus = ConfigurationStatus.Update
                };
                ConfigurationService.Instance.PutSensor<StaticCustomCameraSensor>(sensorRecord);

                return Ok();
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ConfigurationController, PutSensorConfig", e);       
            }

            return new StatusCodeResult(500);
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
                return Unauthorized();
            }

            try
            {
                if (!ConfigurationService.Instance.GetSystem().Sensors.Contains(guid))
                {
                    return NotFound();
                }

                var exists = ConfigurationService.Instance.GetSensor<StaticCustomCameraSensor>(guid: guid, value: out var record);
                if (!exists || record.Image == null)
                {
                    return NotFound();
                }

                return File(record.Image, "image/jpeg");
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ConfigurationController", e);
            }

            return new StatusCodeResult(500);
        }

        /// <summary>
        /// Puts a sensor image.
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="guid"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("sensor/{guid}/image")]
        public async Task<IActionResult> PutSensorImage([FromHeader] string authorization, string guid, IFormFile file)
        {
            try
            {
                var validToken = await AuthenticationService.Instance.ValidateToken(authorization);

                if (!validToken)
                {
                    return Unauthorized();
                }

                if (!ConfigurationService.Instance.GetSystem().Sensors.Contains(guid))
                {
                    return NotFound();
                }

                var exists = ConfigurationService.Instance.GetSensor<StaticCustomCameraSensor>(guid: guid, value: out var record);
                if (!exists)
                {
                    return NotFound();
                }

                var stream = file.OpenReadStream();
                var data = new MemoryStream();
                stream.CopyTo(data);
                record.Image = data.ToArray();
                ConfigurationService.Instance.PutSensor(record);

                return Ok(
                    "https://cp-predix-configure.run.aws-usw02-pr.ice.predix.io/api/sensor/" + guid +
                    "/image");
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ConfigurationController", e);
            }

            return new StatusCodeResult(500);
        }

#endregion


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