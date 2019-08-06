using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTServer.Security
{
	public class ApiAuthenticationService
	{
		private readonly string _userId;
		private readonly string _password;

		public ApiAuthenticationService(IConfiguration configuration)
		{
			_userId = configuration.GetSection("ApiConfiguration")["ApiUserId"];
			_password = configuration.GetSection("ApiConfiguration")["ApiUserPwd"];
		}

		public Task<bool> ValidateAsync(string userId, string password)
		{
			return Task.FromResult(_userId == userId && _password == password);
		}
	}
}
