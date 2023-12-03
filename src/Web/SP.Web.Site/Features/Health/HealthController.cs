using MediatR;
using Microsoft.AspNetCore.Mvc;
using SP.Shared.Common;
using SP.Web.Business.Feature.Health;

namespace SP.Web.Site.Features.Health;

public class HealthController : ControllerBase
{
    private readonly IMediator _mediator;

    public HealthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("health")]
    public async Task<string> Health()
    {
        var userId = GetUserId();
        var health = await _mediator.Send(new HealthCommand(userId.ToString()));
        return health;
    }
}