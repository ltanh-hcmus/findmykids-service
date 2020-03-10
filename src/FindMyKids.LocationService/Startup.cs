using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FindMyKids.LocationService.Models;
using FindMyKids.LocationService.Persistence;
using System;

namespace FindMyKids.LocationService
{
    public class Startup
    {
        public static string[] Args {get; set;} = new string[] {};
        private ILogger logger;

        public Startup(IHostingEnvironment env, ILogger<Startup> _logger)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional:true)
                .AddEnvironmentVariables()
                .AddCommandLine(Startup.Args);

            logger = _logger;

            Configuration = builder.Build();
        }

        public static IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole());
            services.AddLogging(configure => configure.AddDebug());

            //var transient = Boolean.Parse(Configuration.GetSection("transient").Value);
            var transient = true;
            if (Configuration.GetSection("transient") != null) {
                transient = Boolean.Parse(Configuration.GetSection("transient").Value);
            }
            if (transient) {
                logger.LogInformation("Using transient location record repository.");
                services.AddScoped<ILocationRecordRepository, InMemoryLocationRecordRepository>();
            } else {                
                var connectionString = Configuration.GetSection("postgres:cstr").Value;                        
                services.AddEntityFrameworkNpgsql().AddDbContext<LocationDbContext>(options =>
                    options.UseNpgsql(connectionString));
                logger.LogInformation("Using '{0}' for DB connection string.", connectionString);
                services.AddScoped<ILocationRecordRepository, LocationRecordRepository>();
            }

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
