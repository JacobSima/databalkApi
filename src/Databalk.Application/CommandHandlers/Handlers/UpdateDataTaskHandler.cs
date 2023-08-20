using Databalk.Application.Abstractions;
using Databalk.Application.CommandHandlers.Commands;
using Databalk.Application.Exceptions;
using Databalk.Core.Factories;
using Databalk.Core.Repositories;

namespace Databalk.Application.CommandHandlers.Handlers;

public class UpdateDataTaskHandler : ICommandHandler<UpdateDataTask>
{
  private readonly IDataTaskRepository _repository;
  public UpdateDataTaskHandler(IDataTaskRepository repository)
  {
    _repository = repository;
  }

  public async Task HandleAsync(UpdateDataTask command)
  {
    var dataTask = await _repository.GetByIdAsync(command.Id) ?? throw new DataTaskNotFoundException(command.Id);

    var(id, title, description, dueDate, assignee) = command;
    dataTask.Update(title, description, dueDate, assignee);

    await _repository.UpdateAsync(dataTask);
  }
}
