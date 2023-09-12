using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP.Web.Site.Features.PackingList;
using SP.Web.Site.ViewModel;

namespace SP.Web.Site.Features.Packinglist;

[Authorize]
[Route("api/v1/packinglist/")]
public class PackingListApiController : ControllerBase
{
    private readonly IPackingListService _packingListService;

    public PackingListApiController(IPackingListService packingListService)
    {
        _packingListService = packingListService;
    }

    [Route("{id}")]
    public async Task<ActionResult> PackingList(string id)
    {
        var result = await _packingListService.GetPackingListById(id, GetUserId());
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = result
        });
    }
}