
using Databalk.Core.Exceptions;

namespace Databalk.Core.ValueObjects;

public sealed record UserId
{
  public Guid Value {get;}

  public UserId(Guid value)
  {
    if(value == Guid.Empty)
    {
      throw new InvalidEntityException("UserId Id not found"); 
    }

    Value = value;
  }

  public static UserId Create() => new(Guid.NewGuid());

  public static implicit operator Guid(UserId value) => value.Value;

  public static implicit operator UserId(Guid value) => new(value);

  public override string ToString() => Value.ToString(format: "N");

}
