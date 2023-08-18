namespace Databalk.Core.Exceptions;

public sealed class InvalidTitleException : CustomException
{
  public string Title {get;}
  public InvalidTitleException(string title) : base($"The Title: {title} entered is invalid")
  {
    Title = title;
  }
}
