
using Databalk.Application.Security;
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
  private readonly IUserFactory _userFactory;
  private readonly IPasswordManager _passwordManager;

  public DatabaseInitializer(IServiceProvider serviceProvider, IDataTaskFactory factory , IUserFactory userFactory, IPasswordManager passwordManager)
  {
    _serviceProvider = serviceProvider;
    _factory = factory;
    _userFactory = userFactory;
    _passwordManager = passwordManager;
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
      _factory.Create(Guid.Parse("4f6db573-ea6e-4e3f-bd3d-63f02fecd6e1"), "Task 1", "Description for Task 1", new DateTime(2023, 8, 25), Guid.Parse("d0b3d63e-9f7e-4bc0-aa63-5ffef39f5aa4")),
      _factory.Create(Guid.Parse("c38d7df5-7c74-40f7-9563-8952f5a3ce11"), "Task 2", "Description for Task 2", new DateTime(2023, 8, 27), Guid.Parse("c5e1e197-22e2-4b32-8f4d-66e800079f7e")),
      _factory.Create(Guid.Parse("79e077c3-08b4-4e7c-82a3-6b186b4bb9b8"), "Task 3", "Description for Task 3", new DateTime(2023, 8, 28), Guid.Parse("9e2a2fc2-7409-4d6b-95db-bc2f40f3d06f")),
      _factory.Create(Guid.Parse("a6c8dfc8-6895-4a4d-b2a3-9a792b57e55a"), "Task 4", "Description for Task 4", new DateTime(2023, 8, 30), Guid.Parse("0ddde007-895e-4509-85e1-6fb6c4d0c3af")),
      _factory.Create(Guid.Parse("f0eaaabe-6f2d-4515-a9b1-52dd944c189c"), "Task 5", "Description for Task 5", new DateTime(2023, 9, 1), Guid.Parse("c5e1e197-22e2-4b32-8f4d-66e800079f7e"))
    };

    var users =  new List<User>
    {
      _userFactory.Create(Guid.Parse("0ddde007-895e-4509-85e1-6fb6c4d0c3af"), "userokj1@databalk.io", "user1", "secret1"),
      _userFactory.Create(Guid.Parse("9e2a2fc2-7409-4d6b-95db-bc2f40f3d06f"), "userokj2@databalk.io", "user2", "secret2"),
      _userFactory.Create(Guid.Parse("c5e1e197-22e2-4b32-8f4d-66e800079f7e"), "userokj3@databalk.io", "user3", "secret3"),
      _userFactory.Create(Guid.Parse("d0b3d63e-9f7e-4bc0-aa63-5ffef39f5aa4"), "userokj4@databalk.io", "user4", "secret4"),

    };
    
    await dbContext.DataTasks.AddRangeAsync(tasks, cancellationToken);
    await dbContext.Users.AddRangeAsync(users);
    await dbContext.SaveChangesAsync(cancellationToken);
  }

  public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
