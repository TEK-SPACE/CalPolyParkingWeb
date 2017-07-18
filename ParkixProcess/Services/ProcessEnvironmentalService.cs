using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parkix.Shared.Services;
using Parkix.Shared.Entities.Environment;
using Parkix.Shared.Entities.Redis;
using Parkix.Shared.Exceptions;
using Parkix.Shared;

namespace Parkix.Process.Services
{
    /// <summary>
    /// Environmental variables for process instance.
    /// </summary>
    public class ProcessEnvironmentalService : CommonEnvironmentalService<CommonPredixVcapServices>
    {
        /// <summary>
        /// Gets the system database.
        /// </summary>
        /// <value>
        /// The system database.
        /// </value>
        /// <exception cref="EnvironmentalException"></exception>
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
        /// Gets the historical database.
        /// </summary>
        /// <value>
        /// The historical database.
        /// </value>
        /// <exception cref="EnvironmentalException"></exception>
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
