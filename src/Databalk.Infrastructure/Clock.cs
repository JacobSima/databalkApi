using Databalk.Core.Abstractions;

namespace Databalk.Infrastructure;

internal class Clock : IClock
{
  public DateTime Current() => DateTime.UtcNow;
}
