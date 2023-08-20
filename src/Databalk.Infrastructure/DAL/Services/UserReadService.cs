
using Databalk.Application.Services;
using Databalk.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Databalk.Infrastructure.DAL.Services;

public class UserReadService : IUserReadService
{
  private readonly DbSet<User> _users;

  public UserReadService(MyDataBalkDBContext context)
  {
    _users = context.Users;
  }

  public async Task<bool> ExistsByEmailAsync(string email)
    => await _users.AnyAsync(x => x.Email == email);


  Task<bool> IUserReadService.ExistsByUsernameAsync(string username)
    => _users.AnyAsync(x => x.Username == username);
}
