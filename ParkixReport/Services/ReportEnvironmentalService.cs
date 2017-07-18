using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parkix.Shared.Services;
using Parkix.Shared.Entities.Environment;
using Parkix.Shared.Entities.Redis;
using Parkix.Shared.Exceptions;
using Parkix.Shared;

namespace Parkix.Report.Services
{
    /// <summary>
    /// Environmental service for reporting instance.
    /// </summary>
    public class ReportEnvironmentalService : CommonEnvironmentalService<CommonPredixVcapServices>
    {
        /// <summary>
        /// System Database.
        /// </summary>
        public static RedisService SystemDatabase
        {
            get
            {
                RedisService service = PredixServices.Redis.First((s) =>
                {
                    return s.Name == SharedSettings.SystemDatabaseName;
                });

                if (service == null)
                {
                    throw new EnvironmentalException();
                }
                return service;
            }
        }

        /// <summary>
        /// Historical data database reference.
        /// </summary>
        public static RedisService HistoricalDatabase
        {
            get
            {
                RedisService service = PredixServices.Redis.First((s) =>
                {
                    return s.Name == SharedSettings.HistoricalDatabaseName;
                });

                if (service == null)
                {
                    throw new EnvironmentalException();
                }
                return service;
            }
        }
    }
}
