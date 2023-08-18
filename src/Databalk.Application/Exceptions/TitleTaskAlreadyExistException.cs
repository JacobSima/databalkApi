
using Databalk.Core.Exceptions;

namespace Databalk.Application.Exceptions;

public class TitleTaskAlreadyExistException : CustomException
{
  public string Title {get;}
  public TitleTaskAlreadyExistException(string title) : base($"Task with title: {title} already exists")
  {
    Title = title;
  }
}
