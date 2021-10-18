using System;
using System.Linq;
using System.Threading.Tasks;
using AthensLibrary.Configurations;
using AthensLibrary.Model.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AthensLibrary
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = service.GetRequiredService<AthensDbContext>();
                    var userManager = service.GetRequiredService<UserManager<User>>();
                    var roleManager = service.GetRequiredService<RoleManager<Role>>();
                    var authorManager = service.GetRequiredService<UserManager<User>>();
                    if (context.Set<Role>().ToList().Count == 0 || context.Set<User>().ToList().Count == 0)
                    {
                        await DataInitializer.SeedRolesAsync(roleManager);
                        await DataInitializer.SeedAdminAsync(userManager, roleManager);
                        await DataInitializer.SeedAuthorAsync(authorManager, roleManager);
                    }
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred! seeding Failed.");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
