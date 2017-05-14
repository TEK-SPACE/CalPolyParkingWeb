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
        /// Gets the lot summary.
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="guid"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("sensor/{guid}")]
        public async Task<IActionResult> LotSummary([FromHeader] string authorization, string guid, [FromBody] List<ParkingSpotConfiguration> configuration)
        {
            var validToken = await AuthenticationService.Instance.ValidateToken(authorization);

            if (!validToken)
            {
                //return Unauthorized();
            }

            try
            {
                ConfigurationService.Instance.SetLotConfiguration(SensorId: guid, configuration: configuration);
                return Accepted();
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