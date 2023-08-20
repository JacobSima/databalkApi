using Microsoft.EntityFrameworkCore;
using Databalk.Application.Services;
using Databalk.Core.Repositories;
using Databalk.Infrastructure.DAL.Repositories;
using Databalk.Infrastructure.DAL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Databalk.Infrastructure.DAL;

public static class Extentions
{
  private const string OptionSectionName = "postgres";
  public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
  {

    // Configure and connect to PostgresDB 
    services.Configure<PostgresOptions>(configuration.GetRequiredSection(OptionSectionName));
    var postgresOptions = configuration.GetOptions<PostgresOptions>(OptionSectionName);
    services.AddDbContext<MyDataBalkDBContext>(x => x.UseNpgsql(postgresOptions.ConnectionString));

    services.AddScoped<IDataTaskRepository, DataTaskRepository>();
    services.AddScoped<IDataTaskReadService, DataTaskReadService>();
    services.AddScoped<IUserReadService, UserReadService>();
    services.AddScoped<IUserRepositoty, UserRepository>();

    services.AddHostedService<DatabaseInitializer>();

    // EF core + Npsql issue
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    
    return services;
  }
}
