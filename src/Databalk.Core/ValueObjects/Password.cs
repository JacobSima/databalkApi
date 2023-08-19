
using Databalk.Core.Exceptions;

namespace Databalk.Core.ValueObjects;

public sealed record Password
{
   public string Value {get;}

  public Password(string value)
  {
    if(string.IsNullOrWhiteSpace(value))
    {
      throw new InvalidPasswordException(value);
    }

    Value = value;
  }

  public static implicit operator Password(string value) => new(value);
  public static implicit operator string(Password value) => value.Value;
}
