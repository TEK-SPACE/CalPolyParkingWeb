using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Parkix.Report;
using Parkix.Report.Services;
using Parkix.Shared.Services;
using System;
using System.IO;

namespace Parkix.Process
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
                .Build();

            Initialize();

            host.Run();
        }


        private static async void Initialize()
        {

            try
            {
                await AuthenticationService.Instance.Initialize(ReportSettings.PredixUaaClientID, ReportSettings.PredixUaaClientSecret);
                await ReportingService.Instance.Initialize(ReportEnvironmentalService.HistoricalDatabase);
                await SystemService.Instance.Initialize(ReportEnvironmentalService.SystemDatabase);
                PseudoLoggingService.Log("Application", "Initialization Completed. System Ready.");
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("Application", e);
            }
        }
    }
}
