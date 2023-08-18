using Databalk.Core.Entities;
using Databalk.Core.ValueObjects;

namespace Databalk.Core.Factories;

public interface IDataTaskFactory
{
  DataTask Create(DataTaskId id, Title Title,  Description Description, Date DueDate);
}
