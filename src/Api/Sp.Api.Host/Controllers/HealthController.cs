using Microsoft.AspNetCore.Mvc;

namespace Sp.Api.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "Health")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<string> Get()
    {
        _logger.Log(LogLevel.Information, "Check OK @ {Now}", DateTime.Now);
        return Ok($"Sp.Api.Host " + DateTime.Now);
    }
}