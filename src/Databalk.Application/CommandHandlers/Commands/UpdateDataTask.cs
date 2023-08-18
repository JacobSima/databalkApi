
using Databalk.Application.Abstractions;

namespace Databalk.Application.CommandHandlers.Commands;

public record UpdateDataTask(Guid Id, string Title, string Description, DateTime DueDate) : ICommand;
