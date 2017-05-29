using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Parkix.Shared.Services;
using System;
using System.IO;

namespace Parkix.Configure
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
                await AuthenticationService.Instance.Initialize(ConfigureSettings.PredixUaaClientID, ConfigureSettings.PredixUaaClientSecret);
                await ConfigurationService.Instance.Initialize(ConfigureEnvironmentalService.SystemDatabase);
                PseudoLoggingService.Log("Application", "Initialization Completed. System Ready.");
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log("Application", e);
            }
        }
    }
}
