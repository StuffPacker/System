using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP.Web.Business.Feature.Item;
using SP.Web.Business.Feature.PackingList;
using SP.Web.Business.Feature.PackingList.Mapper;
using SP.Web.Business.Feature.PackingList.ViewModel;
using SP.Web.Business.Feature.User.GetUser;
using SP.Web.Business.Feature.User.GetUserList;
using SP.Web.Business.Feature.User.UpdateUser;
using SP.Web.Business.ViewModel;
using SP.Web.Site.Features.Item;

namespace SP.Web.Site.Features.User;

[Authorize]
[Route("api/v1/user/")]
public class UserApiController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [Route("{id}")]
    public async Task<ActionResult> GetUser(string id)
    {
        var result = await _mediator.Send(new GetUserCommand(Guid.Parse(id), GetUserId()));
        if (result == null)
        {
            return Ok(new ResultJsonModel
            {
                Meta = new MetaModel { Code = 404 },
                ResultData = string.Empty
            });
        }

        var vm = new UserProfileViewModel(result!);
        if (result.UserId == GetUserId())
        {
            vm.IsOwner = true;
        }

        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = vm
        });
    }

    [Route("")]
    public async Task<ActionResult> GetUsers()
    {
        var result = await _mediator.Send(new GetUserListCommand(GetUserId()));
        var list = new List<UserProfileViewModel>();
        foreach (var item in result)
        {
            list.Add(new UserProfileViewModel(item));
        }

        return Ok(new ResultJsonModel
            {
                Meta = new MetaModel { Code = 200 },
                ResultData = list
            });
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> GetUser(string id, [FromBody] UserProfileUpdateViewModel model)
    {
        try
        {
            var result = await _mediator.Send(new UpdateUserCommand(Guid.Parse(id), model, GetUserId()));
            return Ok(new ResultJsonModel
            {
                Meta = new MetaModel { Code = 200 },
                ResultData = string.Empty
            });
        }
        catch (Exception)
        {
            return Ok(new ResultJsonModel
            {
                Meta = new MetaModel { Code = 500 },
                ResultData = string.Empty
            });
        }
    }
}