
using Databalk.Core.Exceptions;

namespace Databalk.Application.Exceptions;

public class InvalidDueDateException : CustomException
{
  public DateTime DueDate {get; set;}
  public InvalidDueDateException(DateTime dueDate) : base($"DueDate must be a future date, {dueDate} is invalid")
  {
    DueDate = dueDate;
  }
}
