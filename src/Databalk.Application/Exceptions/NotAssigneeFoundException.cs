
using Databalk.Core.Exceptions;

namespace Databalk.Application.Exceptions;

public class NotAssigneeFoundException : CustomException
{
  public Guid Id {get; set;}
  public NotAssigneeFoundException(Guid id)
    : base($"Not Assignee Found with Id: {id} ")
  {
    Id = id;
  }
}
