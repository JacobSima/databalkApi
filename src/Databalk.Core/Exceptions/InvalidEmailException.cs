
namespace Databalk.Core.Exceptions;

public class InvalidEmailException : CustomException
{
  public InvalidEmailException(string email)
    : base($"The email: '{email}' entered is invalid")
  {
    Email = email;
  }

  public string Email {get;}
}
