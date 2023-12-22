using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sp.Api.Business.Feature.PackingList;

namespace Sp.Api.Host.Controllers;

[Authorize]
public class PackingListController : ControllerBase
{
    private readonly IPackingListService _packingListService;

    public PackingListController(IPackingListService packingListService)
    {
        _packingListService = packingListService;
    }

    [HttpGet("SpApi/v1/packinglist/{id}")]
    public async Task<ActionResult<string>> Get(string id)
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "UserId");
        var dto = await _packingListService.GetPackingListById(id);
        if (dto.UserId == user!.Value)
        {
            return Ok(dto);
        }

        return Forbid();
    }

    [AllowAnonymous]
    [HttpGet("SpApi/v1/packinglist/{id}/public")]
    public async Task<ActionResult<string>> GetPublic(string id)
    {
        var dto = await _packingListService.GetPackingListById(id);
        if (dto.IsPublic)
        {
            return Ok(dto);
        }

        return Forbid();
    }

    [HttpGet("SpApi/v1/packinglist/{id}/print")]
    public async Task<ActionResult<string>> GetPrint(string id)
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "UserId");
        var dto = await _packingListService.GetPackingListById(id);
        if (dto.IsPublic || dto.UserId == user!.Value)
        {
            return Ok(dto);
        }

        return Forbid();
    }
}