
using Databalk.Application.Abstractions;
using Databalk.Application.CommandHandlers.Commands;
using Databalk.Application.Exceptions;
using Databalk.Application.Services;
using Databalk.Core.Abstractions;
using Databalk.Core.Factories;
using Databalk.Core.Repositories;

namespace Databalk.Application.CommandHandlers.Handlers;

public class CreateDataTaskHandler : ICommandHandler<CreateDataTask>
{
  private readonly IDataTaskRepository _repository;
  private readonly IDataTaskFactory _factory;
  private readonly IDataTaskReadService _readService;
  private readonly IClock _clock;

  public CreateDataTaskHandler(
    IDataTaskRepository repository,
    IDataTaskFactory factory,
    IDataTaskReadService readService,
    IClock clock)
  {
    _repository = repository;
    _factory = factory;
    _readService = readService;
    _clock = clock;
  }

  public async Task HandleAsync(CreateDataTask command)
  {
    var(id, title, description, dueDate, assignee) = command;
    if(await _readService.ExistsByTitleAsync(title))
    {
      throw new TitleTaskAlreadyExistException(title);
    }

    if (assignee == Guid.Empty || await _repository.GetByIdAsync(assignee) == null)
    {
      throw new NotAssigneeFoundException(assignee);
    }

    if(dueDate < _clock.Current())
    {
      throw new InvalidDueDateException(dueDate);
    }

    var dataTask = _factory.Create(id, title, description, dueDate, assignee);

    await _repository.AddAsync(dataTask);
  }
}
