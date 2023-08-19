
using Databalk.Core.Exceptions;

namespace Databalk.Application.Exceptions;

public class InvalidCredentialsException : CustomException
{
   public InvalidCredentialsException() : base("Invalid credentials"){}
  public InvalidCredentialsException(string message) : base(message){} 
}
