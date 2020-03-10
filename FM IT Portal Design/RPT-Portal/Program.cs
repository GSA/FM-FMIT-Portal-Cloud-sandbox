using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GSA.FMITPortal
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();

			// The following code is for cloud.gov
			//var config = new ConfigurationBuilder()
			//        .AddCommandLine(args)
			//        .Build();

			//var host = new WebHostBuilder()
			//    .UseKestrel()
			//    .UseConfiguration(config)
			//    .UseStartup<Startup>()
			//    .Build();
			//host.Run();

		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var appsettingsDirectory = Environment.GetEnvironmentVariable("APPSETTINGS_DIRECTORY");
                    if (String.IsNullOrEmpty(appsettingsDirectory))
                    {
                        // Default to appsettings within the app.
                        string[] paths = { hostingContext.HostingEnvironment.ContentRootPath, "appsettings" };
                        appsettingsDirectory = Path.Combine(paths);
                    }
                    config.SetBasePath(appsettingsDirectory);
                    config.AddJsonFile("PORTAL_appsettings.json", optional: false, reloadOnChange: true);
                })
                .UseStartup<Startup>();
	}
}
