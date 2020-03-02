using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using FindMyKids.EventProcessor.Events;
using FindMyKids.EventProcessor.Location;
using FindMyKids.EventProcessor.Location.Redis;
using FindMyKids.EventProcessor.Queues;
using FindMyKids.EventProcessor.Queues.AMQP;
using FindMyKids.EventProcessor.Models;

namespace FindMyKids.EventProcessor
{
    public class Startup
    {
        [System.Obsolete]
        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
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
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddLogging(configure => configure.AddConsole());
            services.AddLogging(configure => configure.AddDebug());

            services.Configure<QueueOptions>(Configuration.GetSection("QueueOptions"));
            services.Configure<AMQPOptions>(Configuration.GetSection("amqp"));

            services.AddRedisConnectionMultiplexer(Configuration);

            services.AddTransient(typeof(IConnectionFactory), typeof(AMQPConnectionFactory));
            services.AddTransient(typeof(EventingBasicConsumer), typeof(AMQPEventingConsumer));

            services.AddSingleton(typeof(ILocationCache), typeof(RedisLocationCache));

            services.AddSingleton(typeof(IEventSubscriber), typeof(AMQPEventSubscriber));
            services.AddSingleton(typeof(IEventEmitter), typeof(AMQPEventEmitter));
            services.AddSingleton(typeof(IEventProcessor), typeof(MemberLocationEventProcessor));
        }

        // Singletons are lazy instantiation.. so if we don't ask for an instance during startup,
        // they'll never get used.
        [System.Obsolete]
        public void Configure(IApplicationBuilder app,
                IHostingEnvironment env,
                ILoggerFactory loggerFactory,
                IEventProcessor eventProcessor)
        {
            app.UseMvc();
            eventProcessor.Start();
        }
    }
}