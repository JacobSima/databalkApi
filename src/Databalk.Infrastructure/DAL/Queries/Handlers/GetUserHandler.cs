
using Databalk.Application.Abstractions;
using Databalk.Application.DTO;
using Databalk.Application.Queries;
using Databalk.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Databalk.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetUserHandler : IQueryHandler<GetUser, UserDto>
{
  private readonly MyDataBalkDBContext _dbContext;

  public GetUserHandler(MyDataBalkDBContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<UserDto> HandleAsync(GetUser query)
  {
    var userId = new UserId(query.UserId);
    var user = await _dbContext.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == userId);
    return user.AsDto();
  }
}
