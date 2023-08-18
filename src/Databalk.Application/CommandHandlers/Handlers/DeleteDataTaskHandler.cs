using Databalk.Application.Abstractions;
using Databalk.Application.CommandHandlers.Commands;
using Databalk.Application.Exceptions;
using Databalk.Core.Repositories;

namespace Databalk.Application.CommandHandlers.Handlers;

public class DeleteDataTaskHandler : ICommandHandler<DeleteDataTask>
{
  private readonly IDataTaskRepository _repository;
  public DeleteDataTaskHandler(IDataTaskRepository repository)
  {
    _repository = repository;
  }

  public async Task HandleAsync(DeleteDataTask command)
  {
    var dataTask = await _repository.GetByIdAsync(command.Id) ?? throw new DataTaskNotFoundException(command.Id);

    await _repository.DeleteAsync(dataTask);
  }
}
