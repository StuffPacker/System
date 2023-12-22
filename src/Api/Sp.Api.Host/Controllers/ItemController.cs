using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sp.Api.Business.Feature.Item.GetItems;

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
}