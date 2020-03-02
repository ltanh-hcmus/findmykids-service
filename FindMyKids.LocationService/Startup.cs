using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FindMyKids.LocationService.Models;
using FindMyKids.LocationService.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace FindMyKids.LocationService
{
    public class Startup
    {
        public static string[] Args { get; set; } = new string[] { };
        private ILogger logger;
        private ILoggerFactory loggerFactory;

        [Obsolete]
        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .AddCommandLine(Startup.Args);

            Configuration = builder.Build();

            this.loggerFactory = loggerFactory;

            this.logger = this.loggerFactory.CreateLogger("Startup");
        }

        public static IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            //var transient = Boolean.Parse(Configuration.GetSection("transient").Value);
            var transient = true;
            if (Configuration.GetSection("transient") != null)
            {
                transient = Boolean.Parse(Configuration.GetSection("transient").Value);
            }
            if (transient)
            {
                logger.LogInformation("Using transient location record repository.");
                services.AddScoped<ILocationRecordRepository, InMemoryLocationRecordRepository>();
            }
            else
            {
                var connectionString = Configuration.GetSection("postgres:cstr").Value;
                services.AddEntityFrameworkNpgsql().AddDbContext<LocationDbContext>(options =>
                    options.UseNpgsql(connectionString));
                logger.LogInformation("Using '{0}' for DB connection string.", connectionString);
                services.AddScoped<ILocationRecordRepository, LocationRecordRepository>();
            }

            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddLogging(configure => configure.AddConsole());
            services.AddLogging(configure => configure.AddDebug());
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
