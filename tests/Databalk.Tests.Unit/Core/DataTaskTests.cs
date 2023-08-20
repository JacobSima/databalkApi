using Databalk.Tests.Unit;
using Microsoft.EntityFrameworkCore;
using Databalk.Core.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Databalk.Core.ValueObjects;

namespace Databalk.Tests.Unit.Core;

public class DataTaskTests
{
  private IUserFactory _userFactory;
  private IDataTaskFactory _taskFactory;

  public DataTaskTests()
  {
    _userFactory = new UserFactory();
    _taskFactory = new DataTaskFactory();
  }

  [Fact]
  public async Task create_task_should_return_a_new_task()
  {
    // ARRANGE 
    var user = _userFactory.Create(Guid.Parse("9e2a2fc2-7409-4d6b-95db-bc2f40f3d06f"), "userokj1@databalk.io", "user1", "secret1");

    // ACT
    var task = _taskFactory.Create(Guid.Parse("4f6db573-ea6e-4e3f-bd3d-63f02fecd6e1"), "Task 1", "Description for Task 1", new DateTime(2023, 8, 30), user.Id);
    
  
    // ASSERT
    user.ShouldNotBeNull();
    task.AssigneeId.ShouldBe(new UserId(user.Id));

  }
}
