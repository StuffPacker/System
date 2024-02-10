using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP.Web.Business.Feature.PackingList;
using SP.Web.Business.Feature.PackingList.Mapper;

namespace SP.Web.Site.Features.PackingList.Printable;

[Authorize]
[Route("packinglist/{id}/print")]
public class PackingListPrintController : ControllerBase
{
    private readonly IMediator _mediator;

    public PackingListPrintController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("")]
    public async Task<ActionResult> Packinglist(string id)
    {
        var model = await _mediator.Send(new GetPackingListPrintCommand(id, GetUserId()));
        if (model.IsPublic)
        {
            return View("PackingListPrint", PackingListViewModelMapper.Map(model));
        }

        if (GetUserId() == Guid.Empty)
        {
            return NotFound();
        }

        if (model.UserId == GetUserId())
        {
            return View("PackingListPrint", PackingListViewModelMapper.Map(model));
        }

        return NotFound();
    }
}