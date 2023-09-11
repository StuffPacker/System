using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP.Web.Site.ViewModel;

namespace SP.Web.Site.Features.Item;

[Authorize]
[Route("api/v1/items/")]
public class ItemApiController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemApiController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [Route("")]
    public async Task<ActionResult> List()
    {
        var items = await _itemService.GetItems(GetUserId());
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = items
        });
    }

    [HttpPost("")]
    public async Task<ActionResult> Create()
    {
        var items = await _itemService.CreateItem(GetUserId());
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = items
        });
    }
}