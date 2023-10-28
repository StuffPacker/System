using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SP.Web.Site.Features.StatusCode;

public class StatusCodeController : Controller
{
    private readonly ILogger<StatusCodeController> _logger;

    public StatusCodeController(ILogger<StatusCodeController> logger)
    {
        _logger = logger;
    }

    [Route("StatusCode")]
    public IActionResult StatusCode([FromQuery]string statusCode)
    {
        var feauter = Request.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
        var path = feauter?.OriginalPath;
        _logger.LogError("StatusCode page, Code: " + statusCode + " and Referer:" + path);
        return View("StatusCode");
    }
}