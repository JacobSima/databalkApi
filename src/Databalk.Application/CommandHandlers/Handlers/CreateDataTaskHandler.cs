
using Databalk.Application.Abstractions;
using Databalk.Application.CommandHandlers.Commands;
using Databalk.Application.Exceptions;
using Databalk.Application.Services;
using Databalk.Core.Factories;
using Databalk.Core.Repositories;

namespace Databalk.Application.CommandHandlers.Handlers;

public class CreateDataTaskHandler : ICommandHandler<CreateDataTask>
{
  private readonly IDataTaskRepository _repository;
  private readonly IDataTaskFactory _factory;
  private readonly IDataTaskReadService _readService;

  public CreateDataTaskHandler(
    IDataTaskRepository repository,
    IDataTaskFactory factory,
    IDataTaskReadService readService)
  {
    _repository = repository;
    _factory = factory;
    _readService = readService;
  }

  public async Task HandleAsync(CreateDataTask command)
  {
    var(id, title, description, dueDate) = command;
    if(await _readService.ExistsByTitleAsync(title))
    {
      throw new TitleTaskAlreadyExistException(title);
    }

    var dataTask = _factory.Create(id, title, description, dueDate);

    await _repository.AddAsync(dataTask);
  }
}
