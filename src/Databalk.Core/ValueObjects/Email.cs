using System.Text.RegularExpressions;
using Databalk.Core.Exceptions;

namespace Databalk.Core.ValueObjects;

public sealed record Email
{
   private static readonly Regex Regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

   public string Value { get; }

  public Email(string value)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      throw new InvalidEmailException(value);
    }

    if (value.Length > 100)
    {
      throw new InvalidEmailException(value);
    }

    value = value.ToLowerInvariant().Trim();
    var isMatch = !Regex.IsMatch(value);
    if (isMatch)
    {
      throw new InvalidEmailException(value);
    }

    Value = value;
  }

  public static implicit operator Email(string value) => new(value);
  
  public static implicit operator string(Email value) => value.Value; 

  public override string ToString() => Value;
}
