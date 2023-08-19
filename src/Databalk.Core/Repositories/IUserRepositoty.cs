
using Databalk.Core.Entities;
using Databalk.Core.ValueObjects;

namespace Databalk.Core.Repositories;

public interface IUserRepositoty
{
  Task<User> GetByIdAsync(UserId id);

  Task<User> GetByEmailAsync(Email email);

  Task<User> GetByUsernameAsync(Username username);
  
  Task AddAsync(User user);
}
