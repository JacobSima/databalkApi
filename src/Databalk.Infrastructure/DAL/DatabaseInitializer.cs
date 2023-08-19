
using Databalk.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Databalk.Infrastructure;

internal sealed class DatabaseInitializer : IHostedService
{
  private readonly IServiceProvider _serviceProvider;

  public DatabaseInitializer(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async Task StartAsync(CancellationToken cancellationToken)
  {
    using var scope = _serviceProvider.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<MyDataBalkDBContext>();
    await dbContext.Database.MigrateAsync(cancellationToken);

    await dbContext.SaveChangesAsync(cancellationToken);
  }

  public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
