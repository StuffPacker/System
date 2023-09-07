using Microsoft.AspNetCore.Mvc;

namespace SP.Web.Site.Features.Home;

public class HomeController : Controller
{
    [Route("health")]
    public string Health()
    {
        return "Health " + DateTime.UtcNow.ToLongDateString();
    }

    [Route("")]
    public ActionResult Index()
    {
        return View("Index");
    }
}