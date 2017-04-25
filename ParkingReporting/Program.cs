using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using ParkingReporting.Services;

namespace ParkingReporting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PseudoLoggingService.Log("It's alive!");

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
            }
            catch (Exception e)
            {
                PseudoLoggingService.Log(e);
            }
            finally
            {
                PseudoLoggingService.Log("Initialization Completed. System Ready.");
            }
        }
    }
}
