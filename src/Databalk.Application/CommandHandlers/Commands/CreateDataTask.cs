
using Databalk.Application.Abstractions;

namespace Databalk.Application.CommandHandlers.Commands;

public record CreateDataTask(Guid Id, string Title, string Description, DateTime DueDate) : ICommand;
