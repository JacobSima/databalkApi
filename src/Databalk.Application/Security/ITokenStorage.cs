
using Databalk.Application.DTO;

namespace Databalk.Application.Security;

public interface ITokenStorage
{
  void Set(JwtDto jwt); 

  JwtDto Get();
}
