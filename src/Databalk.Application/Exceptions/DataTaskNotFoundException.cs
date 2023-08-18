
using Databalk.Core.Exceptions;

namespace Databalk.Application.Exceptions;

public class DataTaskNotFoundException : CustomException
{
  public Guid Id {get;}

  public DataTaskNotFoundException(Guid id) : base($"Data Task with id: {id} was not found")
  {
    Id = id;
  }
}
