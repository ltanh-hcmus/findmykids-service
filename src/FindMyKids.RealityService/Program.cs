using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;


namespace FindMyKids.RealityService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
 				.AddCommandLine(args)
				.Build();

	    	var host = new WebHostBuilder()
                //.UseUrls("http://192.168.1.101:5004/")
				.UseKestrel()
				.UseStartup<Startup>()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseConfiguration(config)
				.Build();

	    	host.Run();
        }
    }
}
