using Microsoft.AspNetCore.Mvc;
using SP.Web.Site.ViewModel;

namespace SP.Web.Site.Features.Item;

[Route("api/v1/items/")]
public class ItemApiController : Controller
{
    private readonly IItemService _itemService;

    public ItemApiController(IItemService itemService)
    {
        _itemService = itemService;
    }

    public ActionResult List()
    {
        var items = _itemService.GetItems();
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = items
        });
    }
}