using Databalk.Application.CommandHandlers.Handlers;
using Databalk.Application.CommandHandlers.Commands;
using Databalk.Application.Security;
using Databalk.Application.Abstractions;
using Databalk.Core.Repositories;
using Databalk.Core.Factories;
using Databalk.Core.Entities;
using Databalk.Application.Exceptions;
using Databalk.Core.ValueObjects;
using Databalk.Application.Services;
using NSubstitute;

namespace Databalk.Tests.Unit.Application;

public class SignupHandlerTest
{
    Task Act(SignUp command)
      => _commandHandler.HandleAsync(command);

    [Fact]
    public async Task handleAsync_throws_emailAlreadyInUseException_when_amail_already_exists()
    {
      var command = new SignUp(Guid.Parse("d0b3d63e-9f7e-4bc0-aa63-5ffef39f5aa4"), "userokj4@databalk.io", "user4", "secret");

      _userReadService.ExistsByEmailAsync(command.Email).Returns(true);

      var exception = await Record.ExceptionAsync(() => Act(command));

      command.Password.ShouldBe("secret");
      exception.ShouldNotBeNull();
      exception.ShouldBeOfType<EmailAlreadyInUseException>();

    }

    [Fact]
    public async Task handleAsync_throws_usernameAlreadyInUseException_when_username_already_esists()
    {
      var command = new SignUp(Guid.Parse("d0b3d63e-9f7e-4bc0-aa63-5ffef39f5aa4"), "userokj4@databalk.io", "user4", "secret");

      _userReadService.ExistsByUsernameAsync(command.Username).Returns(true);

      var exception = await Record.ExceptionAsync(() => Act(command));

      exception.ShouldNotBeNull();
      exception.ShouldBeOfType<UsernameAlreadyInUseException>();
    }

    [Fact]
    public async Task handleAsync_factory_repository_on_success()
    {
      var command = new SignUp(Guid.Parse("d0b3d63e-9f7e-4bc0-aa63-5ffef39f5aa4"), "userokj4@databalk.io", "user4", "secret");
      _userReadService.ExistsByEmailAsync(command.Email).Returns(false);
      _userReadService.ExistsByUsernameAsync(command.Username).Returns(false);

    }

    #region ARRANGE
    private readonly ICommandHandler<SignUp> _commandHandler;
    private readonly IPasswordManager _passwordManager;
    private readonly IUserRepositoty _userRepositoty;
    private readonly IUserFactory _userFactory;
    private readonly IUserReadService _userReadService;

    public SignupHandlerTest()
    {
      _passwordManager = Substitute.For<IPasswordManager>();
      _userRepositoty = Substitute.For<IUserRepositoty>();
      _userFactory = Substitute.For<IUserFactory>();
      _userReadService = Substitute.For<IUserReadService>();

      _commandHandler = new SignupHandler(_passwordManager, _userRepositoty, _userFactory, _userReadService);
    }
    #endregion
}
