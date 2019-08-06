using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace IoTServer.Data.Management
{
	/// <summary>
	/// Entity Framework context service
	/// (Switches the db context according to tenant id field)
	/// </summary>
	public class ContextFactory : IContextFactory
	{
		private readonly HttpContext _httpContext;
		private readonly IConfiguration _configuration;

		public ContextFactory(IHttpContextAccessor httpContentAccessor, IConfiguration configuration)
		{
			_httpContext = httpContentAccessor.HttpContext;
			_configuration = configuration;
		}

		/// <inheritdoc />
		public IDbContext DbContext => new ApiDataContext(this.GetDatabaseConnectionOptions().Options);

		private DbContextOptionsBuilder<ApiDataContext> GetDatabaseConnectionOptions()
		{
			var connectionString = _configuration.GetConnectionString("ApiDataConnectionString");

			return new DbContextOptionsBuilder<ApiDataContext>().UseMySql(connectionString);
		}

		private void ValidateHttpContext()
		{
			if (_httpContext == null)
			{
				throw new ArgumentNullException(nameof(_httpContext));
			}
		}
	}
}