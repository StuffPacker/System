using MediatR;
using Microsoft.AspNetCore.Mvc;
using SP.Web.Business.Feature.PackingList.GetPackingList;

namespace SP.Web.Site.Features.PackingList.PackingListPublic;

public class PackingListPublicController : ControllerBase
{
    private readonly IMediator _mediator;

    public PackingListPublicController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("packinglist/{id}/public")]
    public async Task<ActionResult> Packinglist(string id)
    {
        var model = await _mediator.Send(new GetPackingListCommand(id));
        if (model.IsPublic)
        {
            return View("PackingListPublic", id);
        }

        if (GetUserId() == Guid.Empty)
        {
            return NotFound();
        }

        if (model.UserId == GetUserId())
        {
            return View("PackingListPublic", id);
        }

        return NotFound();
    }
}