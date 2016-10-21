using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Antiforgery;
using HTTPDataCollectorAPI;
using Microsoft.Extensions.Logging;

namespace LogAnalytics.Extensions.Logging.Test
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			
		}

		public IConfigurationRoot Configuration { get; set; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			//Add Logger
			loggerFactory.AddLogAnalytics("{Your_Workspace_Id}", "{Your_Key_Id}", "{Your_Namespace}", LogLevel.Critical);
			app.UseMvcWithDefaultRoute();			
		}


		public static void Main(string[] args)
		{
			var host = new WebHostBuilder()
				  .UseKestrel()
				  .UseIISIntegration()
				  .UseContentRoot(Directory.GetCurrentDirectory())
				  .UseStartup<Startup>()
				  .Build();

			host.Run();

		}
	}
}
