using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sp.Api.Business.Feature.Item.CreateItem;
using Sp.Api.Business.Feature.Item.DeleteItem;
using Sp.Api.Business.Feature.Item.GetItemById;
using Sp.Api.Business.Feature.Item.GetItems;
using Sp.Api.Business.Feature.Item.UpdateItem;

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
        var user = User.Claims.FirstOrDefault(x => x.Type == "UserId");

        var result = await _mediator.Send(new GetItemsCommand(Guid.Parse(user!.Value.ToString())));

        return Ok(result);
    }

    [HttpPut("SpApi/v1/item/")]
    public async Task<ActionResult<string>> Create()
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "UserId");
        var result = await _mediator.Send(new CreateItemCommand(Guid.Parse(user!.Value.ToString())));
        return Ok(result);
    }

    [HttpDelete("SpApi/v1/item/{id}")]
    public async Task<ActionResult<string>> Delete(string id)
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "UserId");
        var result = await _mediator.Send(new DeleteItemCommand(id, Guid.Parse(user!.Value.ToString())));
        return Ok(result);
    }

    [HttpGet("SpApi/v1/item/{id}")]
    public async Task<ActionResult<string>> GetById(string id)
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "UserId");
        var result = await _mediator.Send(new GetItemByIdCommand(id, Guid.Parse(user!.Value.ToString())));
        return Ok(result);
    }

    [HttpPatch("SpApi/v1/item/{id}")]
    public async Task<ActionResult<string>> Update(string id, [FromBody]ItemEditInputDto inputModel)
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "UserId");
        var result = await _mediator.Send(new UpdateItemCommand(id, Guid.Parse(user!.Value.ToString()), inputModel));
        return Ok(result);
    }
}