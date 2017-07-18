using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Parkix.Report.Services;
using Parkix.Shared.Entities.Parking;
using Parkix.Shared.Services;
using System;
using System.Threading.Tasks;

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
        /// Gets the latest .
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="lotid"></param>
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
                PseudoLoggingService.Log("ReportingController", e);
            }

            return new StatusCodeResult(500);
        }

        /// <summary>
        /// Gets the historial lot data.
        /// </summary>
        /// <param name="authorization">The authorization.</param>
        /// <param name="lotid">The lotid.</param>
        /// <param name="start">The starttime.</param>
        /// <param name="end">The endtime.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("lot/{lotid}/historical")]
        public async Task<IActionResult> GetHistorialLotData([FromHeader] string authorization, string lotid, string start, string end)
        {
            try
            {
                var validToken = await AuthenticationService.Instance.ValidateToken(authorization);
                if (!validToken)
                {
                    return Unauthorized();
                }

                DateTime startDateTime = DateTime.Parse(start);
                DateTime endDateTime = DateTime.Parse(end);

                var available = ReportingService.Instance.GetHistoricalParkingLotPercentFull(lotid, startDateTime, endDateTime, out var data);
                if (!available)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(data);
                }
            }
            catch (FormatException e)
            {
                PseudoLoggingService.Log("ReportingController", e);
                return BadRequest("Invalid datetime format.");
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("ReportingController", e);
            }

            return new StatusCodeResult(500);
        }

        /// <summary>
        /// Gets a prediction of tomorrow's parking situation.
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="lotid"></param>
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
                PseudoLoggingService.Log("ReportingController", e);
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