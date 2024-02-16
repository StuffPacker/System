using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sp.Api.Business.Feature.Event.CreateEvent;
using Sp.Api.Business.Feature.Event.DeleteEvent;
using Sp.Api.Business.Feature.Event.GetEventById;
using Sp.Api.Business.Feature.Event.GetEvents;
using Sp.Api.Business.Feature.Item.GetItems;
using SP.Shared.Common.Feature.Event.Dto;

namespace Sp.Api.Host.Controllers;

[Authorize]
public class EventController : SpControllerBase
{
    private readonly IMediator _mediator;

    public EventController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("SpApi/v1/event/")]
    public async Task<ActionResult<string>> Get()
    {
        var result = await _mediator.Send(new GetEventsCommand(GetUser()));

        return Ok(result);
    }

    [HttpPost("SpApi/v1/event/")]
    public async Task<ActionResult<string>> Create([FromBody] EventDto dto)
    {
        var result = await _mediator.Send(new CreateEventsCommand(GetUser(), dto));

        return Ok(result);
    }

    [HttpGet("SpApi/v1/event/{id}")]
    public async Task<ActionResult<string>> GetById(string id)
    {
        var result = await _mediator.Send(new GetEventByIdCommand(id, GetUser()));

        return Ok(result);
    }

    [HttpDelete("SpApi/v1/event/{id}")]
    public async Task<ActionResult<string>> Create(string id)
    {
        var result = await _mediator.Send(new DeleteEventCommand(id, GetUser()));

        return Ok(result);
    }
}