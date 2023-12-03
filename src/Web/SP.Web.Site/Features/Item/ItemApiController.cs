using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP.Shared.Common;
using SP.Web.Business.Feature.Item;
using SP.Web.Business.Feature.Item.CreateItem;
using SP.Web.Business.Feature.Item.DeleteItem;
using SP.Web.Business.Feature.Item.GetItemById;
using SP.Web.Business.Feature.Item.GetItems;
using SP.Web.Business.Feature.Item.UpdateItem;
using SP.Web.Business.ViewModel;

namespace SP.Web.Site.Features.Item;

[Authorize]
[Route("api/v1/items/")]
public class ItemApiController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("")]
    public async Task<ActionResult> List()
    {
        var items = await _mediator.Send(new GetItemsCommand(GetUserId()));
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = items
        });
    }

    [HttpPost("")]
    public async Task<ActionResult> Create()
    {
        var items = await _mediator.Send(new CreateItemCommand(GetUserId()));
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = items
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        await _mediator.Send(new DeleteItemCommand(id, GetUserId()));
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = string.Empty
        });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, [FromBody] ItemEditInputViewModel model)
    {
        await _mediator.Send(new UpdateItemCommand(id, GetUserId(), model));
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = string.Empty
        });
    }

    [Route("{id}")]
    public async Task<ActionResult> GetById(string id)
    {
        var item = await _mediator.Send(new GetItemByIdCommand(id, GetUserId()));
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = item
        });
    }
}