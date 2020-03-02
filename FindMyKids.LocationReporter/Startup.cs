using FindMyKids.LocationReporter.Events;
using FindMyKids.LocationReporter.Models;
using FindMyKids.LocationReporter.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FindMyKids.LocationReporter
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

            services.Configure<AMQPOptions>(Configuration.GetSection("amqp"));
            services.Configure<FamilyServiceOptions>(Configuration.GetSection("familyservice"));

            services.AddSingleton(typeof(IEventEmitter), typeof(AMQPEventEmitter));
            services.AddSingleton(typeof(ICommandEventConverter), typeof(CommandEventConverter));
            services.AddSingleton(typeof(IFamilyServiceClient), typeof(HttpFamilyServiceClient));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        }
    }
}
