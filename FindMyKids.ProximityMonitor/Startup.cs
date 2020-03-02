using FindMyKids.ProximityMonitor.Events;
using FindMyKids.ProximityMonitor.FamilyService;
using FindMyKids.ProximityMonitor.Queues;
using FindMyKids.ProximityMonitor.Realtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;

namespace FindMyKids.ProximityMonitor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOptions();


            services.Configure<QueueOptions>(Configuration.GetSection("QueueOptions"));
            services.Configure<PubnubOptions>(Configuration.GetSection("PubnubOptions"));
            services.Configure<FamilyServiceOptions>(Configuration.GetSection("familyservice"));
            services.Configure<AMQPOptions>(Configuration.GetSection("amqp"));

            services.AddTransient(typeof(IConnectionFactory), typeof(AMQPConnectionFactory));
            services.AddTransient(typeof(EventingBasicConsumer), typeof(RabbitMQEventingConsumer));
            services.AddSingleton(typeof(IEventSubscriber), typeof(RabbitMQEventSubscriber));
            services.AddSingleton(typeof(IEventProcessor), typeof(ProximityDetectedEventProcessor));
            services.AddTransient(typeof(IFamilyServiceClient), typeof(HttpFamilyServiceClient));

            services.AddRealtimeService();
            services.AddSingleton(typeof(IRealtimePublisher), typeof(PubnubRealtimePublisher));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                IEventProcessor eventProcessor,
                IOptions<PubnubOptions> pubnubOptions,
                IRealtimePublisher realtimePublisher)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            realtimePublisher.Validate();
            realtimePublisher.Publish(pubnubOptions.Value.StartupChannel, "{'hello': 'world'}");

            eventProcessor.Start();
        }
    }
}
