
using Databalk.Application.Security;
using Databalk.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Databalk.Infrastructure.Security;

internal static class Extentions
{
  public static IServiceCollection AddSecurity(this IServiceCollection services)
  {
    services
      .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
      .AddSingleton<IPasswordManager, PasswordManager>();
      
    return services;
  }
}
