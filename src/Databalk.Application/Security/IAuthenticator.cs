
using Databalk.Application.DTO;

namespace Databalk.Application.Security;

public interface IAuthenticator
{
  JwtDto CreateToken(Guid userId);  
}
