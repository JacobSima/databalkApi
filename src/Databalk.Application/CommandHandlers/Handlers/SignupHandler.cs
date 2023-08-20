using Databalk.Application.Abstractions;
using Databalk.Application.CommandHandlers.Commands;
using Databalk.Application.Exceptions;
using Databalk.Application.Security;
using Databalk.Core.Factories;
using Databalk.Core.Repositories;

namespace Databalk.Application.CommandHandlers.Handlers;

internal sealed class SignupHandler : ICommandHandler<SignUp>
{
  private readonly IPasswordManager _passwordManager;
  private readonly IUserRepositoty _userRepositoty;
  private readonly IUserFactory _userFactory;

  public SignupHandler(
    IPasswordManager passwordManager,
    IUserRepositoty userRepositoty,
    IUserFactory userFactory
    )
  {
    _passwordManager = passwordManager;
    _userRepositoty = userRepositoty;
    _userFactory = userFactory;
  }

  public async Task HandleAsync(SignUp command)
  {
    var (userId, email, username, password) = command;

    if (await _userRepositoty.GetByEmailAsync(email) is not null)
    {
      throw new EmailAlreadyInUseException(email);
    }

    if (await _userRepositoty.GetByUsernameAsync(username) is not null)
    {
      throw new UsernameAlreadyInUseException(username);
    }

    // Secure password
    var securePassword = _passwordManager.Secure(password);

    var user =  _userFactory.Create(userId, email, username, securePassword);

    // Save into DB
    await _userRepositoty.AddAsync(user);
  }
}
