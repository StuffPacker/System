using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SP.Web.Site.Features.Packinglist;

[Authorize]
public class PackinglistController : Controller
{
    [Route("packinglist/{id}")]
    public ActionResult Packinglist(string id)
    {
        return View("packinglist", id);
    }
}