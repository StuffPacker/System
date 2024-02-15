using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP.Web.Business.Feature.Event;
using SP.Web.Business.Feature.Event.CreateEvent;
using SP.Web.Business.Feature.Event.GetEvents;
using SP.Web.Business.ViewModel;

namespace SP.Web.Site.Features.Event;

[Authorize]
[Route("api/v1/event/")]
public class EventListApiController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventListApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("")]
    public async Task<ActionResult> EventList()
    {
        var result = await _mediator.Send(new GetEventsCommand(GetUserId().ToString()));
        var vm = EventViewModelMapper.Map(result);

        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = vm
        });
    }

    [HttpPost("")]
    public async Task<ActionResult> Create()
    {
        var item = await _mediator.Send(new CreateEventCommand(GetUserId()));

        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = EventViewModelMapper.Map(item)
        });
    }
}