using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ParkingProcessing.Controllers
{
    [Route("debug")]
    public class LoggingController : Controller
    {
        [HttpGet]
        public string Get()
        {
#if DEBUG
            return PseudoLoggingService.EventLogString;
#else
            return "Logging Disabled."
#endif
        }
    }
}