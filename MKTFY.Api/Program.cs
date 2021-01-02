using System;
using System.Threading.Tasks;
using MKTFY.App;
using MKTFY.App.Seeds;
using MKTFY.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Amazon.Extensions.NETCore.Setup;
using Amazon;

namespace MKTFY.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    context.Database.Migrate();
                    Task.Run(async () => await UserAndRoleSeeder.SeedUsersAndRoles(roleManager, userManager)).Wait();

                    Task.Run(async () => await BasicDataSeeder.SeedBasicConfig(context)).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration( (builder) =>
                {
                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    if (env == "Development")
                    {
                        builder.AddSystemsManager(String.Format("/MKTFY/{0}", env), new AWSOptions
                        {
                            Region = RegionEndpoint.CACentral1,
                            Profile = "default"
                        });
                    } 
                    else 
                    {
                        builder.AddSystemsManager(String.Format("/MKTFY/{0}", env), new AWSOptions
                        {
                            Region = RegionEndpoint.CACentral1
                        });

                    }
                }
            );
    }
}
