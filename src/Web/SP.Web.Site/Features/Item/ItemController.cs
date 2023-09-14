using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SP.Web.Site.Features.Item;

[Authorize]
public class ItemController : Controller
{
    [Route("item")]
    public ActionResult List()
    {
        return View("ItemList");
    }

    [Route("item/{id}")]
    public ActionResult GetById(string id)
    {
        return View("Item", id);
    }
}