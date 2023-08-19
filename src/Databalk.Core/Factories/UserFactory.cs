
using Databalk.Core.Entities;
using Databalk.Core.ValueObjects;

namespace Databalk.Core.Factories;

public class UserFactory : IUserFactory
{
  public User Create(UserId id, Email email, Username username, Password password) => new(id, email, username, password);
}
