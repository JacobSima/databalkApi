
using Databalk.Application.Abstractions;
using Databalk.Application.DTO;
using Databalk.Application.Queries;
using Microsoft.EntityFrameworkCore;

namespace Databalk.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetUsersHandler : IQueryHandler<GetUsers, IEnumerable<UserDto>>
{
  private readonly MyDataBalkDBContext _dbContext;

  public GetUsersHandler(MyDataBalkDBContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<IEnumerable<UserDto>> HandleAsync(GetUsers query)
  => await _dbContext.Users
        .AsNoTracking()
        .Select(x => x.AsDto())
        .ToListAsync();
}
