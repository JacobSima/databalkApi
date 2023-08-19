
using Databalk.Application.Abstractions;
using Databalk.Application.DTO;
using Databalk.Application.Queries;
using Databalk.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Databalk.Infrastructure.DAL.Queries.Handlers;

internal class GetDataTaskHandler : IQueryHandler<GetDataTask, DataTaskDto>
{
  private readonly DbSet<DataTask> _dataTaks;

  public GetDataTaskHandler(MyDataBalkDBContext context)
  {
    _dataTaks =  context.DataTasks;
  }

  public async Task<DataTaskDto> HandleAsync(GetDataTask query)
    => await _dataTaks
        .Where(x => x.Id == query.Id)
        .Select(x => x.AsDto())
        .AsNoTracking()
        .SingleOrDefaultAsync();
}
