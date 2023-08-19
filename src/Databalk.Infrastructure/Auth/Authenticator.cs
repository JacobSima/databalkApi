
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Databalk.Application.DTO;
using Databalk.Application.Security;
using Databalk.Core.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Databalk.Infrastructure.Auth;

internal sealed class Authenticator : IAuthenticator
{
  private readonly IClock _clock;
  private readonly string _issuer;
  private readonly string _audience;
  private readonly SigningCredentials _signingCredentials;
  private readonly JwtSecurityTokenHandler _jwtSecurityToken = new ();
  private readonly TimeSpan _expiry;

  public Authenticator(IOptions<AuthOptions> options, IClock clock)
  {
    _clock = clock;
    _issuer = options.Value.Issuer;
    _audience = options.Value.Audience;
    _expiry = options.Value.Expiry ?? TimeSpan.FromHours(1);
    _signingCredentials = new SigningCredentials(
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SigningKey)), SecurityAlgorithms.HmacSha256);
  }

  public JwtDto CreateToken(Guid userId)
  {
    var now = _clock.Current();
    var claims = new List<Claim>
    {
      new(JwtRegisteredClaimNames.Sub, userId.ToString()),
      new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
    };
    var expires = now.Add(_expiry);
    var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expires, _signingCredentials);
    var token = _jwtSecurityToken.WriteToken(jwt);

    return new JwtDto
    {
      AccessToken = token
    };
  }
}
