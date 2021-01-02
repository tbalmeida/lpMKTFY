using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MKTFY.Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((builder) =>
                {
                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    if (env == "Development")
                    {
                        builder.AddSystemsManager(String.Format("/MKTFY/{0}/", env), new AWSOptions
                        {
                            Region = RegionEndpoint.CACentral1,
                            Profile = "default"
                        });
                    }
                    else
                    {
                        builder.AddSystemsManager(String.Format("/MKTFY/{0}/", env), new AWSOptions
                        {
                            Region = RegionEndpoint.CACentral1
                        });

                    }
                }
            );

    }
}
