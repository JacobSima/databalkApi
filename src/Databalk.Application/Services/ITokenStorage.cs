
using Databalk.Application.DTO;

namespace Databalk.Application.Services;

public interface ITokenStorage
{
  void Set(JwtDto jwt);
  JwtDto Get();
}
