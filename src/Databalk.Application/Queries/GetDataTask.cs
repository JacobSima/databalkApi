
using Databalk.Application.Abstractions;
using Databalk.Application.DTO;

namespace Databalk.Application.Queries;

public class GetDataTask : IQuery<DataTaskDto>
{
  public Guid Id {get; set;}
}
