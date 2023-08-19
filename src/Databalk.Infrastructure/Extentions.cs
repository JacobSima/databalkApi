using Microsoft.Extensions.DependencyInjection;
using Databalk.Application.Abstractions;
using Databalk.Infrastructure.DAL;
using Microsoft.Extensions.Configuration;


namespace Databalk.Infrastructure;

public static class Extentions
{
  private const string OptionSectionName = "app";

  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {

    services.Configure<AppOptions>(configuration.GetRequiredSection(OptionSectionName));

    services.AddDAL(configuration);

    var infrastructureAssembly = typeof(AppOptions).Assembly;
    services.Scan(s => s.FromAssemblies(infrastructureAssembly)
      .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
      .AsImplementedInterfaces()
      .WithScopedLifetime());
    
    return services;
  }

  public static T GetOptions<T>(this IConfiguration configuration, string sectionName)
    where T : class, new()
  {
    var options = new T();
    var section = configuration.GetRequiredSection(sectionName);
    section.Bind(options);
    return options;
  }
  
}

