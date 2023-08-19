using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Databalk.Infrastructure.Auth;

public sealed class AuthOptions
{
  public string Issuer {get; set;}        // Server or application that generate this token
  public string Audience {get; set;}      // The allowed consumer of the token
  public string SigningKey {get; set;}    // Secure secret key only knowns by the server, to keep highly secret
  public TimeSpan? Expiry {get; set;}     // Lifetime of the token
}
