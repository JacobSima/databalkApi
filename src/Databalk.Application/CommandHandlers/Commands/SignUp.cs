

using Databalk.Application.Abstractions;

namespace Databalk.Application.CommandHandlers.Commands;

public record SignUp(Guid UserId, string Email, string Username, string Password) : ICommand;
