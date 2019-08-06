using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace IoTServer.Security
{
	public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		private readonly ApiAuthenticationService _authService;
		private readonly IConfiguration _configuration;

		public BasicAuthenticationHandler(
			IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder,
			ISystemClock clock,
			ApiAuthenticationService authService,
			IConfiguration configuration) : base(options, logger, encoder, clock)
		{
			this._authService = authService;
			this._configuration = configuration;
		}

		protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			var enabled = this._configuration.GetValue<bool>("ApiConfiguration:ApiAuthEnabled");

			if (enabled && !this.Request.Headers.ContainsKey("Authorization"))
			{
				return AuthenticateResult.Fail("Missing Authorization Header");
			}

			if (!enabled)
			{
				var ticket = this.GetAuthenticationTicket(new[] { "Anon", "Anon" });
				return AuthenticateResult.Success(ticket);
			}

			try
			{
				var authHeader = AuthenticationHeaderValue.Parse(this.Request.Headers["Authorization"]);
				var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
				var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');

				if (await this._authService.ValidateAsync(credentials[0], credentials[1]))
				{
					var ticket = this.GetAuthenticationTicket(credentials);

					return AuthenticateResult.Success(ticket);
				}
				return AuthenticateResult.Fail("Invalid Username or Password");
			}
			catch
			{
				return AuthenticateResult.Fail("Invalid Authorization Header");
			}
		}

		private AuthenticationTicket GetAuthenticationTicket(string[] credentials)
		{
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, this.Scheme.Name),
				new Claim(ClaimTypes.Name, credentials[0]),
			};
			var identity = new ClaimsIdentity(claims, this.Scheme.Name);
			var principal = new ClaimsPrincipal(identity);
			var ticket = new AuthenticationTicket(principal, this.Scheme.Name);

			return ticket;
		}
	}
}
