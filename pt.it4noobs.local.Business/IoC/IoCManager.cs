using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace pt.it4noobs.local.Business.IoC
{
	public static class IoCManager
	{
		public static IServiceCollection AddPmsBusiness(this IServiceCollection services, string pmsConnectionString)
		{
			if (services == null)
				throw new ArgumentNullException(nameof(services));
			if (pmsConnectionString == null)
				throw new ArgumentNullException(nameof(pmsConnectionString));
			
			if (string.IsNullOrWhiteSpace(pmsConnectionString))
				throw new ArgumentException("Value cannot be whitespace.", nameof(pmsConnectionString));

			return services
				.AddMemoryCache()
				.AddEntityFramework()
				.AddEntityFrameworkSqlServer()
				.AddDatabaseUnitOfWork<SifarmaUnitOfWork, ISifarmaDatabaseSession>(
					k => k.GetRequiredService<SifarmaDatabaseSessionFactory>().Create(k.GetRequiredService<SifarmaOracleConnection>()),
					s =>
					{
						s.AddSingleton<SifarmaXmlSerializer>();
						s.AddSingleton(k =>
							new SifarmaDatabaseSessionFactory(
								sifarmaConnectionString, k.GetRequiredService<ILoggerFactory>()));
						s.AddSingleton<ISifarmaUnitOfWorkFactory, SifarmaUnitOfWorkFactory>();
						s.AddScoped(k => new SifarmaOracleConnection(sifarmaConnectionString));
						s.AddScoped<SifarmaQueryRunner>();
					})
				.AddDatabaseUnitOfWork<SifarmaSecurityUnitOfWork, SifarmaSecurityContext>(extraCfg: s =>
				{
					s.AddDbContext<SifarmaSecurityContext>(options =>
					{
						options.UseSqlServer(sifarmaSecurityConnectionString);
					}).AddScoped<SifarmaSecurityQueryRunner>();
				})
				.AddDatabaseUnitOfWork<SifarmaCacheScopedUnitOfWork, SifarmaCacheContext>(extraCfg: s =>
				{
					s.AddDbContext<SifarmaCacheContext>(options =>
					{
						options.UseSqlServer(sifarmaCacheConnectionString);
					}).AddScoped<ISifarmaCacheSqlRunner, SifarmaCacheSqlRunner>();
				})
				.AddSingleton<ISifarmaCacheUnitOfWorkFactory, SifarmaCacheUnitOfWorkFactory>()
				.AddDatabaseUnitOfWorkFactory()
				.AddPmsBusinessManagers()
				.AddPmsConfigurationManager()
				.AddPmsManagers()
				.AddPmsBusinessBrokers()
				.AddPmsBusinessValidators()
				.AddPmsBusinessServices()
				.AddPmsEngines()
				.AddPmsProviders()
				.AddScoped<ISignDocsProvider, SignDocsProvider>()
				.AddSingleton<B2BTrackingHttpClientWrapper>();

		}

		private class ErrorPayload
		{
			public System.Collections.Generic.Dictionary<string, string[]> ModelState { get; set; }
		}
	}
}
