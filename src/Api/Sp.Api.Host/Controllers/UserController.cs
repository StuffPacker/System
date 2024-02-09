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

    [HttpGet("SpApi/v1/user/")]
    public async Task<ActionResult<string>> GetList()
    {
        var user = GetUser();
        var model = await _userService.GetUserList(user);

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

    [HttpPut("SpApi/v1/user/{id}")]
    public async Task<ActionResult<string>> Update(string id, [FromBody] UserProfileDto dto)
    {
        var user = GetUser();
        if (dto.Id != id)
        {
            return BadRequest();
        }

        var model = _userProfileMapper.Map(dto);
        if (user != model.UserId)
        {
            return StatusCode(301);
        }

        var result = await _userService.UpdateUser(user, model);
        var resultDto = _userProfileMapper.Map(result);
        return Ok(resultDto);
    }
}