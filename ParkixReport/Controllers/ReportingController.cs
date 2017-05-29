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

namespace Parkix.Process.Controllers
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
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ReportingController:GetParkingLots", e);
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
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ReportingController:LotSummary", e);
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
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ReportingController:LotDetail", e);
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