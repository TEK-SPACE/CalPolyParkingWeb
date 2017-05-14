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

namespace ParkingProcessing.Controllers
{
    
    /// <summary>
    /// Report system status
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller"/>
    [Route("api/reporting")]
    public class ReportingController : Controller
    {
        /// <summary>
        /// Gets a list of the available parking lots.
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("parkinglots")]
        public async Task<IActionResult> GetParkingLots([FromHeader] string authorization)
        {
            try
            {
                var validToken = await AuthenticationService.Instance.ValidateToken(authorization);

                if (!validToken)
                {
                    //return Unauthorized();
                }

                //get all assets...
                var lots = new List<string>();
                var tags = await TimeseriesQueryService.Instance.GetTags();

                //get just the parking lot ids...
                tags.ForEach((s =>
                {
                    lots.Add(Regex.Replace(input: s, pattern: @":[0-9a-zA-Z- ]*", replacement: String.Empty));
                }));

                //get distinct ones
                lots = lots.Distinct().ToList();
                
                return Ok(lots);
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ReportingController", e);
            }

            return StatusCode(statusCode: 500);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="lotid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("parkinglot/{lotid}/summary")]
        [ProducesResponseType(typeof(ParkingLotSummary), 200)]
        public async Task<IActionResult> LotSummary([FromHeader] string authorization, string lotid)
        {
            var validToken = await AuthenticationService.Instance.ValidateToken(authorization);

            if (!validToken)
            {
                //return Unauthorized();
            }

            try
            {
                var summary = await TimeseriesQueryService.Instance.GetLotSummary(lotid: lotid);
                return Ok(summary);
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ReportingController", e);
            }

            return BadRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="lotid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("parkinglot/{lotid}/detail")]
        [ProducesResponseType(typeof(ParkingLotDetail), 200)]
        public async Task<IActionResult> LotDetail([FromHeader] string authorization, string lotid)
        {
            var validToken = await AuthenticationService.Instance.ValidateToken(authorization);

            if (!validToken)
            {
                //return Unauthorized();
            }

            try
            {
                var summary = await TimeseriesQueryService.Instance.GetLotDetail	(lotid: lotid);
                return Ok(summary);
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ReportingController", e);
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