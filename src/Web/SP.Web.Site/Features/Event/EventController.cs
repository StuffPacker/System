using Microsoft.AspNetCore.Mvc;

namespace SP.Web.Site.Features.Event;

public class EventController : Controller
{
    [Route("event")]
    public ActionResult Events()
    {
        return View("Events");
    }

    [Route("event/{id}")]
    public ActionResult Event(string id)
    {
        return View("Event", id);
    }
}