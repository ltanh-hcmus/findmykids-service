using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;
using System.Linq;
using FindMyKids.LocationReporter.Models;
using FindMyKids.LocationReporter.Events;
using FindMyKids.LocationReporter.Services;

namespace FindMyKids.LocationReporter
{
    public class Startup
    {        
        public Startup(IHostingEnvironment env) 
        {
            var builder = new ConfigurationBuilder()                
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
		        .AddEnvironmentVariables();		                    

	        Configuration = builder.Build();    		        
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddMvc();
            services.AddOptions();

            services.Configure<AMQPOptions>(Configuration.GetSection("amqp"));            
            services.Configure<TeamServiceOptions>(Configuration.GetSection("teamservice"));

            services.AddSingleton(typeof(IEventEmitter), typeof(AMQPEventEmitter));
            services.AddSingleton(typeof(ICommandEventConverter), typeof(CommandEventConverter));
            services.AddSingleton(typeof(ITeamServiceClient), typeof(HttpTeamServiceClient));

            services.AddLogging(configure => configure.AddConsole());
            services.AddLogging(configure => configure.AddDebug());
        }

        public void Configure(IApplicationBuilder app, 
                IHostingEnvironment env, 
                ILoggerFactory loggerFactory,
                ITeamServiceClient teamServiceClient,
                IEventEmitter eventEmitter) 
        {           
            // Asked for instances of singletons during Startup
            // to force initialization early.
            
            app.UseMvc();
        }
    }
}

