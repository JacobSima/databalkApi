
using Databalk.Application.Abstractions;
using Databalk.Application.DTO;
using Databalk.Application.Queries;
using Databalk.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Databalk.Infrastructure.DAL.Queries.Handlers;

internal class GetDataTasksHandler : IQueryHandler<GetDataTasks, IEnumerable<DataTaskDto>>
{
  private readonly DbSet<DataTask> _dataTaks;

  public GetDataTasksHandler(MyDataBalkDBContext context)
  {
    _dataTaks =  context.DataTasks;
  }

  public async Task<IEnumerable<DataTaskDto>> HandleAsync(GetDataTasks query)
    => await _dataTaks
        .AsNoTracking()
        .Select(x => x.AsDto())
        .ToListAsync();
}
