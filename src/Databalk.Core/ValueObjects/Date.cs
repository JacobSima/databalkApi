using Microsoft.VisualBasic;

namespace Databalk.Core.ValueObjects;

public sealed class Date
{
  public DateTimeOffset Value {get;}

  public Date(DateTimeOffset value) => Value = value.Date;


  public static implicit operator DateTimeOffset(Date date) => date.Value;

  public static implicit operator Date(DateTimeOffset value) => new(value);

  public static implicit operator Date(DueDate v)
  {
    throw new NotImplementedException();
  }

  public static Date Now = new(DateTimeOffset.Now);

  public override string ToString() => Value.ToString(format: "d");
}
