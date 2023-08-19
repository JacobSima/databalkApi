using Databalk.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Databalk.Api.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
  private readonly AppOptions _appOptions;
  public HomeController(IOptionsMonitor<AppOptions> appOptions)
  {
    _appOptions = appOptions.CurrentValue;
  }

  [HttpGet]
  public ActionResult Get() => Ok($"{_appOptions.Name}, {_appOptions.Description}");
}
