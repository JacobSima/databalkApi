using Microsoft.AspNetCore.Mvc;

namespace Databalk.Api.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
  public HomeController()
  {

  }

  [HttpGet]
  public ActionResult Get() => Ok("Welcome to Databalk API");
}
