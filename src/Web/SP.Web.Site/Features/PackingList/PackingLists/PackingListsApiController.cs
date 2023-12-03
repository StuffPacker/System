using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP.Shared.Common;
using SP.Web.Business.Feature.PackingList;
using SP.Web.Business.ViewModel;

namespace SP.Web.Site.Features.PackingList.PackingLists;

[Authorize]
[Route("api/v1/packinglist")]
public class PackingListsApiController : ControllerBase
{
    private readonly IPackingListService _packingListService;

    public PackingListsApiController(IPackingListService packingListService)
    {
        _packingListService = packingListService;
    }

    [Route("")]
    public async Task<ActionResult> PackingLists()
    {
        var result = await _packingListService.GetPackingLists(GetUserId());
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = result
        });
    }

    [HttpPost("")]
    public async Task<ActionResult> Create()
    {
        var packingList = await _packingListService.CreatePackingList(GetUserId());
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = packingList
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        await _packingListService.Delete(id, GetUserId());
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = string.Empty
        });
    }
}