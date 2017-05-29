using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parkix.Shared.Services;
using Parkix.Shared.Entities.Redis;
using Parkix.Shared.Exceptions;
using Parkix.Shared.Entities.Environment;
using Parkix.Shared;

namespace Parkix.Configure
{
    /// <summary>
    /// Environmental Variables for a Configure instance.
    /// </summary>
    public class ConfigureEnvironmentalService : CommonEnvironmentalService<CommonPredixVcapServices>
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
    }
}
