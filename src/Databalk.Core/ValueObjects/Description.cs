using Databalk.Core.Exceptions;

namespace Databalk.Core.ValueObjects;

public sealed record Description
{
  public string Value {get;}

  public Description(string value)
  {
    if(value.Length is > 1000)
    {
      throw new InvalidDescriptionException(value);
    }

    Value = (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) ? string.Empty : value;
  }

  public static implicit operator Description(string value) => new(value);

  public static implicit operator string(Description value) => value.Value;

  public override string ToString() => Value;
}
