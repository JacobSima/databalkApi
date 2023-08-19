
using Databalk.Application.Abstractions;
using Databalk.Application.CommandHandlers.Commands;
using Databalk.Application.Exceptions;
using Databalk.Application.Security;
using Databalk.Core.Factories;
using Databalk.Core.Repositories;
using Databalk.Core.ValueObjects;
using Databalk.Core.Entities;

namespace Databalk.Application.CommandHandlers.Handlers;

internal sealed class SignupHandler : ICommandHandler<SignUp>
{
  private readonly IPasswordManager _passwordManager;
  private readonly IUserRepositoty _userRepositoty;
  private readonly IUserFactory _userFactory;

  public SignupHandler(
    IPasswordManager passwordManager,
    IUserRepositoty userRepositoty)
  {
    _passwordManager = passwordManager;
    _userRepositoty = userRepositoty;
  }

  public async Task HandleAsync(SignUp command)
  {
    var userId = new UserId(command.UserId);
    var email = new Email(command.Email);
    var username = new Username(command.Username);
    var password = new Password(command.Password);

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

    var user = new User(userId, email, username, securePassword);

    // Save into DB
    await _userRepositoty.AddAsync(user);
  }
}
