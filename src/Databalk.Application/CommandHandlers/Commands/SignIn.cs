
using Databalk.Application.Abstractions;

namespace Databalk.Application.CommandHandlers.Commands;

public record SignIn(string Email, string Password) : ICommand;