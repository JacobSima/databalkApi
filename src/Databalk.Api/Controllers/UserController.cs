
using Databalk.Application.Abstractions;
using Databalk.Application.CommandHandlers.Commands;
using Databalk.Application.DTO;
using Databalk.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Databalk.Api.Controllers;

[ApiController]
[Route(template:"users")]
public class UserController : ControllerBase
{
  private readonly ICommandHandler<SignUp> _signUpHandler;
  private IQueryHandler<GetUser, UserDto> _getUserHandler;
  private IQueryHandler<GetUsers, IEnumerable<UserDto>> _getUsersHandlers;

  public UserController(
    ICommandHandler<SignUp> signUpHandler,
    IQueryHandler<GetUser, UserDto> getUserHandler,
    IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandlers
  )
  {
    _signUpHandler = signUpHandler;
    _getUserHandler =  getUserHandler;
    _getUsersHandlers = getUsersHandlers;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromQuery] GetUsers query)
    => Ok(await _getUsersHandlers.HandleAsync(query));
  
  [HttpGet("{userId:guid}")]
  public async Task<ActionResult<UserDto>> Get(Guid userId)
  {
    var user = await _getUserHandler.HandleAsync(new GetUser {UserId = userId});
    return user ?? (ActionResult<UserDto>)NotFound();
  }

  [HttpPost("sign-up")]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult> Post(SignUp command)
  {
    command = command with {UserId = Guid.NewGuid()};
    await _signUpHandler.HandleAsync(command);
    return CreatedAtAction(nameof(Get), new {command.UserId}, null);
  }
}
