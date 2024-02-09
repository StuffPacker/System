using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SP.Web.Business.Feature.User.CreateUser;
using SP.Web.Business.Feature.User.GetUser;
using SP.Web.Site.Model;

namespace SP.Web.Site.Features.User;

public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<StuffPackerUser> _userManager;

    public UserController(IMediator mediator, UserManager<StuffPackerUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    [Route("user")]
    public ActionResult Userlist()
    {
        return View("userList");
    }

    [Route("user/{id}")]
    public async Task<ActionResult> Packinglist(string id)
    {
        var result = await _mediator.Send(new GetUserCommand(Guid.Parse(id), GetUserId()));
        if (result == null)
        {
            // check if exist in identity
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _mediator.Send(new CreateUserCommand(Guid.Parse(id), GetUserId()));
                result = await _mediator.Send(new GetUserCommand(Guid.Parse(id), GetUserId()));
                if (result != null)
                {
                    return NotFound();
                }
            }

            return NotFound();
        }

        return View("user", id);
    }
}