using Databalk.Core.Entities;
using Databalk.Core.Repositories;
using Databalk.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Databalk.Infrastructure.DAL.Repositories;

internal sealed class DataTaskRepository : IDataTaskRepository
{
  private readonly MyDataBalkDBContext _dbContext;
  private readonly DbSet<DataTask> _dataTasks;

  public DataTaskRepository(MyDataBalkDBContext dbContext)
  {
    _dbContext = dbContext;
    _dataTasks = _dbContext.DataTasks;
  }

  public async Task AddAsync(DataTask DataTask)
  {
    await _dataTasks.AddAsync(DataTask);
    await _dbContext.SaveChangesAsync();
  }

  public async Task DeleteAsync(DataTask DataTask)
  {
    _dataTasks.Remove(DataTask);
    await _dbContext.SaveChangesAsync();
  }

  public async Task<DataTask> GetByIdAsync(DataTaskId id) => await _dataTasks.SingleOrDefaultAsync(x => x.Id == id);

  public async Task<DataTask> GetByTitleAsync(Title title) => await _dataTasks.SingleOrDefaultAsync(x => x.Title == title);

  public async Task UpdateAsync(DataTask DataTask)
  {
    _dataTasks.Update(DataTask);
    await _dbContext.SaveChangesAsync();
  }
}
