
using Databalk.Core.Entities;
using Databalk.Core.Factories;
using Databalk.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Databalk.Infrastructure;

internal sealed class DatabaseInitializer : IHostedService
{
  private readonly IServiceProvider _serviceProvider;
  private readonly IDataTaskFactory _factory;

  public DatabaseInitializer(IServiceProvider serviceProvider, IDataTaskFactory factory)
  {
    _serviceProvider = serviceProvider;
    _factory = factory;
  }

  public async Task StartAsync(CancellationToken cancellationToken)
  {
    using var scope = _serviceProvider.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<MyDataBalkDBContext>();
    await dbContext.Database.MigrateAsync(cancellationToken);

    // Seed some data
    if(await dbContext.DataTasks.AnyAsync(cancellationToken))
      return;
    
    var tasks = new List<DataTask>
    {
      _factory.Create(Guid.Parse("4f6db573-ea6e-4e3f-bd3d-63f02fecd6e1"), "Task 1", "Description for Task 1", new DateTime(2023, 8, 25)),
      _factory.Create(Guid.Parse("c38d7df5-7c74-40f7-9563-8952f5a3ce11"), "Task 2", "Description for Task 2", new DateTime(2023, 8, 27)),
      _factory.Create(Guid.Parse("79e077c3-08b4-4e7c-82a3-6b186b4bb9b8"), "Task 3", "Description for Task 3", new DateTime(2023, 8, 28)),
      _factory.Create(Guid.Parse("a6c8dfc8-6895-4a4d-b2a3-9a792b57e55a"), "Task 4", "Description for Task 4", new DateTime(2023, 8, 30)),
      _factory.Create(Guid.Parse("f0eaaabe-6f2d-4515-a9b1-52dd944c189c"), "Task 5", "Description for Task 5", new DateTime(2023, 9, 1))
    };
    
    await dbContext.DataTasks.AddRangeAsync(tasks, cancellationToken);
    await dbContext.SaveChangesAsync(cancellationToken);
  }

  public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
