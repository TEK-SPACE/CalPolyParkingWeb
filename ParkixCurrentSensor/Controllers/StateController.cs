using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Parkix.Shared.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Parkix.Shared.Entities.Sensor;
using Parkix.CurrentSensor.Services;
using Newtonsoft.Json;

namespace Parkix.CurrentSensor.Controllers
{
    /// <summary>
    /// Report system status
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller"/>
    [Route("state")]
    public class StateController : Controller
    {
        /// <summary>
        /// Puts the state of the sensor.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("set")]
        public IActionResult setSensorState([FromBody] Dictionary<string, bool> state, string password)
        {
            try
            {
                if (password != "AFUSfy8oa7sfsfvUFYAOsf")
                {
                    return Unauthorized();
                }

                SpotToLotAdapterService.Instance.SetState(state);
                PseudoLoggingService.Log("StateController", "Sensor state has been manually configured.");
                return Ok();
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ConfigurationController", e);
            }

            return new StatusCodeResult(500);
        }

        /// <summary>
        /// Gets the state of the sensor.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get")]
        public IActionResult GetSensorState(string password)
        {
            if (password != "AFUSfy8oa7sfsfvUFYAOsf")
            {
                return Unauthorized();
            }

            try
            {
                return Ok(SpotToLotAdapterService.Instance.GetState());
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ConfigurationController", e);
            }

            return new StatusCodeResult(500);
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