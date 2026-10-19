using Microsoft.AspNetCore.Mvc;

namespace ReDevice.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "Healthy",
            service = "ReDevice API",
            timestampUtc = DateTime.UtcNow,
            layers = new[]
            {
                "ReDevice.Presentation",
                "ReDevice.Business",
                "ReDevice.Persistence",
                "ReDevice.Database"
            }
        });
    }
}

