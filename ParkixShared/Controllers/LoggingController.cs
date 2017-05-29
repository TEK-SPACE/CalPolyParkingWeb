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


namespace Parkix.Process.Controllers
{
    /// <summary>
    /// Access to the logging service.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("debug")]
    public class LoggingController
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