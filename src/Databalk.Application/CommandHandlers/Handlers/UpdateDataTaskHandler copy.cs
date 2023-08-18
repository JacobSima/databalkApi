using Databalk.Application.Abstractions;
using Databalk.Application.CommandHandlers.Commands;
using Databalk.Application.Exceptions;
using Databalk.Core.Factories;
using Databalk.Core.Repositories;

namespace Databalk.Application.CommandHandlers.Handlers;

public class UpdateDataTaskHandler : ICommandHandler<UpdateDataTask>
{
  private readonly IDataTaskRepository _repository;
  private readonly IDataTaskFactory _factory;
  public UpdateDataTaskHandler(IDataTaskRepository repository, IDataTaskFactory factory)
  {
    _repository = repository;
    _factory = factory;
    
  }

  public async Task HandleAsync(UpdateDataTask command)
  {
    var dataTask = await _repository.GetByIdAsync(command.Id) ?? throw new DataTaskNotFoundException(command.Id);

    var(id, title, description, dueDate) = command;
    var updateDataTask = _factory.Create(dataTask.Id, title, description, dueDate);

    await _repository.UpdateAsync(updateDataTask);
  }
}
