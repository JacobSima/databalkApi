
using Databalk.Core.Exceptions;

namespace Databalk.Application.Exceptions;

public class EmailAlreadyInUseException : CustomException
{
  public string Email {get; set;}
  public EmailAlreadyInUseException(string email)
    : base($"This email address: {email} is already in use")
  {
    Email = email;
  } 
}
