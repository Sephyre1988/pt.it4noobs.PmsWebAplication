using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;


namespace pt.it4noobs.Local.PmsWebAplication
{
	public class Program
	{
		static Program()
		{
			var assemblyLocation = typeof(Program).GetTypeInfo().Assembly.Location;
			var fileVersion = string.IsNullOrWhiteSpace(assemblyLocation)
				? null
				: FileVersionInfo.GetVersionInfo(assemblyLocation);

			NLog.GlobalDiagnosticsContext.Set(
				"fileVersion", fileVersion?.FileVersion ?? "<undefined>");
			NLog.GlobalDiagnosticsContext.Set(
				"productVersion", fileVersion?.ProductVersion ?? "<undefined>");
		}

		public static void Main(string[] args)
		{
			var currentDirectory = Directory.GetCurrentDirectory();

			ILoggerFactory loggerFactory;
			ILogger<Program> logger;
			BuildLoggerFactory(currentDirectory, out loggerFactory, out logger);

			logger.LogInformation("Application started...");

			var host = new WebHostBuilder()
				.CaptureStartupErrors(true)
				.UseKestrel()
				.UseContentRoot(currentDirectory)
				.UseIISIntegration()
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}

		[Obsolete]
		private static void BuildLoggerFactory(string currentDirectory, out ILoggerFactory loggerFactory, out ILogger<Program> logger)
		{
			loggerFactory = new LoggerFactory();

			logger = loggerFactory.CreateLogger<Program>();
			logger.LogInformation("Configuring application logging");

			try
			{
				var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
				if (string.IsNullOrWhiteSpace(environmentName))
					environmentName = "Production";

				var nlogFilePath = Path.Combine(currentDirectory, $"nlog.{environmentName}.config");
				if (!File.Exists(nlogFilePath))
					nlogFilePath = Path.Combine(currentDirectory, "nlog.config");

				logger.LogInformation(@"Configuring NLog
Environment: '{environmentName}'
NLogFilePath: '{nlogFilePath}'", environmentName, nlogFilePath);

				loggerFactory
					.AddNLog()
					.ConfigureNLog(nlogFilePath);
			}
			catch (Exception e)
			{
				logger.LogCritical(0, e, "Failed to configure NLog");
				throw;
			}
		}
	}
}
