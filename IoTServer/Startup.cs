using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IoTServer.Configuration;
using IoTServer.Data.Management;
using IoTServer.Handlers;
using IoTServer.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using AutoMapper;

namespace IoTServer
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		readonly MQTTHandler mqttInitialize = MQTTHandler.Instance;

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddLogging()
				.AddMvc()
				.AddJsonOptions(opt =>
				{
					opt.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
					opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
				})
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddScoped<ApiAuthenticationService>();
			services.AddAuthentication("BasicAuthentication")
				.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

			Mapper.Reset();
			services.AddAutoMapper(AutoMapperConfiguration.Configure);

			IocContainerConfiguration.Configure(services, this.Configuration);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			UpdateDatabase(app);

			//app.UseMiddleware<ExceptionHandlingMiddleware>();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseCors(x => x
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader()
				.AllowCredentials());

			app.UseAuthentication();
			app.UseHttpsRedirection();
			app.UseMvc();
		}

		private static void UpdateDatabase(IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.CreateScope())
			{
				using (var db = scope.ServiceProvider.GetService<IDbContext>())
				{
					db.Migrate();
				}
			}
		}
	}
}
