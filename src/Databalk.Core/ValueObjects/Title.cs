using Databalk.Core.Exceptions;

namespace Databalk.Core.ValueObjects;

public sealed record Title
{
  public string Value {get;}

  public Title(string value)
  {
    if(string.IsNullOrWhiteSpace(value) || value.Length is > 100 or < 2)
    {
      throw new InvalidTitleException(value);
    }

    Value = value;
  }

  public static implicit operator Title(string value) => new(value);

  public static implicit operator string(Title value) => value.Value;

  public override string ToString() => Value;
}
