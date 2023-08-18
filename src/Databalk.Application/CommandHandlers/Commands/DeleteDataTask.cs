
using Databalk.Application.Abstractions;

namespace Databalk.Application.CommandHandlers.Commands;

public record DeleteDataTask(Guid Id) : ICommand;


