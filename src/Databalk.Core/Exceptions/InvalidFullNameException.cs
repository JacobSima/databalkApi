
namespace Databalk.Core.Exceptions;

public sealed class InvalidFullNameException : CustomException
{
  public InvalidFullNameException(string username) : base($"The Usernam: {username} is invalid")
  {
    Username = username;
  }

  public string Username {get;}
}
