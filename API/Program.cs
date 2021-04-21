using System;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var servies = scope.ServiceProvider;
                var loggerFactory = servies.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<Program>();

                try
                {
                    var context = servies.GetRequiredService<StoreContext>();
                    var userManager = servies.GetRequiredService<UserManager<AppUser>>();
                    var roleManager = servies.GetRequiredService<RoleManager<AppRole>>();
					logger.LogInformation("Migrating...");
                    await context.Database.MigrateAsync();
					logger.LogInformation("Seeding the database if empty...");
                    await StoreContextSeed.SeedAsync(context, loggerFactory, userManager, roleManager);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occured during migration...");
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
