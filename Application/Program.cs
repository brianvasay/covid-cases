using System;
using System.Threading.Tasks;
using Application.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // This will create a new scope for the logger factory and the store context objects.
            using (var scope = host.Services.CreateScope())
            {
                // This will get the necessary services from the scope.
                var services = scope.ServiceProvider;
                // This will create a new instance of the logger factory service.
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    // This will create a new instance of the application db context service.
                    var applicationDbContext = services.GetRequiredService<ApplicationDbContext>();
                    // This will update the database with the latest code first migration of the dw_morgan database.
                    await applicationDbContext.Database.MigrateAsync();
                    // This will seed the database with initial data.
                    await ApplicationDbContextSeed.SeedAsync(applicationDbContext, loggerFactory);
                }
                catch (Exception ex)
                {
                    // This will create a new logger instance for the program class.
                    var logger = loggerFactory.CreateLogger<Program>();
                    // This will log the details of the exception thrown during the database migration.
                    logger.LogError(ex, "An error occurred during database migration.");
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
