
using Databalk.Core.Entities;
using Databalk.Core.ValueObjects;

namespace Databalk.Core.Repositories;

public interface IDataTaskRepository
{
  Task<DataTask> GetByIdAsync(DataTaskId id);
  Task<DataTask> GetByTitleAsync(Title title);
  Task AddAsync(DataTask DataTask);
  Task UpdateAsync(DataTask DataTask);
  Task DeleteAsync(DataTask DataTask);
}
