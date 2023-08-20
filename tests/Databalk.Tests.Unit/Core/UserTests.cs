using Databalk.Tests.Unit;
using Microsoft.EntityFrameworkCore;
using Databalk.Core.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Databalk.Core.ValueObjects;

namespace Databalk.Tests.Unit.Core;

public class UserTests : IDisposable
{
  private readonly TestDatabase _testDatabase;
  private IUserFactory _userFactory;
  public UserTests()
  {
    _testDatabase = new TestDatabase();
    _userFactory = new UserFactory();
  }

  [Fact]
  public async Task create_user_should_return_a_new_user()
  {
    // ARRANGE 
    var user = _userFactory.Create(Guid.Parse("0ddde007-895e-4509-85e1-6fb6c4d0c3af"), "userokj1@databalk.io", "user1", "secret1");

    // ACT
    await _testDatabase.Context.Database.MigrateAsync();
    await _testDatabase.Context.Users.AddAsync(user);
    await _testDatabase.Context.SaveChangesAsync();
    var savedUser = await _testDatabase.Context.Users.SingleOrDefaultAsync(x => x.Id == user.Id);

    // ASSERT
    user.ShouldNotBeNull();
    user.Email.ShouldBe(new Email("userokj1@databalk.io"));
    user.Id.ShouldBe(new UserId(Guid.Parse("0ddde007-895e-4509-85e1-6fb6c4d0c3af")));
    savedUser.Username.ShouldBe(new Username("user1"));
  }

  public void Dispose()
  {
    _testDatabase.Dispose();
  }
}
