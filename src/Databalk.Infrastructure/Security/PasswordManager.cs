
using Databalk.Application.Security;
using Databalk.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Databalk.Infrastructure.Security;

public class PasswordManager : IPasswordManager
{
  private readonly IPasswordHasher<User> _passwordHasher;

  public PasswordManager(IPasswordHasher<User> passwordHasher)
  {
    _passwordHasher = passwordHasher;
  }

  public string Secure(string password)
    => _passwordHasher.HashPassword(default, password);

  public bool Validate(string password, string securePassword)
    => _passwordHasher.VerifyHashedPassword(default, securePassword, password)
    == PasswordVerificationResult.Success;
}
