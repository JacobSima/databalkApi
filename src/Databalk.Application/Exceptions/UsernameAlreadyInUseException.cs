
using Databalk.Core.Exceptions;

namespace Databalk.Application.Exceptions;

public class UsernameAlreadyInUseException : CustomException
{
  public string Username {get; set;}
  public UsernameAlreadyInUseException(string username)
    : base($"This username : {username} is already taken")
  {
    Username = username;
  } 
}
