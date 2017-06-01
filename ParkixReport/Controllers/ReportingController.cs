using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using Parkix.Process.Entities.Parking;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Converters;
using Parkix.Shared.Services;
using Parkix.Report.Services;
using Parkix.Shared.Entities.Parking;

namespace Parkix.Report.Controllers
{
    
    /// <summary>
    /// Report system status
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller"/>
    [Route("api")]
    public class ReportingController : Controller
    {
        /// <summary>
        /// Gets a list of the available parking lots.
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("lot/{lotid}/latest")]
        public async Task<IActionResult> GetLatestLotDatapoint([FromHeader] string authorization, string lotid)
        {
            try
            {
                var validToken = await AuthenticationService.Instance.ValidateToken(authorization);
                if (!validToken)
                {
                    return Unauthorized();
                }

                var available = ReportingService.Instance.GetLatestParkingLotPercentFull(lotid: lotid, percentFull: out var percentFull);
                if (!available)
                {
                    return NotFound();
                }

                return Ok(percentFull);
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ProcessingController", e);
            }

            return new StatusCodeResult(500);

            return StatusCode(statusCode: 500);
        }

        /// <summary>
        /// Gets a prediction of tomorrow's parking situation.
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("lot/{lotid}/predict/tomorrow")]
        public async Task<IActionResult> GetTommorrowLotPrediction([FromHeader] string authorization, string lotid)
        {
            var datfsdfa = SimulationHelpers.SimulationOne(20, maxRandom: 5);

            return Ok(datfsdfa);
            
            try
            {
                var validToken = await AuthenticationService.Instance.ValidateToken(authorization);
                if (!validToken)
                {
                    return Unauthorized();
                }

                var exists = SystemService.Instance.GetParkingLot<ParkingLot>(lotid, value: out var lot);

                var data = SimulationHelpers.SimulationOne(lot.TotalSpots, maxRandom: lot.TotalSpots / 10);
           
                return Ok(data);
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ProcessingController", e);
            }

            return new StatusCodeResult(500);

            return StatusCode(statusCode: 500);
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