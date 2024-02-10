using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sp.Api.Business.Feature.PackingList;
using SP.Shared.Common.Feature.PackingList.Dto;
using SP.Shared.Common.Feature.PackingList.Mapper;
using SP.Shared.Common.Feature.PackingList.Model;

namespace Sp.Api.Host.Controllers;

[Authorize]
public class PackingListController : SpControllerBase
{
    private readonly IPackingListService _packingListService;
    private readonly IPackingListMapper _packingListMapper;

    public PackingListController(IPackingListService packingListService, IPackingListMapper packingListMapper)
    {
        _packingListService = packingListService;
        _packingListMapper = packingListMapper;
    }

    [HttpGet("SpApi/v1/packinglist/")]
    public async Task<ActionResult<string>> Get()
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "UserId");
        var dto = await _packingListService.GetPackingListsByUserId(Guid.Parse(user!.Value));

        return Ok(dto);
    }

    [HttpPost("SpApi/v1/packinglist/")]
    public async Task<ActionResult<string>> Create([FromBody] PackingListDto dtoIn)
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "UserId");
        var model = _packingListMapper.Map(dtoIn);
        var dto = await _packingListService.CreatePackingList(Guid.Parse(user!.Value), model);

        return Ok(dto);
    }

    [HttpGet("SpApi/v1/packinglist/{id}")]
    public async Task<ActionResult<string>> GetById(string id)
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

        return Ok(dto);
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

    [HttpDelete("SpApi/v1/packinglist/{id}")]
    public async Task<ActionResult<string>> Delete(string id)
    {
        await _packingListService.Delete(id, GetUser());
        return Ok();
    }

    [HttpPut("SpApi/v1/packinglist/{id}")]
    public async Task<ActionResult<string>> Update(string id, [FromBody]PackingListDto dto)
    {
        var pl = await _packingListService.GetPackingListById(id);
        if (pl.UserId != GetUser().ToString())
        {
            return Forbid();
        }

        await _packingListService.Update(dto);
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("SpApi/v1/packinglist/public")]
    public async Task<ActionResult<string>> GetPublic()
    {
        var list = await _packingListService.GetPackingListsPublic();
        return Ok(list);
    }
}