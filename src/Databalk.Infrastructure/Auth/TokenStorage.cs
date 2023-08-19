
using Databalk.Application.DTO;
using Databalk.Application.Security;
using Microsoft.AspNetCore.Http;

namespace Databalk.Infrastructure.Auth;

internal sealed class TokenStorage : ITokenStorage
{
  private readonly IHttpContextAccessor _httpContextAccessor;
  private const string TokenKey = "jwt";

  public TokenStorage(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public JwtDto Get()
  {
    if(_httpContextAccessor.HttpContext is null) return null;
    if(_httpContextAccessor.HttpContext.Items.TryGetValue(TokenKey, out var jwt)) return jwt as JwtDto;
    return null;
  }

  public void Set(JwtDto jwt) => _httpContextAccessor.HttpContext?.Items.TryAdd(TokenKey, jwt);
}
