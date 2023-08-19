
using Databalk.Core.Entities;
using Databalk.Core.ValueObjects;

namespace Databalk.Core.Factories;

public interface IUserFactory
{
  User Create(UserId id, Email email, Username username, Password password);
}
