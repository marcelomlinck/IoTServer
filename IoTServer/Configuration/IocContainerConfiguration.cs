using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using IoTServer.Data;
using IoTServer.Data.Management;
using Microsoft.EntityFrameworkCore;
using IoTServer.Services;
using IoTServer.Handlers;

namespace IoTServer.Configuration
{
	public class IocContainerConfiguration
	{
		public static void Configure(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
		{
			services.AddHttpContextAccessor();

			services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

			var connectionString = configuration.GetConnectionString("ApiDataConnectionString");

			services.AddDbContext<ApiDataContext>(db => db.UseMySql(connectionString));

			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

			services.AddTransient<IUnitOfWork, UnitOfWork>();
			services.AddTransient<IContextFactory, ContextFactory>();

			services.AddTransient<IAirConditionerService, AirConditionerService>();

			services.AddTransient<IDbContext, ApiDataContext>();

			services.AddSingleton<MQTTHandler>();

			//services.AddHostedService<SubscriberMQTT>
		}
	}
}
