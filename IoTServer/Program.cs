using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace IoTServer
{
	internal class Program
	{
		private static string _environmentName;

		private static void Main(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", false, true)
				.Build();

			/*Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.Enrich.FromLogContext()
				.CreateLogger();*/

			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.Enrich.FromLogContext()
				.CreateLogger();

			try
			{
				CreateWebHostBuilder(args)
					.UseConfiguration(configuration)
					.Build()
					.Run();
			}
			catch (Exception e)
			{
				Log.Error($"Error while running the host: {e}");
			}
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			return WebHost.CreateDefaultBuilder(args)
				.UseKestrel()
				.UseIISIntegration()
				.ConfigureLogging((hostingContext, logging) =>
				{
					logging.ClearProviders();
					_environmentName = hostingContext.HostingEnvironment.EnvironmentName;
				})
				.UseStartup<Startup>();
		}
	}
}
