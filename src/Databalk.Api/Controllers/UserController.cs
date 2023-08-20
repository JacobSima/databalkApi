
using Databalk.Application.Abstractions;
using Databalk.Application.CommandHandlers.Commands;
using Databalk.Application.DTO;
using Databalk.Application.Queries;
using Databalk.Application.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Databalk.Api.Controllers;

[ApiController]
[Route(template:"users")]
public class UserController : ControllerBase
{
  private readonly ICommandHandler<SignUp> _signUpHandler;
  private readonly ICommandHandler<SignIn> _signInHandler;
  private readonly IQueryHandler<GetUser, UserDto> _getUserHandler;
  private readonly IQueryHandler<GetUsers, IEnumerable<UserDto>> _getUsersHandlers;
  private readonly IAuthenticator  _authenticator;
  private readonly ITokenStorage _tokenStorage;

  public UserController(
    ICommandHandler<SignUp> signUpHandler,
    IQueryHandler<GetUser, UserDto> getUserHandler,
    IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandlers,
    IAuthenticator  authenticator,
    ICommandHandler<SignIn> signInHandler,
    ITokenStorage tokenStorage
  )
  {
    _signUpHandler = signUpHandler;
    _getUserHandler =  getUserHandler;
    _getUsersHandlers = getUsersHandlers;
    _authenticator = authenticator;
    _signInHandler = signInHandler;
    _tokenStorage = tokenStorage;
  }

  [Authorize]
  [HttpGet]
  public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromQuery] GetUsers query)
    => Ok(await _getUsersHandlers.HandleAsync(query));
  
  [Authorize]
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

   [HttpPost("sing-in")]
  public async Task<ActionResult<JwtDto>> Post(SignIn command)
  {
    await _signInHandler.HandleAsync(command);
    var jwt = _tokenStorage.Get();
    return Ok(jwt);
  }


  // Token Testing(Case testing)
  [HttpGet(template:"jwt")]
  public ActionResult<JwtDto> GetJwt()
  {
    var token = _authenticator.CreateToken(Guid.NewGuid());
    return token;
  }

  [Authorize]
  [HttpGet("secret")]
  public ActionResult<string> GetSecret()
  {
    var user = HttpContext.User;
    return "Secret";
  }
}
