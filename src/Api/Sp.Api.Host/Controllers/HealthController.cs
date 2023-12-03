using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sp.Api.Host.Controllers;

[ApiController]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet("Health")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<string> Get()
    {
        _logger.Log(LogLevel.Information, "Check OK @ {Now}", DateTime.Now);
        return Ok($"Sp.Api.Host " + DateTime.Now);
    }

    [Authorize]
    [HttpGet("SecureHealth")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<string> GetSecure()
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "UserId");
        _logger.Log(LogLevel.Information, "Check OK @ {Now}", DateTime.Now);
        return Ok($"Sp.Api.Host Secure" + DateTime.Now + "for user: " + user!.Value);
    }
}