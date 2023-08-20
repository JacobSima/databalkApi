using Microsoft.Extensions.DependencyInjection;
using Databalk.Application.Abstractions;
using Databalk.Infrastructure.DAL;
using Microsoft.Extensions.Configuration;
using Databalk.Infrastructure.Security;
using Databalk.Infrastructure.Middleware;
using Databalk.Infrastructure.Auth;
using Databalk.Core.Abstractions;
using Microsoft.OpenApi.Models;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Databalk.Tests.Unit")]
namespace Databalk.Infrastructure;

public static class Extentions
{
  private const string OptionSectionName = "app";

  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {

    services.Configure<AppOptions>(configuration.GetRequiredSection(OptionSectionName));
    services.AddSingleton<ExceptionMiddleware>();
    services.AddSingleton<IClock, Clock>();
    services.AddHttpContextAccessor();

    services.AddDAL(configuration);

    services.AddSecurity();

    services.AddAuth(configuration);

    services.AddSwaggerGen(swagger =>
    {
      swagger.EnableAnnotations();
      swagger.SwaggerDoc("v1", new OpenApiInfo
      {
        Title = "Assessment Overview: Building a User’s and Tasks API",
        Version = "v1"
      });
    });

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

