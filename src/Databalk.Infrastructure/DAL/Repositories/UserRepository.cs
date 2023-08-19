
using Databalk.Core.Entities;
using Databalk.Core.Repositories;
using Databalk.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Databalk.Infrastructure.DAL.Repositories;

internal sealed class UserRepository : IUserRepositoty
{
  private readonly MyDataBalkDBContext _dbContext;
  private readonly DbSet<User> _user;

  public UserRepository(MyDataBalkDBContext dbContext)
  {
    _dbContext = dbContext;
    _user = dbContext.Users;
  }

  public async Task AddAsync(User user)
  {
    await _user.AddAsync(user);
    await _dbContext.SaveChangesAsync();
  }

  public async Task<User> GetByEmailAsync(Email email)
    => await _user.FirstOrDefaultAsync(x => x.Email == email);

  public async Task<User> GetByIdAsync(UserId id)
    => await _user.FirstOrDefaultAsync(x => x.Id == id);

  public async Task<User> GetByUsernameAsync(Username username)
    => await _user.FirstOrDefaultAsync(x => x.Username == username);
}
