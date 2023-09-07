using Microsoft.AspNetCore.Mvc;

namespace SP.Web.Site.Features.Item;

public class ItemController : Controller
{
    [Route("item")]
    public ActionResult List()
    {
        return View("ItemList");
    }
}