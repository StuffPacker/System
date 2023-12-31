using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP.Shared.Common;

namespace SP.Web.Site.Features.PackingList.PackingLists;

[Authorize]
public class PackingListsController : ControllerBase
{
    [Route("packinglist/")]
    public ActionResult Packinglists(string id)
    {
        return View("packinglists", id);
    }
}