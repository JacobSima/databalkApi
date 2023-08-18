using Microsoft.VisualBasic;

namespace Databalk.Core.ValueObjects;

public sealed class Date
{
  public DateTime Value {get;}

  public Date(DateTime value) => Value = value.Date;

  public Date AddDays(int days) => new(Value.AddDays(days));
  
  public static implicit operator DateTime(Date date) => date.Value;

  public static implicit operator Date(DateTime value) => new(value);

  public static Date Now = new(DateTime.Now);

  public override string ToString() => Value.ToString(format: "d");
}
