
using Databalk.Application.Abstractions;
using Databalk.Application.DTO;

namespace Databalk.Application.Queries;

public class GetUser : IQuery<UserDto>
{
  public Guid UserId {get; set;}  
}
