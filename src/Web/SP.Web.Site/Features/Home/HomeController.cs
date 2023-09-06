using Microsoft.AspNetCore.Mvc;

namespace SP.Web.Site.Features.Home;

public class HomeController : Controller
{
    [Route("health")]
    public string Index()
    {
        return "Health " + DateTime.UtcNow.ToLongDateString();
    }
}