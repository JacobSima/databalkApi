using Databalk.Core.Exceptions;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Databalk.Infrastructure.Middleware;

public sealed class ExceptionMiddleware : IMiddleware
{
  private readonly ILogger<ExceptionMiddleware> _logger;

  public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
  {
    _logger = logger;
  }

  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    try
    {
      await next(context);
    }
    catch (Exception exception)
    {
      _logger.LogError(exception, exception.Message);
      await HandleExceptionAsync(exception, context);
    }
  }

  private static async Task HandleExceptionAsync(Exception exception, HttpContext context)
  {
    // pattern matching
    var (statusCode, error) = exception switch
    {
      CustomException => (StatusCodes.Status400BadRequest,
      new Error(exception.GetType().Name.Underscore().Replace("_exception", string.Empty),
      exception.Message)),
      _ => (StatusCodes.Status500InternalServerError, new Error("error", "There was an error"))
    };

    context.Response.StatusCode = statusCode;
    await context.Response.WriteAsJsonAsync(error);
  }

  // Json object to return
  private record Error(string Code, string Reason);
}
