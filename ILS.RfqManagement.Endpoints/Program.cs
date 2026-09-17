using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ILS.RfqManagement.Endpoints
{
    [ExcludeFromCodeCoverage] // JUSTIFICATION: not unit testable due to .NET Core and file system dependencies
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    const string externalApplicationConfigurationRootPath = "/devops/applicationConfiguration";

                    

                    config.SetBasePath(Directory.GetCurrentDirectory());

                    config.AddJsonFile(

                        Path.Combine(externalApplicationConfigurationRootPath,

                            $"{env.EnvironmentName}/configuration.json"), optional: false, reloadOnChange: true);

                    Console.WriteLine("Configuration loaded/reloaded at: " + DateTime.Now);
                })
                .UseStartup<Startup>();
        }
    }
}
