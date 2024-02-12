using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sp.Api.Client.Feature.Client;
using SP.Web.Business.Feature.Item;
using SP.Web.Business.Feature.PackingList;
using SP.Web.Business.Feature.PackingList.Mapper;
using SP.Web.Business.Feature.PackingList.ViewModel;
using SP.Web.Business.Feature.User.GetUser;
using SP.Web.Business.Feature.User.GetUserList;
using SP.Web.Business.Feature.User.UpdateUser;
using SP.Web.Business.ViewModel;
using SP.Web.Site.Features.Item;
using SP.Web.Site.Model;

namespace SP.Web.Site.Features.User;

[Authorize]
[Route("api/v1/user/")]
public class UserApiController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IPackingListService _packingListService;
    private readonly UserManager<StuffPackerUser> _userManager;
    private readonly ISpApiClient _spApiClient;

    public UserApiController(IMediator mediator, IPackingListService packingListService, UserManager<StuffPackerUser> userManager, ISpApiClient spApiClient)
    {
        _mediator = mediator;
        _packingListService = packingListService;
        _userManager = userManager;
        _spApiClient = spApiClient;
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

    [HttpGet("{id}/publicPackingList/")]
    public async Task<ActionResult> GetUserPublicPackingLists(string id)
    {
        var result = await _packingListService.GetPackingListsPublicByUserId(Guid.Parse(id), GetUserId());
        return Ok(new ResultJsonModel
        {
            Meta = new MetaModel { Code = 200 },
            ResultData = result
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

    [AllowAnonymous]
    [Route("GetToken")]
    public async Task<ActionResult> GetTokenString(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (await _userManager.CheckPasswordAsync(user!, password))
        {
            var token = _spApiClient.GetToken(user!.Id);
            return Ok(token);
        }

        return NotFound();
    }
}