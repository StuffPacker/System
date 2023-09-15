using MediatR;
using Microsoft.AspNetCore.Mvc;
using SP.Web.Business.Feature.Health;

namespace SP.Web.Site.Features.Home;

public class HomeController : Controller
{
    private readonly IMediator _mediator;

    public HomeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("health")]
    public async Task<string> Health()
    {
        var result = await _mediator.Send(new HealthCommand());
        return result;
    }

    [Route("")]
    public ActionResult Index()
    {
        return View("Index");
    }
}