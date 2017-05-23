using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace ParkingCurrentSensor
{
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
                await IeParkingIngestService.Instance.Initialize();
                PseudoLoggingService.Log("Application", "Initialization Completed. System Ready.");
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("Application", e);
            }
        }
    }

}
