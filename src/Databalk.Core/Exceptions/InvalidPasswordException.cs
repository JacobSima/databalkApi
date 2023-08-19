
namespace Databalk.Core.Exceptions;

public class InvalidPasswordException : CustomException
{
  public InvalidPasswordException(string password)
    : base($"The password: '{password}' entered is invalid")
  {
    Password = password;
  }

  public string Password {get;}  
}
