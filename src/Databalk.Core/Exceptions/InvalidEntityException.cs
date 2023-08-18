
namespace Databalk.Core.Exceptions;

public class InvalidEntityException : CustomException
{
  public string Error {get;}

  public InvalidEntityException(string error) : base($"The Entity error is: {error}")
  {
    Error = error;
  }
}
