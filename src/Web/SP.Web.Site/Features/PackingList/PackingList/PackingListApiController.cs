using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP.Web.Business.Feature.Item;
using SP.Web.Business.Feature.PackingList;
using SP.Web.Business.Feature.PackingList.Mapper;
using SP.Web.Business.Feature.PackingList.ViewModel;
using SP.Web.Business.ViewModel;
using SP.Web.Site.Features.Item;

namespace SP.Web.Site.Features.PackingList.PackingList;

[Authorize]
[Route("api/v1/packinglist/")]
public class PackingListApiController : ControllerBase
{
    private readonly IPackingListService _packingListService;
    private readonly IMediator _mediator;

    public PackingListApiController(IPackingListService packingListService, IMediator mediator)
    {
        _packingListService = packingListService;
        _mediator = mediator;
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

    [AllowAnonymous]
    [Route("{id}/public")]
    public async Task<ActionResult> PackingListPublic(string id)
    {
        var result = await _mediator.Send(new GetPackingListPublicCommand(id));
        var vm = PackingListViewModelMapper.Map(result);
        if (result.IsPublic == true)
        {
            return Ok(new ResultJsonModel
            {
                Meta = new MetaModel { Code = 200 },
                ResultData = vm
            });
        }

        if (result.UserId == GetUserId())
        {
            return Ok(new ResultJsonModel
            {
                Meta = new MetaModel { Code = 200 },
                ResultData = vm
            });
        }

        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 404 },
            ResultData = string.Empty
        });
    }

    [HttpPost("{id}/Group")]
    public async Task<ActionResult> CreateGroup(string id)
    {
        var model = await _packingListService.GetPackingListById(id, GetUserId());
        var group = new PackingListGroupViewModel
        {
            Name = "New Group",
            Id = Guid.NewGuid().ToString(),
            Items = new List<PackingListGroupItemViewModel>()
        };
        model.Groups.Add(group);
        await _packingListService.Update(model, GetUserId());
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = group
        });
    }

    [HttpDelete("{id}/Group/{groupid}")]
    public async Task<ActionResult> DeleteGroup(string id, string groupid)
    {
        var model = await _packingListService.GetPackingListById(id, GetUserId());

        model.DeleteGroup(groupid);
        await _packingListService.Update(model, GetUserId());
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = string.Empty
        });
    }

    [HttpPatch("{id}/Name")]
    public async Task<ActionResult> UpdateName(string id, [FromBody] ChangeNameInputViewModel input)
    {
        var model = await _packingListService.GetPackingListById(id, GetUserId());
        model.Name = input.Name;
        await _packingListService.Update(model, GetUserId());
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = string.Empty
        });
    }

    [HttpPatch("{id}/Group/{groupid}/Name")]
    public async Task<ActionResult> UpdateGroupName(string id, [FromBody] ChangeNameInputViewModel input, string groupid)
    {
        var model = await _packingListService.GetPackingListById(id, GetUserId());
        model.UpdateGroupName(groupid, input.Name);
        await _packingListService.Update(model, GetUserId());
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = string.Empty
        });
    }

    [HttpPost("{id}/group/{groupid}/item")]
    public async Task<ActionResult> AddItemToGroup(string id, string groupid, [FromBody] AddItemToGroupInputViewModel input)
    {
        var packingList = await _packingListService.GetPackingListById(id, GetUserId());
        var item = await _mediator.Send(new GetItemByIdCommand(input.Id, GetUserId()));
        if (packingList == null || item == null)
        {
            throw new Exception();
        }

        packingList.AddItemToGroup(groupid, item);

        await _packingListService.Update(packingList, GetUserId());
        var group = packingList.Groups.First(x => x.Id == groupid);
        var itemOut = group.Items.First(x => x.Id == input.Id);
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = itemOut
        });
    }

    [HttpDelete("{id}/Group/{groupid}/item/{itemid}")]
    public async Task<ActionResult> DeleteGroupItem(string id, string groupid, string itemid)
    {
        var model = await _packingListService.GetPackingListById(id, GetUserId());

        model.DeleteGroupItem(groupid, itemid);
        await _packingListService.Update(model, GetUserId());
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = string.Empty
        });
    }

    [HttpPatch("{id}/Group/{groupid}/item/{itemid}/Quantity")]
    public async Task<ActionResult> UpdateGroupItemQuantity(string id, string groupid, string itemid, [FromBody] UpdateGroupItemQuantityInputViewModel input)
    {
        var model = await _packingListService.GetPackingListById(id, GetUserId());
        model.UpdateGroupItemQuantity(groupid, itemid, input.Quantity);

        await _packingListService.Update(model, GetUserId());
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = string.Empty
        });
    }

    [HttpPatch("{id}/public")]
    public async Task<ActionResult> UpdatePublic(string id, [FromBody] ChangePublicInputViewModel input)
    {
        var model = await _packingListService.GetPackingListById(id, GetUserId());
        model.IsPublic = input.MakePublic;
        await _packingListService.Update(model, GetUserId());
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = string.Empty
        });
    }

    [HttpPatch("{id}/group/{groupid}/item")]
    public async Task<ActionResult> UpdateGroupItems(string id, string groupId, [FromBody] UpdateGroupItemsInputViewModel input)
    {
        var result = await _mediator.Send(new UpdateGroupItemsCommand(id, groupId, input.SelectedItems, GetUserId()));
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = result
        });
    }
}