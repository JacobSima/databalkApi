
using Databalk.Application.Abstractions;
using Databalk.Application.CommandHandlers.Commands;
using Databalk.Application.Exceptions;
using Databalk.Application.Security;
using Databalk.Core.Repositories;

namespace Databalk.Application.CommandHandlers.Handlers;

public class SignInHandler : ICommandHandler<SignIn>
{
  private readonly IUserRepositoty _userRepositoty;
  private readonly IPasswordManager _passwordManager;
  private readonly IAuthenticator _authenticator;

  public SignInHandler(
    IUserRepositoty userRepositoty,
    IPasswordManager passwordManager,
    IAuthenticator authenticator
    )
  {
    _userRepositoty = userRepositoty;
    _passwordManager = passwordManager;
    _authenticator = authenticator;
  }

  public async Task HandleAsync(SignIn command)
  {
    var user = await _userRepositoty .GetByEmailAsync(command.Email) ?? throw new InvalidEmailCredentialsException(command.Email);
    if(!_passwordManager.Validate(command.Password, user.Password))
    {
      throw new InvalidCredentialsException("Invalid Password");
    }

    var jwt = _authenticator.CreateToken(user.Id);
    // _tokenStorage.Set(jwt);
  }
}
