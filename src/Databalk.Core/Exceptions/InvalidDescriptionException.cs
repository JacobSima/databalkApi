namespace Databalk.Core.Exceptions;

public sealed class InvalidDescriptionException : CustomException
{
  public string Description {get;}
  public InvalidDescriptionException(string description) : base($"The Description: {description} entered is invalid")
  {
    Description = description;
  }
}
