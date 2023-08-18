using Databalk.Core.Exceptions;

namespace Databalk.Core.ValueObjects;

public sealed class DataTaskId
{
  public Guid Value {get;}

  public DataTaskId(Guid value)
  {
    if(value == Guid.Empty)
    {
      throw new InvalidEntityException("Task Id not found");
    }

    Value = value;
  }

  public static DataTaskId Create() => new(Guid.NewGuid());

  public static implicit operator Guid(DataTaskId value) => value.Value;

  public static implicit operator DataTaskId(Guid value) => new(value);
}
