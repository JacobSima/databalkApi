
using System.Text;
using Databalk.Application.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Databalk.Infrastructure.Auth;

internal static class Extentions
{
  private const string OptionsSectionName = "auth";
  public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
  {
    var options = configuration.GetOptions<AuthOptions>(OptionsSectionName);

    services
      .Configure<AuthOptions>(configuration.GetRequiredSection(OptionsSectionName))
      .AddSingleton<IAuthenticator, Authenticator>()
      .AddSingleton<ITokenStorage, TokenStorage>();
    
    services
    .AddAuthentication(o =>
    {
      o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
      o.Audience = options.Audience;
      o.IncludeErrorDetails = true;
      o.TokenValidationParameters = new TokenValidationParameters
      {
        ValidIssuer = options.Issuer,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey))
      };
    });

    return services;
  }
}
