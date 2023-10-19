using Microsoft.AspNetCore.Mvc;

namespace SP.Web.Site.Features.StatusCode;

public class StatusCodeController : Controller
{
    private readonly ILogger<StatusCodeController> _logger;

    public StatusCodeController(ILogger<StatusCodeController> logger)
    {
        _logger = logger;
    }

    [Route("StatusCode/{code}")]
    public ActionResult StatusCode(string code)
    {
        string referer = Request.Headers["Referer"].ToString();
        _logger.LogError("StatusCode page, Code: " + code + " and Referer:" + referer);
        return View("StatusCode");
    }
}