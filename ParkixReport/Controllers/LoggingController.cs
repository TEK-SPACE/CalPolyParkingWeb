using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Parkix.Process.Controllers
{
    /// <summary>
    /// Access to the logging service.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("debug")]
    public class LoggingController : Controller
    {
        /// <summary>
        /// Gets the log.
        /// </summary>
        /// <returns></returns>
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