using Microsoft.Extensions.Configuration;
using Databalk.Infrastructure;
using Databalk.Infrastructure.DAL;

namespace Databalk.Tests.Unit;

public sealed class OptionsProvider
{
  private readonly IConfiguration _configuration;

  public OptionsProvider()
  {
    _configuration = GetConfigurationRoot();
  }

  public T Get<T>(string sectionName) where T : class, new()
  => _configuration.GetOptions<T>(sectionName);

  private static IConfigurationRoot GetConfigurationRoot()
    => new ConfigurationBuilder().AddJsonFile("appsettings.test.json", true)
            .AddEnvironmentVariables()
            .Build();
}
