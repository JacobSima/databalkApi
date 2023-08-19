

using Databalk.Core.Exceptions;

namespace Databalk.Application.Exceptions;

public class InvalidEmailCredentialsException : CustomException
{
   public string Email {get; set;}
   public InvalidEmailCredentialsException(string email)
    : base($"Email: {email} is invalid")
  {
    Email = email;
  } 
}
