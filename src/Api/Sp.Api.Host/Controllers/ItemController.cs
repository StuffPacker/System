using System.Security.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sp.Api.Business.Feature.Item.CreateItem;
using Sp.Api.Business.Feature.Item.DeleteItem;
using Sp.Api.Business.Feature.Item.GetItemById;
using Sp.Api.Business.Feature.Item.GetItems;
using Sp.Api.Business.Feature.Item.UpdateItem;
using SP.Shared.Common.Feature.PackingList.Dto;

namespace Sp.Api.Host.Controllers;

[Authorize]
public class ItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("SpApi/v1/item/")]
    public async Task<ActionResult<string>> Get()
    {
        var result = await _mediator.Send(new GetItemsCommand(GetUser()));

        return Ok(result);
    }

    [HttpPost("SpApi/v1/item/")]
    public async Task<ActionResult<string>> Create()
    {
        var result = await _mediator.Send(new CreateItemCommand(GetUser()));
        if (result == null)
        {
            result = new ItemDto();
        }

        return Ok(result);
    }

    [HttpDelete("SpApi/v1/item/{id}")]
    public async Task<ActionResult<string>> Delete(string id)
    {
        var result = await _mediator.Send(new DeleteItemCommand(id, GetUser()));
        if (result.IsNullOrEmpty())
        {
            result = string.Empty;
        }

        return Ok(result);
    }

    [HttpGet("SpApi/v1/item/{id}")]
    public async Task<ActionResult<string>> GetById(string id)
    {
        var result = await _mediator.Send(new GetItemByIdCommand(id, GetUser()));
        if (result == null)
        {
            result = new ItemDto();
        }

        return Ok(result);
    }

    [HttpPatch("SpApi/v1/item/{id}")]
    public async Task<ActionResult<string>> Update(string id, [FromBody] ItemEditInputDto inputModel)
    {
        var result = await _mediator.Send(new UpdateItemCommand(id, GetUser(), inputModel));

        return Ok(result);
    }

    private Guid GetUser()
    {
        if (User == null)
        {
            throw new AuthenticationException("No User pressent");
        }

        var user = User.Claims.FirstOrDefault(x => x.Type == "UserId");
        if (user == null)
        {
            throw new AuthenticationException("No userid pressent");
        }

        var parsed = Guid.TryParse(user!.Value, out var validUser);
        if (parsed == false)
        {
            throw new Exception("Cant parse userId");
        }

        return validUser;
    }
}