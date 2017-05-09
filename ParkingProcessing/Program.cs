using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using ParkingProcessing.Entities.IeParking;
using ParkingProcessing.Services;

namespace ParkingProcessing
{
    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            PseudoLoggingService.Log("Application", "It's alive!");

            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(config)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            Initialize();

            host.Run();
        }


        private static async void Initialize()
        {
            try
            {
                await AuthenticationService.Instance.Initialize();
                await TimeseriesService.Instance.Initialize();
                PseudoLoggingService.Log("Application", "Initialization Completed. System Ready.");

                //32.786062, -117.254059
                //32.613200, -116.959316

                //UCSD:
                //32.889313, -117.242800
                //32.872085, -117.230891
                var list = await  IeParkingIngestService.Instance.FindAssets(latitudeOne: 32.715675, longitudeOne: -117.161230,
                    latitudeTwo: 32.708498, longitudeTwo: -117.151681);

                PseudoLoggingService.Log("IEParking", "the following assets have been found:");
                foreach (string asset in list)
                {
                    PseudoLoggingService.Log("IEParking", asset);
                }

                await IeParkingIngestService.Instance.OpenConnection(list.First());
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("Application", e);
            }

        }
    }
}
