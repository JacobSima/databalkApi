
using Databalk.Application.Services;
using Databalk.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Databalk.Infrastructure.DAL.Services;

internal class DataTaskReadService : IDataTaskReadService
{
  private readonly DbSet<DataTask> _dataTasks;

  public DataTaskReadService(MyDataBalkDBContext context)
  {
    _dataTasks = context.DataTasks;
  }

  public async Task<bool> ExistsByTitleAsync(string title) => await _dataTasks.AnyAsync(x => x.Title == title);
}
