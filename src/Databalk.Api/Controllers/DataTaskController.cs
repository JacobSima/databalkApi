
using Databalk.Application.Abstractions;
using Databalk.Application.CommandHandlers.Commands;
using Databalk.Application.DTO;
using Databalk.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Databalk.Api.Controllers;

[ApiController]
[Route(template:"tasks")]
public class DataTaskController : ControllerBase
{
  // Commands
  private readonly ICommandHandler<CreateDataTask> _createTaskHanlder;
  private readonly ICommandHandler<DeleteDataTask> _deleteTaskHandler;
  private readonly ICommandHandler<UpdateDataTask> _updateTaskHandler;

  // Queries
  private readonly IQueryHandler<GetDataTask, DataTaskDto>  _getTaskHandler; 
  private readonly IQueryHandler<GetDataTasks, IEnumerable<DataTaskDto>> _getTasksHandler;

  public DataTaskController(
    ICommandHandler<CreateDataTask> createTaskHanlder,
    ICommandHandler<DeleteDataTask> deleteTaskHandler,
    ICommandHandler<UpdateDataTask> updateTaskHandler,
    IQueryHandler<GetDataTask, DataTaskDto> getTaskHandler,
    IQueryHandler<GetDataTasks, IEnumerable<DataTaskDto>> getTasksHandler)
  {
    _createTaskHanlder = createTaskHanlder;
    _deleteTaskHandler = deleteTaskHandler;
    _updateTaskHandler = updateTaskHandler;
    _getTaskHandler = getTaskHandler;
    _getTasksHandler = getTasksHandler;
  }

  [Authorize]
  [HttpGet]
  [SwaggerOperation("Get list of all the tasks")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  public async Task<ActionResult<IEnumerable<DataTaskDto>>> Get([FromQuery] GetDataTasks query)
    => Ok(await _getTasksHandler.HandleAsync(query));

  [Authorize]
  [HttpGet("{id:guid}")]
  [SwaggerOperation("Get a single task")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  public async Task<ActionResult<DataTaskDto>> Get([FromRoute] GetDataTask query)
    => Ok(await _getTaskHandler.HandleAsync(query));

  [Authorize]
  [HttpPost]
  [SwaggerOperation("Create a Task")]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  public async Task<ActionResult> Post([FromBody] CreateDataTask query)
  {
    await _createTaskHanlder.HandleAsync(query);
    return Ok();
  }

  [Authorize]
  [HttpPut]
  [SwaggerOperation("Get list of all the tasks")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  public async Task<ActionResult> Update([FromBody] UpdateDataTask query)
  {
    await _updateTaskHandler.HandleAsync(query);
    return Ok();
  }

  [Authorize]
  [HttpDelete("{id:guid}")]
   [SwaggerOperation("Delete a Task")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  public async Task<ActionResult> Delete([FromRoute] DeleteDataTask query)
  {
    await _deleteTaskHandler.HandleAsync(query);
    return Ok();
  }
    
}
