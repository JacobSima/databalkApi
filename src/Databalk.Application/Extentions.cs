using Databalk.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Databalk.Application;

public static class Extentions
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    var applicationAssembly = typeof(ICommandHandler<>).Assembly;

    services.Scan(s => s.FromAssemblies(applicationAssembly)
      .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
      .AsImplementedInterfaces()
      .WithScopedLifetime());
      
    return services;
  }
}
