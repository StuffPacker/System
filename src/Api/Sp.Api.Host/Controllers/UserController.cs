using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sp.Api.Business.Feature.User;
using SP.Shared.Common.Feature.User.Dto;
using SP.Shared.Common.Feature.User.Mapper;

namespace Sp.Api.Host.Controllers;

[Authorize]
public class UserController : SpControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserProfileMapper _userProfileMapper;

    public UserController(IUserService userService, IUserProfileMapper userProfileMapper)
    {
        _userService = userService;
        _userProfileMapper = userProfileMapper;
    }

    [HttpGet("SpApi/v1/user/{id}")]
    public async Task<ActionResult<string>> Get(string id)
    {
        var user = GetUser();
        var model = await _userService.GetUser(Guid.Parse(id), user);
        if (model == null)
        {
            return NotFound();
        }

        var dto = _userProfileMapper.Map(model);
        return Ok(dto);
    }

    [HttpPost("SpApi/v1/user")]
    public async Task<ActionResult<string>> Create([FromBody] UserProfileDto dto)
    {
        var user = GetUser();
        var model = _userProfileMapper.Map(dto);
        var result = await _userService.CreateUser(user, model);
        var resultDto = _userProfileMapper.Map(result);
        return Ok(resultDto);
    }
}