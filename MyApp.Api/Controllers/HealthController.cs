using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MyApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Status = "Healthy",
            Server = Dns.GetHostName(),
            Time = DateTime.UtcNow
        });
    }
}
