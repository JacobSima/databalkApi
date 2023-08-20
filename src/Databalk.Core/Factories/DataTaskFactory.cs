using Databalk.Core.Entities;
using Databalk.Core.ValueObjects;

namespace Databalk.Core.Factories;

public class DataTaskFactory : IDataTaskFactory
{
  public DataTask Create(DataTaskId id, Title title, Description description, Date dueDate, UserId assignee) => new(id, title, description, dueDate, assignee);

}
