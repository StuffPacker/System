using Microsoft.AspNetCore.Mvc;

namespace SP.Web.Site.Features.Packinglist;

public class PackinglistController : Controller
{
    [Route("packinglist/{id}")]
    public ActionResult Packinglist(string id)
    {
        return View("packinglist", id);
    }
}